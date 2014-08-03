using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

/***********************************************
 * This API Manages the flows from the views to individual managers. It's basically a flow controller.
 * 
 * Methods:
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * @Auther: Huang Purong
 * @Date: 27/08/2013
 * *********************************************/

namespace Hypermarket_Shop_Management_Tool._2_Controller
{
    class CoordinatingController
    {
        private static CoordinatingController instance;
        private ProductManager productController = ProductManager.getInstance();
        private AccountManager accountController = AccountManager.getInstance();
        private SalesManager salesController = SalesManager.getInstance();
        private WarehouseManager warehouseController = WarehouseManager.getInstance();
        private SynchronizationManager syncController = SynchronizationManager.getInstance();

        private CoordinatingController()
        {
            
        }

        public static CoordinatingController getInstance()
        {
            if (instance == null)
            {
                instance = new CoordinatingController();
            }
            return instance;
        }

        #region Product Manager

        public DataTable GetListOfProduct()
        {
            try
            {
                return productController.FetchProduct();
            }
            catch (Exception e)
            {
                ErrorLog.Log(Constant.ERROR_FETCH + Constant.PRODUCT_TABLE + Constant.ACTUAL_ERROR + e.Message);
                throw new Exception(Constant.ERROR_FETCH + Constant.PRODUCT_TABLE);
            }
        }

        public DataTable GetListOfManufacturer()
        {
            try
            {
                return productController.FetchManufacturer();
            }
            catch (Exception e)
            {
                ErrorLog.Log(Constant.ERROR_FETCH + Constant.MANUFACTURER_TABLE + Constant.ACTUAL_ERROR + e.Message);
                throw new Exception(Constant.ERROR_FETCH + Constant.MANUFACTURER_TABLE);
            }
        }

        #endregion

        #region  Sales Manager

        public DataTable GetListOfReport()
        {
            try
            {
                return salesController.FetchReport();;
            }
            catch(Exception e)
            {
                ErrorLog.Log(Constant.ERROR_FETCH + Constant.REPORT_TABLE + Constant.ACTUAL_ERROR + e.Message);
                throw new Exception(Constant.ERROR_FETCH + Constant.REPORT_TABLE);
            }
        }

        public DataTable GetListOfDiscountPolicy()
        {
            try
            {
                return salesController.FetchDiscount();
            }
            catch (Exception e)
            {
                ErrorLog.Log(Constant.ERROR_FETCH + Constant.DISCOUNT_TABLE + Constant.ACTUAL_ERROR + e.Message);
                throw new Exception(Constant.ERROR_FETCH + Constant.DISCOUNT_TABLE);
            }
        }

        public DataTable GetListOfTransaction()
        {
            try
            {
                return salesController.FetchTransaction();
            }
            catch (Exception e)
            {
                ErrorLog.Log(Constant.ERROR_FETCH + Constant.TRANSACTION_TABLE + Constant.ACTUAL_ERROR + e.Message);
                throw new Exception(Constant.ERROR_FETCH + Constant.TRANSACTION_TABLE);
            }
        }

        public bool IsValidUserForSendingReport(string userID, string password)
        {
            try
            {
                return salesController.IsValidUserForSendingReport(userID, password);
            }
            catch(Exception ex)
            {
                ErrorLog.Log(Constant.ERROR_REPORT_VERIFY + Constant.ACTUAL_ERROR + ex.Message);
                throw new Exception(Constant.ERROR_REPORT_VERIFY);
            }
        }

        public void SubmitReport(string productID, DateTime reportDate, string unitSold, string costPrice, string sellingPrice, string revenue, string staffID)
        {
            try
            {
                salesController.SubmitReport(productID, reportDate, unitSold, costPrice, sellingPrice, revenue, staffID);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(Constant.ERROR_REPORT_SUBMIT + Constant.ACTUAL_ERROR + ex.Message);
                throw new Exception(Constant.ERROR_REPORT_SUBMIT);
            }
        }

        public bool IsReportSubmittedBefore(DateTime reportDate)
        {
            try
            {
                return salesController.IsReportSubmittedBefore(reportDate);
            }
            catch(Exception ex)
            {
                ErrorLog.Log(Constant.ERROR_CHECK_REPORT + Constant.ACTUAL_ERROR + ex.Message);
                throw new Exception(Constant.ERROR_CHECK_REPORT);
            }
        }

        public void RemoveReportFromHQ(DateTime reportDate)
        {
            try
            {
                salesController.RemoveReportFromHQ(reportDate);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(Constant.ERROR_REPORT_SUBMIT + Constant.ACTUAL_ERROR + ex.Message);
                throw new Exception(Constant.ERROR_REPORT_SUBMIT);
            }
        }

