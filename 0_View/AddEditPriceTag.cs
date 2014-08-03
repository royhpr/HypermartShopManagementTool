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
    public partial class AddEditPriceTag : Form
    {
        private _2_Controller.CoordinatingController mainController = _2_Controller.CoordinatingController.getInstance();
        private DataTable productTable;
        private DataTable stockTable;
        private BackgroundWorker backgroundthread = new BackgroundWorker();
        private bool isLoaded = false;
        private string productIDCopy;
        ActionType currentAction;

        public AddEditPriceTag(DataTable product, DataTable stock)
        {
            InitializeComponent();
            try
            {
                lblPriceTagID.Text = mainController.GetNextAvailablePriceTagID();
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.OK)
                {
                    this.Close();
                }
            }
            productTable = product;
            stockTable = stock;
            currentAction = ActionType.Add;
        }

        public AddEditPriceTag(string priceTagID, string productID, string batchID, bool isActivated, DataTable product, DataTable stock)
        {
            InitializeComponent();

            lblPriceTagID.Text = priceTagID;
            productIDCopy = productID;
            cbBatchID.Text = batchID;
            chkActive.Checked = isActivated;

            productTable = product;
            stockTable = stock;

            currentAction = ActionType.Edit;
        }

        private void AddEditPriceTag_Load(object sender, EventArgs e)
        {
            checkLoadReady.Start();
            InitializeBackgroundworker();
            isLoaded = true;
        }

        private void InitializeBackgroundworker()
        {
            backgroundthread.DoWork += new DoWorkEventHandler(backgroundthread_DoWork);
            backgroundthread.ProgressChanged += new ProgressChangedEventHandler
                    (backgroundthread_ProgressChanged);
            backgroundthread.RunWorkerCompleted += new RunWorkerCompletedEventHandler
                    (backgroundthread_RunWorkerCompleted);
            backgroundthread.WorkerReportsProgress = true;
            backgroundthread.WorkerSupportsCancellation = true;
        }

        void backgroundthread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                lblProgress.Text = Constant.ERROR_DURING_POPULATING_LIST;
            }
            else
            {
                lblProgress.Visible = false;
                this.Cursor = Cursors.Default;
            }
        }

        void backgroundthread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            cbProductName.DataSource = (string[])e.UserState;

            if (currentAction == ActionType.Edit)
            {
                lblProductID.Text = productIDCopy;
                cbProductName.Text = GetProductName(productIDCopy);
                cbManufacturer.Text = GetManufacturerName(productIDCopy);
            }
        }

        void backgroundthread_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable dt = (DataTable)e.Argument;

            var productList = from DataRow row in dt.AsEnumerable().Distinct()
                              select row[Constant.P_NAME].ToString();


            backgroundthread.ReportProgress(Constant.CONSTANT_ZERO, productList.ToArray());
        }

        private string[] GetManufacturerComboboxSource(string product)
        {
            var resultList = from DataRow itemRow in productTable.AsEnumerable()
                             where itemRow[Constant.P_NAME].ToString().Equals(product)
                             select itemRow[Constant.P_M_Name].ToString();

            return resultList.ToArray();
        }

        private string[] GetBatchComboboxSource(string manufacturer)
        {
            var resultList = from DataRow itemRow in stockTable.AsEnumerable()
                             where itemRow[Constant.STOCK_P_NAME].ToString().Equals(cbProductName.Text) && itemRow[Constant.STOCK_M_NAME].ToString().Equals(cbManufacturer.Text)
                             select itemRow[Constant.STOCK_BATCH_ID].ToString();

            return resultList.ToArray();
        }

        private string GetProductID(string manufacturerName, string productName)
        {
            if (string.IsNullOrEmpty(productName))
                return string.Empty;

            var result = from DataRow itemRow in productTable.AsEnumerable()
                         where itemRow[Constant.P_M_Name].ToString().Equals(manufacturerName) && itemRow[Constant.P_NAME].ToString().Equals(productName)
                         select itemRow[Constant.P_ID].ToString();

            if (!result.Any())
                return string.Empty;

            return result.ToArray()[Constant.CONSTANT_ZERO].ToString();
        }

        private string GetProductName(string productID)
        {
            var result = from DataRow itemRow in stockTable.AsEnumerable()
                         where itemRow[Constant.STOCK_P_ID].ToString().Equals(productID)
                         select itemRow[Constant.STOCK_P_NAME].ToString();

            if (!result.Any())
                return string.Empty;

            return result.ToArray()[Constant.CONSTANT_ZERO].ToString();
        }

        private string GetManufacturerName(string productID)
        {
            var result = from DataRow itemRow in stockTable.AsEnumerable()
                         where itemRow[Constant.STOCK_P_ID].ToString().Equals(productID)
                         select itemRow[Constant.STOCK_M_NAME].ToString();

            if (!result.Any())
                return string.Empty;

            return result.ToArray()[Constant.CONSTANT_ZERO].ToString();
        }

        private void populateManufacturerComboBoxDataSource(object sender, EventArgs e)
        {
            ComboBox currentBox = sender as ComboBox;
            string[] dataSource = GetManufacturerComboboxSource(currentBox.SelectedItem.ToString());
            cbManufacturer.DataSource = dataSource;
        }

        private void populateBatchComboBoxDataSource()
        {
            string[] dataSource = GetBatchComboboxSource(cbManufacturer.SelectedItem.ToString());
            cbBatchID.DataSource = dataSource;
        }

        private void cbManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblProductID.Text = GetProductID(cbManufacturer.Text, cbProductName.Text);
            populateBatchComboBoxDataSource();
        }

        private bool IsAllFieldCheckPass()
        {
            if (string.IsNullOrEmpty(cbProductName.Text) || string.IsNullOrWhiteSpace(cbProductName.Text) || !cbProductName.Items.Contains(cbProductName.Text))
            {
                MessageBox.Show(Constant.ERROR_EMPTY_PRODUCT_NAME, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(cbManufacturer.Text) || string.IsNullOrWhiteSpace(cbManufacturer.Text) || !cbManufacturer.Items.Contains(cbManufacturer.Text))
            {
                MessageBox.Show(Constant.ERROR_EMPTY_MANUFACTURER, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(cbBatchID.Text) || string.IsNullOrWhiteSpace(cbBatchID.Text) || !cbBatchID.Items.Contains(cbBatchID.Text))
            {
                MessageBox.Show(Constant.ERROR_EMPTY_BATCH, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void checkLoadReady_Tick(object sender, EventArgs e)
        {
            if (isLoaded == true && backgroundthread.IsBusy == false)
            {
                this.Cursor = Cursors.AppStarting;
                lblProgress.Visible = true;
                DataTable dt = productTable.Copy();
                backgroundthread.RunWorkerAsync(dt);
                checkLoadReady.Stop();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsAllFieldCheckPass())
                {
                    if (currentAction == ActionType.Add)
                    {
                        mainController.AddPriceTag(lblPriceTagID.Text, chkActive.Checked ? Constant.STATUS_ACTIVATED : Constant.STATUS_DEACTIVATED, lblProductID.Text, Convert.ToDateTime(cbBatchID.Text));
                        this.Close();
                    }
                    else
                    {
                        mainController.UpdatePriceTag(lblPriceTagID.Text, chkActive.Checked ? Constant.STATUS_ACTIVATED : Constant.STATUS_DEACTIVATED, lblProductID.Text, Convert.ToDateTime(cbBatchID.Text));
                        this.Close();
                    }
                }
            }
            catch(Exception ex)
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
