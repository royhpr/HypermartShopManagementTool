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
    public delegate void UpdateStaffList(string newStaff);
    public partial class AddEditStaff : Form
    {
        private ActionType currentAction;
        private _2_Controller.CoordinatingController mainController = _2_Controller.CoordinatingController.getInstance();
        private bool isDefaultPassword;
        public UpdateStaffList updateStaff;
        public DataTable staffList;

        public AddEditStaff()
        {
            InitializeComponent();
            currentAction = ActionType.Add;

            try
            {
                lblStaffID.Text = mainController.GenerateNextStaffID();
                dtpPswExpirateDate.Enabled = false;
                chkBlocked.Checked = false;
                chkBlocked.Enabled = false;
                chkDefaultPsw.Checked = true;
                chkDefaultPsw.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public AddEditStaff(string name, string staffID, string email, DateTime renewPasswordDate, DateTime dateOfBirth, DateTime joinDate, string gender, string position, string contact, bool defaultPassword, bool blocked)
        {
            InitializeComponent();
            lblStaffID.Text = staffID;
            txtName.Text = name;
            txtEmail.Text = email;
            dtpPswExpirateDate.Value = renewPasswordDate;
            dtpDoB.Value = dateOfBirth;
            dtpJoinDate.Value = joinDate;
            cbGender.Text = gender;
            isDefaultPassword = defaultPassword;

            cbGender.DropDownStyle = ComboBoxStyle.DropDown;
            cbPosition.DropDownStyle = ComboBoxStyle.DropDown;
            cbPosition.Text = position;
            txtContact.Text = contact;
            chkDefaultPsw.Checked = defaultPassword;
            chkBlocked.Checked = blocked;
            
            txtName.Enabled = false;
            txtEmail.Enabled = false;
            dtpPswExpirateDate.Enabled = false;
            dtpDoB.Enabled = false;
            dtpJoinDate.Enabled = false;
            cbGender.Enabled = false;

            if (defaultPassword == true)
            {
                chkDefaultPsw.Enabled = false;
            }

            currentAction = ActionType.Edit;
        }

        private void AddEditStaff_Load(object sender, EventArgs e)
        {
            dtpPswExpirateDate.Value = DateTime.Now.Date.AddDays(Constant.PASSWORD_VALID_DAYS);
        }

        private void AddProductInterace_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    startProcess();
                }
                catch (Exception ex)
                {
                    txtError.Text = ex.Message;
                }
            }
        }

        private bool IsStringNullOrEmptyOrWhiteSpaces(string inputString)
        {
            return (string.IsNullOrEmpty(inputString) || string.IsNullOrWhiteSpace(inputString));
        }

        private bool IsAllFieldCheckPass()
        {
            Int64 contact;

            try
            {
                if (IsStringNullOrEmptyOrWhiteSpaces(txtName.Text))
                {
                    txtError.Text = Constant.ERROR_ADD_STAFF_EMPTY_NAME;
                    txtName.Focus();
                    return false;
                }
                else if (IsStringNullOrEmptyOrWhiteSpaces(cbGender.Text))
                {
                    txtError.Text = Constant.ERROR_ADD_STAFF_EMPTY_GENDER;
                    cbGender.SelectedIndex = Constant.CONSTANT_ZERO;
                    cbGender.Focus();
                    return false;
                }
                else if (IsStringNullOrEmptyOrWhiteSpaces(txtEmail.Text))
                {
                    txtError.Text = Constant.ERROR_ADD_STAFF_EMPTY_EMAIL;
                    txtEmail.Focus();
                    return false;
                }
                else if (IsStringNullOrEmptyOrWhiteSpaces(txtContact.Text))
                {
                    txtError.Text = Constant.ERROR_ADD_STAFF_EMPTY_CONTACT;
                    txtContact.Focus();
                    return false;
                }
                else if (IsStringNullOrEmptyOrWhiteSpaces(cbPosition.Text))
                {
                    txtError.Text = Constant.ERROR_ADD_STAFF_EMPTY_POSITION;
                    cbPosition.SelectedIndex = Constant.CONSTANT_ZERO;
                    cbPosition.Focus();
                    return false;
                }
                else if (!Int64.TryParse(txtContact.Text, out contact))
                {
                    txtError.Text = Constant.ERROR_ADD_MANUFACTURER_IVALID_CONTACT;
                    txtContact.Focus();
                    return false;
                }
                else if (DateTime.Compare(dtpDoB.Value.Date, DateTime.Now.Date) > 0)
                {
                    txtError.Text = Constant.ERROR_ADD_STAFF_WRONG_DATE_OF_BIRTH;
                    dtpDoB.Focus();
                    return false;
                }
                else if (!Int64.TryParse(txtContact.Text, out contact))
                {
                    txtError.Text = Constant.ERROR_ADD_STAFF_WRONG_CONTACT;
                    txtContact.Focus();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }

        private bool IsEditFieldCheckPass()
        {
            Int64 contact;
            try
            {
                if (IsStringNullOrEmptyOrWhiteSpaces(txtEmail.Text))
                {
                    txtError.Text = Constant.ERROR_ADD_STAFF_EMPTY_EMAIL;
                    txtEmail.Focus();
                    return false;
                }
                else if (IsStringNullOrEmptyOrWhiteSpaces(txtContact.Text))
                {
                    txtError.Text = Constant.ERROR_ADD_STAFF_EMPTY_CONTACT;
                    txtContact.Focus();
                    return false;
                }
                else if (IsStringNullOrEmptyOrWhiteSpaces(cbPosition.Text))
                {
                    txtError.Text = Constant.ERROR_ADD_STAFF_EMPTY_POSITION;
                    cbPosition.SelectedIndex = Constant.CONSTANT_ZERO;
                    cbPosition.Focus();
                    return false;
                }
                else if (!Int64.TryParse(txtContact.Text, out contact))
                {
                    txtError.Text = Constant.ERROR_ADD_STAFF_WRONG_CONTACT;
                    txtContact.Focus();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }

        private void PerformAddStaff()
        {
            try
            {
                if (IsAllFieldCheckPass())
                {
                    mainController.AddStaff(lblStaffID.Text, txtName.Text, txtEmail.Text, dtpDoB.Value.Date, dtpJoinDate.Value.Date, cbGender.Text, cbPosition.Text, txtContact.Text);
                    this.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        private void PerformEditStaff()
        {
            try
            {
                if (IsEditFieldCheckPass())
                {
                    mainController.UpdateStaff(lblStaffID.Text, txtContact.Text, cbPosition.Text, chkDefaultPsw.Checked, chkBlocked.Checked);
                    if (chkDefaultPsw.Checked && !isDefaultPassword)
                    {
                        mainController.ResetPassword(txtEmail.Text);
                    }
                    this.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        private void startProcess()
        {
            try
            {
                txtError.Visible = true;
                if (currentAction == ActionType.Add)
                {
                    PerformAddStaff();
                }
                else
                {
                    PerformEditStaff();
                }
            }
            catch
            {
                throw;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                startProcess();
            }
            catch (Exception ex)
            {
                txtError.Text = ex.Message;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateExpirateDate(object sender, EventArgs e)
        {
            DateTime currentJoinDate = dtpJoinDate.Value.Date;
            dtpPswExpirateDate.Value = currentJoinDate.AddDays(Constant.PASSWORD_VALID_DAYS);
        }
    }
}