        #endregion

        #region Warehouse Manager

        public DataTable GetListOfStock()
        {
            try
            {
                return warehouseController.FetchStock();
            }
            catch (Exception e)
            {
                ErrorLog.Log(Constant.ERROR_FETCH + Constant.STOCK_TABLE + Constant.ACTUAL_ERROR + e.Message);
                throw new Exception(Constant.ERROR_FETCH + Constant.STOCK_TABLE);
            }
        }

        public void AddStock(string productName, string manufacturerName, string productID, DateTime batchID, string importPrice, string sellPrice, DateTime expireDate, string quantity)
        {
            try
            {
                warehouseController.AddStock(productName, manufacturerName, productID, batchID, importPrice, sellPrice, expireDate, quantity);
            }
            catch (Exception e)
            {
                ErrorLog.Log(Constant.ERROR_ADD + Constant.STOCK_TABLE + Constant.ACTUAL_ERROR + e.Message);
                throw new Exception(Constant.ERROR_ADD + Constant.STOCK_TABLE);
            }
        }

        public void UpdateStock(string productID, DateTime batchID, string importPrice, string sellPrice, DateTime expireDate, string quantity)
        {
            try
            {
                warehouseController.UpdateStock(productID, batchID, importPrice, sellPrice, expireDate, quantity);
            }
            catch (Exception e)
            {
                ErrorLog.Log(Constant.ERROR_UPDATE + Constant.STOCK_TABLE + Constant.ACTUAL_ERROR + e.Message);
                throw new Exception(Constant.ERROR_UPDATE + Constant.STOCK_TABLE);
            }
        }

        public void UpdateStockQuantity(string productID, DateTime batchID, string quantity)
        {
            try
            {
                warehouseController.UpdateStockQuantity(productID, batchID, quantity);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(Constant.ERROR_UPDATE + Constant.STOCK_TABLE + Constant.ACTUAL_ERROR + ex.Message);
                throw new Exception(Constant.ERROR_UPDATE + Constant.STOCK_TABLE);
            }
        }

        public DataTable GetListOfCashierRegister()
        {
            try
            {
                return warehouseController.FetchCashierRegister();
            }
            catch (Exception ex)
            {
                ErrorLog.Log(Constant.ERROR_CASHIER_FETCH + Constant.ACTUAL_ERROR + ex.Message);
                throw new Exception(Constant.ERROR_CASHIER_FETCH);
            }
        }

        public void AddCashierRegister(string ID, string status)
        {
            try
            {
                warehouseController.AddCashierRegister(ID, status);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(Constant.ERROR_CASHIER_ADD + Constant.ERROR_NEXT_ACTION + Constant.ACTUAL_ERROR + ex.Message);
                throw new Exception(Constant.ERROR_CASHIER_ADD + Constant.ERROR_NEXT_ACTION);
            }
        }

        public void UpdateCashierRegister(string ID, string status)
        {
            try
            {
                warehouseController.UpdateCashierRegisterStatus(ID, status);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(Constant.ERROR_CASHIER_UPDATE + Constant.ERROR_NEXT_ACTION + Constant.ACTUAL_ERROR + ex.Message);
                throw new Exception(Constant.ERROR_CASHIER_UPDATE + Constant.ERROR_NEXT_ACTION);
            }
        }

        public void DeleteCashierRegister(string ID)
        {
            try
            {
                warehouseController.DeleteCashierRegister(ID);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(Constant.ERROR_CASHIER_DELETE + ID + Constant.FULL_STOP + Constant.ERROR_NEXT_ACTION + Constant.ACTUAL_ERROR + ex.Message);
                throw new Exception(Constant.ERROR_CASHIER_DELETE + ID + Constant.FULL_STOP + Constant.ERROR_NEXT_ACTION);
            }
        }

        public string GetNextAvailableCashierRegisterID()
        {
            try
            {
                return warehouseController.GenerateNextAvailableCashierRegisterID();
            }
            catch (Exception ex)
            {
                ErrorLog.Log(Constant.ERROR_GENERATE_NEXT_CASHIER_ID + Constant.ACTUAL_ERROR + ex.Message);
                throw new Exception(Constant.ERROR_GENERATE_NEXT_CASHIER_ID);
            }
        }

        public DataTable GetListOfPriceTag()
        {
            try
            {
                return warehouseController.FetchPriceTag();
            }
            catch (Exception ex)
            {
                ErrorLog.Log(Constant.ERROR_PRICE_TAG_FETCH + Constant.ACTUAL_ERROR + ex.Message);
                throw new Exception(Constant.ERROR_PRICE_TAG_FETCH);
            }
        }

