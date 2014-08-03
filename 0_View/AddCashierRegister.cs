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
    public partial class AddCashierRegister : Form
    {
        private static _2_Controller.CoordinatingController mainController = _2_Controller.CoordinatingController.getInstance();

        public AddCashierRegister()
        {
            InitializeComponent();
        }

        public AddCashierRegister(string nextAvailableID)
        {
            InitializeComponent();
            lblID.Text = nextAvailableID;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                mainController.AddCashierRegister(lblID.Text, chkActive.Checked ? Constant.STATUS_ACTIVATED : Constant.STATUS_DEACTIVATED);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
