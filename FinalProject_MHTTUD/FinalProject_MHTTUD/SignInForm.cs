using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FinalProject_MHTTUD
{
    public partial class SignInForm : Form
    {
        private List<Account> _db = null;
        private bool _isSignIn = false;
        public int id = -1;

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
            return true;
        }
        private bool isSignIn()
        {
            this.id = -1;
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
                if (this._db[i].getEmailAddress() == this.emailTextBox.Text
                    && this._db[i].checkPassphrase(this.passwordTextBox.Text))
                {
                    this.id = i;
                    return true;
                }
            }
            this.warningMsgLabel.Text = "Waring: Wrong Email or Password";
            this.warningMsgLabel.Visible = true;
            return false;
        }

        public SignInForm(List<Account> db)
        {
            InitializeComponent();
            this._db = db;
        }
        
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void emailTextBox_TextChanged(object sender, EventArgs e)
        {
            checkEmailConstraints();
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {
            checkPasswordConstraints();
        }

        private void SignInForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._isSignIn)
            {
                this._isSignIn = false;
                if (isSignIn()) e.Cancel = false;
                else e.Cancel = true;
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

        private void signInButton_Click(object sender, EventArgs e)
        {
            this._isSignIn = true;
            this.Close();
        }
    }
}
