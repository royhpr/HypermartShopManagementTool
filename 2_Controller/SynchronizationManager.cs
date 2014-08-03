using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypermarket_Shop_Management_Tool._2_Controller
{
    class SynchronizationManager
    {
        private static SynchronizationManager instance;
        private _1_Model.DBManager DBController = _1_Model.DBManager.getInstance();

        private SynchronizationManager()
        {
        }

        public static SynchronizationManager getInstance()
        {
            if (instance == null)
            {
                instance = new SynchronizationManager();
            }
            return instance;
        }

        public void SyncInventory()
        {
            try
            {
                DBController.SyncInventory();
            }
            catch
            {
                throw;
            }
        }

        public void SyncTransaction()
        {
            try
            {
                DBController.SyncTransaction();
            }
            catch
            {
                throw;
            }
        }

        public void SyncRequest()
        {
            try
            {
                DBController.SyncRequest();
            }
            catch
            {
                throw;
            }
        }
    }
}
