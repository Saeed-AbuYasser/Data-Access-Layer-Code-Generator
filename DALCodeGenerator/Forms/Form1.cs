using BusinessLogicDALCodeGenerator;
using BusinessLogicDALCodeGenerator.ServerInfo;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Immutable;
using static Azure.Core.HttpHeader;
namespace DALCodeGenerator
{
    public partial class Form1 : Form
    {
        private Server Server = new();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Server = new();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
            //foreach (string db in Server.Databases)
            //{
            //    cbDatabases.Items.Add(db);
            //}

            cbDatabases.DataSource = Server.Databases.ToImmutableList();


        }

        private void cbDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if None; empty everything, else fill cbDTOS with DTOs, with default value of 'None'


            cbDTOs.Enabled = true;
            Server.LoadStoredProcedures(cbDatabases.SelectedItem!.ToString()!);
            Server.LoadDTOs(Server.StoredProcedures);
            List<string> items = Server.DTOs.Select(dto => dto.Name).ToList();items.Add("None");
            cbDTOs.DataSource = items;

        }

        private void cbDTOs_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if None; empty everything, else fill stored procedures combo boxes with sotred procedures, and set default value for each one either the corresponding 
            // stored procedure if exists, or None if not.
            // if ReadOneByOther combo box is not None fill corresponding Entities combo box with fields of DTO.
            // if ExistsByOther combo box is not None fill corresponding Entities combo box with fields of DTO.

            if(cbDTOs.Items.Count == 0 || cbDTOs.SelectedItem!.ToString() == "None")
            {
                Empty_SpComboBoxes();
                ChangeComboBoxesEnablity(false);
            }
            else
            {
                ChangeComboBoxesEnablity(true);
                ChangeStoredProceduresInComboBoxes();

            }
            ChangeCheckBoxesCheckability();
        }
        private void Empty_SpComboBoxes()
        {
            cbDeletesp.Items.Clear();
            cbDeletesp.Items.Add("None");
            cbUpdatesp.Items.Clear();
            cbUpdatesp.Items.Add("None");
            cbReadAllsp.Items.Clear();
            cbReadAllsp.Items.Add("None");
            cbExistsByOthersp.Items.Clear();
            cbExistsByOthersp.Items.Add("None");
            cbExistsByIDsp.Items.Clear();
            cbExistsByIDsp.Items.Add("None");
            cbReadOneByOthersp.Items.Clear();
            cbReadOneByOthersp.Items.Add("None");
            cbReadOneByIDsp.Items.Clear();
            cbReadOneByIDsp.Items.Add("None");
            cbCreatesp.Items.Clear();
            cbCreatesp.Items.Add("None");
        }
        private void ChangeStoredProceduresInComboBoxes()
        {
            //it's garanted that there is a selected DTO.

            //fill combo boxes with stored procedures.
            IList<sp_Procedure> StoredProcedures = Server.DTOs.FirstOrDefault(dt => dt.Name == cbDTOs.SelectedItem!.ToString())!.StoredProcedures.ToImmutableList();
            string[] spNames = [..StoredProcedures.Select(sp => sp.OriginalName).ToArray(),"None"];
            cbDeletesp.Items.Clear();
            cbDeletesp.Items.AddRange(spNames);
            cbUpdatesp.Items.Clear();
            cbUpdatesp.Items.AddRange(spNames);
            cbReadAllsp.Items.Clear();
            cbReadAllsp.Items.AddRange(spNames);
            cbExistsByOthersp.Items.Clear();
            cbExistsByOthersp.Items.AddRange(spNames);
            cbExistsByIDsp.Items.Clear();
            cbExistsByIDsp.Items.AddRange(spNames);
            cbReadOneByOthersp.Items.Clear();
            cbReadOneByOthersp.Items.AddRange(spNames);
            cbReadOneByIDsp.Items.Clear();
            cbReadOneByIDsp.Items.AddRange(spNames);
            cbCreatesp.Items.Clear();
            cbCreatesp.Items.AddRange(spNames);


            //set default value the corresponding stored procedrue for each combo box, if the combo box doesn't have a corresponding stored procedre; set 'None'.

            sp_Procedure? sp = StoredProcedures.FirstOrDefault(sp => sp.OperationType == enCRUDOperations.Delete);
            if (sp != null)
            {
                if (!sp.OriginalName.IsNullOrEmpty())
                {
                    cbDeletesp.SelectedItem = sp.OriginalName;
                }
                else cbDeletesp.SelectedItem = "None";
            }
            else cbDeletesp.SelectedItem = "None";


            sp = StoredProcedures.FirstOrDefault(sp => sp.OperationType == enCRUDOperations.Update);
            if (sp != null)
            {
                if (!sp.OriginalName.IsNullOrEmpty())
                {
                    cbUpdatesp.SelectedItem = sp.OriginalName;
                }
                else cbUpdatesp.SelectedItem = "None";
            }else cbUpdatesp.SelectedItem = "None";

            sp = StoredProcedures.FirstOrDefault(sp => sp.OperationType == enCRUDOperations.ReadAll);
            if (sp != null)
            {
                if (!sp.OriginalName.IsNullOrEmpty())
                {
                    cbReadAllsp.SelectedItem = sp.OriginalName;
                }
                else cbReadAllsp.SelectedItem = "None";
            }else cbReadAllsp.SelectedItem = "None";

            sp = StoredProcedures.FirstOrDefault(sp => sp.OperationType == enCRUDOperations.ReadIsExistByOther);
            if (sp != null)
            {
                if (!sp.OriginalName.IsNullOrEmpty())
                {
                    cbExistsByOthersp.SelectedItem = sp.OriginalName;
                }
                else cbExistsByOthersp.SelectedItem = "None";
            }else cbExistsByOthersp.SelectedItem = "None";

            sp = StoredProcedures.FirstOrDefault(sp => sp.OperationType == enCRUDOperations.ReadIsExistByPK);
            if (sp != null)
            {
                if (!sp.OriginalName.IsNullOrEmpty())
                {
                    cbExistsByIDsp.SelectedItem = sp.OriginalName;
                }
                else cbExistsByIDsp.SelectedItem = "None";
            }else cbExistsByIDsp.SelectedItem = "None";

            sp = StoredProcedures.FirstOrDefault(sp => sp.OperationType == enCRUDOperations.ReadOneByOther);
            if (sp != null)
            {
                if (!sp.OriginalName.IsNullOrEmpty())
                {
                    cbReadOneByOthersp.SelectedItem = sp.OriginalName;
                }
                else cbReadOneByOthersp.SelectedItem = "None";
            }else cbReadOneByOthersp.SelectedItem = "None";

            sp = StoredProcedures.FirstOrDefault(sp => sp.OperationType == enCRUDOperations.ReadOneByPK);
            if (sp != null)
            {
                if (!sp.OriginalName.IsNullOrEmpty())
                {
                    cbReadOneByIDsp.SelectedItem = sp.OriginalName;
                }
                else cbReadOneByIDsp.SelectedItem = "None";
            }else cbReadOneByIDsp.SelectedItem = "None";

            sp = StoredProcedures.FirstOrDefault(sp => sp.OperationType == enCRUDOperations.Create);
            if (sp != null)
            {
                if (!sp.OriginalName.IsNullOrEmpty())
                {
                    cbCreatesp.SelectedItem = sp.OriginalName;
                }
                else cbCreatesp.SelectedItem = "None";
            }else cbCreatesp.SelectedItem = "None";

        }

        private void ChangeComboBoxesEnablity(bool Enabled)
        {
            cbDeletesp.Enabled = Enabled;
            cbUpdatesp.Enabled = Enabled;
            cbReadAllsp.Enabled = Enabled;
            cbExistsByOthersp.Enabled = Enabled;
            cbExistsByIDsp.Enabled = Enabled;
            cbReadOneByOthersp.Enabled = Enabled;
            cbReadOneByIDsp.Enabled = Enabled;
            cbCreatesp.Enabled = Enabled;

            chbDelete.Enabled = Enabled;
            chbUpdate.Enabled = Enabled;
            chbReadAll.Enabled = Enabled;
            chbExistsByOther.Enabled = Enabled;
            chbExistsByID.Enabled = Enabled;
            chbReadOneByOther.Enabled = Enabled;
            chbReadOneByID.Enabled = Enabled;
            chbCreate.Enabled = Enabled;

        }
        private void ChangeCheckBoxesCheckability()
        {
            if (cbDeletesp.Enabled && cbDeletesp.SelectedItem.ToString() == "None")
            {
                chbDelete.Checked = false;
            }
            else 
            {
                chbDelete.Checked = true;
            }
            if (cbUpdatesp.Enabled && cbUpdatesp.SelectedItem.ToString() == "None")
            {
                chbUpdate.Checked = false;
            }
            else
            {
                chbUpdate.Checked = true;
            }
            if (cbReadAllsp.Enabled && cbReadAllsp.SelectedItem.ToString() == "None")
            {
                chbReadAll.Checked = false;
            }
            else
            {
                chbReadAll.Checked = true;
            }
            if (cbExistsByOthersp.Enabled && cbExistsByOthersp.SelectedItem.ToString() == "None")
            {
                chbExistsByOther.Checked = false;
            }
            else
            {
                chbExistsByOther.Checked = true;
            }
            if (cbExistsByIDsp.Enabled && cbExistsByIDsp.SelectedItem.ToString() == "None")
            {
                chbExistsByID.Checked = false;
            }
            else
            {
                chbExistsByID.Checked = true;
            }
            if (cbReadOneByOthersp.Enabled && cbReadOneByOthersp.SelectedItem.ToString() == "None")
            {
                chbReadOneByOther.Checked = false;
            }
            else
            {
                chbReadOneByOther.Checked = true;
            }
            if (cbReadOneByIDsp.Enabled && cbReadOneByIDsp.SelectedItem.ToString() == "None")
            {
                chbReadOneByID.Checked = false;
            }
            else
            {
                chbReadOneByID.Checked = true;
            }
            if (cbCreatesp.Enabled && cbCreatesp.SelectedItem.ToString() == "None")
            {
                chbCreate.Checked = false;
            }
            else
            {
                chbCreate.Checked = true;
            }
        }


        private void cbReadOneByOthersp_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cbExistsByOthersp_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnGenerateCode_Click(object sender, EventArgs e)
        {
            if (!IsDataValid()) return;
            tbCode.Text = GenerateCode();

        }
        private string GenerateCode()
        {
            DTO dto = Server.DTOs.FirstOrDefault(dto => dto.Name == cbDTOs.SelectedItem!.ToString())!;
            HashSet<sp_Procedure> newSPs = new HashSet<sp_Procedure>();
            if (chbCreate.Checked)
            {
                var sp = dto.StoredProcedures.FirstOrDefault(sp => sp.OriginalName == cbCreatesp.SelectedItem!.ToString())!;
                sp.OperationType = enCRUDOperations.Create;
                newSPs.Add(sp);
            }
            if (chbReadAll.Checked)
            {
                var sp = dto.StoredProcedures.FirstOrDefault(sp => sp.OriginalName == cbReadAllsp.SelectedItem!.ToString())!;
                sp.OperationType = enCRUDOperations.ReadAll;
                newSPs.Add(sp);
                
            }
            if (chbReadOneByID.Checked)
            {
                var sp = dto.StoredProcedures.FirstOrDefault(sp => sp.OriginalName == cbReadOneByIDsp.SelectedItem!.ToString())!;
                sp.OperationType = enCRUDOperations.ReadOneByPK;
                newSPs.Add(sp);
                
            }
            if (chbReadOneByOther.Checked)
            {
                var sp = dto.StoredProcedures.FirstOrDefault(sp => sp.OriginalName == cbReadOneByOthersp.SelectedItem!.ToString())!;
                sp.OperationType = enCRUDOperations.ReadOneByOther;
                newSPs.Add(sp);
                
            }
            if (chbExistsByID.Checked)
            {
                var sp = dto.StoredProcedures.FirstOrDefault(sp => sp.OriginalName == cbExistsByIDsp.SelectedItem!.ToString())!;
                sp.OperationType = enCRUDOperations.ReadIsExistByPK;
                newSPs.Add(sp);
                
            }
            if (chbExistsByOther.Checked)
            {
                var sp = dto.StoredProcedures.FirstOrDefault(sp => sp.OriginalName == cbExistsByOthersp.SelectedItem!.ToString())!;
                sp.OperationType = enCRUDOperations.ReadIsExistByOther;
                newSPs.Add(sp);
                
            }
            if (chbUpdate.Checked)
            {
                var sp = dto.StoredProcedures.FirstOrDefault(sp => sp.OriginalName == cbUpdatesp.SelectedItem!.ToString())!;
                sp.OperationType = enCRUDOperations.Update;
                newSPs.Add(sp);
                
            }
            if (chbDelete.Checked)
            {
                var sp = dto.StoredProcedures.FirstOrDefault(sp => sp.OriginalName == cbDeletesp.SelectedItem!.ToString())!;
                sp.OperationType = enCRUDOperations.Delete;
                newSPs.Add(sp);
                
            }
            dto.StoredProcedures = newSPs;
            return clsCRUDFunctions.GetCode(dto,Server.GetConnectionString(cbDatabases.SelectedItem!.ToString()!));
        }
        private bool IsDataValid()
        {
            bool isvalid = true;

            if (cbDatabases.SelectedItem == null)
            {
                isvalid = false;
                errorProvider1.SetError(cbDatabases, "must select a database!");

            }
            else
            {
                errorProvider1.SetError(cbDatabases, "");
            }
            if (cbDTOs.SelectedItem == null)
            {
                isvalid = false;
                errorProvider1.SetError(cbDTOs, "must select a DTO!");

            }
            else
            {
                errorProvider1.SetError(cbDTOs,"");
            }
            if (chbCreate.Checked && (cbCreatesp.SelectedItem == null || cbCreatesp.SelectedItem.ToString() == "None"))
            {
                isvalid = false;
                errorProvider1.SetError(chbCreate, "no 'Create' stored procedure selected though it's required!");

            }
            else
            {
                errorProvider1.SetError(chbCreate,"");
            }
            if (chbReadAll.Checked && (cbReadAllsp.SelectedItem == null || cbReadAllsp.SelectedItem.ToString() == "None"))
            {
                isvalid = false;
                errorProvider1.SetError(chbReadAll, "no 'ReadAll' stored procedure selected though it's required!");

            }
            else
            {
                errorProvider1.SetError(chbReadAll,"");
            }
            if (chbReadOneByID.Checked && (cbReadOneByIDsp.SelectedItem == null || cbReadOneByIDsp.SelectedItem.ToString() == "None"))
            {
                isvalid = false;
                errorProvider1.SetError(chbReadOneByID, "no 'ReadOneByID' stored procedure selected though it's required!");

            }
            else
            {
                errorProvider1.SetError(chbReadOneByID,"");
            }
            if (chbReadOneByOther.Checked && (cbReadOneByOthersp.SelectedItem == null || cbReadOneByOthersp.SelectedItem.ToString() == "None"))
            {
                isvalid = false;
                errorProvider1.SetError(chbReadOneByOther, "no 'ReadOneByOther' stored procedure selected though it's required!");

            }
            else
            {
                errorProvider1.SetError(chbReadOneByOther,"");
            }
            if (chbExistsByID.Checked && (cbExistsByIDsp.SelectedItem == null || cbExistsByIDsp.SelectedItem.ToString() == "None"))
            {
                isvalid = false;
                errorProvider1.SetError(chbExistsByID, "no 'ExistsByID' stored procedure selected though it's required!");

            }
            else
            {
                errorProvider1.SetError(chbExistsByID,"");
            }
            if (chbExistsByOther.Checked && (cbExistsByOthersp.SelectedItem == null || cbExistsByOthersp.SelectedItem.ToString() == "None"))
            {
                isvalid = false;
                errorProvider1.SetError(chbExistsByOther, "no 'ExistsByOther' stored procedure selected though it's required!");

            }
            else
            {
                errorProvider1.SetError(chbExistsByOther,"");
            }
            if (chbUpdate.Checked && (cbUpdatesp.SelectedItem == null || cbUpdatesp.SelectedItem.ToString() == "None"))
            {
                isvalid = false;
                errorProvider1.SetError(chbUpdate, "no 'Update' stored procedure selected though it's required!");

            }
            else
            {
                errorProvider1.SetError(chbUpdate,"");
            }
            if (chbDelete.Checked && (cbDeletesp.SelectedItem == null || cbDeletesp.SelectedItem.ToString() == "None"))
            {
                isvalid = false;
                errorProvider1.SetError(chbDelete, "no 'Delete' stored procedure selected though it's required!");

            }
            else
             
            errorProvider1.SetError(chbDelete,"");{
            }

                return isvalid;
        }
    }
}
