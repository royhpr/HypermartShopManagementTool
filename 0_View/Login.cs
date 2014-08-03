using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hypermarket_Shop_Management_Tool._0_View
{
    public partial class Login : Form
    {
        _1_Model.DBManager DBAccessor = _1_Model.DBManager.getInstance();
        private _2_Controller.CoordinatingController mainController = _2_Controller.CoordinatingController.getInstance();
        private string email;
        private string password;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtUserID.Focus();
        }

        #region Start Login
       
        private static void ThreadProc()
        {
            Application.Run(new Main());
        }

        private bool IsStringEmptyOrNull(string inputString)
        {
            return (string.IsNullOrEmpty(inputString) || string.IsNullOrWhiteSpace(inputString));
        }

        private void LoginInterfaceTextField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                LoginToSystem();
            }
        }

        private void GoToMainInterface()
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
            this.Close();
        }
        
        private void LoginToSystem()
        {
            lblErrorFeedback.Visible = true;
            email = txtUserID.Text;
            password = txtPassword.Text;

            try
            {
                if (IsStringEmptyOrNull(email))
                {
                    lblErrorFeedback.Text = Constant.ERROR_EMPTY_USER_ID;
                    txtUserID.Focus();
                }
                else if (IsStringEmptyOrNull(password))
                {
                    lblErrorFeedback.Text = Constant.ERROR_EMPTY_PASSWORD;
                    txtPassword.Focus();
                }
                else if (!mainController.IsValidUser(email))
                {
                    lblErrorFeedback.Text = Constant.ERROR_INVALID_USER_ID;
                    txtUserID.Focus();
                }
                else if (!mainController.VerifyUserIdAndPassword(email, password))
                {
                    lblErrorFeedback.Text = Constant.ERROR_WRONG_PASSWORD;
                    txtPassword.Focus();
                }
                else
                {
                    if (mainController.RequireChangingPassword(email))
                    {
                        pnlLogin.Visible = false;
                        pnlChangingPassword.Visible = true;
                        txtNewPassword.Focus();
                    }
                    else
                    {
                        GoToMainInterface();
                    }
                }
            }
            catch (Exception e)
            {
                lblErrorFeedback.Text = e.Message;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginToSystem();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUserID.Clear();
            txtPassword.Clear();
        }

        #endregion


        #region Start changing password

        private void ClearTextField()
        {
            txtNewPassword.Clear();
            txtReenterNewPassword.Clear();
        }

        private void SavePasswordAndLogin()
        {
            lblNewPasswordError.Visible = true;
            if (IsStringEmptyOrNull(txtNewPassword.Text))
            {
                lblNewPasswordError.Text = Constant.ERROR_EMPTY_NEW_PASSWORD;
                txtNewPassword.Clear();
                txtNewPassword.Focus();
            }
            else if (IsStringEmptyOrNull(txtReenterNewPassword.Text))
            {
                lblNewPasswordError.Text = Constant.ERROR_EMPTY_REENTER_PASSWORD;
                txtReenterNewPassword.Clear();
                txtReenterNewPassword.Focus();
            }
            else if (txtNewPassword.Text.Length < Constant.PASSWORD_LIMITED_LENGTH || !isAlphanumeric(txtNewPassword.Text))
            {
                lblNewPasswordError.Text = Constant.ERROR_PASSWORD_LENGTH;
                ClearTextField();
                txtNewPassword.Focus();
            }
            else if (!txtNewPassword.Text.Equals(txtReenterNewPassword.Text))
            {
                lblNewPasswordError.Text = Constant.ERROR_PASSWORD_MISMATCH;
                txtReenterNewPassword.Clear();
                txtReenterNewPassword.Focus();
            }
            else
            {
                mainController.ChangePassword(email, txtNewPassword.Text);
                GlobalVariableAccessor.PasswordValidDays = Constant.PASSWORD_VALID_DAYS.ToString();
                GoToMainInterface();
            }
        }

        private bool isAlphanumeric(string password)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(password, @"
                # Match string having one letter and one digit (min).
                    \A                        # Anchor to start of string.
                      (?=[^0-9]*[0-9])        # at least one number and
                      (?=[^A-Za-z]*[A-Za-z])  # at least one letter.
                      \w+                     # Match string of alphanums.
                    \Z                        # Anchor to end of string.
                    ",
                   System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace))
             {
                 return true;
             }
            return false;
        }

        private void ChangingPasswordInterface_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SavePasswordAndLogin();
            }
        }

        private void btnClearNewPassword_Click(object sender, EventArgs e)
        {
            ClearTextField();
        }

        private void btnNewPasswordExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirmNewPassword_Click(object sender, EventArgs e)
        {
            SavePasswordAndLogin();
        }

        private void btnCancelChangingPassword_Click(object sender, EventArgs e)
        {
            ClearTextField();
            pnlChangingPassword.Visible = false;
            pnlLogin.Visible = true;
        }

        #endregion
    }
}
