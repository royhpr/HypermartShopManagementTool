using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

/****************************************
 *The class is responsible for managing stock and approving or rejecting request from outlets
 *
 * Methods:
 * 
 * @Author: Huang Purong
 * @Date: 09/11/2013
 * *****************************************/

namespace Hypermarket_Shop_Management_Tool._2_Controller
{
    class WarehouseManager
    {
        private static WarehouseManager instance;
        private _1_Model.DBManager DBController = _1_Model.DBManager.getInstance();
        private DataTable cashierRegisterList;
        private DataTable priceTagList;

        private WarehouseManager()
        {
        }

        public static WarehouseManager getInstance()
        {
            if (instance == null)
            {
                instance = new WarehouseManager();
            }
            return instance;
        }

        #region Stock

        public void AddStock(string prodcutName, string manufacturerName, string productID, DateTime batchID, 
                            string importPrice, string sellPrice, DateTime expireDate, string quantity)
        {
            try
            {
                DBController.AddStock(prodcutName, manufacturerName, productID, batchID, importPrice, sellPrice, expireDate, quantity, true);
            }
            catch
            {
                throw;
            }
        }

        public void UpdateStock(string productID, DateTime batchID, string importPrice, string sellPrice, DateTime expireDate, string quantity)
        {
            try
            {
                DBController.UpdateStock(productID, batchID, importPrice, sellPrice, expireDate, quantity);
            }
            catch
            {
                throw;
            }  
        }

        public DataTable FetchStock()
        {
            try
            {
                return DBController.FetchStock();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateStockQuantity(string productID, DateTime batchID, string quantity)
        {
            try
            {
                DBController.UpdateStockQuantity(productID, batchID, quantity);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Cashier Register

        public DataTable FetchCashierRegister()
        {
            try
            {
                return DBController.FetchCashierRegister();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateCashierRegisterStatus(string ID, string status)
        {
            try
            {
                DBController.UpdateCashierRegisterStatus(ID, status);
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
                DBController.AddCashierRegister(ID, status);
            }
            catch
            {
                throw;
            }
        }

        public void DeleteCashierRegister(string ID)
        {
            try
            {
                DBController.DeleteCashierRegister(ID);
            }
            catch
            {
                throw;
            }
        }

        public string GenerateNextAvailableCashierRegisterID()
        {
            try
            {
                cashierRegisterList = DBController.FetchCashierRegister();
                return RetriveNewCashierRegisterID().PadLeft(Constant.CASHIER_REGISTER_ID_LENGTH, Constant.LEFT_PAD_CHAR);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string RetriveNewCashierRegisterID()
        {
            List<int> cashierIDList = cashierRegisterList.AsEnumerable().Select(al => al.Field<string>(Constant.CASHIER_ID)).Select(int.Parse).ToList();
            if (cashierIDList.Count == 0)
            {
                return Convert.ToString(1);
            }
            else
            {
                return GetAvailableCashierRegisterID(cashierIDList);
            }
        }

        private static string GetAvailableCashierRegisterID(List<int> cashierIDList)
        {
            cashierIDList.Sort();
            for (int i = 0; i < cashierIDList.Count; i++)
            {
                if (i != cashierIDList[i])
                {
                    return Convert.ToString(i);
                }
            }
            int cashierID = cashierIDList.Max() + 1;
            return Convert.ToString(cashierID);
        }

        #endregion

        #region Price Tag

        public DataTable FetchPriceTag()
        {
            try
            {
                return DBController.FetchPriceTag();
            }
            catch
            {
                throw;
            }
        }

        public void UpdatePriceTag(string ID, string status, string productID, DateTime batch)
        {
            try
            {
                DBController.UpdatePriceTag(ID, status, productID, batch);
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
                DBController.AddPriceTag(ID, status, productID, batch);
            }
            catch
            {
                throw;
            }
        }

        public void DeletePriceTag(string ID)
        {
            try
            {
                DBController.DeletePriceTag(ID);
            }
            catch
            {
                throw;
            }
        }

        public string GenerateNextAvailablePriceTagID()
        {
            try
            {
                priceTagList = DBController.FetchPriceTag();
                return RetriveNewPriceTagID().PadLeft(Constant.CASHIER_REGISTER_ID_LENGTH, Constant.LEFT_PAD_CHAR);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string RetriveNewPriceTagID()
        {
            List<int> priceTagIDList = priceTagList.AsEnumerable().Select(al => al.Field<string>(Constant.CASHIER_ID)).Select(int.Parse).ToList();
            if (priceTagIDList.Count == 0)
            {
                return Convert.ToString(1);
            }
            else
            {
                return GetAvailablePriceTagID(priceTagIDList);
            }
        }

        private static string GetAvailablePriceTagID(List<int> priceTagIDList)
        {
            priceTagIDList.Sort();
            for (int i = 0; i < priceTagIDList.Count; i++)
            {
                if (i != priceTagIDList[i])
                {
                    return Convert.ToString(i);
                }
            }
            int priceTagID = priceTagIDList.Max() + 1;
            return Convert.ToString(priceTagID);
        }

        #endregion

        #region Request

        public DataTable FetchPendingRequest()
        {
            return DBController.FetchPendingRequest();
        }

        public DataTable FetchApprovedRequest()
        {
            return DBController.FetchApprovedRequest();
        }

        public DataTable FetchRejectedRequest()
        {
            return DBController.FetchRejectedRequest();
        }

        public DataTable FetchIncomingRequest()
        {
            return DBController.FetchIncomingRequest();
        }

        public void AddPendingRequest(string productID, string manufacturer, string productName, DateTime requestDate, string quantity, bool urgency)
        {
            try
            {
                DBController.AddPendingRequest(productID, manufacturer, productName, requestDate, quantity, urgency);
            }
            catch
            {
                throw;
            }
        }

        public void UpdateIncomingStock(string productID, DateTime batchID, DateTime confirmDate, DateTime receiveDate, bool receiveStatus)
        {
            try
            {
                DBController.UpdateIncomingStock(productID, batchID, confirmDate, receiveDate, receiveStatus);
            }
            catch
            {
                throw;
            }
        }


        #endregion
    }
}
