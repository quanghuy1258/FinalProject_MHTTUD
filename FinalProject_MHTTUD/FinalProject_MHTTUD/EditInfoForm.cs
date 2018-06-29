using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace FinalProject_MHTTUD
{
    public partial class EditInfoForm : Form
    {
        private bool _isSave = false;
        private List<Account> _db = null;
        private int _id = -1;

        private bool checkPasswordConstraints()
        {
            this.warningMsgLabel.Visible = false;
            if (this.passwordTextBox.Text == "")
            {
                this.warningMsgLabel.Text = "Waring: Empty Password not allowed";
                this.warningMsgLabel.Visible = true;
                return false;
            }
            return true;
        }
        private bool checkNewPasswordConstraints()
        {
            this.warningMsgLabel.Visible = false;
            if (this.newPasswordTextBox.Text == "")
            {
                this.warningMsgLabel.Text = "Waring: Empty New Password not allowed";
                this.warningMsgLabel.Visible = true;
                return false;
            }
            if (this.newPasswordTextBox.Text != this.confirmPasswordTextBox.Text)
            {
                this.warningMsgLabel.Text = "Waring: New Password does not match the confirm password";
                this.warningMsgLabel.Visible = true;
                return false;
            }
            return true;
        }
        private bool isSave()
        {
            this.warningMsgLabel.Visible = false;
            if (this._db == null)
            {
                this.warningMsgLabel.Text = "Waring: Null Database";
                this.warningMsgLabel.Visible = true;
                this.emailTextBox.Text = "";
                return false;
            }
            if (this.newPasswordCheckBox.Checked && !checkNewPasswordConstraints()) return false;
            if (this.fullNameCheckBox.Checked)
                this._db[this._id].changeFullName(this.passwordTextBox.Text, this.fullNameTextBox.Text);
            if (this.birthdayCheckBox.Checked)
                this._db[this._id].changeBirthday(this.passwordTextBox.Text, this.birthdayDateTimePicker.Value.Date.ToString("dd/MM/yyyy"));
            if (this.phoneCheckBox.Checked)
                this._db[this._id].changePhoneNumber(this.passwordTextBox.Text, this.phoneTextBox.Text);
            if (this.addressCheckBox.Checked)
                this._db[this._id].changeAddress(this.passwordTextBox.Text, this.addressTextBox.Text);
            if (this.newPasswordCheckBox.Checked)
                this._db[this._id].changePassphrase(this.passwordTextBox.Text, this.newPasswordTextBox.Text);
            return true;
        }
        private void isSaveEnabled()
        {
            if (this.fullNameCheckBox.Checked
                || this.birthdayCheckBox.Checked
                || this.phoneCheckBox.Checked
                || this.addressCheckBox.Checked
                || this.newPasswordCheckBox.Checked)
            {
                this.saveButton.Enabled = true;
            }
            else this.saveButton.Enabled = false;
        }

        public EditInfoForm(ref List<Account> db, string email)
        {
            InitializeComponent();
            this._db = db;
            this.emailTextBox.Text = email;
            this.emailTextBox.Enabled
                = this.fullNameTextBox.Enabled
                = this.fullNameCheckBox.Enabled
                = this.birthdayDateTimePicker.Enabled
                = this.birthdayCheckBox.Enabled
                = this.phoneTextBox.Enabled
                = this.phoneCheckBox.Enabled
                = this.addressTextBox.Enabled
                = this.addressCheckBox.Enabled
                = this.newPasswordTextBox.Enabled
                = this.newPasswordCheckBox.Enabled
                = this.confirmPasswordTextBox.Enabled
                = this.saveButton.Enabled = false;
            this.birthdayDateTimePicker.Value = DateTime.Now;
        }

        private void verifyButton_Click(object sender, EventArgs e)
        {
            this.warningMsgLabel.Visible = false;
            if (this._db == null)
            {
                this.warningMsgLabel.Text = "Waring: Null Database";
                this.warningMsgLabel.Visible = true;
                this.emailTextBox.Text = "";
                return;
            }
            if (!checkPasswordConstraints()) return;
            for (int i = 0; i < this._db.Count; i++)
            {
                if (this._db[i].getEmailAddress() == this.emailTextBox.Text
                    && this._db[i].checkPassphrase(this.passwordTextBox.Text))
                {
                    this._id = i;
                    this.passwordTextBox.Enabled 
                        = this.verifyButton.Enabled = false;
                    this.fullNameCheckBox.Enabled
                        = this.birthdayCheckBox.Enabled
                        = this.phoneCheckBox.Enabled
                        = this.addressCheckBox.Enabled
                        = this.newPasswordCheckBox.Enabled = true;
                    this.fullNameTextBox.Text = this._db[this._id].getFullName();
                    this.birthdayDateTimePicker.Value = DateTime.Parse(this._db[this._id].getBirthday(), new CultureInfo("fr-FR"));
                    this.phoneTextBox.Text = this._db[this._id].getPhoneNumber();
                    this.addressTextBox.Text = this._db[this._id].getAddress();
                    return;
                }
            }
            this.warningMsgLabel.Text = "Waring: Wrong email or Password";
            this.warningMsgLabel.Visible = true;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this._isSave = true;
            this.Close();
        }

        private void EditInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._isSave)
            {
                this._isSave = false;
                this.saveButton.Enabled = false;
                this.cancelButton.Enabled = false;
                if (isSave()) e.Cancel = false;
                else e.Cancel = true;
                this.saveButton.Enabled = true;
                this.cancelButton.Enabled = true;
            }
            else if (this._id != -1)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to close?", "Closing", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    e.Cancel = false;
                }
                else e.Cancel = true;
            }
            else e.Cancel = false;
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {
            checkPasswordConstraints();
        }

        private void newPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            checkNewPasswordConstraints();
        }

        private void confirmPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            checkNewPasswordConstraints();
        }

        private void fullNameCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.fullNameTextBox.Enabled = this.fullNameCheckBox.Checked;
            isSaveEnabled();
        }

        private void birthdayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.birthdayDateTimePicker.Enabled = this.birthdayCheckBox.Checked;
            isSaveEnabled();
        }

        private void phoneCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.phoneTextBox.Enabled = this.phoneCheckBox.Checked;
            isSaveEnabled();
        }

        private void addressCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.addressTextBox.Enabled = this.addressCheckBox.Checked;
            isSaveEnabled();
        }

        private void newPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.newPasswordTextBox.Enabled = this.newPasswordCheckBox.Checked;
            this.confirmPasswordTextBox.Enabled = this.newPasswordCheckBox.Checked;
            isSaveEnabled();
        }
    }
}
