using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.IO;
using System.Diagnostics;
/*********************************************
 * Handle database retirval and update
 * 
 * Methods:
 * public DataTable FetchShop()
 * public void AddShop(string shopID, string country, string location, string contact)
 * public void DeleteShop(string shopID)
 * public void UpdateShop(string shopID, string country, string location, string contact)
 * 
 * public DataTable FetchProduct()
 * public void AddProduct(string manufacturerName, string productID, string name, string category, bool perishable)
 * public void DeleteProduct(string manufacturerID, string productID)
 * public void UpdateProduct(string manufacturerID, string productID, string name, string category, bool perishable)
 * 
 * public DataTable FetchStock()
 * public void AddStock(string manufacturerID, string productID, string warehouseID, DateTime batchID, string importPrice, string sellPrice, DateTime expieryDate, string quantity)
 * public void DeleteStock(string manufacturerID, string productID, string warehouseID, DateTime batchID)
 * public void UpdateStock(string manufacturerID, string productID, string warehouseID, DateTime batchID, string importPrice, string sellPrice, DateTime expieryDate, string quantity)
 * 
 * public DataTable FetchManufacturer()
 * public void AddManufacturer(string manufacturerID, string name, string country, string location, string contact)
 * public void DeleteManufacturer(string manufacturerID)
 * public void UpdateManufacturer(string manufacturerID, string name, string country, string location, string contact)
 * 
 * public DataTable FetchStaff()
 * public void AddStaff(string staffID, DateTime renewPasswordDate, DateTime dateOfBirth, DateTime joinDate, string gender, string religion, string position, string contact)
 * public void DeleteStaff(string staffID)
 * public void UpdateStaff(string staffID, string contact, string position)
 * 
 * public DataTable SubmitReport()
 * 
 * 
 * 
 * 
 * @Author: Huang Purong
 * @Date: 26/08/2013
 * ********************************************/

namespace Hypermarket_Shop_Management_Tool._1_Model
{
    class DBManager
    {
        public static DBManager DBManagerInstance;
        private DBConnectionManager DBConn = DBConnectionManager.getInstance();
        private DataSet dsTableList = new DataSet();
        private SqlConnection SQLConn = new SqlConnection();
        private SqlConnection SQLConnHQ = new SqlConnection();
        private string currentShopID;

        private SqlDataAdapter productAdapter;
        private SqlDataAdapter stockAdapter;
        private SqlDataAdapter manufacturerAdapter;
        private SqlDataAdapter staffAdapter;
        private SqlDataAdapter discountAdapter;
        private SqlDataAdapter transactionAdapter;
        private SqlDataAdapter cashierRegisterAdapter;
        private SqlDataAdapter priceTagAdapter;
        private SqlDataAdapter reportAdapter;
        private SqlDataAdapter pendingRequestAdapter;
        private SqlDataAdapter approvedRequestAdapter;
        private SqlDataAdapter rejectedRequestAdapter;
        private SqlDataAdapter incomingRequestAdapter;

        private SqlCommandBuilder staffCommandBuilder;
        private SqlCommandBuilder cashierRegisterCommandBuilder;
        private SqlCommandBuilder priceTagCommandBuilder;


        public static DBManager getInstance()
        {
            if (DBManagerInstance == null)
            {
                DBManagerInstance = new DBManager();
            }
            return DBManagerInstance;
        }