        public void AddPriceTag(string ID, string status, string productID, DateTime batch)
        {
            try
            {
                warehouseController.AddPriceTag(ID, status, productID, batch);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(Constant.ERROR_PRICE_TAG_ADD + Constant.ERROR_NEXT_ACTION + Constant.ACTUAL_ERROR + ex.Message);
                throw new Exception(Constant.ERROR_PRICE_TAG_ADD + Constant.ERROR_NEXT_ACTION);
            }
        }

        public void UpdatePriceTag(string ID, string status, string productID, DateTime batch)
        {
            try
            {
                warehouseController.UpdatePriceTag(ID, status, productID, batch);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(Constant.ERROR_PRICE_TAG_UPDATE + Constant.ERROR_NEXT_ACTION + Constant.ACTUAL_ERROR + ex.Message);
                throw new Exception(Constant.ERROR_PRICE_TAG_UPDATE + Constant.ERROR_NEXT_ACTION);
            }
        }

        public void DeletePriceTag(string ID)
        {
            try
            {
                warehouseController.DeletePriceTag(ID);
            }
            catch (Exception ex)
            {
                ErrorLog.Log(Constant.ERROR_PRICE_TAG_DELETE + ID + Constant.FULL_STOP + Constant.ERROR_NEXT_ACTION + Constant.ACTUAL_ERROR + ex.Message);
                throw new Exception(Constant.ERROR_PRICE_TAG_DELETE + ID + Constant.FULL_STOP + Constant.ERROR_NEXT_ACTION);
            }
        }

        public string GetNextAvailablePriceTagID()
        {
            try
            {
                return warehouseController.GenerateNextAvailablePriceTagID();
            }
            catch (Exception ex)
            {
                ErrorLog.Log(Constant.ERROR_GENERATE_NEXT_PRICE_TAG_ID + Constant.ACTUAL_ERROR + ex.Message);
                throw new Exception(Constant.ERROR_GENERATE_NEXT_PRICE_TAG_ID);
            }
        }

        public DataTable GetListOfPendingRequest()
        {
            return warehouseController.FetchPendingRequest();
        }

        public DataTable GetListOfApprovedRequest()
        {
            return warehouseController.FetchApprovedRequest();
        }

        public DataTable GetListOfRejectedRequest()
        {
            return warehouseController.FetchRejectedRequest();
        }

        public DataTable GetListOfIncomingRequest()
        {
            return warehouseController.FetchIncomingRequest();
        }

        public void AddPendingRequest(string productID, string manufacturer, string productName, DateTime requestDate, string quantity, bool urgency)
        {
            try
            {
                warehouseController.AddPendingRequest(productID, manufacturer, productName, requestDate, quantity, urgency);
            }
            catch(Exception ex)
            {
                ErrorLog.Log(Constant.ERROR_ADD_PENDING_REQUEST + Constant.ERROR_NEXT_ACTION + Constant.ACTUAL_ERROR + ex.Message);
                throw new Exception(Constant.ERROR_ADD_PENDING_REQUEST + Constant.ERROR_NEXT_ACTION);
            }
        }

        public void UpdateIncomingStock(string productID, DateTime batchID, DateTime confirmDate, DateTime receiveDate, bool receiveStatus)
        {
            try
            {
                warehouseController.UpdateIncomingStock(productID, batchID, confirmDate, receiveDate, receiveStatus);
            }
            catch(Exception ex)
            {
                ErrorLog.Log(Constant.ERROR_UPDATE_INCOMING_STOCK_STATUS + Constant.ERROR_NEXT_ACTION + Constant.ACTUAL_ERROR + ex.Message);
                throw new Exception(Constant.ERROR_UPDATE_INCOMING_STOCK_STATUS + Constant.ERROR_NEXT_ACTION);
            }
        }

        #endregion

        #region Account Manager
        
        public bool IsValidUser(string email)
        {
            try
            {
                return accountController.IsValidUser(email);
            }
            catch(Exception e)
            {
                ErrorLog.Log(Constant.ERROR_VERIFYING_USER + Constant.ACTUAL_ERROR + e.Message);
                throw new Exception(Constant.ERROR_VERIFYING_USER);
            }
        }

        public bool VerifyUserIdAndPassword(string email, string password)
        {
            try
            {
                return accountController.VerifyUserIdAndPassword(email, password);
            }
            catch (Exception e)
            {
                ErrorLog.Log(Constant.ERROR_VERIFYING_PASSWORD + Constant.ACTUAL_ERROR + e.Message);
                throw new Exception(Constant.ERROR_VERIFYING_PASSWORD);
            }
        }

