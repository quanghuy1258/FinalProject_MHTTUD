namespace FinalProject_MHTTUD
{
    partial class EditInfoForm
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
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.emailLabel = new System.Windows.Forms.Label();
            this.verifyButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.addressLabel = new System.Windows.Forms.Label();
            this.phoneTextBox = new System.Windows.Forms.TextBox();
            this.birthdayDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.fullNameTextBox = new System.Windows.Forms.TextBox();
            this.phoneLabel = new System.Windows.Forms.Label();
            this.birthdayLabel = new System.Windows.Forms.Label();
            this.fullNameLabel = new System.Windows.Forms.Label();
            this.warningMsgLabel = new System.Windows.Forms.Label();
            this.newPasswordTextBox = new System.Windows.Forms.TextBox();
            this.newPasswordLabel = new System.Windows.Forms.Label();
            this.fullNameCheckBox = new System.Windows.Forms.CheckBox();
            this.birthdayCheckBox = new System.Windows.Forms.CheckBox();
            this.phoneCheckBox = new System.Windows.Forms.CheckBox();
            this.addressCheckBox = new System.Windows.Forms.CheckBox();
            this.newPasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.confirmPasswordTextBox = new System.Windows.Forms.TextBox();
            this.confirmPasswordLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(122, 32);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(200, 20);
            this.passwordTextBox.TabIndex = 1;
            this.passwordTextBox.TextChanged += new System.EventHandler(this.passwordTextBox_TextChanged);
            // 
            // emailTextBox
            // 
            this.emailTextBox.Location = new System.Drawing.Point(122, 6);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(200, 20);
            this.emailTextBox.TabIndex = 0;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(11, 35);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(66, 13);
            this.passwordLabel.TabIndex = 17;
            this.passwordLabel.Text = "Password (*)";
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(12, 9);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(45, 13);
            this.emailLabel.TabIndex = 16;
            this.emailLabel.Text = "Email (*)";
            // 
            // verifyButton
            // 
            this.verifyButton.Location = new System.Drawing.Point(247, 58);
            this.verifyButton.Name = "verifyButton";
            this.verifyButton.Size = new System.Drawing.Size(75, 23);
            this.verifyButton.TabIndex = 2;
            this.verifyButton.Text = "Verify";
            this.verifyButton.UseVisualStyleBackColor = true;
            this.verifyButton.Click += new System.EventHandler(this.verifyButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(166, 259);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 15;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(247, 259);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 14;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // addressTextBox
            // 
            this.addressTextBox.Location = new System.Drawing.Point(122, 168);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.Size = new System.Drawing.Size(200, 20);
            this.addressTextBox.TabIndex = 10;
            // 
            // addressLabel
            // 
            this.addressLabel.AutoSize = true;
            this.addressLabel.Location = new System.Drawing.Point(12, 171);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Size = new System.Drawing.Size(45, 13);
            this.addressLabel.TabIndex = 21;
            this.addressLabel.Text = "Address";
            // 
            // phoneTextBox
            // 
            this.phoneTextBox.Location = new System.Drawing.Point(122, 141);
            this.phoneTextBox.Name = "phoneTextBox";
            this.phoneTextBox.Size = new System.Drawing.Size(200, 20);
            this.phoneTextBox.TabIndex = 8;
            // 
            // birthdayDateTimePicker
            // 
            this.birthdayDateTimePicker.CustomFormat = "dd/MM/yyyy";
            this.birthdayDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.birthdayDateTimePicker.Location = new System.Drawing.Point(122, 114);
            this.birthdayDateTimePicker.Name = "birthdayDateTimePicker";
            this.birthdayDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.birthdayDateTimePicker.TabIndex = 6;
            this.birthdayDateTimePicker.Value = new System.DateTime(2018, 6, 24, 23, 26, 0, 0);
            // 
            // fullNameTextBox
            // 
            this.fullNameTextBox.Location = new System.Drawing.Point(122, 87);
            this.fullNameTextBox.Name = "fullNameTextBox";
            this.fullNameTextBox.Size = new System.Drawing.Size(200, 20);
            this.fullNameTextBox.TabIndex = 4;
            // 
            // phoneLabel
            // 
            this.phoneLabel.AutoSize = true;
            this.phoneLabel.Location = new System.Drawing.Point(12, 144);
            this.phoneLabel.Name = "phoneLabel";
            this.phoneLabel.Size = new System.Drawing.Size(38, 13);
            this.phoneLabel.TabIndex = 20;
            this.phoneLabel.Text = "Phone";
            // 
            // birthdayLabel
            // 
            this.birthdayLabel.AutoSize = true;
            this.birthdayLabel.Location = new System.Drawing.Point(12, 115);
            this.birthdayLabel.Name = "birthdayLabel";
            this.birthdayLabel.Size = new System.Drawing.Size(45, 13);
            this.birthdayLabel.TabIndex = 19;
            this.birthdayLabel.Text = "Birthday";
            // 
            // fullNameLabel
            // 
            this.fullNameLabel.AutoSize = true;
            this.fullNameLabel.Location = new System.Drawing.Point(12, 90);
            this.fullNameLabel.Name = "fullNameLabel";
            this.fullNameLabel.Size = new System.Drawing.Size(54, 13);
            this.fullNameLabel.TabIndex = 18;
            this.fullNameLabel.Text = "Full Name";
            // 
            // warningMsgLabel
            // 
            this.warningMsgLabel.AutoSize = true;
            this.warningMsgLabel.ForeColor = System.Drawing.Color.Red;
            this.warningMsgLabel.Location = new System.Drawing.Point(11, 243);
            this.warningMsgLabel.Name = "warningMsgLabel";
            this.warningMsgLabel.Size = new System.Drawing.Size(182, 13);
            this.warningMsgLabel.TabIndex = 24;
            this.warningMsgLabel.Text = "Warning Message Warning Message";
            this.warningMsgLabel.Visible = false;
            // 
            // newPasswordTextBox
            // 
            this.newPasswordTextBox.Location = new System.Drawing.Point(122, 194);
            this.newPasswordTextBox.Name = "newPasswordTextBox";
            this.newPasswordTextBox.PasswordChar = '*';
            this.newPasswordTextBox.Size = new System.Drawing.Size(200, 20);
            this.newPasswordTextBox.TabIndex = 12;
            this.newPasswordTextBox.TextChanged += new System.EventHandler(this.newPasswordTextBox_TextChanged);
            // 
            // newPasswordLabel
            // 
            this.newPasswordLabel.AutoSize = true;
            this.newPasswordLabel.Location = new System.Drawing.Point(12, 197);
            this.newPasswordLabel.Name = "newPasswordLabel";
            this.newPasswordLabel.Size = new System.Drawing.Size(91, 13);
            this.newPasswordLabel.TabIndex = 22;
            this.newPasswordLabel.Text = "New Password (*)";
            // 
            // fullNameCheckBox
            // 
            this.fullNameCheckBox.AutoSize = true;
            this.fullNameCheckBox.Location = new System.Drawing.Point(328, 90);
            this.fullNameCheckBox.Name = "fullNameCheckBox";
            this.fullNameCheckBox.Size = new System.Drawing.Size(15, 14);
            this.fullNameCheckBox.TabIndex = 3;
            this.fullNameCheckBox.UseVisualStyleBackColor = true;
            this.fullNameCheckBox.CheckedChanged += new System.EventHandler(this.fullNameCheckBox_CheckedChanged);
            // 
            // birthdayCheckBox
            // 
            this.birthdayCheckBox.AutoSize = true;
            this.birthdayCheckBox.Location = new System.Drawing.Point(328, 115);
            this.birthdayCheckBox.Name = "birthdayCheckBox";
            this.birthdayCheckBox.Size = new System.Drawing.Size(15, 14);
            this.birthdayCheckBox.TabIndex = 5;
            this.birthdayCheckBox.UseVisualStyleBackColor = true;
            this.birthdayCheckBox.CheckedChanged += new System.EventHandler(this.birthdayCheckBox_CheckedChanged);
            // 
            // phoneCheckBox
            // 
            this.phoneCheckBox.AutoSize = true;
            this.phoneCheckBox.Location = new System.Drawing.Point(328, 143);
            this.phoneCheckBox.Name = "phoneCheckBox";
            this.phoneCheckBox.Size = new System.Drawing.Size(15, 14);
            this.phoneCheckBox.TabIndex = 7;
            this.phoneCheckBox.UseVisualStyleBackColor = true;
            this.phoneCheckBox.CheckedChanged += new System.EventHandler(this.phoneCheckBox_CheckedChanged);
            // 
            // addressCheckBox
            // 
            this.addressCheckBox.AutoSize = true;
            this.addressCheckBox.Location = new System.Drawing.Point(328, 170);
            this.addressCheckBox.Name = "addressCheckBox";
            this.addressCheckBox.Size = new System.Drawing.Size(15, 14);
            this.addressCheckBox.TabIndex = 9;
            this.addressCheckBox.UseVisualStyleBackColor = true;
            this.addressCheckBox.CheckedChanged += new System.EventHandler(this.addressCheckBox_CheckedChanged);
            // 
            // newPasswordCheckBox
            // 
            this.newPasswordCheckBox.AutoSize = true;
            this.newPasswordCheckBox.Location = new System.Drawing.Point(328, 197);
            this.newPasswordCheckBox.Name = "newPasswordCheckBox";
            this.newPasswordCheckBox.Size = new System.Drawing.Size(15, 14);
            this.newPasswordCheckBox.TabIndex = 11;
            this.newPasswordCheckBox.UseVisualStyleBackColor = true;
            this.newPasswordCheckBox.CheckedChanged += new System.EventHandler(this.newPasswordCheckBox_CheckedChanged);
            // 
            // confirmPasswordTextBox
            // 
            this.confirmPasswordTextBox.Location = new System.Drawing.Point(122, 220);
            this.confirmPasswordTextBox.Name = "confirmPasswordTextBox";
            this.confirmPasswordTextBox.PasswordChar = '*';
            this.confirmPasswordTextBox.Size = new System.Drawing.Size(200, 20);
            this.confirmPasswordTextBox.TabIndex = 13;
            this.confirmPasswordTextBox.TextChanged += new System.EventHandler(this.confirmPasswordTextBox_TextChanged);
            // 
            // confirmPasswordLabel
            // 
            this.confirmPasswordLabel.AutoSize = true;
            this.confirmPasswordLabel.Location = new System.Drawing.Point(12, 223);
            this.confirmPasswordLabel.Name = "confirmPasswordLabel";
            this.confirmPasswordLabel.Size = new System.Drawing.Size(104, 13);
            this.confirmPasswordLabel.TabIndex = 23;
            this.confirmPasswordLabel.Text = "Confirm Password (*)";
            // 
            // EditInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 294);
            this.Controls.Add(this.confirmPasswordTextBox);
            this.Controls.Add(this.confirmPasswordLabel);
            this.Controls.Add(this.newPasswordCheckBox);
            this.Controls.Add(this.addressCheckBox);
            this.Controls.Add(this.phoneCheckBox);
            this.Controls.Add(this.birthdayCheckBox);
            this.Controls.Add(this.fullNameCheckBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.addressTextBox);
            this.Controls.Add(this.addressLabel);
            this.Controls.Add(this.phoneTextBox);
            this.Controls.Add(this.birthdayDateTimePicker);
            this.Controls.Add(this.fullNameTextBox);
            this.Controls.Add(this.phoneLabel);
            this.Controls.Add(this.birthdayLabel);
            this.Controls.Add(this.fullNameLabel);
            this.Controls.Add(this.warningMsgLabel);
            this.Controls.Add(this.newPasswordTextBox);
            this.Controls.Add(this.newPasswordLabel);
            this.Controls.Add(this.verifyButton);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.emailTextBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.emailLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditInfoForm";
            this.Text = "Edit Information";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditInfoForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.Button verifyButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox addressTextBox;
        private System.Windows.Forms.Label addressLabel;
        private System.Windows.Forms.TextBox phoneTextBox;
        private System.Windows.Forms.DateTimePicker birthdayDateTimePicker;
        private System.Windows.Forms.TextBox fullNameTextBox;
        private System.Windows.Forms.Label phoneLabel;
        private System.Windows.Forms.Label birthdayLabel;
        private System.Windows.Forms.Label fullNameLabel;
        private System.Windows.Forms.Label warningMsgLabel;
        private System.Windows.Forms.TextBox newPasswordTextBox;
        private System.Windows.Forms.Label newPasswordLabel;
        private System.Windows.Forms.CheckBox fullNameCheckBox;
        private System.Windows.Forms.CheckBox birthdayCheckBox;
        private System.Windows.Forms.CheckBox phoneCheckBox;
        private System.Windows.Forms.CheckBox addressCheckBox;
        private System.Windows.Forms.CheckBox newPasswordCheckBox;
        private System.Windows.Forms.TextBox confirmPasswordTextBox;
        private System.Windows.Forms.Label confirmPasswordLabel;
    }
}