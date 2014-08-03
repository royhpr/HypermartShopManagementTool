using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hypermarket_Shop_Management_Tool
{
    public partial class CustomMessageBox : Form
    {
        private static CustomMessageBox msgForm; 
        public static DialogResult result = DialogResult.No;
        private int counter = 3;

        public CustomMessageBox()
        {
            InitializeComponent();
        }

        public static DialogResult Show(string message, string type)
        {
            msgForm = new CustomMessageBox();
            msgForm.txtMessage.Text = message;
            msgForm.ActiveControl = msgForm.btnOk;
            

            if (type.Equals(Constant.MSG_ERROR))
            {
                msgForm.txtMessage.ForeColor = Color.Red;
                msgForm.btnCancel.Visible = false;
            }
            else if (type.Equals(Constant.MSG_WARNING))
            {
                msgForm.txtMessage.ForeColor = Color.Black;
                msgForm.btnCancel.Visible = true;
            }
            else
            {
                msgForm.txtMessage.ForeColor = Color.Black;
                msgForm.btnOk.Visible = false;
                msgForm.btnCancel.Visible = false;
                msgForm.lblCounter.Visible = true;
                msgForm.countDownTimer.Start();
            }
            msgForm.ShowDialog();
            return result;
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            result = DialogResult.Cancel;
            msgForm.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            result = DialogResult.OK;
            msgForm.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            result = DialogResult.Cancel;
            msgForm.Close();
        }

        private void CustomMessageBox_Load(object sender, EventArgs e)
        {

        }

        private void countDownTimer_Tick(object sender, EventArgs e)
        {
            msgForm.counter--;
            msgForm.lblCounter.Text = counter.ToString();
            if (counter == Constant.CONSTANT_ZERO)
            {
                msgForm.countDownTimer.Stop();
                this.Close();
            }
        }
    }
}
