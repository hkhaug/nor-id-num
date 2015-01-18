namespace NinUi
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBoxVerification = new System.Windows.Forms.GroupBox();
            this.buttonVerify = new System.Windows.Forms.Button();
            this.textBoxVerifyNumber = new System.Windows.Forms.TextBox();
            this.groupBoxVerificationType = new System.Windows.Forms.GroupBox();
            this.radioButtonVerifyUnknown = new System.Windows.Forms.RadioButton();
            this.radioButtonVerifyDNumber = new System.Windows.Forms.RadioButton();
            this.radioButtonVerifyBirthNumber = new System.Windows.Forms.RadioButton();
            this.radioButtonVerifyOrganizationNumber = new System.Windows.Forms.RadioButton();
            this.groupBoxGeneration = new System.Windows.Forms.GroupBox();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.comboBoxGender = new System.Windows.Forms.ComboBox();
            this.labelGender = new System.Windows.Forms.Label();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.labelDateTo = new System.Windows.Forms.Label();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.labelDateFrom = new System.Windows.Forms.Label();
            this.textBoxPattern = new System.Windows.Forms.TextBox();
            this.labelPattern = new System.Windows.Forms.Label();
            this.comboBoxVaryUsing = new System.Windows.Forms.ComboBox();
            this.labelVaryUsing = new System.Windows.Forms.Label();
            this.comboBoxGenerateWhat = new System.Windows.Forms.ComboBox();
            this.labelGenerateWhat = new System.Windows.Forms.Label();
            this.groupBoxVerification.SuspendLayout();
            this.groupBoxVerificationType.SuspendLayout();
            this.groupBoxGeneration.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxVerification
            // 
            this.groupBoxVerification.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxVerification.Controls.Add(this.buttonVerify);
            this.groupBoxVerification.Controls.Add(this.textBoxVerifyNumber);
            this.groupBoxVerification.Controls.Add(this.groupBoxVerificationType);
            this.groupBoxVerification.Location = new System.Drawing.Point(12, 12);
            this.groupBoxVerification.Name = "groupBoxVerification";
            this.groupBoxVerification.Size = new System.Drawing.Size(154, 202);
            this.groupBoxVerification.TabIndex = 0;
            this.groupBoxVerification.TabStop = false;
            this.groupBoxVerification.Text = "Kontroll";
            // 
            // buttonVerify
            // 
            this.buttonVerify.Location = new System.Drawing.Point(40, 170);
            this.buttonVerify.Name = "buttonVerify";
            this.buttonVerify.Size = new System.Drawing.Size(75, 23);
            this.buttonVerify.TabIndex = 2;
            this.buttonVerify.Text = "Kontroller";
            this.buttonVerify.UseVisualStyleBackColor = true;
            this.buttonVerify.Click += new System.EventHandler(this.buttonVerify_Click);
            this.buttonVerify.Enter += new System.EventHandler(this.verifyControl_Enter);
            // 
            // textBoxVerifyNumber
            // 
            this.textBoxVerifyNumber.Location = new System.Drawing.Point(6, 143);
            this.textBoxVerifyNumber.MaxLength = 20;
            this.textBoxVerifyNumber.Name = "textBoxVerifyNumber";
            this.textBoxVerifyNumber.Size = new System.Drawing.Size(141, 20);
            this.textBoxVerifyNumber.TabIndex = 1;
            this.textBoxVerifyNumber.Enter += new System.EventHandler(this.verifyControl_Enter);
            // 
            // groupBoxVerificationType
            // 
            this.groupBoxVerificationType.Controls.Add(this.radioButtonVerifyUnknown);
            this.groupBoxVerificationType.Controls.Add(this.radioButtonVerifyDNumber);
            this.groupBoxVerificationType.Controls.Add(this.radioButtonVerifyBirthNumber);
            this.groupBoxVerificationType.Controls.Add(this.radioButtonVerifyOrganizationNumber);
            this.groupBoxVerificationType.Location = new System.Drawing.Point(6, 19);
            this.groupBoxVerificationType.Name = "groupBoxVerificationType";
            this.groupBoxVerificationType.Size = new System.Drawing.Size(141, 115);
            this.groupBoxVerificationType.TabIndex = 0;
            this.groupBoxVerificationType.TabStop = false;
            this.groupBoxVerificationType.Text = "&Type ID-nummer";
            this.groupBoxVerificationType.Enter += new System.EventHandler(this.verifyControl_Enter);
            // 
            // radioButtonVerifyUnknown
            // 
            this.radioButtonVerifyUnknown.AutoSize = true;
            this.radioButtonVerifyUnknown.Checked = true;
            this.radioButtonVerifyUnknown.Location = new System.Drawing.Point(6, 91);
            this.radioButtonVerifyUnknown.Name = "radioButtonVerifyUnknown";
            this.radioButtonVerifyUnknown.Size = new System.Drawing.Size(98, 17);
            this.radioButtonVerifyUnknown.TabIndex = 3;
            this.radioButtonVerifyUnknown.TabStop = true;
            this.radioButtonVerifyUnknown.Text = "&Samma det, vel";
            this.radioButtonVerifyUnknown.UseVisualStyleBackColor = true;
            this.radioButtonVerifyUnknown.Enter += new System.EventHandler(this.verifyControl_Enter);
            // 
            // radioButtonVerifyDNumber
            // 
            this.radioButtonVerifyDNumber.AutoSize = true;
            this.radioButtonVerifyDNumber.Location = new System.Drawing.Point(6, 67);
            this.radioButtonVerifyDNumber.Name = "radioButtonVerifyDNumber";
            this.radioButtonVerifyDNumber.Size = new System.Drawing.Size(73, 17);
            this.radioButtonVerifyDNumber.TabIndex = 2;
            this.radioButtonVerifyDNumber.Text = "&D-nummer";
            this.radioButtonVerifyDNumber.UseVisualStyleBackColor = true;
            this.radioButtonVerifyDNumber.Enter += new System.EventHandler(this.verifyControl_Enter);
            // 
            // radioButtonVerifyBirthNumber
            // 
            this.radioButtonVerifyBirthNumber.AutoSize = true;
            this.radioButtonVerifyBirthNumber.Location = new System.Drawing.Point(6, 43);
            this.radioButtonVerifyBirthNumber.Name = "radioButtonVerifyBirthNumber";
            this.radioButtonVerifyBirthNumber.Size = new System.Drawing.Size(98, 17);
            this.radioButtonVerifyBirthNumber.TabIndex = 1;
            this.radioButtonVerifyBirthNumber.Text = "&Fødselsnummer";
            this.radioButtonVerifyBirthNumber.UseVisualStyleBackColor = true;
            this.radioButtonVerifyBirthNumber.Enter += new System.EventHandler(this.verifyControl_Enter);
            // 
            // radioButtonVerifyOrganizationNumber
            // 
            this.radioButtonVerifyOrganizationNumber.AutoSize = true;
            this.radioButtonVerifyOrganizationNumber.Location = new System.Drawing.Point(6, 19);
            this.radioButtonVerifyOrganizationNumber.Name = "radioButtonVerifyOrganizationNumber";
            this.radioButtonVerifyOrganizationNumber.Size = new System.Drawing.Size(128, 17);
            this.radioButtonVerifyOrganizationNumber.TabIndex = 0;
            this.radioButtonVerifyOrganizationNumber.Text = "&Organisasjonsnummer";
            this.radioButtonVerifyOrganizationNumber.UseVisualStyleBackColor = true;
            this.radioButtonVerifyOrganizationNumber.Enter += new System.EventHandler(this.verifyControl_Enter);
            // 
            // groupBoxGeneration
            // 
            this.groupBoxGeneration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxGeneration.Controls.Add(this.buttonGenerate);
            this.groupBoxGeneration.Controls.Add(this.comboBoxGender);
            this.groupBoxGeneration.Controls.Add(this.labelGender);
            this.groupBoxGeneration.Controls.Add(this.dateTimePickerTo);
            this.groupBoxGeneration.Controls.Add(this.labelDateTo);
            this.groupBoxGeneration.Controls.Add(this.dateTimePickerFrom);
            this.groupBoxGeneration.Controls.Add(this.labelDateFrom);
            this.groupBoxGeneration.Controls.Add(this.textBoxPattern);
            this.groupBoxGeneration.Controls.Add(this.labelPattern);
            this.groupBoxGeneration.Controls.Add(this.comboBoxVaryUsing);
            this.groupBoxGeneration.Controls.Add(this.labelVaryUsing);
            this.groupBoxGeneration.Controls.Add(this.comboBoxGenerateWhat);
            this.groupBoxGeneration.Controls.Add(this.labelGenerateWhat);
            this.groupBoxGeneration.Location = new System.Drawing.Point(172, 12);
            this.groupBoxGeneration.Name = "groupBoxGeneration";
            this.groupBoxGeneration.Size = new System.Drawing.Size(243, 202);
            this.groupBoxGeneration.TabIndex = 1;
            this.groupBoxGeneration.TabStop = false;
            this.groupBoxGeneration.Text = "Generering";
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(83, 170);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerate.TabIndex = 12;
            this.buttonGenerate.Text = "Generer";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            this.buttonGenerate.Enter += new System.EventHandler(this.generateControl_Enter);
            // 
            // comboBoxGender
            // 
            this.comboBoxGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGender.FormattingEnabled = true;
            this.comboBoxGender.Items.AddRange(new object[] {
            "Samma det, vel",
            "Kvinne",
            "Mann"});
            this.comboBoxGender.Location = new System.Drawing.Point(83, 143);
            this.comboBoxGender.Name = "comboBoxGender";
            this.comboBoxGender.Size = new System.Drawing.Size(148, 21);
            this.comboBoxGender.TabIndex = 11;
            this.comboBoxGender.Enter += new System.EventHandler(this.generateControl_Enter);
            // 
            // labelGender
            // 
            this.labelGender.AutoSize = true;
            this.labelGender.Location = new System.Drawing.Point(6, 146);
            this.labelGender.Name = "labelGender";
            this.labelGender.Size = new System.Drawing.Size(34, 13);
            this.labelGender.TabIndex = 10;
            this.labelGender.Text = "&Kjønn";
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTo.Location = new System.Drawing.Point(83, 118);
            this.dateTimePickerTo.MaxDate = new System.DateTime(2039, 12, 31, 0, 0, 0, 0);
            this.dateTimePickerTo.MinDate = new System.DateTime(1854, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(148, 20);
            this.dateTimePickerTo.TabIndex = 9;
            this.dateTimePickerTo.Value = new System.DateTime(2039, 12, 31, 0, 0, 0, 0);
            this.dateTimePickerTo.Enter += new System.EventHandler(this.generateControl_Enter);
            // 
            // labelDateTo
            // 
            this.labelDateTo.AutoSize = true;
            this.labelDateTo.Location = new System.Drawing.Point(6, 122);
            this.labelDateTo.Name = "labelDateTo";
            this.labelDateTo.Size = new System.Drawing.Size(42, 13);
            this.labelDateTo.TabIndex = 8;
            this.labelDateTo.Text = "T&il dato";
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFrom.Location = new System.Drawing.Point(83, 93);
            this.dateTimePickerFrom.MaxDate = new System.DateTime(2039, 12, 31, 0, 0, 0, 0);
            this.dateTimePickerFrom.MinDate = new System.DateTime(1854, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(148, 20);
            this.dateTimePickerFrom.TabIndex = 7;
            this.dateTimePickerFrom.Value = new System.DateTime(1854, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerFrom.Enter += new System.EventHandler(this.generateControl_Enter);
            // 
            // labelDateFrom
            // 
            this.labelDateFrom.AutoSize = true;
            this.labelDateFrom.Location = new System.Drawing.Point(6, 97);
            this.labelDateFrom.Name = "labelDateFrom";
            this.labelDateFrom.Size = new System.Drawing.Size(46, 13);
            this.labelDateFrom.TabIndex = 6;
            this.labelDateFrom.Text = "F&ra dato";
            // 
            // textBoxPattern
            // 
            this.textBoxPattern.Location = new System.Drawing.Point(83, 68);
            this.textBoxPattern.MaxLength = 20;
            this.textBoxPattern.Name = "textBoxPattern";
            this.textBoxPattern.Size = new System.Drawing.Size(148, 20);
            this.textBoxPattern.TabIndex = 5;
            this.textBoxPattern.Enter += new System.EventHandler(this.generateControl_Enter);
            // 
            // labelPattern
            // 
            this.labelPattern.AutoSize = true;
            this.labelPattern.Location = new System.Drawing.Point(6, 71);
            this.labelPattern.Name = "labelPattern";
            this.labelPattern.Size = new System.Drawing.Size(45, 13);
            this.labelPattern.TabIndex = 4;
            this.labelPattern.Text = "&Mønster";
            // 
            // comboBoxVaryUsing
            // 
            this.comboBoxVaryUsing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVaryUsing.FormattingEnabled = true;
            this.comboBoxVaryUsing.Items.AddRange(new object[] {
            "Mønster",
            "Datoer og kjønn"});
            this.comboBoxVaryUsing.Location = new System.Drawing.Point(83, 42);
            this.comboBoxVaryUsing.Name = "comboBoxVaryUsing";
            this.comboBoxVaryUsing.Size = new System.Drawing.Size(148, 21);
            this.comboBoxVaryUsing.TabIndex = 3;
            this.comboBoxVaryUsing.SelectedIndexChanged += new System.EventHandler(this.comboBoxVaryUsing_SelectedIndexChanged);
            this.comboBoxVaryUsing.Enter += new System.EventHandler(this.generateControl_Enter);
            // 
            // labelVaryUsing
            // 
            this.labelVaryUsing.AutoSize = true;
            this.labelVaryUsing.Location = new System.Drawing.Point(6, 45);
            this.labelVaryUsing.Name = "labelVaryUsing";
            this.labelVaryUsing.Size = new System.Drawing.Size(57, 13);
            this.labelVaryUsing.TabIndex = 2;
            this.labelVaryUsing.Text = "&Varier med";
            // 
            // comboBoxGenerateWhat
            // 
            this.comboBoxGenerateWhat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGenerateWhat.FormattingEnabled = true;
            this.comboBoxGenerateWhat.Items.AddRange(new object[] {
            "Organisasjonsnummer",
            "Fødselsnummer",
            "D-nummer"});
            this.comboBoxGenerateWhat.Location = new System.Drawing.Point(83, 16);
            this.comboBoxGenerateWhat.Name = "comboBoxGenerateWhat";
            this.comboBoxGenerateWhat.Size = new System.Drawing.Size(148, 21);
            this.comboBoxGenerateWhat.TabIndex = 1;
            this.comboBoxGenerateWhat.SelectedIndexChanged += new System.EventHandler(this.comboBoxGenerateWhat_SelectedIndexChanged);
            this.comboBoxGenerateWhat.Enter += new System.EventHandler(this.generateControl_Enter);
            // 
            // labelGenerateWhat
            // 
            this.labelGenerateWhat.AutoSize = true;
            this.labelGenerateWhat.Location = new System.Drawing.Point(6, 19);
            this.labelGenerateWhat.Name = "labelGenerateWhat";
            this.labelGenerateWhat.Size = new System.Drawing.Size(57, 13);
            this.labelGenerateWhat.TabIndex = 0;
            this.labelGenerateWhat.Text = "&Generer et";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(425, 226);
            this.Controls.Add(this.groupBoxGeneration);
            this.Controls.Add(this.groupBoxVerification);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Norsk ID-nummer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBoxVerification.ResumeLayout(false);
            this.groupBoxVerification.PerformLayout();
            this.groupBoxVerificationType.ResumeLayout(false);
            this.groupBoxVerificationType.PerformLayout();
            this.groupBoxGeneration.ResumeLayout(false);
            this.groupBoxGeneration.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxVerification;
        private System.Windows.Forms.GroupBox groupBoxVerificationType;
        private System.Windows.Forms.RadioButton radioButtonVerifyUnknown;
        private System.Windows.Forms.RadioButton radioButtonVerifyDNumber;
        private System.Windows.Forms.RadioButton radioButtonVerifyBirthNumber;
        private System.Windows.Forms.RadioButton radioButtonVerifyOrganizationNumber;
        private System.Windows.Forms.Button buttonVerify;
        private System.Windows.Forms.TextBox textBoxVerifyNumber;
        private System.Windows.Forms.GroupBox groupBoxGeneration;
        private System.Windows.Forms.ComboBox comboBoxGenerateWhat;
        private System.Windows.Forms.Label labelGenerateWhat;
        private System.Windows.Forms.Label labelVaryUsing;
        private System.Windows.Forms.ComboBox comboBoxGender;
        private System.Windows.Forms.Label labelGender;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.Label labelDateTo;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.Label labelDateFrom;
        private System.Windows.Forms.TextBox textBoxPattern;
        private System.Windows.Forms.Label labelPattern;
        private System.Windows.Forms.ComboBox comboBoxVaryUsing;
        private System.Windows.Forms.Button buttonGenerate;
    }
}

