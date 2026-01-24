using System.Collections.Generic;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace BusinessLogicDALCodeGenerator.ServerInfo
{
    public class db_Table
    {
        public string Name { get; set; } = string.Empty;
        public HashSet<db_Attribute> DBAttributes { get; set; } = new();
        public db_Table()
        {
        
        }
    }
    public class db_Attribute
    {
        public string Name { get; set; } = string.Empty;
        public string SqlDBType { get; set; } = string.Empty;
        public db_Attribute()
        {
        
        }
    }
    public class sp_Procedure
    {
        public string OriginalName { get;
            set
            {
                if(field != value)
                {
                    field = value;
                    if(value != null)
                    {
                        string cn = "";
                        string en = "";
                        if(clsServerServices.TrySeperateSpNameToCommandAndEntity(value,out cn,out en))
                        {
                            CommandName = cn;
                            EntityName = en;
                        }
                    }
                }
            }
        } = string.Empty;
        public string CommandName { get; set; } = string.Empty;
        public string EntityName {  get; set; } = string.Empty;
        public HashSet<sp_Parameter> Parameters { get; set; } = new();
        public enCRUDOperations OperationType { get; set; } = enCRUDOperations.Unknown;
        public sp_Procedure()
        {
        
        }
    }
    public class sp_Parameter
    {
        public string Name { get; set; } = string.Empty;
        public string SqlDBType { get; set; } = string.Empty;
        public bool IsOutput { get; set; } = false;
        public bool IsOptional { get; set; } = true;
        public bool IsUnique { get; set; } = false;
        public sp_Parameter()
        {
        
        }
    }
    public class cs_Property
    {
        public string Name { get; set; } = string.Empty;
        public string DataType { get; set; } = string.Empty;
        public bool IsMainUniqueID { get; set; } = false;
    }
    public class DTO
    {
        public string Name { get; set; } = string.Empty;
        public HashSet<sp_Procedure> StoredProcedures { get; set; } = new();
        public HashSet<cs_Property> Properties { get; set; } = new();
        public DTO() { }
        public DTO(string name, HashSet<sp_Procedure> storedProcedures, HashSet<cs_Property> properties)
        {
            Name = name;
            StoredProcedures = storedProcedures;
            Properties = properties;
        }
        public DTO(sp_Procedure ps)
        {
            DTO dto = clsServerServices.GetDTO(ps);
            Name = dto.Name;
            StoredProcedures = dto.StoredProcedures;
            Properties = dto.Properties;
        }
    }

    public enum enCRUDOperations
    {
        Create,
        ReadOneByPK,
        ReadOneByOther,
        ReadAll,
        ReadIsExistByPK,
        ReadIsExistByOther,
        Update,
        Delete,
        Unknown
    }
    public class Server
    {
        private string _connectionString = string.Empty;
        private bool TryAutoDetectLocalServer(out string ServerName)
        {
            ServerName = string.Empty;
            // Common local instances to attempt
            string[] potentialServers = { ".", "(local)", "localhost", ".\\SQLEXPRESS", "(localdb)\\MSSQLLocalDB" };
            
            foreach (var server in potentialServers)
            {
                string tempConnString = $"Server={server}; Integrated Security=True; TrustServerCertificate=True; Connection Timeout=2;";
                try
                {
                    using (SqlConnection Connection = new SqlConnection(tempConnString))
                    {
                        Connection.Open();
                        // If we get here, a server was found. We don't select a DB yet.
                        _connectionString = tempConnString; // Store base connection
            
                        using (SqlCommand command = new SqlCommand("SELECT SERVERPROPERTY('ServerName') AS 'Server Name'", Connection))
                        {
                            command.CommandType = CommandType.Text;
                            ServerName = (string)command.ExecuteScalar();
                        }
                        return true;
                    }
                }
                catch
                {
                    // Continue to next potential server
                }
            }
            
            return false;
        }
        private HashSet<string> GetDatabasesNames()
        {
            
            ValidateConnection();
            var databases = new HashSet<string>();
            
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                // Retrieve list of databases, excluding system ones
                string query = "SELECT name FROM sys.databases WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb') ORDER BY name";
                using (var cmd = new SqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        databases.Add(reader.GetString(0));
                    }
                }
            }
            return databases;
        }
        private bool ContainsAny(string input, params string[] keywords)
        {
            foreach (var k in keywords)
            {
                if (input.ToLower().Contains(k,StringComparison.OrdinalIgnoreCase)) return true;
            }
            return false;
        }
        private enCRUDOperations ClassifyByKeywords(string spName)
        {
            string name = spName.ToLower();
            
            // 1. Check for CREATE
            if (ContainsAny(name, "insert", "create", "add", "save", "insertnew", "createnew", "addnew", "savenew", "insert_new", "create_new", "add_new", "save_new"))
                return enCRUDOperations.Create;
            
            // 2. Check for LISTS/ALL first (specific)
            else if (ContainsAny(name, "getall", "list", "fetchall", "readall", "get_all", "listall", "fetch_all", "read_all", "list_all"))
                return enCRUDOperations.ReadAll;
            
            // 3. Check for READ ONE (specific)
            else if (ContainsAny(name, "get", "fetch", "read", "find", "load", "search"))
            {
                if (ContainsAny(name, "by"))
                {
                    if (ContainsAny(name, "pk", "id", "key", "uniqueid", "unique_id"))
                        return enCRUDOperations.ReadOneByPK;
                    else
                        return enCRUDOperations.ReadOneByOther;
                }
                return enCRUDOperations.ReadOneByPK;
            }
        
            // 4. Check for EXISTENCE (specific)
            else if (ContainsAny(name, "exist", "check", "verify", "isvalid"))
            {
                if (ContainsAny(name, "by"))
                {
                    if (ContainsAny(name, "pk", "id", "key", "uniqueid", "unique_id"))
                        return enCRUDOperations.ReadIsExistByPK;
                    else
                        return enCRUDOperations.ReadIsExistByOther;
                }
                return enCRUDOperations.ReadIsExistByPK;
            }
            
            // 5. Check for UPDATE
            else if (ContainsAny(name, "update", "modify", "edit", "change", "alter"))
                return enCRUDOperations.Update;
            
            // 6. Check for DELETE
            else if (ContainsAny(name, "delete", "remove", "drop"))
                return enCRUDOperations.Delete;
            
            // 7. Default to UNKNOWN
            return enCRUDOperations.Unknown;
        }
        private void SetTargetDatabase(string databaseName)
        {
            // Update connection string to include the selected Initial Catalog
            var builder = new SqlConnectionStringBuilder(_connectionString);
            builder.InitialCatalog = databaseName;
            _connectionString = builder.ConnectionString;
        }
        private void ValidateConnection()
        {
            if (string.IsNullOrEmpty(_connectionString))
                throw new InvalidOperationException("No SQL Server detected. Run Auto-Detect first.");
        }
        private HashSet<sp_Procedure> GetStoredProcedures(string DatabaseName)
        {
            SetTargetDatabase(DatabaseName);
            ValidateConnection();
            HashSet<sp_Procedure> procedures = new();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
                SELECT 
                    p.name AS ProcedureName
                FROM sys.procedures p
                WHERE p.is_ms_shipped = 0
                    AND p.name NOT LIKE '%%diagram%%'
                ORDER BY p.name";
            
                using (var cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string pName = (string)reader["ProcedureName"];
                            enCRUDOperations type = ClassifyByKeywords(pName);
                            procedures.Add(new sp_Procedure { OriginalName = pName, OperationType = type });
                        }
                    }
                }
            
                string paramQuery = @"SELECT
                                            SUBSTRING(par.name,2) AS ParameterName,
                                            t.name AS DataType,
                                            par.is_output AS IsOutput
                                        FROM sys.procedures p
                                        JOIN sys.parameters par ON p.object_id = par.object_id
                                        JOIN sys.types t ON par.user_type_id = t.user_type_id
                                        WHERE p.name = @ProcedureName
                                        ORDER BY p.name, par.parameter_id";
                using (SqlCommand command = new SqlCommand(paramQuery, conn))
                {
                    foreach (sp_Procedure procedure in procedures)
                    {
            
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@ProcedureName", procedure.OriginalName);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                procedure.Parameters.Add(new sp_Parameter
                                {
                                    Name = (string)reader["ParameterName"],
                                    SqlDBType = clsServerServices.GetSqlDbTypeString((string)reader["DataType"]),
                                    IsOutput = (bool)reader["IsOutput"]
                                });
                            }
                        }
            
                    }
                }
            }
                return procedures;

        }

        private HashSet<DTO> GetDTOs(HashSet<sp_Procedure> procedures)
        {
            // Now, create DTOs from 'Create' procedures
            HashSet<DTO> dtos = new();
            foreach (var proc in procedures)
            {
                if (proc.OperationType == enCRUDOperations.Create)
                {
                    // get dto + add procedure to its procedures
                    DTO dto = clsServerServices.GetDTO(proc);

                    dtos.Add(dto);
                }
            }
            foreach(var dto in dtos)
            {
                dto.StoredProcedures = [.. procedures.Where(p => dto.Name.Contains(p.EntityName))];
            }
            
            return dtos;
        }

        public string GetConnectionString(string DbName)
        {
            return clsServerServices.GetConnectionString(Name, DbName);
        }

        public string Name { get; } = string.Empty;
        public HashSet<string> Databases { get; } = new();
        public HashSet<sp_Procedure> StoredProcedures { get; set; } = new();
        public HashSet<DTO> DTOs { get; set; } = new();
        public void LoadStoredProcedures(string DatabaseName)
        {
            //Set database name to the given name, and get stored procedures.
            StoredProcedures = GetStoredProcedures(DatabaseName);

        }
        public void LoadDTOs(HashSet<sp_Procedure> StoredProcedures)
        {
            DTOs = GetDTOs(StoredProcedures);
        }
        public Server()
        {
            string serverName = string.Empty;
            try
            {
                if (TryAutoDetectLocalServer(out serverName))
                {
                    Name = serverName;
                    Databases = GetDatabasesNames();
                }
                else
                {
                    throw new Exception("No SQL Server instance detected on the local machine.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error initializing Server info: " + ex.Message);
            }
            
        }
        
        
    }
    
    internal static class clsServerServices
    {
        public static string GetCSharpTypeFromSql(string sqlType, bool isNullable)
        {
            // Normalize to lowercase to ensure matching works
            // e.g., "NVARCHAR" becomes "nvarchar"
            string cleanSqlType = sqlType.ToLower().Trim();
            
            switch (cleanSqlType)
            {
                // Text types
                case "varchar":
                case "nvarchar":
                case "char":
                case "nchar":
                case "text":
                case "ntext":
                case "xml":
                    return isNullable? "string?" : "string";
            
                // Integer types
                case "int":
                    return isNullable ? "int?" : "int";
                case "bigint":
                    return isNullable ? "long?" : "long";
                case "smallint":
                    return isNullable ? "short?" : "short";
                case "tinyint":
                    return isNullable ? "byte?" : "byte";
            
                // Boolean
                case "bit":
                    return isNullable ? "bool?" : "bool";
            
                // Date and Time
                case "datetime":
                case "datetime2":
                case "date":
                case "smalldatetime":
                    return isNullable ? "DateTime?" : "DateTime";
                case "time":
                    return isNullable ? "TimeSpan?" : "TimeSpan";
                case "datetimeoffset":
                    return isNullable ? "DateTimeOffset?" : "DateTimeOffset";
            
                // Decimals and Floating Point
                case "decimal":
                case "money":
                case "smallmoney":
                case "numeric":
                    return isNullable ? "decimal?" : "decimal";
                case "float":
                    return isNullable ? "double?" : "double";
                case "real":
                    return isNullable ? "float?" : "float";
            
                // Binary
                case "binary":
                case "varbinary":
                case "image":
                case "timestamp":
                case "rowversion":
                    return "byte[]"; // Arrays are reference types, so they are already nullable
            
                // Unique Identifier
                case "uniqueidentifier":
                    return isNullable ? "Guid?" : "Guid";
            
                // Fallback
                default:
                    return "object";
            }
        }
        public static string GetSqlDbTypeString(string sqlType)
        {
            string cleanSqlType = sqlType.ToLower().Trim();
            
            return cleanSqlType switch
            {
                "int" => "Int",
                "bigint" => "BigInt",
                "smallint" => "SmallInt",
                "tinyint" => "TinyInt",
                "bit" => "Bit",
                "varchar" => "VarChar",
                "nvarchar" => "NVarChar",
                "char" => "Char",
                "nchar" => "NChar",
                "text" => "Text",
                "ntext" => "NText",
                "xml" => "Xml",
                "datetime" => "DateTime",
                "datetime2" => "DateTime2",
                "date" => "Date",
                "smalldatetime" => "SmallDateTime",
                "time" => "Time",
                "datetimeoffset" => "DateTimeOffset",
                "decimal" => "Decimal",
                "numeric" => "Decimal", // Numeric maps to SqlDbType.Decimal
                "money" => "Money",
                "smallmoney" => "SmallMoney",
                "float" => "Float",
                "real" => "Real",
                "uniqueidentifier" => "UniqueIdentifier",
                "binary" => "Binary",
                "varbinary" => "VarBinary",
                "image" => "Image",
                "timestamp" => "Timestamp",
                _ => "Variant"
            };
        }
    
        /// <summary>
        /// Creates a data transfer object (DTO) definition based on the specified stored procedure intended for
        /// creating new records.
        /// </summary>
        /// <remarks>The method expects the stored procedure name to begin with one of several standard
        /// prefixes (such as 'sp_AddNew', 'sp_Create', or 'sp_Insert'). The output parameter in the procedure is used
        /// to identify the main unique identifier property in the resulting DTO.</remarks>
        /// <param name="CreateProcedure">The stored procedure definition representing a 'Create' operation. Must have at least one parameter and a
        /// name that starts with a recognized 'Create' prefix.</param>
        /// <returns>A DTO object whose name and properties are derived from the stored procedure's name and parameters. The DTO
        /// will have properties corresponding to each parameter, with the main unique identifier designated from the
        /// output parameter.</returns>
        /// <exception cref="Exception">Thrown if the stored procedure has no parameters, is not marked as a 'Create' operation, its name does not
        /// start with a recognized prefix, or if it does not have exactly one output parameter to serve as the main
        /// unique identifier.</exception>
        public static DTO GetDTO(sp_Procedure CreateProcedure)
        {
            //make sure that the procedure has parameters
            if (CreateProcedure.Parameters.Count == 0)
            {
                throw new Exception("Create procedure has no parameters." + Environment.NewLine + "make sure that you have a 'Create' stored procedure with parameters to create the DTO from.");
            }
            if (CreateProcedure.OperationType != enCRUDOperations.Create)
            {
                throw new Exception("The provided stored procedure is not designated as a 'Create' operation." + Environment.NewLine + "Make sure to provide a stored procedure that is intended for creating new records.");
            }
            DTO dto = new DTO();
            dto.StoredProcedures.Add(CreateProcedure);
            // define possible prefixes
            string[] PotentialPrefixes = new string[]
            {
                // order is important: longer prefixes first
                "sp_AddNew_",
                "sp_Add_New_",
                "sp_CreateNew_",
                "sp_Create_New_",
                "sp_InsertNew_",
                "sp_Insert_New_",
                "sp_AddNew",
                "sp_Add_New",
                "sp_CreateNew",
                "sp_Create_New",
                "sp_InsertNew",
                "sp_Insert_New",
                "sp_Add_",
                "sp_Create_",
                "sp_Insert_",
                "sp_Add",
                "sp_Create",
                "sp_Insert"
            };

            // Determine DTO name by stripping known prefixes
            /*foreach (var prefix in PotentialPrefixes)
            {
                if (CreateProcedure.OriginalName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    dto.Name = CreateProcedure.OriginalName.Substring(prefix.Length) + "DTO";
                    if (dto.Name.IsNullOrEmpty())
                    {
                        dto.Name = dto.Properties.FirstOrDefault(p=>p.IsMainUniqueID)!.Name + "DTO";
                    }
                    break;
                }
            }*/

            // Determine DTO name from Entity name
            dto.Name = CreateProcedure.EntityName + "DTO";

            // If no valid prefix was found, throw an exception
            if (dto.Name.IsNullOrEmpty())
            {
                throw new Exception("Procedure name does not match expected {'sp_AddNew', 'sp_Add_New', 'sp_CreateNew, 'sp_Create_New', 'sp_InsertNew', 'sp_Insert_New', 'sp_Add', 'sp_Create', 'sp_Insert'} prefixes." + Environment.NewLine + "make sure that you have a 'Create' stored procedure, and it has one of the previous prefixes");
            }
            
            // Map parameters to DTO properties
            foreach (var parameter in CreateProcedure.Parameters)
            {
            
                cs_Property property = new cs_Property
                {
                    Name = parameter.Name,
                    DataType = GetCSharpTypeFromSql(parameter.SqlDBType, true),
                    IsMainUniqueID = parameter.IsOutput
                };
                dto.Properties.Add(property);
            }
            
            // Ensure at there is exactly one output parameter designated as the main unique ID
            if (dto.Properties.Count(p => p.IsMainUniqueID) is > 1 or < 1)
            {
                throw new Exception("Either no output parameter or more than one parameter found in the create procedure to designate as the main unique ID." + Environment.NewLine + "Make sure that the create procedure has exactly one output parameter for the main unique identifier.");
            }
            
            return dto;
            
        }
        
        public static string GetConnectionString(string ServerName, string DatabaseName)
        {
            return $"Server={ServerName}; Initial Catalog={DatabaseName}; Integrated Security=True; TrustServerCertificate=True;";
        }
        public static bool TrySeperateSpNameToCommandAndEntity(string StoredProcedure, out string CommandName, out string EntityName)
        {
            /*if (!Regex.IsMatch(StoredProcedure, "%%_%%_%%")) 
             *{
             *    CommandName = string.Empty;
             *    EntityName = string.Empty;
             *    return false;
             *    
             *    pattern: sp_comandName_EntityName
             }*/

            if (StoredProcedure.Substring(0, 3).ToLower() == "sp_")
            {
                StoredProcedure = StoredProcedure.Remove(0, 3);
            }
            CommandName = StoredProcedure.Substring(0,StoredProcedure.IndexOf('_'));
            StoredProcedure = StoredProcedure.Remove(0, CommandName.Length);
            StoredProcedure = StoredProcedure.Trim("_").ToString();
            EntityName = StoredProcedure;
            if (CommandName.IsNullOrEmpty() || EntityName.IsNullOrEmpty())
            {
                return false;
            }
            return true;


        }


    }




}
