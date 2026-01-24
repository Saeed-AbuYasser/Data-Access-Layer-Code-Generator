namespace DALCodeGenerator
{
    partial class frmAddFunction
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblFunctionParameters = new Label();
            label10 = new Label();
            btnAdd = new Button();
            gbParameterInfo = new GroupBox();
            panel1 = new Panel();
            label1 = new Label();
            radioButton2 = new RadioButton();
            textBox5 = new TextBox();
            radioButton1 = new RadioButton();
            button5 = new Button();
            checkBox2 = new CheckBox();
            comboBox7 = new ComboBox();
            label9 = new Label();
            textBox4 = new TextBox();
            label8 = new Label();
            label7 = new Label();
            tbFunctionName = new TextBox();
            label6 = new Label();
            cbFunctionReturnType = new ComboBox();
            btnSaveFunction = new Button();
            dataGridView1 = new DataGridView();
            gbParameterInfo.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // lblFunctionParameters
            // 
            lblFunctionParameters.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFunctionParameters.Location = new Point(12, 116);
            lblFunctionParameters.Name = "lblFunctionParameters";
            lblFunctionParameters.Size = new Size(312, 188);
            lblFunctionParameters.TabIndex = 37;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(12, 96);
            label10.Name = "label10";
            label10.Size = new Size(85, 20);
            label10.TabIndex = 36;
            label10.Text = "Parameters:";
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAdd.BackColor = Color.DarkOrchid;
            btnAdd.BackgroundImageLayout = ImageLayout.Zoom;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Tahoma", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(359, 86);
            btnAdd.Margin = new Padding(4, 3, 4, 3);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(232, 38);
            btnAdd.TabIndex = 35;
            btnAdd.Text = "Add Primitive Parameter";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // gbParameterInfo
            // 
            gbParameterInfo.Controls.Add(panel1);
            gbParameterInfo.Controls.Add(button5);
            gbParameterInfo.Controls.Add(checkBox2);
            gbParameterInfo.Controls.Add(comboBox7);
            gbParameterInfo.Controls.Add(label9);
            gbParameterInfo.Controls.Add(textBox4);
            gbParameterInfo.Controls.Add(label8);
            gbParameterInfo.Location = new Point(344, 99);
            gbParameterInfo.Name = "gbParameterInfo";
            gbParameterInfo.Size = new Size(312, 289);
            gbParameterInfo.TabIndex = 34;
            gbParameterInfo.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Controls.Add(radioButton2);
            panel1.Controls.Add(textBox5);
            panel1.Controls.Add(radioButton1);
            panel1.Location = new Point(48, 151);
            panel1.Name = "panel1";
            panel1.Size = new Size(232, 97);
            panel1.TabIndex = 39;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(49, 1);
            label1.Name = "label1";
            label1.Size = new Size(101, 20);
            label1.TabIndex = 38;
            label1.Text = "Default Value:";
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(8, 23);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(57, 24);
            radioButton2.TabIndex = 19;
            radioButton2.TabStop = true;
            radioButton2.Text = "Null";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(94, 52);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(105, 27);
            textBox5.TabIndex = 24;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(8, 55);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(80, 24);
            radioButton1.TabIndex = 20;
            radioButton1.TabStop = true;
            radioButton1.Text = "Custom";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(98, 254);
            button5.Name = "button5";
            button5.Size = new Size(94, 29);
            button5.TabIndex = 26;
            button5.Text = "Save";
            button5.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(15, 115);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(89, 24);
            checkBox2.TabIndex = 22;
            checkBox2.Text = "Optional";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // comboBox7
            // 
            comboBox7.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox7.FormattingEnabled = true;
            comboBox7.Items.AddRange(new object[] { "byte", "sbyte", "short", "ushort", "long", "ulong", "float", "ufloat", "double", "udouble", "decimal", "udecimal", "int", "uint", "string", "char", "bool" });
            comboBox7.Location = new Point(67, 74);
            comboBox7.Name = "comboBox7";
            comboBox7.Size = new Size(105, 28);
            comboBox7.TabIndex = 19;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(6, 77);
            label9.Name = "label9";
            label9.Size = new Size(40, 20);
            label9.TabIndex = 20;
            label9.Text = "Type";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(67, 31);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(105, 27);
            textBox4.TabIndex = 19;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 34);
            label8.Name = "label8";
            label8.Size = new Size(49, 20);
            label8.TabIndex = 0;
            label8.Text = "Name";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 56);
            label7.Name = "label7";
            label7.Size = new Size(109, 20);
            label7.TabIndex = 33;
            label7.Text = "Function Name";
            // 
            // tbFunctionName
            // 
            tbFunctionName.Location = new Point(148, 53);
            tbFunctionName.Name = "tbFunctionName";
            tbFunctionName.Size = new Size(151, 27);
            tbFunctionName.TabIndex = 32;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 9);
            label6.Name = "label6";
            label6.Size = new Size(87, 20);
            label6.TabIndex = 31;
            label6.Text = "Return Type";
            // 
            // cbFunctionReturnType
            // 
            cbFunctionReturnType.FormattingEnabled = true;
            cbFunctionReturnType.Items.AddRange(new object[] { "byte", "sbyte", "short", "ushort", "long", "ulong", "float", "ufloat", "double", "udouble", "decimal", "udecimal", "int", "uint", "string", "char", "bool" });
            cbFunctionReturnType.Location = new Point(148, 6);
            cbFunctionReturnType.Name = "cbFunctionReturnType";
            cbFunctionReturnType.Size = new Size(151, 28);
            cbFunctionReturnType.TabIndex = 30;
            // 
            // btnSaveFunction
            // 
            btnSaveFunction.Location = new Point(806, 665);
            btnSaveFunction.Name = "btnSaveFunction";
            btnSaveFunction.Size = new Size(94, 29);
            btnSaveFunction.TabIndex = 29;
            btnSaveFunction.Text = "Save";
            btnSaveFunction.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(175, 428);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(742, 188);
            dataGridView1.TabIndex = 38;
            // 
            // frmAddFunction
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(973, 706);
            Controls.Add(dataGridView1);
            Controls.Add(lblFunctionParameters);
            Controls.Add(label10);
            Controls.Add(btnAdd);
            Controls.Add(gbParameterInfo);
            Controls.Add(label7);
            Controls.Add(tbFunctionName);
            Controls.Add(label6);
            Controls.Add(cbFunctionReturnType);
            Controls.Add(btnSaveFunction);
            Name = "frmAddFunction";
            Text = "frmAddFunction";
            gbParameterInfo.ResumeLayout(false);
            gbParameterInfo.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblFunctionParameters;
        private Label label10;
        private Button btnAdd;
        private GroupBox gbParameterInfo;
        private Button button5;
        private RadioButton radioButton1;
        private TextBox textBox5;
        private RadioButton radioButton2;
        private CheckBox checkBox2;
        private ComboBox comboBox7;
        private Label label9;
        private TextBox textBox4;
        private Label label8;
        private Label label7;
        private TextBox tbFunctionName;
        private Label label6;
        private ComboBox cbFunctionReturnType;
        private Button btnSaveFunction;
        private Panel panel1;
        private Label label1;
        private DataGridView dataGridView1;
    }
}