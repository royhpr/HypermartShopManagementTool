using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Hypermarket_Shop_Management_Tool._0_View
{
    public delegate void ChangeCursorBackDel();
    public partial class AddEditStock : Form
    {
        private _2_Controller.CoordinatingController mainController = _2_Controller.CoordinatingController.getInstance();
        private ActionType currentAction;
        private DataTable productTable;
        private DataTable stockTable;
        private BackgroundWorker backgroundthread = new BackgroundWorker();
        private bool isLoaded = false;
        public ChangeCursorBackDel cursorDel;

        public AddEditStock(DataTable product, DataTable stock)
        {
            InitializeComponent();
            productTable = product;
            stockTable = stock;
            currentAction = ActionType.Add;
        }

        public AddEditStock(string productName, string manufacturerName, string productID, DateTime batchID, string importPrice, string sellPrice, DateTime expireDate, string quantity)
        {
            InitializeComponent();
            cbProductName.Text = productName;
            cbManufacturerName.Text = manufacturerName;
            lblProductID.Text = productID;
            dtpBatchID.Value = batchID;
            txtImportPrice.Text = importPrice;
            txtSellPrice.Text = sellPrice;
            dtpExpiryDate.Value = expireDate;
            txtQuantity.Text = quantity;
            this.ActiveControl = txtSellPrice;
            gbProducInfo.Text = Constant.ADD_STOCK_PRDUCT_GROUP_BOX_CAPTION;

            DisableComponents();
            currentAction = ActionType.Edit;
        }

        private void AddEditStock_Load(object sender, EventArgs e)
        {
            if (currentAction == ActionType.Add)
            {
                checkLoadReady.Start();
                InitializeBackgroundworker();
                isLoaded = true;
            }
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
        }

        void backgroundthread_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable dt = (DataTable)e.Argument;

            var productList = from DataRow row in dt.AsEnumerable().Distinct()
                              select row[Constant.P_NAME].ToString();

            
            backgroundthread.ReportProgress(Constant.CONSTANT_ZERO, productList.ToArray());  
        }

        private void DisableComponents()
        {
            lblProductID.Visible = true;
            lblProductLabel.Visible = true;
            cbManufacturerName.Enabled = false;
            cbProductName.Enabled = false;
            dtpBatchID.Enabled = false;
            dtpExpiryDate.Enabled = false;
            txtImportPrice.Enabled = false;
        }

        private string[] GetManufacturerComboboxSource(string product)
        {
            var resultList = from DataRow itemRow in productTable.AsEnumerable()
                             where itemRow[Constant.P_NAME].ToString().Equals(product)
                             select itemRow[Constant.P_M_Name].ToString();

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

        private void populateManufacturerComboBoxDataSource(object sender, EventArgs e)
        {
            ComboBox currentBox = sender as ComboBox;
            string[] dataSource = GetManufacturerComboboxSource(currentBox.SelectedItem.ToString());
            cbManufacturerName.DataSource = dataSource;
        }

        private void AddStockInterace_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    StartProcess();
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
            if (IsStringNullOrEmptyOrWhiteSpaces(cbProductName.Text))
            {
                txtError.Text = Constant.ERROR_ADD_STOCK_EMPTY_PRODUCT_NAME;
                return false;
            }
            else if (IsStringNullOrEmptyOrWhiteSpaces(cbManufacturerName.Text))
            {
                txtError.Text = Constant.ERROR_ADD_STOCK_EMPTY_MANUFACTURER_NAME;
                return false;
            }
            else if (IsStringNullOrEmptyOrWhiteSpaces(txtImportPrice.Text))
            {
                txtError.Text = Constant.ERROR_ADD_STOCK_EMPTY_IMPORT_PRICE;
                return false;
            }
            else if (IsStringNullOrEmptyOrWhiteSpaces(txtSellPrice.Text))
            {
                txtError.Text = Constant.ERROR_ADD_STOCK_EMPTY_SELL_PRICE;
                return false;
            }
            else if (IsStringNullOrEmptyOrWhiteSpaces(txtQuantity.Text))
            {
                txtError.Text = Constant.ERROR_ADD_STOCK_EMPTY_QUANTITY;
                return false;
            }

            if (!CheckAdditionalCondition())
                return false;

            return true;
        }

        private bool CheckAdditionalCondition()
        {
            Int64 quantity = Constant.CONSTANT_ZERO;
            double importprice, sellprice;
            if (!double.TryParse(txtImportPrice.Text, out importprice))
            {
                txtError.Text = Constant.ERROR_ADD_STOCK_INVALID_IMPORT_PRICE;
                return false;
            }
            else if (!double.TryParse(txtSellPrice.Text, out sellprice))
            {
                txtError.Text = Constant.ERROR_ADD_STOCK_INVALID_SELL_PRICE;
                return false;
            }
            else if (!Int64.TryParse(txtQuantity.Text, out quantity))
            {
                txtError.Text = Constant.ERROR_ADD_STOCK_INVALID_QUANTITY;
                return false;
            }
            else if (DateTime.Compare(dtpBatchID.Value, dtpExpiryDate.Value) > Constant.CONSTANT_ZERO ? true : false)
            {
                txtError.Text = Constant.ERROR_ADD_STOCK_INVALID_DATE;
                return false;
            }
            else if(!IsValidProduct())
            {
                txtError.Text = Constant.ERROR_ADD_STOCK_INVALID_PRODUCT;
                return false;
            }
            else if (IsStockExist())
            {
                txtError.Text = Constant.ERROR_ADD_STOCK_PRODUCT_BATCH_EXIST;
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsValidProduct()
        {
            if(string.IsNullOrEmpty(lblProductID.Text))
                return false;
            var result = from DataRow row in productTable.AsEnumerable()
                         where row[Constant.P_M_Name].ToString().Equals(cbManufacturerName.Text) &&
                         row[Constant.P_NAME].ToString().Equals(cbProductName.Text) &&
                         row[Constant.P_ID].ToString().Equals(lblProductID.Text)
                         select row[Constant.P_ID].ToString();

            if (!result.Any())
                return false;

            return true;
        }

        private bool IsStockExist()
        {
            var result = from DataRow row in stockTable.AsEnumerable()
                         where row[Constant.STOCK_P_ID].ToString().Equals(lblProductID.Text) && row[Constant.STOCK_BATCH_ID].ToString().Equals(dtpBatchID.Value.Date.ToString())
                         select row[Constant.STOCK_P_ID].ToString();

            if (result.Any())
                return true;
            return false;
        }

        private void PerformAddStock()
        {
            if (IsAllFieldCheckPass())
            {
                try
                {
                    mainController.AddStock(cbProductName.Text, cbManufacturerName.Text, lblProductID.Text, dtpBatchID.Value, txtImportPrice.Text, txtSellPrice.Text, dtpExpiryDate.Value, txtQuantity.Text);
                    this.Close();
                }
                catch
                {
                    throw;
                }
            }
        }

        private bool IsEditFieldCheckPass()
        {
            double sellPrice;
            Int64 quantity;
            if (IsStringNullOrEmptyOrWhiteSpaces(txtSellPrice.Text))
            {
                txtError.Text = Constant.ERROR_ADD_STOCK_EMPTY_SELL_PRICE;
                return false;
            }
            else if(IsStringNullOrEmptyOrWhiteSpaces(txtQuantity.Text))
            {
                txtError.Text = Constant.ERROR_ADD_STOCK_EMPTY_QUANTITY;
                return false;
            }
            else if (!double.TryParse(txtSellPrice.Text, out sellPrice))
            {
                txtError.Text = Constant.ERROR_ADD_STOCK_INVALID_SELL_PRICE;
                return false;
            }
            else if (!Int64.TryParse(txtQuantity.Text, out quantity))
            {
                txtError.Text = Constant.ERROR_ADD_STOCK_INVALID_QUANTITY;
                return false;
            }
            else
            {
                return true;
            }
        }

        private void PerformEditStock()
        {
            if (IsEditFieldCheckPass())
            {
                try
                {
                    mainController.UpdateStock(lblProductID.Text, dtpBatchID.Value, txtImportPrice.Text, txtSellPrice.Text, dtpExpiryDate.Value, txtQuantity.Text);
                    this.Close();
                }
                catch
                {
                    throw;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            if (currentAction == ActionType.Add)
            {
                cursorDel();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                StartProcess();
            }
            catch (Exception ex)
            {
                txtError.Text = ex.Message;
            }
            finally
            {
                if(currentAction == ActionType.Add)
                    cursorDel();
            }
        }

        private void StartProcess()
        {
            try
            {
                txtError.Visible = true;
                if (currentAction == ActionType.Add)
                {
                    PerformAddStock();
                }
                else
                {
                    PerformEditStock();
                }
            }
            catch
            {
                throw;
            }
        }

        private void UpdateProductID(object sender, EventArgs e)
        {
            lblProductID.Text = GetProductID(cbManufacturerName.Text, cbProductName.Text);
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
    }
}