        public bool RequireChangingPassword(string staffID)
        {
            try
            {
                return (accountController.IsFirstTimeLogin(staffID) || accountController.IsPasswordExpired(staffID));
            }
            catch (Exception e)
            {
                ErrorLog.Log(Constant.ERROR_CHECK_DEFAULT_PASSWORD + Constant.ACTUAL_ERROR + e.Message);
                throw new Exception(Constant.ERROR_CHECK_DEFAULT_PASSWORD);
            }
        }

        public DataTable GetListOfStaff()
        {
            try
            {
                return accountController.FetchStaff();
            }
            catch (Exception e)
            {
                ErrorLog.Log(Constant.ERROR_FETCH + Constant.STAFF_TABLE + Constant.ACTUAL_ERROR + e.Message);
                throw new Exception(Constant.ERROR_FETCH + Constant.STAFF_TABLE);
            }
        }

        public void AddStaff(string staffID, string name, string email, DateTime dateOfBirth, DateTime joinDate, string gender, string position, string contact)
        {
            try
            {
                accountController.AddStaff(staffID, name, email, dateOfBirth, joinDate, gender, position, contact);
            }
            catch (Exception e)
            {
                ErrorLog.Log(Constant.ERROR_FETCH + Constant.STAFF_TABLE + Constant.ACTUAL_ERROR + e.Message);
                throw new Exception(Constant.ERROR_FETCH + Constant.STAFF_TABLE);
            }
        }

        public void DeleteStaff(string staffID)
        {
            try
            {
                accountController.DeleteStaff(staffID);
            }
            catch (Exception e)
            {
                ErrorLog.Log(Constant.ERROR_DELETE + Constant.STAFF_TABLE + Constant.ACTUAL_ERROR + e.Message);
                throw new Exception(Constant.ERROR_DELETE + Constant.STAFF_TABLE);
            }
        }

        public void UpdateStaff(string staffID, string contact, string position, bool defaultPassword, bool blocked)
        {
            try
            {
                accountController.UpdateStaff(staffID, contact, position, defaultPassword, blocked);
            }
            catch (Exception e)
            {
                ErrorLog.Log(Constant.ERROR_UPDATE + Constant.STAFF_TABLE + Constant.ACTUAL_ERROR + e.Message);
                throw new Exception(Constant.ERROR_UPDATE + Constant.STAFF_TABLE);
            }
        }

        public void ChangePassword(string email, string password)
        {
            try
            {
                accountController.ChangePassword(email, password);
            }
            catch (Exception e)
            {
                ErrorLog.Log(Constant.ERROR_UPDATE_PASSWORD + Constant.ACTUAL_ERROR + e.Message);
                throw new Exception(Constant.ERROR_UPDATE_PASSWORD);
            }
        }

        public void ResetPassword(string staffID)
        {
            accountController.ResetPassword(staffID);
        }

        public void BlockStaff()
        {
        }

        public string GenerateNextStaffID()
        {
            try
            {
                return accountController.GenerateNextAvailableStaffID();
            }
            catch (Exception ex)
            {
                ErrorLog.Log(Constant.ERROR_GENERATE_NEXT_STAFF_ID + Constant.ACTUAL_ERROR + ex.Message);
                throw new Exception(Constant.ERROR_GENERATE_NEXT_STAFF_ID);
            }
        }

        #endregion

        #region Synchronization Manager

        public void SynchronizeInventory()
        {
            try
            {
                syncController.SyncInventory();
            }
            catch (Exception ex)
            {
                ErrorLog.Log(Constant.ERROR_SYNC_INVENTORY + Constant.ACTUAL_ERROR + ex.Message);
                throw new Exception(Constant.ERROR_SYNC_INVENTORY);
            }
        }

        public void SynchronizeTransaction()
        {
            try
            {
                syncController.SyncTransaction();
            }
            catch (Exception ex)
            {
                ErrorLog.Log(Constant.ERROR_SYNC_TRANSACTION + Constant.ACTUAL_ERROR + ex.Message);
                throw new Exception(Constant.ERROR_SYNC_TRANSACTION);
            }
        }

        public void SynchronizeRequest()
        {
            try
            {
                syncController.SyncRequest();
            }
            catch(Exception ex)
            {
                ErrorLog.Log(Constant.ERROR_SYNC_REQUEST + Constant.ACTUAL_ERROR + ex.Message);
                throw new Exception(Constant.ERROR_SYNC_REQUEST);
            }
        }

        #endregion
    }
}
