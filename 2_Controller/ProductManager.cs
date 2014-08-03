using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/****************************************
 *The class is responsible for managing product list and manufacturer info
 *
 * Methods:
 * public void AddProduct(string productID, string manufacturerName, string name, string category, bool perishable)
 * public void DeleteProduct(string productID, string manufacturerName )
 * public void UpdateProduct(string productID, string manufacturerName, string name, string category, bool perishable)
 * public void AddManufacturer(string manufacturerName, string location, string country, bool contact)
 * public void DeleteManufacturer(string manufacturerName)
 * public void UpdateManufacturer(string manufacturerName, string country, string location, string contact)
 * public string GenerateNextAvailableProductID()
 * public DataTable FetchProduct()
 * public DataTable FetchManufacturer() 
 * 
 * 
 * @Auther:
 * @Date:
 * *****************************************/

namespace Hypermarket_Shop_Management_Tool._2_Controller
{
    class ProductManager
    {
        private static ProductManager instance;

        private _1_Model.DBManager DBController = _1_Model.DBManager.getInstance();

        private ProductManager()
        {
        }

        public static ProductManager getInstance()
        {
            if (instance == null)
            {
                instance = new ProductManager();
            }
            return instance;
        }

        public DataTable FetchProduct()
        {
            try
            {
                return DBController.FetchProduct();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataTable FetchManufacturer()
        {
            try
            {
                return DBController.FetchManufacturer();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
