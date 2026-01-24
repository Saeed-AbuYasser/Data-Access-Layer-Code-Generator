namespace DALCodeGenerator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            chbDelete = new CheckBox();
            chbUpdate = new CheckBox();
            chbReadAll = new CheckBox();
            chbExistsByOther = new CheckBox();
            chbExistsByID = new CheckBox();
            chbReadOneByOther = new CheckBox();
            chbReadOneByID = new CheckBox();
            chbCreate = new CheckBox();
            label17 = new Label();
            cbDeletesp = new ComboBox();
            label16 = new Label();
            cbUpdatesp = new ComboBox();
            label15 = new Label();
            cbReadAllsp = new ComboBox();
            label14 = new Label();
            cbExistsByOthersp = new ComboBox();
            label13 = new Label();
            cbExistsByIDsp = new ComboBox();
            label12 = new Label();
            cbReadOneByOthersp = new ComboBox();
            label11 = new Label();
            cbReadOneByIDsp = new ComboBox();
            label1 = new Label();
            cbCreatesp = new ComboBox();
            ulbl1 = new Label();
            cbDatabases = new ComboBox();
            label2 = new Label();
            cbDTOs = new ComboBox();
            btnGenerateCode = new Button();
            tbCode = new TextBox();
            errorProvider1 = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // chbDelete
            // 
            chbDelete.AutoSize = true;
            chbDelete.Checked = true;
            chbDelete.CheckState = CheckState.Checked;
            chbDelete.Enabled = false;
            chbDelete.Location = new Point(571, 622);
            chbDelete.Name = "chbDelete";
            chbDelete.Size = new Size(94, 26);
            chbDelete.TabIndex = 54;
            chbDelete.Text = "Activate";
            chbDelete.UseVisualStyleBackColor = true;
            // 
            // chbUpdate
            // 
            chbUpdate.AutoSize = true;
            chbUpdate.Checked = true;
            chbUpdate.CheckState = CheckState.Checked;
            chbUpdate.Enabled = false;
            chbUpdate.Location = new Point(571, 566);
            chbUpdate.Name = "chbUpdate";
            chbUpdate.Size = new Size(94, 26);
            chbUpdate.TabIndex = 53;
            chbUpdate.Text = "Activate";
            chbUpdate.UseVisualStyleBackColor = true;
            // 
            // chbReadAll
            // 
            chbReadAll.AutoSize = true;
            chbReadAll.Checked = true;
            chbReadAll.CheckState = CheckState.Checked;
            chbReadAll.Enabled = false;
            chbReadAll.Location = new Point(571, 510);
            chbReadAll.Name = "chbReadAll";
            chbReadAll.Size = new Size(94, 26);
            chbReadAll.TabIndex = 52;
            chbReadAll.Text = "Activate";
            chbReadAll.UseVisualStyleBackColor = true;
            // 
            // chbExistsByOther
            // 
            chbExistsByOther.AutoSize = true;
            chbExistsByOther.Checked = true;
            chbExistsByOther.CheckState = CheckState.Checked;
            chbExistsByOther.Enabled = false;
            chbExistsByOther.Location = new Point(571, 371);
            chbExistsByOther.Name = "chbExistsByOther";
            chbExistsByOther.Size = new Size(94, 26);
            chbExistsByOther.TabIndex = 51;
            chbExistsByOther.Text = "Activate";
            chbExistsByOther.UseVisualStyleBackColor = true;
            // 
            // chbExistsByID
            // 
            chbExistsByID.AutoSize = true;
            chbExistsByID.Checked = true;
            chbExistsByID.CheckState = CheckState.Checked;
            chbExistsByID.Enabled = false;
            chbExistsByID.Location = new Point(572, 206);
            chbExistsByID.Name = "chbExistsByID";
            chbExistsByID.Size = new Size(94, 26);
            chbExistsByID.TabIndex = 50;
            chbExistsByID.Text = "Activate";
            chbExistsByID.UseVisualStyleBackColor = true;
            // 
            // chbReadOneByOther
            // 
            chbReadOneByOther.AutoSize = true;
            chbReadOneByOther.Checked = true;
            chbReadOneByOther.CheckState = CheckState.Checked;
            chbReadOneByOther.Enabled = false;
            chbReadOneByOther.Location = new Point(571, 323);
            chbReadOneByOther.Name = "chbReadOneByOther";
            chbReadOneByOther.Size = new Size(94, 26);
            chbReadOneByOther.TabIndex = 49;
            chbReadOneByOther.Text = "Activate";
            chbReadOneByOther.UseVisualStyleBackColor = true;
            // 
            // chbReadOneByID
            // 
            chbReadOneByID.AutoSize = true;
            chbReadOneByID.Checked = true;
            chbReadOneByID.CheckState = CheckState.Checked;
            chbReadOneByID.Enabled = false;
            chbReadOneByID.Location = new Point(571, 145);
            chbReadOneByID.Name = "chbReadOneByID";
            chbReadOneByID.Size = new Size(94, 26);
            chbReadOneByID.TabIndex = 48;
            chbReadOneByID.Text = "Activate";
            chbReadOneByID.UseVisualStyleBackColor = true;
            // 
            // chbCreate
            // 
            chbCreate.AutoSize = true;
            chbCreate.Checked = true;
            chbCreate.CheckState = CheckState.Checked;
            chbCreate.Enabled = false;
            chbCreate.Location = new Point(571, 84);
            chbCreate.Name = "chbCreate";
            chbCreate.Size = new Size(94, 26);
            chbCreate.TabIndex = 47;
            chbCreate.Text = "Activate";
            chbCreate.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(11, 623);
            label17.Name = "label17";
            label17.Size = new Size(272, 22);
            label17.TabIndex = 46;
            label17.Text = "Select \"Delete\" Stored Procedure:";
            // 
            // cbDeletesp
            // 
            cbDeletesp.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDeletesp.Enabled = false;
            cbDeletesp.FormattingEnabled = true;
            cbDeletesp.Location = new Point(376, 620);
            cbDeletesp.Margin = new Padding(4, 3, 4, 3);
            cbDeletesp.Name = "cbDeletesp";
            cbDeletesp.Size = new Size(188, 30);
            cbDeletesp.TabIndex = 45;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(11, 567);
            label16.Name = "label16";
            label16.Size = new Size(279, 22);
            label16.TabIndex = 44;
            label16.Text = "Select \"Update\" Stored Procedure:";
            // 
            // cbUpdatesp
            // 
            cbUpdatesp.DropDownStyle = ComboBoxStyle.DropDownList;
            cbUpdatesp.Enabled = false;
            cbUpdatesp.FormattingEnabled = true;
            cbUpdatesp.Location = new Point(376, 564);
            cbUpdatesp.Margin = new Padding(4, 3, 4, 3);
            cbUpdatesp.Name = "cbUpdatesp";
            cbUpdatesp.Size = new Size(188, 30);
            cbUpdatesp.TabIndex = 43;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(11, 511);
            label15.Name = "label15";
            label15.Size = new Size(287, 22);
            label15.TabIndex = 42;
            label15.Text = "Select \"Read All\" Stored Procedure:";
            // 
            // cbReadAllsp
            // 
            cbReadAllsp.DropDownStyle = ComboBoxStyle.DropDownList;
            cbReadAllsp.Enabled = false;
            cbReadAllsp.FormattingEnabled = true;
            cbReadAllsp.Location = new Point(376, 508);
            cbReadAllsp.Margin = new Padding(4, 3, 4, 3);
            cbReadAllsp.Name = "cbReadAllsp";
            cbReadAllsp.Size = new Size(188, 30);
            cbReadAllsp.TabIndex = 41;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(11, 370);
            label14.Name = "label14";
            label14.Size = new Size(332, 22);
            label14.TabIndex = 40;
            label14.Text = "Select \"ExistsByOther\" Stored Procedure:";
            // 
            // cbExistsByOthersp
            // 
            cbExistsByOthersp.DropDownStyle = ComboBoxStyle.DropDownList;
            cbExistsByOthersp.Enabled = false;
            cbExistsByOthersp.FormattingEnabled = true;
            cbExistsByOthersp.Location = new Point(376, 367);
            cbExistsByOthersp.Margin = new Padding(4, 3, 4, 3);
            cbExistsByOthersp.Name = "cbExistsByOthersp";
            cbExistsByOthersp.Size = new Size(188, 30);
            cbExistsByOthersp.TabIndex = 39;
            cbExistsByOthersp.SelectedIndexChanged += cbExistsByOthersp_SelectedIndexChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(12, 207);
            label13.Name = "label13";
            label13.Size = new Size(307, 22);
            label13.TabIndex = 38;
            label13.Text = "Select \"ExistsByID\" Stored Procedure:";
            // 
            // cbExistsByIDsp
            // 
            cbExistsByIDsp.DropDownStyle = ComboBoxStyle.DropDownList;
            cbExistsByIDsp.Enabled = false;
            cbExistsByIDsp.FormattingEnabled = true;
            cbExistsByIDsp.Location = new Point(377, 204);
            cbExistsByIDsp.Margin = new Padding(4, 3, 4, 3);
            cbExistsByIDsp.Name = "cbExistsByIDsp";
            cbExistsByIDsp.Size = new Size(188, 30);
            cbExistsByIDsp.TabIndex = 37;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(11, 324);
            label12.Name = "label12";
            label12.Size = new Size(358, 22);
            label12.TabIndex = 36;
            label12.Text = "Select \"ReadOneByOther\" Stored Procedure:";
            // 
            // cbReadOneByOthersp
            // 
            cbReadOneByOthersp.DropDownStyle = ComboBoxStyle.DropDownList;
            cbReadOneByOthersp.Enabled = false;
            cbReadOneByOthersp.FormattingEnabled = true;
            cbReadOneByOthersp.Location = new Point(376, 321);
            cbReadOneByOthersp.Margin = new Padding(4, 3, 4, 3);
            cbReadOneByOthersp.Name = "cbReadOneByOthersp";
            cbReadOneByOthersp.Size = new Size(188, 30);
            cbReadOneByOthersp.TabIndex = 35;
            cbReadOneByOthersp.SelectedIndexChanged += cbReadOneByOthersp_SelectedIndexChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(11, 146);
            label11.Name = "label11";
            label11.Size = new Size(333, 22);
            label11.TabIndex = 34;
            label11.Text = "Select \"ReadOneByID\" Stored Procedure:";
            // 
            // cbReadOneByIDsp
            // 
            cbReadOneByIDsp.DropDownStyle = ComboBoxStyle.DropDownList;
            cbReadOneByIDsp.Enabled = false;
            cbReadOneByIDsp.FormattingEnabled = true;
            cbReadOneByIDsp.Location = new Point(376, 143);
            cbReadOneByIDsp.Margin = new Padding(4, 3, 4, 3);
            cbReadOneByIDsp.Name = "cbReadOneByIDsp";
            cbReadOneByIDsp.Size = new Size(188, 30);
            cbReadOneByIDsp.TabIndex = 33;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 85);
            label1.Name = "label1";
            label1.Size = new Size(273, 22);
            label1.TabIndex = 32;
            label1.Text = "Select \"Create\" Stored Procedure:";
            // 
            // cbCreatesp
            // 
            cbCreatesp.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCreatesp.Enabled = false;
            cbCreatesp.FormattingEnabled = true;
            cbCreatesp.Location = new Point(376, 82);
            cbCreatesp.Margin = new Padding(4, 3, 4, 3);
            cbCreatesp.Name = "cbCreatesp";
            cbCreatesp.Size = new Size(188, 30);
            cbCreatesp.TabIndex = 31;
            // 
            // ulbl1
            // 
            ulbl1.AutoSize = true;
            ulbl1.Location = new Point(11, 15);
            ulbl1.Name = "ulbl1";
            ulbl1.Size = new Size(140, 22);
            ulbl1.TabIndex = 30;
            ulbl1.Text = "Select Database:";
            // 
            // cbDatabases
            // 
            cbDatabases.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDatabases.FormattingEnabled = true;
            cbDatabases.Location = new Point(158, 12);
            cbDatabases.Margin = new Padding(4, 3, 4, 3);
            cbDatabases.Name = "cbDatabases";
            cbDatabases.Size = new Size(188, 30);
            cbDatabases.TabIndex = 29;
            cbDatabases.SelectedIndexChanged += cbDatabases_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(383, 15);
            label2.Name = "label2";
            label2.Size = new Size(104, 22);
            label2.TabIndex = 56;
            label2.Text = "Select DTO:";
            // 
            // cbDTOs
            // 
            cbDTOs.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDTOs.FormattingEnabled = true;
            cbDTOs.Location = new Point(488, 12);
            cbDTOs.Margin = new Padding(4, 3, 4, 3);
            cbDTOs.Name = "cbDTOs";
            cbDTOs.Size = new Size(188, 30);
            cbDTOs.TabIndex = 55;
            cbDTOs.SelectedIndexChanged += cbDTOs_SelectedIndexChanged;
            // 
            // btnGenerateCode
            // 
            btnGenerateCode.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnGenerateCode.AutoSize = true;
            btnGenerateCode.Location = new Point(263, 753);
            btnGenerateCode.Name = "btnGenerateCode";
            btnGenerateCode.Size = new Size(136, 32);
            btnGenerateCode.TabIndex = 61;
            btnGenerateCode.Text = "Generate Code";
            btnGenerateCode.UseVisualStyleBackColor = true;
            btnGenerateCode.Click += btnGenerateCode_Click;
            // 
            // tbCode
            // 
            tbCode.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbCode.BackColor = Color.FromArgb(54, 54, 54);
            tbCode.Font = new Font("Cascadia Code", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbCode.ForeColor = SystemColors.HighlightText;
            tbCode.Location = new Point(729, 0);
            tbCode.MaxLength = 1000000;
            tbCode.Multiline = true;
            tbCode.Name = "tbCode";
            tbCode.ReadOnly = true;
            tbCode.ScrollBars = ScrollBars.Both;
            tbCode.Size = new Size(233, 797);
            tbCode.TabIndex = 62;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 22F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(962, 797);
            Controls.Add(tbCode);
            Controls.Add(btnGenerateCode);
            Controls.Add(label2);
            Controls.Add(cbDTOs);
            Controls.Add(chbDelete);
            Controls.Add(chbUpdate);
            Controls.Add(chbReadAll);
            Controls.Add(chbExistsByOther);
            Controls.Add(chbExistsByID);
            Controls.Add(chbReadOneByOther);
            Controls.Add(chbReadOneByID);
            Controls.Add(chbCreate);
            Controls.Add(label17);
            Controls.Add(cbDeletesp);
            Controls.Add(label16);
            Controls.Add(cbUpdatesp);
            Controls.Add(label15);
            Controls.Add(cbReadAllsp);
            Controls.Add(label14);
            Controls.Add(cbExistsByOthersp);
            Controls.Add(label13);
            Controls.Add(cbExistsByIDsp);
            Controls.Add(label12);
            Controls.Add(cbReadOneByOthersp);
            Controls.Add(label11);
            Controls.Add(cbReadOneByIDsp);
            Controls.Add(label1);
            Controls.Add(cbCreatesp);
            Controls.Add(ulbl1);
            Controls.Add(cbDatabases);
            Font = new Font("Tahoma", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4, 3, 4, 3);
            MinimumSize = new Size(980, 844);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox chbCreate;
        private CheckBox chbReadAll;
        private CheckBox chbReadOneByID;
        private CheckBox chbReadOneByOther;
        private CheckBox chbExistsByID;
        private CheckBox chbExistsByOther;
        private CheckBox chbUpdate;
        private CheckBox chbDelete;
        private Label label17;
        private Label label16;
        private Label label15;
        private Label label14;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label1;
        private Label ulbl1;
        private Label label2;
        private ComboBox cbCreatesp;
        private ComboBox cbReadAllsp;
        private ComboBox cbReadOneByIDsp;
        private ComboBox cbReadOneByOthersp;
        private ComboBox cbExistsByIDsp;
        private ComboBox cbExistsByOthersp;
        private ComboBox cbUpdatesp;
        private ComboBox cbDeletesp;
        private ComboBox cbDatabases;
        private ComboBox cbDTOs;
        private Button btnGenerateCode;
        private TextBox tbCode;
        private ErrorProvider errorProvider1;
    }
}
