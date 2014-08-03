using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Data.SqlClient;

namespace Hypermarket_Shop_Management_Tool._0_View
{
    public struct SearchboxPair
    {
        public BindingSource currentBindingSource { get; set; }
        public PictureBox currentIconBox { get; set; }
        public string currentFilterQuery { get; set; }
    }

    public partial class Operation : Form
    {
        private static _2_Controller.CoordinatingController mainController = _2_Controller.CoordinatingController.getInstance();
        private BindingSource productBindingSource = new BindingSource();
        private BindingSource stockBindingSource = new BindingSource();
        private BindingSource manufacturerBindingSource = new BindingSource();
        private BindingSource staffBindingSource = new BindingSource();
        private BindingSource discountBindingSource = new BindingSource();
        private BindingSource transactionBindingSource = new BindingSource();
        private BindingSource cashierRegisterBindingSource = new BindingSource();
        private BindingSource priceTagBindingSource = new BindingSource();
        private BindingSource reportBindingSource = new BindingSource();
        private BindingSource pendingBindingSource = new BindingSource();
        private BindingSource approveBindingSource = new BindingSource();
        private BindingSource rejectBindingSource = new BindingSource();
        private BindingSource incomingBindingSource = new BindingSource();

        private bool isFirstTimeEnterReportInterface = true;

        public Operation()
        {
            InitializeComponent();
        }

        public Operation(string tabName)
        {
            InitializeComponent();
            InitializeProductBindingSource();
            InitializeStockBindingSource();
            InitializeStaffBindingSource();
            InitializeManufacturerBindingSource();
            InitializeTransactionBindingSource();
            InitializeCashierRegisterBindingSource();
            InitializePriceTagBindingSource();
            InitializeDiscountBindingSource();
            InitializeReportBindingSource();
            InitializePendingBindingSource();
            InitializeApproveBindingSource();
            InitializeRejectBindingSource();
            InitializeIncomingBindingSource();
            tcOperation.SelectTab(tabName);
            if (tabName.Equals(Constant.OPERATION_TAB_STAFF))
            {
                ColorAndSortStaff();
            }
        }

        private void Operation_Load(object sender, EventArgs e)
        {
            initializeProfile();
            UpdateGreeting(GlobalVariableAccessor.PasswordValidDays);
        }

        #region Initialize Binding Source

        private void InitializeProductBindingSource()
        {
            productBindingSource.DataSource = mainController.GetListOfProduct();
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.DataSource = productBindingSource;
        }

        private void InitializeStockBindingSource()
        {
            stockBindingSource.DataSource = mainController.GetListOfStock();
            dgvStock.AutoGenerateColumns = false;
            dgvStock.DataSource = stockBindingSource;
        }

        private void InitializeManufacturerBindingSource()
        {
            manufacturerBindingSource.DataSource = mainController.GetListOfManufacturer();
            dgvManufacturer.AutoGenerateColumns = false;
            dgvManufacturer.DataSource = manufacturerBindingSource;
        }

        private void InitializeStaffBindingSource()
        {
            staffBindingSource.DataSource = mainController.GetListOfStaff();
            dgvStaff.AutoGenerateColumns = false;
            dgvStaff.DataSource = staffBindingSource;
        }

        private void InitializeTransactionBindingSource()
        {
            transactionBindingSource.DataSource = mainController.GetListOfTransaction();
            dgvTransaction.AutoGenerateColumns = false;
            dgvTransaction.DataSource = transactionBindingSource;
        }

        private void InitializeCashierRegisterBindingSource()
        {
            cashierRegisterBindingSource.DataSource = mainController.GetListOfCashierRegister();
            dgvCashierRegister.AutoGenerateColumns = false;
            dgvCashierRegister.DataSource = cashierRegisterBindingSource;
        }

        private void InitializePriceTagBindingSource()
        {
            priceTagBindingSource.DataSource = mainController.GetListOfPriceTag();
            dgvPriceTag.AutoGenerateColumns = false;
            dgvPriceTag.DataSource = priceTagBindingSource;
        }

        private void InitializeDiscountBindingSource()
        {
            discountBindingSource.DataSource = mainController.GetListOfDiscountPolicy();
            dgvDiscount.AutoGenerateColumns = false;
            dgvDiscount.DataSource = discountBindingSource;
        }

        private void InitializeReportBindingSource()
        {
            reportBindingSource.DataSource = mainController.GetListOfReport();
            dgvReport.AutoGenerateColumns = false;
            dgvReport.DataSource = reportBindingSource;
        }

        private void InitializePendingBindingSource()
        {
            pendingBindingSource.DataSource = mainController.GetListOfPendingRequest();
            dgvPending.AutoGenerateColumns = false;
            dgvPending.DataSource = pendingBindingSource;
        }

        private void InitializeApproveBindingSource()
        {
            approveBindingSource.DataSource = mainController.GetListOfApprovedRequest();
            dgvApproved.AutoGenerateColumns = false;
            dgvApproved.DataSource = approveBindingSource;
        }

        private void InitializeRejectBindingSource()
        {
            rejectBindingSource.DataSource = mainController.GetListOfRejectedRequest();
            dgvRejected.AutoGenerateColumns = false;
            dgvRejected.DataSource = rejectBindingSource;
        }