        private DBManager()
        {
            SetShopID();
            SQLConn.ConnectionString = DBConn.getLocalConnectionString();
            SQLConnHQ.ConnectionString = DBConn.getHQConnectionString();
            try
            {
                using(SQLConn)
                using(SQLConnHQ)
                {
                    SQLConn.Open();
                    SQLConnHQ.Open();
                    InitializeSelectCommand();
                    InitializeAdapterCommands();
                    FillDataset();
                    SQLConnHQ.Close();
                    SQLConn.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CheckAndCloseSQLConnection();
            }
        }

        private void SetShopID()
        {
            StreamReader sr = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)+"\\ShopManagementTool\\shopinfo.txt");
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                currentShopID = line;
                break;
            }
            sr.Close();
            //currentShopID = "0013";
        }


        #region Initialize Dataset

        private void InitializeSelectCommand()
        {
            productAdapter = new SqlDataAdapter(Constant.PRODUCT_TABLE_SELECT_COMMAND, SQLConn);
            stockAdapter = new SqlDataAdapter(Constant.STOCK_TABLE_SELECT_COMMAND, SQLConn);
            manufacturerAdapter = new SqlDataAdapter(Constant.MANUFACTURER_TABLE_SELECT_COMMAND, SQLConn);
            staffAdapter = new SqlDataAdapter(Constant.STAFF_TABLE_SELECT_COMMAND, SQLConn);
            discountAdapter = new SqlDataAdapter(Constant.DISCOUNT_TABLE_SELECT_COMMAND, SQLConn);
            transactionAdapter = new SqlDataAdapter(Constant.TRANSACTION_TABLE_SELECT_COMMAND, SQLConn);
            cashierRegisterAdapter = new SqlDataAdapter(Constant.CASHIER_REGISTER_TABLE_SELECT_COMMAND, SQLConn);
            priceTagAdapter = new SqlDataAdapter(Constant.PRICE_TAG_TABLE_SELECT_COMMAND, SQLConn);
            reportAdapter = new SqlDataAdapter(Constant.REPORT_TABLE_SELECT_COMMAND, SQLConn);
            pendingRequestAdapter = new SqlDataAdapter(string.Format(Constant.PENDING_REQUEST_SELECT_COMMAND,currentShopID) , SQLConnHQ);
            approvedRequestAdapter = new SqlDataAdapter(string.Format(Constant.APPROVED_REQUEST_SELECT_COMMAND,currentShopID), SQLConnHQ);
            rejectedRequestAdapter = new SqlDataAdapter(string.Format(Constant.REJECTED_REQUEST_SELECT_COMMAND,currentShopID), SQLConnHQ);
            incomingRequestAdapter = new SqlDataAdapter(string.Format(Constant.INCOMMING_REQUEST_SELECT_COMMAND,currentShopID), SQLConnHQ);
        }

        private void InitializeAdapterCommands()
        {
            staffCommandBuilder = new SqlCommandBuilder(staffAdapter);
            cashierRegisterCommandBuilder = new SqlCommandBuilder(cashierRegisterAdapter);
            priceTagCommandBuilder = new SqlCommandBuilder(priceTagAdapter);
        }

        private void FillDataset()
        {
            productAdapter.Fill(dsTableList, Constant.PRODUCT_TABLE);
            stockAdapter.Fill(dsTableList, Constant.STOCK_TABLE);
            manufacturerAdapter.Fill(dsTableList, Constant.MANUFACTURER_TABLE);
            staffAdapter.Fill(dsTableList, Constant.STAFF_TABLE);
            discountAdapter.Fill(dsTableList, Constant.DISCOUNT_TABLE);
            transactionAdapter.Fill(dsTableList, Constant.TRANSACTION_TABLE);
            cashierRegisterAdapter.Fill(dsTableList, Constant.CASHIER_REGISTER_TABLE);
            priceTagAdapter.Fill(dsTableList, Constant.PRICE_TAG_TABLE);
            reportAdapter.Fill(dsTableList, Constant.REPORT_TABLE);
            pendingRequestAdapter.Fill(dsTableList, Constant.PENDING_REQUEST_TABLE);
            approvedRequestAdapter.Fill(dsTableList, Constant.APPROVED_REQUEST_TABLE);
            rejectedRequestAdapter.Fill(dsTableList, Constant.REJECTED_REQUEST_TABLE);
            incomingRequestAdapter.Fill(dsTableList, Constant.INCOMING_REQUEST_TABLE);
        }

        #endregion


        #region Shared Methods

        private void CheckAndCloseSQLConnection()
        {
            if (SQLConn.State == ConnectionState.Open)
            {
                SQLConn.Close();
            }

            if (SQLConnHQ.State == ConnectionState.Open)
            {
                SQLConnHQ.Close();
            }
        }

        private void SynchronizeDataTableWithDatabaseTable(string tableName)
        {
            try
            {
                SQLConn.ConnectionString = DBConn.getLocalConnectionString();
                SQLConnHQ.ConnectionString = DBConn.getHQConnectionString();
                using(SQLConn)
                using (SQLConnHQ)
                {
                    SQLConn.Open();
                    SQLConnHQ.Open();
                    switch (tableName)
                    {
                        case Constant.STOCK_TABLE:
                            stockAdapter.Update(dsTableList, Constant.STOCK_TABLE);
                            break;
                        case Constant.STAFF_TABLE:
                            staffAdapter.Update(dsTableList, Constant.STAFF_TABLE);
                            break;
                        case Constant.CASHIER_REGISTER_TABLE:
                            cashierRegisterAdapter.Update(dsTableList, Constant.CASHIER_REGISTER_TABLE);
                            break;
                        case Constant.PRICE_TAG_TABLE:
                            priceTagAdapter.Update(dsTableList, Constant.PRICE_TAG_TABLE);
                            break;
                        case Constant.PENDING_REQUEST_TABLE:
                            pendingRequestAdapter.Update(dsTableList, Constant.PENDING_REQUEST_TABLE);
                            break;
                        case Constant.INCOMING_REQUEST_TABLE:
                            incomingRequestAdapter.Update(dsTableList, Constant.INCOMING_REQUEST_TABLE);
                            break;
                        default:
                            break;
                    }
                    SQLConnHQ.Close();
                    SQLConn.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CheckAndCloseSQLConnection();
            }
        }

        #endregion


        #region Manually Build Other Commands

        private void SetStockAdapterUpdateCommand(string importPrice, string sellPrice, DateTime expireDate, string quantity, string productID, DateTime batchID)
        {
            try
            {
                SqlCommand updateCommand = new SqlCommand(Constant.UPDATE_STOCK_COMMAND, SQLConn);
                updateCommand.Parameters.Add(Constant.IMPORT_PRICE, SqlDbType.Decimal).Value = Convert.ToDouble(importPrice);
                updateCommand.Parameters.Add(Constant.SELL_PRICE, SqlDbType.Decimal).Value = Convert.ToDouble(sellPrice);
                updateCommand.Parameters.Add(Constant.STOCK_EXPIRE_DATE, SqlDbType.Date).Value = expireDate.Date;
                updateCommand.Parameters.Add(Constant.QUANTITY, SqlDbType.Int).Value = quantity;
                updateCommand.Parameters.Add(Constant.PRODUCT_ID, SqlDbType.NVarChar).Value = productID;
                updateCommand.Parameters.Add(Constant.BATCH_ID, SqlDbType.Date).Value = batchID.Date;
                stockAdapter.UpdateCommand = updateCommand;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void SetStockAdapterInsertCommand(string productID, DateTime batchID, string importPrice, string sellPrice, DateTime expireDate, string quantity, bool defaultDiscount)
        {
            try
            {
                SqlCommand insertCommand = new SqlCommand(Constant.ADD_STOCK_COMMAND, SQLConn);
                insertCommand.Parameters.Add(Constant.PRODUCT_ID, SqlDbType.NVarChar).Value = productID;
                insertCommand.Parameters.Add(Constant.BATCH_ID, SqlDbType.Date).Value = batchID.Date;
                insertCommand.Parameters.Add(Constant.IMPORT_PRICE, SqlDbType.Decimal).Value = Convert.ToDouble(importPrice);
                insertCommand.Parameters.Add(Constant.SELL_PRICE, SqlDbType.Decimal).Value = Convert.ToDouble(sellPrice);
                insertCommand.Parameters.Add(Constant.EXPIRE_DATE, SqlDbType.Date).Value = expireDate.Date;
                insertCommand.Parameters.Add(Constant.QUANTITY, SqlDbType.Int).Value = Convert.ToInt32(quantity);
                insertCommand.Parameters.Add(Constant.DEFAULT_DISCOUNT, SqlDbType.Bit).Value = defaultDiscount;
                stockAdapter.InsertCommand = insertCommand;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void SetSendStockAdapterDeleteCommand(string productID, DateTime batchID, string shopID)
        {
            try
            {
                SqlCommand deleteCommand = new SqlCommand(Constant.DELETE_SEND_STOCK_COMMAND, SQLConn);
                deleteCommand.Parameters.Add(Constant.PRODUCT_ID, SqlDbType.NVarChar).Value = productID;
                deleteCommand.Parameters.Add(Constant.BATCH_ID, SqlDbType.Date).Value = batchID.Date;
                deleteCommand.Parameters.Add(Constant.SHOP_ID, SqlDbType.NVarChar).Value = shopID;
                stockAdapter.DeleteCommand = deleteCommand;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void SetIncomingStockUpdateCommand(string productID, DateTime batchID, DateTime confirmDate, DateTime receiveDate, bool receiveStatus)
        {
            try
            {
                SqlCommand updateCommand = new SqlCommand(Constant.UPDATE_INCOMING_STOCK_STATUS_COMMAND, SQLConnHQ);
                updateCommand.Parameters.Add(Constant.PRODUCT_ID, SqlDbType.NVarChar).Value = productID;
                updateCommand.Parameters.Add(Constant.BATCH_ID, SqlDbType.DateTime).Value = batchID;
                updateCommand.Parameters.Add(Constant.SHOP_ID, SqlDbType.NVarChar).Value = currentShopID;
                updateCommand.Parameters.Add(Constant.CONFIRM_DATE, SqlDbType.DateTime).Value = confirmDate;

                updateCommand.Parameters.Add(Constant.RECEIVE_DATE, SqlDbType.DateTime).Value = receiveDate;
                updateCommand.Parameters.Add(Constant.RECEIVE_STATUS, SqlDbType.Bit).Value = receiveStatus;
                incomingRequestAdapter.UpdateCommand = updateCommand;
            }
            catch
            {
                throw;
            }
        }

        private void SetPendingRequestInsertCommand(string productID, DateTime requestDate, string quantity, bool urgency)
        {
            try
            {
                SqlCommand insertCommand = new SqlCommand(Constant.ADD_PENDING_REQUEST_COMMAND, SQLConnHQ);
                insertCommand.Parameters.Add(Constant.PRODUCT_ID, SqlDbType.NVarChar).Value = productID;
                insertCommand.Parameters.Add(Constant.SHOP_ID, SqlDbType.NVarChar).Value = currentShopID;
                insertCommand.Parameters.Add(Constant.REQUEST_DATE, SqlDbType.DateTime).Value = requestDate;
                insertCommand.Parameters.Add(Constant.QUANTITY, SqlDbType.NVarChar).Value = quantity;
                insertCommand.Parameters.Add(Constant.URGENCY, SqlDbType.Bit).Value = urgency;

                pendingRequestAdapter.InsertCommand = insertCommand;
            }
            catch
            {
                throw;
            }
        }

        #endregion


        #region Sychronization

        public void SyncInventory()
        {
            try
            {
                SQLConn.ConnectionString = DBConn.getLocalConnectionString();

                using (SQLConn)
                {
                    if (SQLConn.State == ConnectionState.Closed)
                    {
                        SQLConn.Open();
                    }
                    dsTableList.Tables[Constant.MANUFACTURER_TABLE].Clear();
                    manufacturerAdapter.Fill(dsTableList, Constant.MANUFACTURER_TABLE);

                    dsTableList.Tables[Constant.PRODUCT_TABLE].Clear();
                    productAdapter.Fill(dsTableList, Constant.PRODUCT_TABLE);

                    dsTableList.Tables[Constant.STOCK_TABLE].Clear();
                    stockAdapter.Fill(dsTableList, Constant.STOCK_TABLE);
                    SQLConn.Close();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                CheckAndCloseSQLConnection();
            }
        }

        public void SyncTransaction()
        {
            try
            {
                SQLConn.ConnectionString = DBConn.getLocalConnectionString();
                using (SQLConn)
                {
                    if (SQLConn.State == ConnectionState.Closed)
                    {
                        SQLConn.Open();
                    }
                    dsTableList.Tables[Constant.TRANSACTION_TABLE].Clear();
                    transactionAdapter.Fill(dsTableList, Constant.TRANSACTION_TABLE);

                    SQLConn.Close();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                CheckAndCloseSQLConnection();
            }
        }

        public void SyncRequest()
        {
            try
            {
                SQLConnHQ.ConnectionString = DBConn.getHQConnectionString();
                using (SQLConnHQ)
                {
                    if (SQLConnHQ.State == ConnectionState.Closed)
                    {
                        SQLConnHQ.Open();
                    }
                    dsTableList.Tables[Constant.PENDING_REQUEST_TABLE].Clear();
                    dsTableList.Tables[Constant.APPROVED_REQUEST_TABLE].Clear();
                    dsTableList.Tables[Constant.REJECTED_REQUEST_TABLE].Clear();
                    dsTableList.Tables[Constant.INCOMING_REQUEST_TABLE].Clear();

                    pendingRequestAdapter.Fill(dsTableList, Constant.PENDING_REQUEST_TABLE);
                    approvedRequestAdapter.Fill(dsTableList, Constant.APPROVED_REQUEST_TABLE);
                    rejectedRequestAdapter.Fill(dsTableList, Constant.REJECTED_REQUEST_TABLE);
                    incomingRequestAdapter.Fill(dsTableList, Constant.INCOMING_REQUEST_TABLE);

                    SQLConnHQ.Close();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                CheckAndCloseSQLConnection();
            }
        }

        #endregion


        #region Product Operation

        public DataTable FetchProduct()
        {
            return dsTableList.Tables[Constant.PRODUCT_TABLE];
        }
        #endregion


        #region Stock Operation

        public DataTable FetchStock()
        {
            return dsTableList.Tables[Constant.STOCK_TABLE];
        }

        public void AddStock(string productName, string manufacturerName, string productID, DateTime batchID, string importPrice, string sellPrice, DateTime expireDate, string quantity, bool defaultDiscount)
        {
            try
            {
                DataRow newRow = dsTableList.Tables[Constant.STOCK_TABLE].NewRow();
                newRow[Constant.STOCK_P_NAME] = productName;
                newRow[Constant.STOCK_M_NAME] = manufacturerName;
                newRow[Constant.STOCK_P_ID] = productID;
                newRow[Constant.STOCK_BATCH_ID] = batchID.Date;
                newRow[Constant.STOCK_IMPORT_PRICE] = Convert.ToDouble(importPrice);
                newRow[Constant.STOCK_SELL_PRICE] = Convert.ToDouble(sellPrice);
                newRow[Constant.STOCK_EXPIRE_DATE] = expireDate.Date;
                newRow[Constant.STOCK_QUANTITY] = Convert.ToInt64(quantity);
                newRow[Constant.STOCK_DEFAULT_DISCOUNT_POLICY] = defaultDiscount;
                dsTableList.Tables[Constant.STOCK_TABLE].Rows.InsertAt(newRow, Constant.CONSTANT_ZERO);
                SetStockAdapterInsertCommand(productID, batchID, importPrice, sellPrice, expireDate, quantity, defaultDiscount);
                SynchronizeDataTableWithDatabaseTable(Constant.STOCK_TABLE);
            }
            catch
            {
                throw;
            }
        }

        public void UpdateStock(string productID, DateTime batchID, string importPrice, string sellPrice, DateTime expireDate, string quantity)
        {
            DataTable dt = dsTableList.Tables[Constant.STOCK_TABLE];
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][Constant.STOCK_P_ID].ToString().Equals(productID) && DateTime.Equals(dt.Rows[i][Constant.STOCK_BATCH_ID], batchID))
                    {
                        dsTableList.Tables[Constant.STOCK_TABLE].Rows[i][Constant.STOCK_IMPORT_PRICE] = Convert.ToDouble(importPrice);
                        dsTableList.Tables[Constant.STOCK_TABLE].Rows[i][Constant.STOCK_SELL_PRICE] = Convert.ToDouble(sellPrice);
                        dsTableList.Tables[Constant.STOCK_TABLE].Rows[i][Constant.STOCK_EXPIRE_DATE] = expireDate.Date;
                        dsTableList.Tables[Constant.STOCK_TABLE].Rows[i][Constant.STOCK_QUANTITY] = Convert.ToInt32(quantity);
                        SetStockAdapterUpdateCommand(importPrice, sellPrice, expireDate, quantity, productID, batchID);
                        SynchronizeDataTableWithDatabaseTable(Constant.STOCK_TABLE);
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public ArrayList getLatestBatchInfo(string productID)
        {
            DataRow[] result = dsTableList.Tables[Constant.STOCK_TABLE].Select("ProductID = '" + productID + "' AND  Quantity <> '0" + "'", "BatchID DESC");

            return new ArrayList(){result[Constant.CONSTANT_ZERO][Constant.CONSTANT_ZERO], result[Constant.CONSTANT_ZERO][Constant.OPERATION_INDEX_ONE], result[Constant.CONSTANT_ZERO][Constant.OPERATION_INDEX_TWO], result[Constant.CONSTANT_ZERO][Constant.OPERATION_INDEX_THREE],
                                    result[Constant.CONSTANT_ZERO][Constant.OPERATION_INDEX_FOUR], result[Constant.CONSTANT_ZERO][Constant.OPERATION_INDEX_FIVE], result[Constant.CONSTANT_ZERO][Constant.OPERATION_INDEX_SIX], result[Constant.CONSTANT_ZERO][Constant.OPERATION_INDEX_SEVEN],
                                    result[Constant.CONSTANT_ZERO][Constant.OPERATION_INDEX_EIGHT]};
        }

        public void UpdateStockQuantity(string productID, DateTime batchID, string newQuantity)
        {
            SQLConn.ConnectionString = DBConn.getLocalConnectionString();

            try
            {
                using (SqlCommand SQLComm = new SqlCommand(Constant.UPDATE_STOCK_QUANTITY, SQLConn))
                {
                    if (SQLConn.State == ConnectionState.Closed)
                    {
                        SQLConn.Open();
                    }

                    SQLComm.Parameters.Add(Constant.PRODUCT_ID, SqlDbType.NVarChar).Value = productID;
                    SQLComm.Parameters.Add(Constant.BATCH_ID, SqlDbType.DateTime).Value = batchID;
                    SQLComm.Parameters.Add(Constant.QUANTITY, SqlDbType.Int).Value = Convert.ToInt16(newQuantity);

                    SQLComm.ExecuteNonQuery();

                    SQLConn.Close();
                }

                DataTable dt = dsTableList.Tables[Constant.STOCK_TABLE];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][Constant.STOCK_P_ID].ToString().Equals(productID) && DateTime.Equals(dt.Rows[i][Constant.STOCK_BATCH_ID], batchID))
                    {
                        dsTableList.Tables[Constant.STOCK_TABLE].Rows[i][Constant.STOCK_QUANTITY] = Convert.ToInt32(newQuantity);
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion


        #region Manufacturer Operation

        public DataTable FetchManufacturer()
        {
            return dsTableList.Tables[Constant.MANUFACTURER_TABLE];
        }

        #endregion


        #region Transaction Operation

        public DataTable FetchTransaction()
        {
            return dsTableList.Tables[Constant.TRANSACTION_TABLE];
        }

        #endregion


        #region Staff Operation

        public DataTable FetchStaff()
        {
            return dsTableList.Tables[Constant.STAFF_TABLE];
        }

        public void AddStaff(string staffID, string name, string email, DateTime renewPasswordDate, DateTime dateOfBirth, DateTime joinDate, string gender, string position, string contact, bool isDefaultPassword, bool isBlocked)
        {
            try
            {
                dsTableList.Tables[Constant.STAFF_TABLE].Rows.Add(staffID, name, Constant.DEFAULT_PASSWORD_ENCRYPTED, renewPasswordDate, email, dateOfBirth, joinDate, gender, position, contact, isDefaultPassword, isBlocked);
                SynchronizeDataTableWithDatabaseTable(Constant.STAFF_TABLE);
            }
            catch
            {
                throw;
            }
        }

        public void DeleteStaff(string staffID)
        {
            DataTable dt = dsTableList.Tables[Constant.STAFF_TABLE];
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][Constant.STAFF_ID].ToString().Equals(staffID))
                    {
                        dsTableList.Tables[Constant.STAFF_TABLE].Rows[i].Delete();
                        SynchronizeDataTableWithDatabaseTable(Constant.STAFF_TABLE);
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void UpdateStaff(string staffID, string contact, string position, bool defaultPassword, bool blocked)
        {
            DataTable dt = dsTableList.Tables[Constant.STAFF_TABLE];
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][Constant.STAFF_ID].ToString().Equals(staffID))
                    {
                        dt.Rows[i][Constant.STAFF_CONTACT] = Convert.ToInt64(contact);
                        dt.Rows[i][Constant.STAFF_POSITION] = position;
                        dt.Rows[i][Constant.STAFF_IS_DEFAULT_PASSWORD] = defaultPassword;
                        dt.Rows[i][Constant.STAFF_BLOCKED] = blocked;
                        SynchronizeDataTableWithDatabaseTable(Constant.STAFF_TABLE);
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void UpdateStaffPassword(string email, string newPassword)
        {
            DataTable dt = dsTableList.Tables[Constant.STAFF_TABLE];
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][Constant.EMAIL].ToString().Equals(email))
                    {
                        dt.Rows[i][Constant.STAFF_PASSWORD] = newPassword;
                        dt.Rows[i][Constant.STAFF_RENEWED_PASSWORD_DATE] = DateTime.Now.Date.AddDays(Constant.PASSWORD_VALID_DAYS);
                        dt.Rows[i][Constant.STAFF_IS_DEFAULT_PASSWORD] = false;
                        SynchronizeDataTableWithDatabaseTable(Constant.STAFF_TABLE);
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void ResetStaffPassword(string staffID)
        {
            DataTable dt = dsTableList.Tables[Constant.STAFF_TABLE];
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][Constant.STAFF_ID].ToString().Equals(staffID))
                    {
                        dt.Rows[i][Constant.STAFF_PASSWORD] = Constant.DEFAULT_PASSWORD_ENCRYPTED;
                        dt.Rows[i][Constant.STAFF_RENEWED_PASSWORD_DATE] = DateTime.Now.Date.AddDays(Constant.PASSWORD_VALID_DAYS);
                        dt.Rows[i][Constant.STAFF_IS_DEFAULT_PASSWORD] = true;
                        SynchronizeDataTableWithDatabaseTable(Constant.STAFF_TABLE);
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion
    

        #region Request Operation

        public DataTable FetchPendingRequest()
        {
            return dsTableList.Tables[Constant.PENDING_REQUEST_TABLE];
        }

        public DataTable FetchApprovedRequest()
        {
            return dsTableList.Tables[Constant.APPROVED_REQUEST_TABLE];
        }

        public DataTable FetchRejectedRequest()
        {
            return dsTableList.Tables[Constant.REJECTED_REQUEST_TABLE];
        }

        public DataTable FetchIncomingRequest()
        {
            return dsTableList.Tables[Constant.INCOMING_REQUEST_TABLE];
        }

        public void AddPendingRequest(string productID, string manufacturer, string productName, DateTime requestDate, string quantity, bool urgency)
        {
            try
            {
                dsTableList.Tables[Constant.PENDING_REQUEST_TABLE].Rows.Add(productID, manufacturer, productName, requestDate.ToString(), quantity, urgency);
                SetPendingRequestInsertCommand(productID, requestDate, quantity, urgency);
                SynchronizeDataTableWithDatabaseTable(Constant.PENDING_REQUEST_TABLE);
            }
            catch
            {
                throw;
            }
        }

        public void UpdateIncomingStock(string productID, DateTime batchID, DateTime confirmDate, DateTime receiveDate, bool receiveStatus)
        {
            DataTable dt = dsTableList.Tables[Constant.INCOMING_REQUEST_TABLE];
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][Constant.P_ID].ToString().Equals(productID) && DateTime.Equals((DateTime)dt.Rows[i][Constant.STOCK_BATCH_ID], batchID)
                        && DateTime.Equals((DateTime)dt.Rows[i][Constant.INCOMING_REQUEST_CONFIRM_DATE], confirmDate))
                    {
                        dt.Rows[i][Constant.INCOMING_REQUEST_RECEIVE_DATE] = receiveDate;
                        dt.Rows[i][Constant.INCOMING_REQUEST_RECEIVE_STATUS] = receiveStatus;

                        SetIncomingStockUpdateCommand(productID, batchID, confirmDate, receiveDate, receiveStatus);
                        SynchronizeDataTableWithDatabaseTable(Constant.INCOMING_REQUEST_TABLE);
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion


        #region Discount Operation

        public DataTable FetchDiscountPolicy()
        {
            return dsTableList.Tables[Constant.DISCOUNT_TABLE];
        }

        #endregion


        #region Cashier Register Operation

        public DataTable FetchCashierRegister()
        {
            try
            {
                return dsTableList.Tables[Constant.CASHIER_REGISTER_TABLE];
            }
            catch
            {
                throw;
            }
        }

        public void UpdateCashierRegisterStatus(string ID, string status)
        {
            DataTable dt = dsTableList.Tables[Constant.CASHIER_REGISTER_TABLE];
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][Constant.CASHIER_ID].ToString().Equals(ID))
                    {
                        dt.Rows[i][Constant.CASHIER_STATUS] = status;
                        SynchronizeDataTableWithDatabaseTable(Constant.CASHIER_REGISTER_TABLE);
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddCashierRegister(string ID, string status)
        {
            try
            {
                dsTableList.Tables[Constant.CASHIER_REGISTER_TABLE].Rows.Add(ID, status);
                SynchronizeDataTableWithDatabaseTable(Constant.CASHIER_REGISTER_TABLE);
            }
            catch
            {
                throw;
            }
        }

        public void DeleteCashierRegister(string ID)
        {
            DataTable dt = dsTableList.Tables[Constant.CASHIER_REGISTER_TABLE];
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][Constant.CASHIER_ID].ToString().Equals(ID))
                    {
                        dsTableList.Tables[Constant.CASHIER_REGISTER_TABLE].Rows[i].Delete();
                        SynchronizeDataTableWithDatabaseTable(Constant.CASHIER_REGISTER_TABLE);
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion


        #region Price Tag

        public DataTable FetchPriceTag()
        {
            try
            {
                return dsTableList.Tables[Constant.PRICE_TAG_TABLE];
            }
            catch
            {
                throw;
            }
        }

        public void UpdatePriceTag(string ID, string status, string productID, DateTime batch)
        {
            DataTable dt = dsTableList.Tables[Constant.PRICE_TAG_TABLE];
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][Constant.PRICE_TAG_ID].ToString().Equals(ID))
                    {
                        dt.Rows[i][Constant.PRICE_TAG_STATUS] = status;
                        dt.Rows[i][Constant.PRICE_TAG_P_ID] = productID;
                        dt.Rows[i][Constant.PRICE_TAG_B_ID] = batch;
                        SynchronizeDataTableWithDatabaseTable(Constant.PRICE_TAG_TABLE);
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddPriceTag(string ID, string status, string productID, DateTime batch)
        {
            try
            {
                dsTableList.Tables[Constant.PRICE_TAG_TABLE].Rows.Add(ID, status, productID, batch);
                SynchronizeDataTableWithDatabaseTable(Constant.PRICE_TAG_TABLE);
            }
            catch
            {
                throw;
            }
        }

        public void DeletePriceTag(string ID)
        {
            DataTable dt = dsTableList.Tables[Constant.PRICE_TAG_TABLE];
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][Constant.PRICE_TAG_ID].ToString().Equals(ID))
                    {
                        dsTableList.Tables[Constant.PRICE_TAG_TABLE].Rows[i].Delete();
                        SynchronizeDataTableWithDatabaseTable(Constant.PRICE_TAG_TABLE);
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion


        #region Report Operation

        public DataTable FetchReport()
        {
            return dsTableList.Tables[Constant.REPORT_TABLE];
        }

        public bool IsValidUserForSendingReport(string userID, string password)
        {
            _2_Controller.SecurityManager securityController = _2_Controller.SecurityManager.getInstance();
            try
            {
                using (SqlConnection hqConn = new SqlConnection(DBConn.getHQConnectionString()))
                using (SqlCommand hqComm = new SqlCommand(Constant.REPORT_VERIFY_USER, hqConn))
                {
                    hqComm.CommandType = CommandType.Text;
                    hqComm.Parameters.Add(Constant.REPORT_USER_ID, SqlDbType.NVarChar);
                    hqComm.Parameters[Constant.REPORT_USER_ID].Value = userID;
                    hqComm.Parameters.Add(Constant.REPORT_PASSWORD, SqlDbType.NVarChar);
                    hqComm.Parameters[Constant.REPORT_PASSWORD].Value = securityController.ComplexEncrypt(password);
                    hqComm.Parameters.Add(Constant.REPORT_USER_POSITION, SqlDbType.NVarChar);
                    hqComm.Parameters[Constant.REPORT_USER_POSITION].Value = Constant.REPORT_AUTHORISED_POSITION;

                    hqConn.Open();
                    int count = (int)hqComm.ExecuteScalar();
                    hqConn.Close();

                    if (count > 0)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch
            {
                throw;
            }
        }

        public bool IsReportSubmittedBefore(DateTime reportDate)
        {
            try
            {
                using (SqlConnection hqConn = new SqlConnection(DBConn.getHQConnectionString()))
                using (SqlCommand hqComm = new SqlCommand(Constant.REPORT_EXIST, hqConn))
                {
                    hqComm.CommandType = CommandType.Text;
                    hqComm.Parameters.Add(Constant.SHOP_ID, SqlDbType.NVarChar);
                    hqComm.Parameters[Constant.SHOP_ID].Value = currentShopID;
                    hqComm.Parameters.Add(Constant.REPORT_DATE, SqlDbType.DateTime);
                    hqComm.Parameters[Constant.REPORT_DATE].Value = reportDate.Date;

                    hqConn.Open();
                    int count = (int)hqComm.ExecuteScalar();
                    hqConn.Close();

                    if (count > 0)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch
            {
                throw;
            }
        }

        public void RemoveReportFromHQ(DateTime reportDate)
        {
            try
            {
                using (SqlConnection hqConn = new SqlConnection(DBConn.getHQConnectionString()))
                using (SqlCommand hqComm = new SqlCommand(Constant.REPORT_REMOVE, hqConn))
                {
                    hqComm.CommandType = CommandType.Text;
                    hqComm.Parameters.Add(Constant.SHOP_ID, SqlDbType.NVarChar);
                    hqComm.Parameters[Constant.SHOP_ID].Value = currentShopID;
                    hqComm.Parameters.Add(Constant.REPORT_DATE, SqlDbType.DateTime);
                    hqComm.Parameters[Constant.REPORT_DATE].Value = reportDate.Date;

                    hqConn.Open();
                    hqComm.ExecuteNonQuery();
                    hqConn.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        public void SubmitReport(string productID, DateTime reportDate, string unitSold, string costPrice, string sellingPrice, string revenue, string staffID)
        {
            try
            {
                using (SqlConnection hqConn = new SqlConnection(DBConn.getHQConnectionString()))
                using (SqlCommand hqComm = new SqlCommand(Constant.REPORT_SUBMIT, hqConn))
                {
                    hqComm.CommandType = CommandType.Text;

                    hqComm.Parameters.Add(Constant.SHOP_ID, SqlDbType.NVarChar);
                    hqComm.Parameters[Constant.SHOP_ID].Value = currentShopID;

                    hqComm.Parameters.Add(Constant.PRODUCT_ID, SqlDbType.NVarChar);
                    hqComm.Parameters[Constant.PRODUCT_ID].Value = productID;

                    hqComm.Parameters.Add(Constant.REPORT_DATE, SqlDbType.DateTime);
                    hqComm.Parameters[Constant.REPORT_DATE].Value = reportDate.Date;

                    hqComm.Parameters.Add(Constant.REPORT_TOTAL_UNIT_SOLD, SqlDbType.Float);
                    hqComm.Parameters[Constant.REPORT_TOTAL_UNIT_SOLD].Value = Convert.ToDouble(unitSold);

                    hqComm.Parameters.Add(Constant.REPORT_TOTAL_COST_PRICE, SqlDbType.Float);
                    hqComm.Parameters[Constant.REPORT_TOTAL_COST_PRICE].Value = Convert.ToDouble(costPrice);

                    hqComm.Parameters.Add(Constant.REPORT_TOTAL_SELLING_PRICE, SqlDbType.Float);
                    hqComm.Parameters[Constant.REPORT_TOTAL_SELLING_PRICE].Value = Convert.ToDouble(sellingPrice);

                    hqComm.Parameters.Add(Constant.REPORT_TOTAL_REVENUE, SqlDbType.Float);
                    hqComm.Parameters[Constant.REPORT_TOTAL_REVENUE].Value = Convert.ToDouble(revenue);

                    hqComm.Parameters.Add(Constant.REPORT_USER_ID, SqlDbType.NVarChar);
                    hqComm.Parameters[Constant.REPORT_USER_ID].Value = staffID;

                    hqConn.Open();
                    hqComm.ExecuteNonQuery();
                    hqConn.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
