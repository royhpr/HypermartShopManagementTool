using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace Hypermarket_Shop_Management_Tool._2_Controller
{
    class SalesManager
    {
        private static SalesManager instance;

        private _1_Model.DBManager DBController = _1_Model.DBManager.getInstance();

        private SalesManager()
        {
        }

        public static SalesManager getInstance()
        {
            if (instance == null)
            {
                instance = new SalesManager();
            }
            return instance;
        }

        public DataTable FetchTransaction()
        {
            try
            {
                return DBController.FetchTransaction();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool IsValidUserForSendingReport(string userID, string password)
        {
            try
            {
                return DBController.IsValidUserForSendingReport(userID, password);
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
               DBController.SubmitReport(productID, reportDate, unitSold, costPrice, sellingPrice, revenue, staffID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable FetchReport()
        {
            try
            {
                return DBController.FetchReport();
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
                return DBController.IsReportSubmittedBefore(reportDate);
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
                DBController.RemoveReportFromHQ(reportDate);
            }
            catch
            {
                throw;
            }
        }


        #region Discount

        public DataTable FetchDiscount()
        {
            try
            {
                return DBController.FetchDiscountPolicy();
            }
            catch
            {
                throw;
            }
        }


        #endregion

    }
}
