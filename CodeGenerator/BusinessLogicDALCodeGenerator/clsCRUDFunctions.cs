using BusinessLogicDALCodeGenerator.ServerInfo;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessLogicDALCodeGenerator
{

    public static class clsCRUDFunctions
    {
        
        public static string GetCode(DTO dto, string ConnectionString)
        {
            string DTOFieldsStructure = string.Empty;

            foreach(var property in dto.Properties)
            {
                DTOFieldsStructure += $"public {property.DataType} {property.Name} {{ get; set; }} {Environment.NewLine}    ";
            }

            return
                $@"
public class {dto.Name}
{{
    {DTOFieldsStructure}
}}


public static class cls_{dto.Name[..dto.Name.IndexOf("DTO")]}
{{

    static string ConnectionString {{ get; }} = ""{ConnectionString}"";
    
    {GetCreateFunction(dto)}
    {GetReadAllFunction(dto)}
    {GetReadOneByIDFunction(dto)}
    {GetReadOneByOtherFunction(dto)}
    {GetExistsByIDFunction(dto)}
    {GetExistsByOtherFunciton(dto)}
    {GetUpdateFunction(dto)}
    {GetDeleteFunction(dto)}
}}

";

        }

        private static string GetCreateFunction(DTO dto) 
        {
            sp_Procedure? procedure = dto.StoredProcedures.FirstOrDefault(sp => sp.OperationType == enCRUDOperations.Create);
            if (procedure == null) return "";
            string returnValue = string.Empty;
            string parameters = string.Empty;
            cs_Property MainProperty = dto.Properties.FirstOrDefault(p => p.IsMainUniqueID)!;
            foreach(var param in procedure.Parameters)
            {
                if (!param.IsOutput)
                {
                    parameters+= $"command.Parameters.AddWithValue(\"@{param.Name}\",{dto.Name.ToLower()}.{param.Name});{Environment.NewLine}                ";
                }
                else
                {
                    if(param.Name == MainProperty.Name)
                    {
                        returnValue = $"Output{param.Name}Param.Value == DBNull.Value? null : ({MainProperty.DataType})Output{param.Name}Param.Value;";
                    }
                    parameters += $@"var Output{param.Name}Param = new SqlParameter(""@{param.Name}"", SqlDbType.{param.SqlDBType})
                {{
                    Direction = ParameterDirection.Output
                }};
                command.Parameters.Add(Output{param.Name}Param);
                ";
                }
            }
            string result = $@"
    public static {dto.Properties.FirstOrDefault(p=> p.IsMainUniqueID)!.DataType} {procedure.CommandName + procedure.EntityName}({dto.Name} {dto.Name.ToLower()})
    {{
        {dto.Properties.FirstOrDefault(p => p.IsMainUniqueID)!.DataType} result = default;
        try
        {{
            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {{
                connection.Open();
                using(SqlCommand command = new SqlCommand(""{procedure.OriginalName}"",connection))
                {{
                    command.CommandType = CommandType.StoredProcedure;
                    {parameters}
                    
                    command.ExecuteNonQuery();

                    result = {returnValue}
                }}
            }}
        }}
        catch(SqlException)
        {{
            throw;
        }}
        return result;
    }}
";
            return result;
        }
        private static string GetReadAllFunction(DTO dto)
        {
            sp_Procedure? procedure = dto.StoredProcedures.FirstOrDefault(sp => sp.OperationType == enCRUDOperations.ReadAll);
            if (procedure == null) return "";
            string Fields = string.Empty;
            int NumOfPro = 0;
            foreach (var field in dto.Properties)
            {
                Fields += $@"{field.Name} = Reader[""{field.Name}""] != DBNull.Value? ({field.DataType})Reader[""{field.Name}""] : null{(++NumOfPro == dto.Properties.Count()? string.Empty : ',')}{Environment.NewLine}                                 ";
            }

            string result = $@"
    public static List<{dto.Name}> {procedure.CommandName + procedure.EntityName}()
    {{
        List<{dto.Name}> Source = new List<{dto.Name}>();
        try
        {{
            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {{
                connection.Open();
                using(SqlCommand command = new SqlCommand(""{procedure.OriginalName}"",connection))
                {{
                    command.CommandType = CommandType.StoredProcedure;
                    
                    using (SqlDataReader Reader = command.ExecuteReader())
                            {{
                                while (Reader.Read())
                                {{
                                    {dto.Name} {dto.Name.ToLower()} = new {dto.Name}
                                    {{

                                        {Fields}
                                        
                                    }};
                                    Source.Add({dto.Name.ToLower()});
                                }}
                            }}
                    
                    

                }}
            }}
        }}
        catch(SqlException)
        {{
            throw;
        }}
        return Source;
    }}
";
            return result;
        }
        private static string GetReadOneByIDFunction(DTO dto)
        {
            sp_Procedure? procedure = dto.StoredProcedures.FirstOrDefault(sp=>sp.OperationType == enCRUDOperations.ReadOneByPK);
            if (procedure == null) return "";
            cs_Property? MainProperty = dto.Properties.FirstOrDefault(pr => pr.Name == procedure!.Parameters.First().Name);
            string Fields = string.Empty;
            int NumOfPro = 0;
            foreach (var field in dto.Properties)
            {
                Fields += $@"{field.Name} = Reader[""{field.Name}""] != DBNull.Value? ({field.DataType})Reader[""{field.Name}""] : null{(++NumOfPro == dto.Properties.Count() ? string.Empty : ',')}{Environment.NewLine}                                 ";
            }

            string result = $@"
    public static {dto.Name} {procedure.CommandName + procedure.EntityName}({MainProperty!.DataType} {MainProperty.Name})
    {{
        {dto.Name} Source = new {dto.Name}();
        try
        {{
            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {{
                connection.Open();
                using(SqlCommand command = new SqlCommand(""{procedure.OriginalName}"",connection))
                {{
                    command.CommandType = CommandType.StoredProcedure;
                    
                    command.Parameters.AddWithValue(""@{procedure.Parameters.First().Name}"",{MainProperty.Name});
                    
                    using (SqlDataReader Reader = command.ExecuteReader())
                    {{
                        while (Reader.Read())
                        {{
                            Source = new {dto.Name}
                            {{

                                {Fields}
                                
                            }};
                        }}
                    }}
                }}
            }}
        }}
        catch(SqlException)
        {{
            throw;
        }}
        
        return Source;
    }}
";
            return result;
        }
        private static string GetReadOneByOtherFunction(DTO dto)
        {
            sp_Procedure? procedure = dto.StoredProcedures.FirstOrDefault(sp=>sp.OperationType == enCRUDOperations.ReadOneByOther);
            if(procedure == null)return "";

            //Adding parameters to stored procedure
            string parameters = string.Empty;
            foreach (var param in procedure.Parameters)
            {
                if (!param.IsOutput)
                {
                    parameters += $"command.Parameters.AddWithValue(\"@{param.Name}\",{dto.Name.ToLower()}.{param.Name});{Environment.NewLine}                ";
                }
                else
                {
                    parameters += $@"var Output{param.Name}Param = new SqlParameter(""@{param.Name}"", SqlDbType.{param.SqlDBType})
                {{
                    Direction = ParameterDirection.Output
                }};
                command.Parameters.Add(Output{param.Name}Param);
                ";
                }
            }

            //reading fields from SqlDataReader
            string Fields = string.Empty;
            int NumOfPro = 0;
            foreach (var field in dto.Properties)
            {
                Fields += $@"{field.Name} = Reader[""{field.Name}""] != DBNull.Value? ({field.DataType})Reader[""{field.Name}""] : null{(++NumOfPro == dto.Properties.Count() ? string.Empty : ',')}{Environment.NewLine}                                 ";
            }

            string result = $@"
    public static {dto.Name} {procedure.CommandName + procedure.EntityName}({dto.Properties.FirstOrDefault(pr => pr.IsMainUniqueID)!.DataType} {dto.Properties.FirstOrDefault(pr => pr.IsMainUniqueID)!.Name})
    {{
        {dto.Name} Source = new {dto.Name}();
        try
        {{
            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {{
                connection.Open();
                using(SqlCommand command = new SqlCommand(""{procedure.OriginalName}"",connection))
                {{
                    command.CommandType = CommandType.StoredProcedure;
                    {parameters}
                    
                    using (SqlDataReader Reader = command.ExecuteReader())
                    {{
                        while (Reader.Read())
                        {{
                            Source = new {dto.Name}
                            {{

                                {Fields}
                                
                            }};
                        }}
                    }}
                }}
            }}
        }}
        catch(SqlException)
        {{
            throw;
        }}
        return Source;
    }}
";
            return result;
        }
        private static string GetExistsByIDFunction(DTO dto)
        {
            sp_Procedure? procedure = dto.StoredProcedures.FirstOrDefault(sp=>sp.OperationType == enCRUDOperations.ReadIsExistByPK);
            if(procedure == null)return "";
            string parameters = string.Empty;
            foreach (var param in procedure.Parameters)
            {
                if (!param.IsOutput)
                {
                    parameters += $"command.Parameters.AddWithValue(\"@{param.Name}\",{param.Name});{Environment.NewLine}                ";
                }
                else
                {
                    parameters += $@"var Output{param.Name}Param = new SqlParameter(""@{param.Name}"", SqlDbType.{param.SqlDBType})
                {{
                    Direction = ParameterDirection.Output
                }};
                command.Parameters.Add(Output{param.Name}Param);
                ";
                }
            }


            string Fields = string.Empty;
            int NumOfPro = 0;
            foreach (var field in dto.Properties)
            {
                Fields += $@"{field.Name} = Reader[""{field.Name}""] != DBNull.Value? ({field.DataType})Reader[""{field.Name}""] : null{(++NumOfPro == dto.Properties.Count() ? string.Empty : ',')}{Environment.NewLine}                                 ";
            }

            string result = $@"
    public static bool {procedure.CommandName + procedure.EntityName}({dto.Properties.FirstOrDefault(pr => pr.IsMainUniqueID)!.DataType} {dto.Properties.FirstOrDefault(pr => pr.IsMainUniqueID)!.Name})
    {{
        bool result = false;
        try
        {{
            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {{
                connection.Open();
                using(SqlCommand command = new SqlCommand(""{procedure.OriginalName}"",connection))
                {{
                    command.CommandType = CommandType.StoredProcedure;
                    {parameters}
                    
                    result = (int)command.ExecuteScalar() > 0;   
                }}
            }}
        }}
        catch (SqlException)
        {{
            throw;
        }}
        return result;
    }}
";
            return result;
        }
        private static string GetExistsByOtherFunciton(DTO dto)
        {
            sp_Procedure? procedure = dto.StoredProcedures.FirstOrDefault(sp=>sp.OperationType == enCRUDOperations.ReadIsExistByOther);
            if(procedure == null)return "";
            string SPparameters = string.Empty;
            string FNParameters = string.Empty;
            int NumOfPars = 0;
            foreach (var param in procedure.Parameters)
            {
                if (param.IsOutput)
                {
                    SPparameters += $"command.Parameters.AddWithValue(\"@{param.Name}\",{param.Name});{Environment.NewLine}                ";
                    FNParameters += $"out {dto.Properties.FirstOrDefault(pr => pr.Name == param.Name)!.DataType} {param.Name}{(++NumOfPars == procedure.Parameters.Count ? "" : ", ")}";
                }
                else
                {
                    SPparameters += $@"var Output{param.Name}Param = new SqlParameter(""@{param.Name}"", SqlDbType.{param.SqlDBType})
                {{
                    Direction = ParameterDirection.Output
                }};
                command.Parameters.Add(Output{param.Name}Param);
                ";
                    FNParameters += $"{dto.Properties.FirstOrDefault(pr => pr.Name == param.Name)!.DataType} {param.Name}{(++NumOfPars == procedure.Parameters.Count ? "" : ", ")}";
                }
            }
            

            string result = $@"
    public static bool {procedure.CommandName + procedure.EntityName}({FNParameters})
    {{
        bool result = false;
        try
        {{
            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {{
                connection.Open();
                using(SqlCommand command = new SqlCommand(""{procedure.OriginalName}"",connection))
                {{
                    command.CommandType = CommandType.StoredProcedure;
                    {SPparameters}
                    
                    result = (int)command.ExecuteScalar() > 0;   
                }}
            }}
        }}
        catch (SqlException)
        {{
            throw;
        }}
        return result;
    }}
";
            return result;
        }
        private static string GetUpdateFunction(DTO dto)
        {
            sp_Procedure? procedure = dto.StoredProcedures.FirstOrDefault(sp=>sp.OperationType == enCRUDOperations.Update);
            if(procedure == null)return "";
            string parameters = string.Empty;
            foreach (var param in procedure.Parameters)
            {
                if (!param.IsOutput)
                {
                    parameters += $"command.Parameters.AddWithValue(\"@{param.Name}\",{dto.Name.ToLower()}.{param.Name});{Environment.NewLine}                ";
                }
                else
                {
                    parameters += $@"var Output{param.Name}Param = new SqlParameter(""@{param.Name}"", SqlDbType.{param.SqlDBType})
                {{
                    Direction = ParameterDirection.Output
                }};
                command.Parameters.Add(Output{param.Name}Param);
                ";
                }
            }
            string result = $@"
    public static bool {procedure.CommandName + procedure.EntityName}({dto.Name} {dto.Name.ToLower()})
    {{
        bool result = false;
        try
        {{
            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {{
                connection.Open();
                using(SqlCommand command = new SqlCommand(""{procedure.OriginalName}"",connection))
                {{
                    command.CommandType = CommandType.StoredProcedure;
                    {parameters}
                    
                    var ReturnParameter = command.Parameters.Add(""@ReturnVal"", SqlDbType.Int);
                    ReturnParameter.Direction = ParameterDirection.ReturnValue;
                    
                    command.ExecuteNonQuery();
                    
                    result = ((int)ReturnParameter.Value == 1);
                }}
            }}
        }}
        catch(SqlException)
        {{
            throw;
        }}
        return result;
    }}
";
            return result;
        }
        private static string GetDeleteFunction(DTO dto)
        {
            sp_Procedure? procedure = dto.StoredProcedures.FirstOrDefault(sp=>sp.OperationType == enCRUDOperations.Delete);
            if(procedure == null)return "";
            string SPparameters = string.Empty;
            string FNParameters = string.Empty;
            int NumOfPars = 0;
            foreach (var param in procedure.Parameters)
            {
                if (!param.IsOutput)
                {
                    SPparameters += $"command.Parameters.AddWithValue(\"@{param.Name}\",{param.Name});{Environment.NewLine}                ";
                    FNParameters += $"out {dto.Properties.FirstOrDefault(pr => pr.Name == param.Name)!.DataType} {param.Name}{(++NumOfPars == procedure.Parameters.Count ? "" : ", ")}";
                }
                else
                {
                    SPparameters += $@"var Output{param.Name}Param = new SqlParameter(""@{param.Name}"", SqlDbType.{param.SqlDBType})
                {{
                    Direction = ParameterDirection.Output
                }};
                command.Parameters.Add(Output{param.Name}Param);
                ";
                    FNParameters += $"{dto.Properties.FirstOrDefault(pr => pr.Name == param.Name)!.DataType} {param.Name}{(++NumOfPars == procedure.Parameters.Count ? "" : ", ")}";
                }
            }

            string result = $@"
    public static bool {procedure.CommandName + procedure.EntityName}({FNParameters})
    {{
        bool result = false;
        try
        {{
            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {{
                connection.Open();
                using(SqlCommand command = new SqlCommand(""{procedure.OriginalName}"",connection))
                {{
                    command.CommandType = CommandType.StoredProcedure;
                    {SPparameters}
                    
                    result = command.ExecuteNonQuery() > 0;
                }}
            }}
        }}
        catch(SqlException)
        {{
            throw;
        }}
        return result;
    }}
";
            return result;
        }

    }
}
