using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Hypermarket_Shop_Management_Tool._1_Model
{
    class DBConnectionManager
    {
        public static DBConnectionManager DBConn;
        private string LocalConnectionString;
        private string HQConnectionString;
        private _2_Controller.SecurityManager SecurityProtector = _2_Controller.SecurityManager.getInstance();

        private DBConnectionManager()
        {
            SetConnectionString();
        }

        private void SetConnectionString()
        {
            try
            {
                string localEncryptedString = ConfigurationManager.ConnectionStrings["HypermarketLocal.Properties.Settings.HypermartLocalConnectionString"].ConnectionString;
                LocalConnectionString = SecurityProtector.Decrypt(localEncryptedString);

                string HQEncryptedString = ConfigurationManager.ConnectionStrings["HypermarketHQ.Properties.Settings.HypermartHQConnectionString"].ConnectionString;
                HQConnectionString = SecurityProtector.Decrypt(HQEncryptedString);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DBConnectionManager getInstance()
        {
            if (DBConn == null)
            {
                DBConn = new DBConnectionManager();
            }
            return DBConn;
        }

        public string getLocalConnectionString()
        {
            return LocalConnectionString;
        }

        public string getHQConnectionString()
        {
            return HQConnectionString;
        }
    }
}
