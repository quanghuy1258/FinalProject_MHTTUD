using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FinalProject_MHTTUD
{
    public partial class RegisterForm : Form
    {
        private List<Account> _db = null;
        private bool _isRegister = false;

        private bool checkEmailConstraints()
        {
            this.warningMsgLabel.Visible = false;
            if (this.emailTextBox.Text == "")
            {
                this.warningMsgLabel.Text = "Waring: Empty Email not allowed";
                this.warningMsgLabel.Visible = true;
                return false;
            }
            if (!Regex.IsMatch(this.emailTextBox.Text, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
            {
                this.warningMsgLabel.Text = "Waring: Not a Valid Email Address";
                this.warningMsgLabel.Visible = true;
                return false;
            }
            return true;
        }
        private bool checkPasswordConstraints()
        {
            this.warningMsgLabel.Visible = false;
            if (this.passwordTextBox.Text == "")
            {
                this.warningMsgLabel.Text = "Waring: Empty Password not allowed";
                this.warningMsgLabel.Visible = true;
                return false;
            }
            if (this.passwordTextBox.Text != this.confirmPasswordTextBox.Text)
            {
                this.warningMsgLabel.Text = "Waring: Password does not match the confirm password";
                this.warningMsgLabel.Visible = true;
                return false;
            }
            return true;
        }
        private bool isRegister()
        {
            this.warningMsgLabel.Visible = false;
            if (this._db == null)
            {
                this.warningMsgLabel.Text = "Waring: Null Database";
                this.warningMsgLabel.Visible = true;
                this.emailTextBox.Text = "";
                return false;
            }
            if (!checkEmailConstraints()) return false;
            if (!checkPasswordConstraints()) return false;
            for (int i = 0; i < this._db.Count; i++)
            {
                if (this._db[i].getEmailAddress() == this.emailTextBox.Text)
                {
                    this.warningMsgLabel.Text = "Waring: Email already exists";
                    this.warningMsgLabel.Visible = true;
                    return false;
                }
            }
            this._db.Add(new Account()
                .inputInfo(this.emailTextBox.Text,
                this.fullNameTextBox.Text,
                this.birthdayDateTimePicker.Value.Date.ToString("dd/MM/yyyy"),
                this.phoneTextBox.Text,
                this.addressTextBox.Text)
                .generateKey(this.passwordTextBox.Text, (int)this.keySizeComboBox.SelectedItem));
            return true;
        }

        public RegisterForm(ref List<Account> db)
        {
            InitializeComponent();
            this._db = db;
            List<int> keySizeList = new List<int>();
            for (int i = 1024; i >= 512; i-=64) keySizeList.Add(i);
            this.keySizeComboBox.DataSource = keySizeList;
            this.birthdayDateTimePicker.Value = DateTime.Now;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            this._isRegister = true;
            this.Close();
        }

        private void RegisterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._isRegister)
            {
                this._isRegister = false;
                this.registerButton.Enabled = false;
                this.cancelButton.Enabled = false;
                if (isRegister()) e.Cancel = false;
                else e.Cancel = true;
                this.registerButton.Enabled = true;
                this.cancelButton.Enabled = true;
            }
            else if (this.emailTextBox.Text != "")
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

        private void emailTextBox_TextChanged(object sender, EventArgs e)
        {
            checkEmailConstraints();
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {
            checkPasswordConstraints();
        }

        private void confirmPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            checkPasswordConstraints();
        }
    }
}
