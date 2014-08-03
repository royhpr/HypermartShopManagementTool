using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//public delegate void UpdateGreetingInfoDelegate(string passwordValidDaysLeft);

namespace Hypermarket_Shop_Management_Tool._0_View
{
    public partial class ChangePassword : Form
    {
        private _2_Controller.CoordinatingController mainController = _2_Controller.CoordinatingController.getInstance();
        //public UpdateGreetingInfoDelegate UpdateMainInterfaceGreetingInfoDel;
        private string newPassword;
        private string reenterPassword;

        public ChangePassword()
        {
            InitializeComponent();
        }

        private bool IsStringEmptyOrNull(string inputString)
        {
            return (string.IsNullOrEmpty(inputString) || string.IsNullOrWhiteSpace(inputString));
        }

        private void VerifyAndUpdatePassword()
        {
            lblFeedback.Visible = true;
            newPassword = txtNewPassword.Text.ToLower();
            reenterPassword = txtReenterNewPassword.Text.ToLower();
            try
            {
                if (IsStringEmptyOrNull(txtNewPassword.Text))
                {
                    lblFeedback.Text = Constant.ERROR_EMPTY_NEW_PASSWORD;
                    txtNewPassword.Focus();
                }
                else if (IsStringEmptyOrNull(txtReenterNewPassword.Text))
                {
                    lblFeedback.Text = Constant.ERROR_EMPTY_REENTER_PASSWORD;
                    txtReenterNewPassword.Clear();
                    txtReenterNewPassword.Focus();
                }
                else if (!newPassword.Equals(reenterPassword))
                {
                    lblFeedback.Text = Constant.ERROR_PASSWORD_MISMATCH;
                    txtReenterNewPassword.Focus();
                }
                else if (newPassword.Length < Constant.PASSWORD_LIMITED_LENGTH || !isAlphanumeric(newPassword))
                {
                    lblFeedback.Text = Constant.ERROR_PASSWORD_LENGTH;
                    txtNewPassword.Clear();
                    txtReenterNewPassword.Clear();
                    txtNewPassword.Focus();
                }
                else
                {
                    mainController.ChangePassword(GlobalVariableAccessor.CurrentStaffID, newPassword);
                    GlobalVariableAccessor.PasswordValidDays = Constant.PASSWORD_VALID_DAYS.ToString();
                    this.Close();
                }
            }
            catch
            {
                throw;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                VerifyAndUpdatePassword();
            }
            catch (Exception ex)
            {
                lblFeedback.Text = ex.Message;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChangePasswordInterface_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                VerifyAndUpdatePassword();
            }
        }
    }
}