        private void InitializeIncomingBindingSource()
        {
            incomingBindingSource.DataSource = mainController.GetListOfIncomingRequest();
            dgvIncoming.AutoGenerateColumns = false;
            dgvIncoming.DataSource = incomingBindingSource;
        }

        #endregion

        #region Private Methods

        public void UpdateGreeting(string days)
        {
            lblGreeting.Text = Constant.GREETING0 + GlobalVariableAccessor.Salution + GlobalVariableAccessor.CurrentStaffName + Constant.COMMA + Constant.GREETING1 + days + Constant.DAYS;
        }

        private bool IsTwoStringEqual(string a, string b)
        {
            return a.Equals(b);
        }

        public static void goBackToHomePageThread()
        {
            Application.Run(new _0_View.Main());
        }

        public static void goBackToLoginPageThread()
        {
            Application.Run(new _0_View.Login());
        }

        private static void SynchronizeInventory()
        {
            try
            {
                mainController.SynchronizeInventory();
            }
            catch
            {
                throw;
            }
        }

        private void ShowSyncResult()
        {
            StreamReader sr = new StreamReader("SyncResult.txt");

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                MessageBox.Show(line, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                break;
            }

            sr.Close();
        }

        private void SelectGridView(Button currentButton)
        {
            DataGridView dgvCurrent = null;
            switch (currentButton.Parent.Parent.Name)
            {
                case Constant.OPERATION_TAB_PRODUCT:
                    dgvCurrent = dgvProducts;
                    break;
                case Constant.OPERATION_TAB_STOCK:
                    dgvCurrent = dgvStock;
                    break;
                case Constant.OPERATION_TAB_STAFF:
                    dgvCurrent = dgvStaff;
                    break;

                default:
                    break;
            }
            foreach (DataGridViewRow row in dgvCurrent.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                chk.Value = !(chk.Value == null ? false : (bool)chk.Value);
            }
        }

        private void ColorAndSortStaff()
        {
            foreach (DataGridViewRow row in dgvStaff.Rows)
            {
                DataGridViewTextBoxCell blocked = (DataGridViewTextBoxCell)row.Cells[11];

                if ((bool)blocked.Value == true)
                {
                    dgvStaff.Rows[blocked.RowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }

            dgvStaff.Sort(dgvStaff.Columns["clBlocked"], ListSortDirection.Ascending);
        }

        #endregion

        #region Events

        private void llblChangePsw_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ChangePassword frmChangePsw = new ChangePassword();
            frmChangePsw.ShowDialog();
            UpdateGreeting(GlobalVariableAccessor.PasswordValidDays);
        }

        private void ChangeBackGroundColor(object sender, EventArgs e)
        {
            PictureBox currentBox = sender as PictureBox;
            currentBox.BackColor = Color.FromArgb(0, 0, 64);
        }

        private void ResetBackGroundColor(object sender, EventArgs e)
        {
            PictureBox currentBox = sender as PictureBox;
            currentBox.BackColor = Color.FromArgb(128, 128, 255);
        }

        private void SetTextAlignment(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;

            TabPage _tabPage = tcOperation.TabPages[e.Index];

            Rectangle _tabBounds = tcOperation.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {
                _textBrush = new SolidBrush(Color.Red);
                g.FillRectangle(Brushes.Gray, e.Bounds);
            }
            else
            {
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                e.DrawBackground();
            }

            Font _tabFont = new Font("Arial", (float)14.0, FontStyle.Bold, GraphicsUnit.Pixel);
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        }

        private void MoveCurserToFront(object sender, EventArgs e)
        {
            TextBox txtActive = sender as TextBox;
            if (txtActive.Text.Equals(Constant.SEARCH_HINT))
            {
                txtActive.Select(Constant.CONSTANT_ZERO, Constant.CONSTANT_ZERO);
            }
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pbHome_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(goBackToHomePageThread));
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
            this.Close();
        }

        private void pbSync_Click(object sender, EventArgs e)
        {
            ProcessStartInfo synChronizeShopDBProcessInfo = new ProcessStartInfo("SynchronizeInventory.exe");
            Process synChronizeShopDBProcess =  Process.Start(synChronizeShopDBProcessInfo);
            synChronizeShopDBProcess.WaitForExit();

            this.Cursor = Cursors.AppStarting;
            SynchronizeInventory();
            this.Cursor = Cursors.Default;

            ShowSyncResult();
        }

        private void pbLogout_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(goBackToLoginPageThread));
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
            this.Close();
        }

        private void SelectOrDeselectAll(object sender, EventArgs e)
        {
            Button currentButton = sender as Button;
            if (currentButton.Text.Equals(Constant.OPERATION_SELECT_ALL))
            {
                currentButton.Text = Constant.OPERATION_UNSELECT_ALL;
            }
            else
            {
                currentButton.Text = Constant.OPERATION_SELECT_ALL;
            }
            SelectGridView(currentButton);
        }

        private void dgvStaff_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var grid = (DataGridView)sender;
            if (grid.Columns[e.ColumnIndex].Name == "clBlocked")
            {
                e.Value = (bool)e.Value ? "YES" : "NO";
                e.FormattingApplied = true;
            }
        }

        private void dgvStaff_Enter(object sender, EventArgs e)
        {
            ColorAndSortStaff();
        }

        private void pbSyncTransactions_Click(object sender, EventArgs e)
        {
            try
            {
                mainController.SynchronizeTransaction();
                MessageBox.Show(Constant.SYNC_SUCCESSFULLY, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region All about search

        private void SearchFieldTextChange(object sender, EventArgs e)
        {
            TextBox currentTextbox = sender as TextBox;
            try
            {
                SearchboxPair currentPair = getCurrentPair(currentTextbox.Name);
                currentPair.currentIconBox.Image = global::Hypermarket_Shop_Management_Tool.Properties.Resources.delete;
                if (currentTextbox.Text.Equals(Constant.OPERATION_EMPTY))
                {
                    currentPair.currentBindingSource.Filter = string.Empty;
                    currentPair.currentIconBox.Image = Properties.Resources.search;
                }
            }
            catch
            {
                MessageBox.Show(Constant.ERROR_SEARCH_VERIFY_CONTENT, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private SearchboxPair getCurrentPair(string currentTextbox)
        {
            SearchboxPair currentPair = new SearchboxPair();
            try
            {
                switch (currentTextbox)
                {
                    case Constant.MANUFACTURER_SEARCH_BOX:
                        currentPair.currentBindingSource = manufacturerBindingSource;
                        currentPair.currentIconBox = pbManufacturerSign;
                        currentPair.currentFilterQuery = Constant.SEARCH_MANUFACTURER;
                        break;
                    case Constant.STOCK_SEARCH_BOX:
                        currentPair.currentBindingSource = stockBindingSource;
                        currentPair.currentIconBox = pbStockSign;
                        currentPair.currentFilterQuery = Constant.SEARCH_STOCK;
                        break;
                    case Constant.PRODUCT_SEARCH_BOX:
                        currentPair.currentBindingSource = productBindingSource;
                        currentPair.currentIconBox = pbProductSign;
                        currentPair.currentFilterQuery = Constant.SEARCH_PRODUCT;
                        break;
                    case Constant.STAFF_SEARCH_BOX:
                        currentPair.currentBindingSource = staffBindingSource;
                        currentPair.currentIconBox = pbStaffSign;
                        currentPair.currentFilterQuery = Constant.SEARCH_STAFF;
                        break;
                    case Constant.TRANSACTION_SEARCH_BOX:
                        currentPair.currentBindingSource = transactionBindingSource;
                        currentPair.currentIconBox = pbTransactionSign;
                        currentPair.currentFilterQuery = Constant.SEARCH_TRANSACTION;
                        break;
                    case Constant.DISCOUNT_SEARCH_BOX:
                        currentPair.currentBindingSource = discountBindingSource;
                        currentPair.currentIconBox = pbDiscountSign;
                        currentPair.currentFilterQuery = Constant.SEARCH_DISCOUNT;
                        break;
                    case Constant.CASHIER_REGISTER_SEARCH_BOX:
                        currentPair.currentBindingSource = cashierRegisterBindingSource;
                        currentPair.currentIconBox = pbCashierRegisterSign;
                        currentPair.currentFilterQuery = Constant.SEARCH_CASHIER_REGISTER;
                        break;
                    case Constant.PRICE_TAG_SEARCH_BOX:
                        currentPair.currentBindingSource = priceTagBindingSource;
                        currentPair.currentIconBox = pbPriceTagSign;
                        currentPair.currentFilterQuery = Constant.SEARCH_PRICE_TAG;
                        break;
                    case Constant.PENDING_SEARCH_BOX:
                        currentPair.currentBindingSource = pendingBindingSource;
                        currentPair.currentIconBox = pbPendingSign;
                        currentPair.currentFilterQuery = Constant.SEARCH_PENDING;
                        break;
                    case Constant.APPROVED_SEARCH_BOX:
                        currentPair.currentBindingSource = approveBindingSource;
                        currentPair.currentIconBox = pbApprovedSign;
                        currentPair.currentFilterQuery = Constant.SEARCH_APPROVED;
                        break;
                    case Constant.REJECTED_SEARCH_BOX:
                        currentPair.currentBindingSource = rejectBindingSource;
                        currentPair.currentIconBox = pbRejectedSign;
                        currentPair.currentFilterQuery = Constant.SEARCH_REJECTED;
                        break;
                    case Constant.INCOMING_SEARCH_BOX:
                        currentPair.currentBindingSource = incomingBindingSource;
                        currentPair.currentIconBox = pbIncomingSign;
                        currentPair.currentFilterQuery = Constant.SEARCH_INCOMING;
                        break; 


                    default:
                        break;
                }
                return currentPair;
            }
            catch
            {
                throw;
            }
        }

        private void SearchField_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    Cursor.Current = Cursors.AppStarting;
                    Search(sender as TextBox);
                    Cursor.Current = Cursors.Default;
                }
            }
            catch
            {
                CustomMessageBox.Show(Constant.ERROR_SEARCH, Constant.MSG_ERROR);
            }
        }

        private void Search(TextBox currentTextbox)
        {
            try
            {
                SearchboxPair currentPair = getCurrentPair(currentTextbox.Name);
                currentPair.currentBindingSource.Filter = string.Format(currentPair.currentFilterQuery, currentTextbox.Text.Replace(Constant.OPERATION_SINGLE_QUOTE, Constant.OPERATION_DOUBLE_SINGLE_QUOTE));
            }
            catch
            {
                CustomMessageBox.Show(Constant.ERROR_SEARCH + Constant.PRODUCT_TABLE, Constant.MSG_ERROR);
            }
        }

        private void iconBox_Click(object sender, EventArgs e)
        {
            PictureBox currentIconBox = sender as PictureBox;
            TextBox currentTextbox = null;
            object currentObject = null;
            for (int i = 0; i < currentIconBox.Parent.Size.Width; i += currentIconBox.Parent.Size.Width / 20)
            {
                if ((currentObject = currentIconBox.Parent.GetChildAtPoint(new Point(i, currentIconBox.Height / 2))) is TextBox)
                {
                    currentTextbox = (TextBox)currentObject;
                    break;
                }
            }

            if (!currentTextbox.Text.Equals(Constant.SEARCH_HINT))
            {
                currentTextbox.Clear();
            }
        }

        #endregion

        #region All About Profile

        private bool IsAllFieldPass()
        {
            Int64 contact;

            if (string.IsNullOrEmpty(txtContact.Text))
            {
                CustomMessageBox.Show(Constant.ERROR_ADD_STAFF_EMPTY_CONTACT, Constant.MSG_WARNING);
                return false;
            }
            else if (!Int64.TryParse(txtContact.Text, out contact))
            {
                CustomMessageBox.Show(Constant.ERROR_ADD_STAFF_WRONG_CONTACT, Constant.MSG_WARNING);
                return false;
            }
            else if (!cbPosition.Items.Contains(cbPosition.Text))
            {
                CustomMessageBox.Show(Constant.ERROR_ADD_STAFF_WRONG_POSITION, Constant.MSG_WARNING);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void initializeProfile()
        {
            DataTable dt = (DataTable)staffBindingSource.DataSource;
            string staffID = GlobalVariableAccessor.CurrentStaffID;

            var result = from DataRow row in dt.AsEnumerable()
                         where row[Constant.STAFF_ID].ToString().Equals(staffID)
                         select new{staffID = row[Constant.STAFF_ID].ToString(), staffName = row[Constant.STAFF_NAME].ToString(), gender = row[Constant.STAFF_GENDER].ToString(),
                         dateOfBirth = row[Constant.STAFF_DATE_OF_BIRTH], position = row[Constant.STAFF_POSITION].ToString(), contact = row[Constant.STAFF_CONTACT].ToString(),
                         joinDate = row[Constant.STAFF_JOIN_DATE]};

            DateTime birthDate = (DateTime)result.ToArray()[Constant.CONSTANT_ZERO].dateOfBirth;
            DateTime joinDate = (DateTime)result.ToArray()[Constant.CONSTANT_ZERO].joinDate;

            lblDoB.Text = birthDate.Date.ToShortDateString();
            lblGender.Text = result.ToArray()[Constant.CONSTANT_ZERO].gender;
            lblJoinDate.Text = joinDate.Date.ToShortDateString();
            lblName.Text = result.ToArray()[Constant.CONSTANT_ZERO].staffName;
            lblStaffID.Text = result.ToArray()[Constant.CONSTANT_ZERO].staffID;
            txtContact.Text = result.ToArray()[Constant.CONSTANT_ZERO].contact;
            cbPosition.Text = result.ToArray()[Constant.CONSTANT_ZERO].position;
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            _0_View.ChangePassword changepasswordInterface = new _0_View.ChangePassword();
            changepasswordInterface.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsAllFieldPass())
            {
                mainController.UpdateStaff(lblStaffID.Text, txtContact.Text, cbPosition.Text, false, false);
            }
        }

        #endregion

        #region All About stock

        private void btnAddStock_Click(object sender, EventArgs e)
        {
            DataTable dtProduct = (DataTable)productBindingSource.DataSource;
            DataTable dtStock = (DataTable)stockBindingSource.DataSource;
            this.Cursor = Cursors.AppStarting;
            _0_View.AddEditStock addStockInterface = new _0_View.AddEditStock(dtProduct,dtStock);
            addStockInterface.cursorDel = new ChangeCursorBackDel(ChangeCuresorToDefault);
            addStockInterface.ShowDialog();
        }

        private void ChangeCuresorToDefault()
        {
            this.Cursor = Cursors.Default;
        }

        private void EditStock(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            if (dgvStock.SelectedRows.Count == Constant.CONSTANT_ZERO)
                return;
            DataGridViewRow selectedRow = dgvStock.SelectedRows[Constant.CONSTANT_ZERO];

            string productID = selectedRow.Cells[Constant.CONSTANT_ZERO].Value == null ? string.Empty : selectedRow.Cells[Constant.CONSTANT_ZERO].Value.ToString();
            string productName = selectedRow.Cells[Constant.OPERATION_INDEX_ONE].Value == null ? string.Empty : selectedRow.Cells[Constant.OPERATION_INDEX_ONE].Value.ToString();
            string manufacturerName = selectedRow.Cells[Constant.OPERATION_INDEX_TWO].Value == null ? string.Empty : selectedRow.Cells[Constant.OPERATION_INDEX_TWO].Value.ToString();
            DateTime batchID = (DateTime)selectedRow.Cells[Constant.OPERATION_INDEX_THREE].Value;
            string importPrice = selectedRow.Cells[Constant.OPERATION_INDEX_FOUR].Value == null ? string.Empty : selectedRow.Cells[Constant.OPERATION_INDEX_FOUR].Value.ToString();
            string sellPrice = selectedRow.Cells[Constant.OPERATION_INDEX_FIVE].Value == null ? string.Empty : selectedRow.Cells[Constant.OPERATION_INDEX_FIVE].Value.ToString();
            DateTime expireDate = (DateTime)selectedRow.Cells[Constant.OPERATION_INDEX_SIX].Value;
            string quantity = selectedRow.Cells[Constant.OPERATION_INDEX_SEVEN].Value == null ? string.Empty : selectedRow.Cells[Constant.OPERATION_INDEX_SEVEN].Value.ToString();

            AddEditStock frmEditStock = new AddEditStock(productName, manufacturerName, productID, batchID, importPrice, sellPrice, expireDate, quantity);
            frmEditStock.ShowDialog();
        }

        #endregion

        #region All about request

        private void tcOrder_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font _tabFont = new Font("Arial", (float)14.0, FontStyle.Bold, GraphicsUnit.Pixel);
            switch (e.Index)
            {
                case 0:
                    e.Graphics.FillRectangle(new SolidBrush(Color.Pink), e.Bounds);
                    break;
                case 1:
                    e.Graphics.FillRectangle(new SolidBrush(Color.Green), e.Bounds);
                    break;
                case 2:
                    e.Graphics.FillRectangle(new SolidBrush(Color.Gray), e.Bounds);
                    break;
                case 3:
                    e.Graphics.FillRectangle(new SolidBrush(Color.LightBlue), e.Bounds);
                    break;
                default:
                    break;
            }

            Rectangle paddedBounds = e.Bounds;
            paddedBounds.Inflate(-2, -2);
            e.Graphics.DrawString(tcOrder.TabPages[e.Index].Text, _tabFont, SystemBrushes.HighlightText, paddedBounds);
        }

        private void btnNewRequest_Click(object sender, EventArgs e)
        {
            AddPendingRequest newAddPendingRequest = new AddPendingRequest((DataTable)productBindingSource.DataSource);
            newAddPendingRequest.ShowDialog();
        }

        private void btnAcknowledge_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime timeStamp = DateTime.Now;
                DateTime currentTime = new DateTime(timeStamp.Year, timeStamp.Month, timeStamp.Day, timeStamp.Hour, timeStamp.Minute, timeStamp.Second);
                foreach (DataGridViewRow dgr in dgvIncoming.Rows)
                {
                    if (Convert.ToBoolean(dgr.Cells[Constant.CONSTANT_ZERO].Value) == true && dgr.Cells[Constant.OPERATION_INDEX_SEVEN].Value != DBNull.Value && dgr.Cells[Constant.OPERATION_INDEX_EIGHT].Value == DBNull.Value)
                    {
                        string productID = dgr.Cells[Constant.OPERATION_INDEX_ONE].Value.ToString();
                        DateTime batchID = (DateTime)dgr.Cells[Constant.OPERATION_INDEX_FOUR].Value;
                        string productName = dgr.Cells[Constant.OPERATION_INDEX_THREE].Value.ToString();
                        string manufacturer = dgr.Cells[Constant.OPERATION_INDEX_TWO].Value.ToString();
                        string quantity = dgr.Cells[Constant.OPERATION_INDEX_SIX].Value.ToString();

                        string importPrice = string.Empty;
                        string sellPrice = string.Empty;
                        DateTime expireDate = new DateTime();
                        getStockInfoFromHQ(ref importPrice, ref sellPrice, ref expireDate, productID, batchID);

                        string[] result = IsThisTypeofProductExist(productID, batchID);

                        if (result.Count() != Constant.CONSTANT_ZERO)
                        {
                            mainController.UpdateStockQuantity(productID, batchID, (Convert.ToInt16(result[0].ToString()) + Convert.ToInt16(quantity)).ToString());
                        }
                        else
                        {
                            mainController.AddStock(productName, manufacturer, productID, batchID, importPrice, sellPrice, expireDate, quantity);
                        }
                        mainController.UpdateIncomingStock(productID, batchID, (DateTime)dgr.Cells[Constant.OPERATION_INDEX_FIVE].Value, currentTime, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getStockInfoFromHQ(ref string importPrice, ref string sellPrice, ref DateTime expireDate, string productID, DateTime batchID)
        {
            _1_Model.DBConnectionManager DBConn = _1_Model.DBConnectionManager.getInstance();

            try
            {
                using (SqlConnection SQLConn = new SqlConnection(DBConn.getHQConnectionString()))
                using (SqlCommand SQLComm = new SqlCommand(Constant.STOCK_REQUEST_FOR_DETAIL, SQLConn))
                {
                    if (SQLConn.State == ConnectionState.Closed)
                    {
                        SQLConn.Open();
                    }
                    SQLComm.Parameters.Add(Constant.PRODUCT_ID, SqlDbType.NVarChar).Value = productID;
                    SQLComm.Parameters.Add(Constant.BATCH_ID, SqlDbType.DateTime).Value = batchID;

                    SqlDataReader reader = SQLComm.ExecuteReader();
                    while (reader.Read())
                    {
                        importPrice = reader[Constant.STOCK_IMPORT_PRICE].ToString();
                        sellPrice = reader[Constant.STOCK_SELL_PRICE].ToString();
                        expireDate = Convert.ToDateTime(reader[Constant.STOCK_EXPIRE_DATE].ToString());
                    }
                    reader.Close();
                    SQLConn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string[] IsThisTypeofProductExist(string productID, DateTime batchID)
        {
            DataTable dt = (DataTable)stockBindingSource.DataSource;
            var result = from DataRow row in dt.AsEnumerable()
                         where row[Constant.STOCK_P_ID].ToString().Equals(productID) && DateTime.Equals((DateTime)row[Constant.STOCK_BATCH_ID],batchID)
                         select row[Constant.STOCK_P_ID].ToString();

            if (result.Any())
                return result.ToArray();

            return new string[]{};
        }

        private void pbPendingRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                mainController.SynchronizeRequest();
                MessageBox.Show(Constant.SYNC_REQUEST_SUCCESSFULLY, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPending_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var grid = (DataGridView)sender;
            if (grid.Columns[e.ColumnIndex].Name == "clPendingUrgency" || grid.Columns[e.ColumnIndex].Name == "clApprovedUrgency" || grid.Columns[e.ColumnIndex].Name == "clRejectedUrgency" || grid.Columns[e.ColumnIndex].Name == "clReceiveStatus" || grid.Columns[e.ColumnIndex].Name == "clDeliveryStatus")
            {
                e.Value = (bool)e.Value ? "YES" : "NO";
                e.FormattingApplied = true;
            }
        }

        #endregion

        #region All about manufacturer

        private void EditManufacturer(object sender, DataGridViewCellEventArgs e)
        {

        }

        #endregion

        #region All about report

        private void btnVerify_Click(object sender, EventArgs e)
        {
            VerifyInfoWithHQ();
        }

        private void VerifyInfoWithHQ()
        {
            try
            {
                lblError.Visible = true;
                if (mainController.IsValidUserForSendingReport(txtReportUserID.Text, txtReportPassword.Text))
                {
                    pnlReportPanel.Visible = true;
                    dgvReport.Visible = true;
                    gbVerification.Visible = false;
                }
                else
                {
                    txtReportUserID.Clear();
                    txtReportPassword.Clear();
                    txtReportUserID.Focus();
                    lblError.Text = Constant.ERROR_REPORT_SHOP_NOT_FOUND;
                }
            }
            catch(Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private void tbSendReport_Enter(object sender, EventArgs e)
        {
            if (isFirstTimeEnterReportInterface)
            {
                txtReportUserID.Focus();
                reportBindingSource.Filter = string.Format(Constant.FIELTER_REPORT_BY_DATE, DateTime.Now.Date);
                isFirstTimeEnterReportInterface = false;
            }
        }

        private void dtpReportDate_ValueChanged(object sender, EventArgs e)
        {
            reportBindingSource.Filter = string.Format(Constant.FIELTER_REPORT_BY_DATE, dtpReportDate.Value.Date);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                if (mainController.IsReportSubmittedBefore(dtpReportDate.Value.Date))
                {
                    this.Cursor = Cursors.Default;
                    if ( MessageBox.Show(Constant.REPORT_SUMITTED_BEFORE, Constant.SPACE, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
                    {
                        this.Cursor = Cursors.AppStarting;
                        mainController.RemoveReportFromHQ(dtpReportDate.Value.Date);

                        foreach (DataGridViewRow dr in dgvReport.Rows)
                        {
                            mainController.SubmitReport(dr.Cells[Constant.OPERATION_INDEX_TWO].Value.ToString(), dtpReportDate.Value.Date, dr.Cells[Constant.OPERATION_INDEX_FOUR].Value.ToString(), dr.Cells[Constant.OPERATION_INDEX_FIVE].Value.ToString(), dr.Cells[Constant.OPERATION_INDEX_SIX].Value.ToString(), dr.Cells[Constant.OPERATION_INDEX_SEVEN].Value.ToString(), txtReportUserID.Text);
                        }
                        MessageBox.Show(Constant.REPORT_SUMITTED_SUCCESSFULLY, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    foreach (DataGridViewRow dr in dgvReport.Rows)
                    {
                        mainController.SubmitReport(dr.Cells[Constant.OPERATION_INDEX_TWO].Value.ToString(), dtpReportDate.Value.Date, dr.Cells[Constant.OPERATION_INDEX_FOUR].Value.ToString(), dr.Cells[Constant.OPERATION_INDEX_FIVE].Value.ToString(), dr.Cells[Constant.OPERATION_INDEX_SIX].Value.ToString(), dr.Cells[Constant.OPERATION_INDEX_SEVEN].Value.ToString(), txtReportUserID.Text);
                    }
                    MessageBox.Show(Constant.REPORT_SUMITTED_SUCCESSFULLY, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtReportUserID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                VerifyInfoWithHQ();
            }
        }

        #endregion

        #region All about staff

        private void removeStaffRows(ArrayList pendingList)
        {
            try
            {
                foreach (string staffID in pendingList)
                {
                    mainController.DeleteStaff(staffID);
                }
            }
            catch(Exception ex)
            {
                CustomMessageBox.Show(ex.Message, Constant.MSG_ERROR);
            }
        }

        private void btnDeleteStaff_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList pendingRemoveArray = new ArrayList();
                foreach (DataGridViewRow dgr in dgvStaff.Rows)
                {
                    if (Convert.ToBoolean(dgr.Cells[Constant.CONSTANT_ZERO].Value) == true)
                    {
                        pendingRemoveArray.Add(dgr.Cells[Constant.OPERATION_INDEX_ONE].Value.ToString());
                    }
                }
                if (pendingRemoveArray.Count == Constant.CONSTANT_ZERO)
                {
                    MessageBox.Show(Constant.NO_ROW_SELECTED, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show(Constant.CONFIRM_DELETE_STAFF, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
                {
                    removeStaffRows(pendingRemoveArray);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditStaff(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            if (dgvStaff.SelectedRows.Count == Constant.CONSTANT_ZERO)
                return;
            DataGridViewRow selectedRow = dgvStaff.SelectedRows[Constant.CONSTANT_ZERO];
            string staffID = selectedRow.Cells[Constant.OPERATION_INDEX_ONE].Value == null ? string.Empty : selectedRow.Cells[Constant.OPERATION_INDEX_ONE].Value.ToString();
            string name = selectedRow.Cells[Constant.OPERATION_INDEX_TWO].Value == null ? string.Empty : selectedRow.Cells[Constant.OPERATION_INDEX_TWO].Value.ToString();
            string gender = selectedRow.Cells[Constant.OPERATION_INDEX_THREE].Value == null ? string.Empty : selectedRow.Cells[Constant.OPERATION_INDEX_THREE].Value.ToString();
            DateTime joinDate = (DateTime)selectedRow.Cells[Constant.OPERATION_INDEX_FOUR].Value;
            string position = selectedRow.Cells[Constant.OPERATION_INDEX_FIVE].Value == null ? string.Empty : selectedRow.Cells[Constant.OPERATION_INDEX_FIVE].Value.ToString();
            string email = selectedRow.Cells[Constant.OPERATION_INDEX_SIX].Value == null ? string.Empty : selectedRow.Cells[Constant.OPERATION_INDEX_SIX].Value.ToString();
            string contact = selectedRow.Cells[Constant.OPERATION_INDEX_SEVEN].Value == null ? string.Empty : selectedRow.Cells[Constant.OPERATION_INDEX_SEVEN].Value.ToString();
            DateTime passwordExpireDate = (DateTime)selectedRow.Cells[Constant.OPERATION_INDEX_EIGHT].Value;
            DateTime birthDate = (DateTime)selectedRow.Cells[Constant.OPERATION_INDEX_NINE].Value;
            bool defaultPassword = (bool)selectedRow.Cells[Constant.OPERATION_INDEX_TEN].Value;
            bool blocked = (bool)selectedRow.Cells[Constant.OPERATION_INDEX_ELEVEN].Value;

            AddEditStaff frmEditStaff = new AddEditStaff(name, staffID, email, passwordExpireDate, birthDate, joinDate, gender, position, contact, defaultPassword, blocked);
            frmEditStaff.ShowDialog();
        }

        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            AddEditStaff frmAddStaff = new AddEditStaff();
            frmAddStaff.ShowDialog();
            ColorAndSortStaff();
        }

        #endregion
      
        #region All about cashier register

        private void removeCashierRegisterRows(ArrayList pendingList)
        {
            try
            {
                foreach (string cashierID in pendingList)
                {
                    mainController.DeleteCashierRegister(cashierID);
                }
            }
            catch
            {
                throw;
            }
        }

        private void btnAddCashierRegister_Click_1(object sender, EventArgs e)
        {
            string nextAvailableID = mainController.GetNextAvailableCashierRegisterID();
             AddCashierRegister newCashierRegister = new AddCashierRegister(nextAvailableID);
             newCashierRegister.ShowDialog();
        }

        private void btnDeleteCashierRegister_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList pendingRemoveArray = new ArrayList();
                foreach (DataGridViewRow dgr in dgvCashierRegister.Rows)
                {
                    if (Convert.ToBoolean(dgr.Cells[Constant.CONSTANT_ZERO].Value) == true)
                    {
                        pendingRemoveArray.Add(dgr.Cells[Constant.OPERATION_INDEX_ONE].Value.ToString());
                    }
                }
                if (pendingRemoveArray.Count == Constant.CONSTANT_ZERO)
                {
                    MessageBox.Show(Constant.NO_ROW_SELECTED, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show(Constant.CONFIRM_DELETE_CASHIER_REGISTER, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
                {
                    removeCashierRegisterRows(pendingRemoveArray);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCashierRegister_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dgvCashierRegister.Columns["clCashierRegisterStatus"].Index) 
                return;

            try
            {
                if (string.Equals((string)dgvCashierRegister[Constant.OPERATION_INDEX_TWO, e.RowIndex].Value, Constant.STATUS_ACTIVATED))
                {
                    mainController.UpdateCashierRegister((string)dgvCashierRegister[Constant.OPERATION_INDEX_ONE, e.RowIndex].Value, Constant.STATUS_DEACTIVATED);
                    dgvCashierRegister[Constant.OPERATION_INDEX_TWO, e.RowIndex].Value = Constant.STATUS_DEACTIVATED;
                }
                else
                {
                    mainController.UpdateCashierRegister((string)dgvCashierRegister[Constant.OPERATION_INDEX_ONE, e.RowIndex].Value, Constant.STATUS_ACTIVATED);
                    dgvCashierRegister[Constant.OPERATION_INDEX_TWO, e.RowIndex].Value = Constant.STATUS_ACTIVATED;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region All about price tag

        private void removePriceTagRows(ArrayList pendingList)
        {
            try
            {
                foreach (string priceTagID in pendingList)
                {
                    mainController.DeletePriceTag(priceTagID);
                }
            }
            catch
            {
                throw;
            }
        }

        private void btnAddPriceTag_Click(object sender, EventArgs e)
        {
            AddEditPriceTag newAddEditPriceTagForm = new AddEditPriceTag((DataTable)productBindingSource.DataSource, (DataTable)stockBindingSource.DataSource);
            newAddEditPriceTagForm.ShowDialog();
        }

        private void dgvPriceTag_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dgvPriceTag.Columns["clPriceTagStatus"].Index)
                return;

            try
            {
                if (string.Equals((string)dgvPriceTag[Constant.OPERATION_INDEX_FOUR, e.RowIndex].Value, Constant.STATUS_ACTIVATED))
                {
                    mainController.UpdatePriceTag(dgvPriceTag[Constant.OPERATION_INDEX_ONE, e.RowIndex].Value.ToString(), Constant.STATUS_DEACTIVATED, dgvPriceTag[Constant.OPERATION_INDEX_TWO, e.RowIndex].Value.ToString(), Convert.ToDateTime(dgvPriceTag[Constant.OPERATION_INDEX_THREE, e.RowIndex].Value.ToString()));
                    dgvPriceTag[Constant.OPERATION_INDEX_FOUR, e.RowIndex].Value = Constant.STATUS_DEACTIVATED;
                }
                else
                {
                    mainController.UpdatePriceTag(dgvPriceTag[Constant.OPERATION_INDEX_ONE, e.RowIndex].Value.ToString(), Constant.STATUS_ACTIVATED, dgvPriceTag[Constant.OPERATION_INDEX_TWO, e.RowIndex].Value.ToString(), Convert.ToDateTime(dgvPriceTag[Constant.OPERATION_INDEX_THREE, e.RowIndex].Value.ToString()));
                    dgvPriceTag[Constant.OPERATION_INDEX_FOUR, e.RowIndex].Value = Constant.STATUS_ACTIVATED;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPriceTag_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            if (dgvPriceTag.SelectedRows.Count == Constant.CONSTANT_ZERO)
                return;
            DataGridViewRow selectedRow = dgvPriceTag.SelectedRows[Constant.CONSTANT_ZERO];
            string priceTagID = selectedRow.Cells[Constant.OPERATION_INDEX_ONE].Value == null ? string.Empty : selectedRow.Cells[Constant.OPERATION_INDEX_ONE].Value.ToString();
            string productID = selectedRow.Cells[Constant.OPERATION_INDEX_TWO].Value == null ? string.Empty : selectedRow.Cells[Constant.OPERATION_INDEX_TWO].Value.ToString();
            string batchID = selectedRow.Cells[Constant.OPERATION_INDEX_THREE].Value == null ? string.Empty : selectedRow.Cells[Constant.OPERATION_INDEX_THREE].Value.ToString();
            bool status = selectedRow.Cells[Constant.OPERATION_INDEX_FOUR].Value.ToString() == Constant.STATUS_ACTIVATED ? true : false;

            AddEditPriceTag editPriceTag = new AddEditPriceTag(priceTagID, productID, batchID, status, (DataTable)productBindingSource.DataSource, (DataTable)stockBindingSource.DataSource);
            editPriceTag.ShowDialog();
        }

        private void btnDeletePriceTag_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList pendingRemoveArray = new ArrayList();
                foreach (DataGridViewRow dgr in dgvPriceTag.Rows)
                {
                    if (Convert.ToBoolean(dgr.Cells[Constant.CONSTANT_ZERO].Value) == true)
                    {
                        pendingRemoveArray.Add(dgr.Cells[Constant.OPERATION_INDEX_ONE].Value.ToString());
                    }
                }
                if (pendingRemoveArray.Count == Constant.CONSTANT_ZERO)
                {
                    MessageBox.Show(Constant.NO_ROW_SELECTED, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show(Constant.CONFIRM_DELETE_PRICE_TAG, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
                {
                    removePriceTagRows(pendingRemoveArray);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Constant.SPACE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion        



        

    }
}
