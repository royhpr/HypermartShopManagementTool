using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypermarket_Shop_Management_Tool
{
    public enum ActionType { Add, Edit };
    class Constant
    {
        public const string DEFAULT_PASSWORD_ENCRYPTED = "D1qU/ibus6/wqtrUj1cWpA==";
        public const string DEFAULT_PASSWORD_NORMAL = "12345678";
        public const int CONSTANT_ZERO = 0;
        public const int PASSWORD_LIMITED_LENGTH = 8;
        public const int PRODUCT_ID_LENGTH = 8;
        public const int STAFF_ID_LENGTH = 4;
        public const int SHOP_ID_LENGTH = 4;
        public const int CASHIER_REGISTER_ID_LENGTH = 4;
        public const char LEFT_PAD_CHAR = '0';
        public const string SEARCH_HINT = "type to search in any field...";
        public const string CONVERT_TO = "Convert(";
        public const string SYSTEM_STRING = ", 'System.String')";
        public const string NO_ROW_SELECTED = "Please select at least one row";
        public const string DELETE_SUCCESS = " has been successfully removed from database\nPlease wait...";
        public const string ERROR_DURING_POPULATING_LIST = "Error in retrieving data";
        public const string FULL_STOP = ".";

        /*****************All About Discount Table************************/
        public const string DISCOUNT_TABLE = "discount";
        //Columns
        public const string D_M_NAME = "ManufacturerName";
        public const string D_P_NAME = "ProductName";
        public const string D_P_ID = "ProductID";
        public const string D_EFFECTIVE_DATE = "EffectiveDate";
        public const string D_BUNDLE_UNIT = "BundleUnit";
        public const string D_FREE_ITEM_QUANTITY = "FreeItemQuantity";
        public const string D_DISCOUNT_PERCENTAGE = "Discount";
        /*****************End Discount Table*******************************/

        /*****************All About Manufacturer Table********************/
        public const string MANUFACTURER_TABLE = "manufacturer";
        //Columns
        public const string M_NAME = "ManufacturerName";
        public const string M_COUNTRY = "Country";
        public const string M_ADDRESS = "Address";
        public const string M_CONTACT = "Contact";
        /******************End Manufacturer Table**************************/

        /******************All About Product Table************************/
        public const string PRODUCT_TABLE = "product";
        //Columns
        public const string P_M_Name = "ManufacturerName";
        public const string P_ID = "ProductID";
        public const string P_NAME = "Name";
        public const string P_CATEGORY = "Category";
        public const string P_MAX = "MaximumStock";
        public const string P_MIN = "MinimumStock";
        /*****************End Product Table********************************/


        /******************All About Report Table************************/
        public const string REPORT_TABLE = "report";
        //Columns
        public const string R_S_COUNTRY = "Country";
        public const string R_S_LOCATION = "Location";
        public const string R_S_ID = "ShopId";
        public const string R_REPORT_DATE = "ReportDate";
        public const string R_TOTAL_PRICE = "TotalPrice";
        public const string R_TOTAL_SALE = "TotalSales";
        public const string R_SALE_AMOUNT = "Amount";

        public const string REPORT_USER_ID = "@StaffID";
        public const string REPORT_PASSWORD = "@Password";
        public const string REPORT_USER_POSITION = "@Position";
        public const string REPORT_DATE = "@ReportDate";
        public const string REPORT_TOTAL_COST_PRICE = "@CostPrice";
        public const string REPORT_TOTAL_SELLING_PRICE = "@SellingPrice";
        public const string REPORT_TOTAL_UNIT_SOLD = "@UnitSold";
        public const string REPORT_TOTAL_REVENUE = "@Revenue";
        public const string REPORT_AUTHORISED_POSITION = "Shop Manager";
        public const string REPORT_VERIFY_USER = "SELECT COUNT(*) FROM Staff WHERE StaffID=@StaffID AND Password=@Password AND Position=@Position";
        public const string REPORT_SUBMIT = "INSERT INTO Report VALUES (@ShopID, @ProductID, @ReportDate, @UnitSold, @CostPrice, @SellingPrice, @Revenue, @StaffID)";

        public const string ERROR_REPORT_VERIFY = "System error: error occurred while\n verifying the shop info";
        public const string ERROR_REPORT_SUBMIT = "System error: error occurred while\n submitting the report to HQ";
        public const string ERROR_REPORT_SHOP_NOT_FOUND = "Shop is not found in HQ Database! Please retry!";
        public const string ERROR_REPORT_FETCH = "System error: error occurred while\n fetching report details";
        public const string ERROR_CHECK_REPORT = "System error: error occurred while querying for HQ database";
        public const string REPORT_SUMITTED_BEFORE = "Report has been submitted before.\n Do you want to overwrite it?";
        public const string REPORT_SUMITTED_SUCCESSFULLY = "Submitted Successfully!";

        public const string REPORT_SHOP_INFO_FILE = "shopinfo.txt";

        public const string REPORT_FETCH_BY_DATE = "SELECT SUM(t.CostPrice * t.UnitSold), SUM(t.SellingPrice * (100 - t.Discount) * t.UnitSold / 100), SUM(t.UnitSold) FROM view_transactions t WHERE DATEPART(d,TransactionDate)=DATEPART(d,@ReportDate) AND DATEPART(m,TransactionDate)=DATEPART(m,@ReportDate) AND DATEPART(y,TransactionDate)=DATEPART(y,@ReportDate)";
        public const string REPORT_EXIST = "SELECT COUNT(*) FROM Report WHERE ShopID=@ShopID AND ReportDate=@ReportDate";
        public const string REPORT_REMOVE = "DELETE FROM Report WHERE ShopID=@ShopID AND ReportDate=@ReportDate";
        
        /*****************End Report Table********************************/


        /******************All About Request Table************************/
        public const string REQUEST_TABLE = "request";
        //Columns
        public const string RE_P_NAME = "ProductName";
        public const string RE_M_NAME = "Manufacturer";
        public const string RE_P_ID = "ProductID";
        public const string RE_S_ID = "ShopID";
        public const string RE_S_COUNTRY = "ShopCountry";
        public const string RE_S_LOCATION = "ShopLocation";
        public const string RE_REQUEST_DATE = "RequestDate";
        public const string RE_RECEIVE_DATE = "ReceiveDate";
        public const string RE_SEND_DATE = "SendDate";
        public const string RE_APPROVED_DATE = "ApprovedDate";
        public const string RE_REJECTED_DATE = "RejectedDate";
        public const string RE_QUANTITY = "Quantity";
        public const string RE_REQUESTED_QUANTITY = "RequestedQuantity";
        public const string RE_INCOMING_QUANTITY = "IncomingQuantity";
        public const string RE_APPROVED_QUANTITY = "ApprovedQuantity";
        public const string RE_COMMENT = "Comment";
        public const string RE_URGENCY = "Urgency";
        public const string RE_BATCH_ID = "BatchID";
        public const string RE_RECEIVE_STATUS = "ReceiveStatus";
        /*****************End Request Table********************************/


        /******************All About RequestApproveStatus Table************************/
        public const string REQUEST_APPROVE_STATUS_TABLE = "requestapprovestatus";
        public const string PENDING_REQUEST_TABLE = "pendingRequest";
        public const string APPROVED_REQUEST_TABLE = "approvedRequest";
        public const string REJECTED_REQUEST_TABLE = "rejectedRequest";
        //Columns
        public const string RE_AP_M_Name = "ManufacturerName";
        public const string RE_AP_P_Name = "ProductName";
        public const string RE_AP_P_ID = "ProductID";
        public const string RE_AP_S_ID = "ShopID";
        public const string RE_AP_S_COUNTRY = "ShopCountry";
        public const string RE_AP_S_LOCATION = "Location";
        public const string RE_AP_REQUEST_DATE = "RequestDate";
        public const string RE_AP_STAFF_ID = "StaffID";
        public const string RE_AP_APPROVED = "Approved";
        public const string RE_AP_APPROVED_DATE = "ApprovedDate";
        public const string RE_AP_REJECTED_DATE = "RejectedDate";
        public const string RE_AP_SEND_DATE = "SendDate";
        public const string RE_AP_QUANTITY = "Quantity";
        public const string RE_AP_URGENCY = "Urgency";
        public const string RE_AP_DELEVERY_STATUS = "DeliveryStatus";
        /*****************End RequestApproveStatus Table********************************/


        /******************All About Shop Table************************/
        public const string SHOP_TABLE = "shop";
        //Columns
        public const string S_ID = "ShopID";
        public const string S_COUNTRY = "Country";
        public const string S_LOCATION = "Location";
        public const string S_CONTACT = "Contact";
        /*****************End Shop Table********************************/


        /******************All About Stafff Table************************/
        public const string STAFF_TABLE = "staff";
        //Columns
        public const string STAFF_ID = "StaffID";
        public const string EMAIL = "Email";
        public const string STAFF_NAME = "Name";
        public const string STAFF_PASSWORD = "Password";
        public const string STAFF_RENEWED_PASSWORD_DATE = "RenewPasswordDate";
        public const string STAFF_DATE_OF_BIRTH = "DateOfBirth";
        public const string STAFF_JOIN_DATE = "JoinDate";
        public const string STAFF_GENDER = "Gender";
        public const string STAFF_POSITION = "Position";
        public const string STAFF_CONTACT = "Contact";
        public const string STAFF_IS_DEFAULT_PASSWORD = "DefaultPassword";
        public const string MALE = "Male";
        public const string STAFF_BLOCKED = "Blocked";
        public const string FEMALE = "Female";
        public const string MR = " (Mr) ";
        public const string MS = " (Ms) ";
        public const string SPACE = " ";
        public const string COMMA = ",";
        public const string GREETING0 = "Welcome";
        public const string GREETING1 = " your password will be expired after ";
        public const string DAYS = " day(s)";
        /*****************End Staff Table********************************/

        /******************All About Stock Table************************/
        public const string STOCK_TABLE = "stock";
        //Columns
        public const string STOCK_M_NAME = "ManufacturerName";
        public const string STOCK_P_NAME = "ProductName";
        public const string STOCK_P_ID = "ProductID";
        public const string STOCK_BATCH_ID = "BatchID";
        public const string STOCK_IMPORT_PRICE = "ImportPrice";
        public const string STOCK_SELL_PRICE = "SellingPrice";
        public const string STOCK_EXPIRE_DATE = "ExpireDate";
        public const string STOCK_QUANTITY = "Quantity";
        public const string STOCK_DEFAULT_DISCOUNT_POLICY = "DefaultDiscountPolicy";

        public const string STOCK_REQUEST_FOR_DETAIL = "SELECT st.ImportPrice, st.SellingPrice, st.ExpireDate FROM stock st WHERE st.ProductID=@ProductID AND st.BatchID=@BatchID";
        public const string ERROR_OCCURR_DURING_UPDATE_STOCK = "error occurred during updating stock quantity.";
        /*****************End Stock Table********************************/

        /*****************All About Transaction Table********************/
        public const string TRANSACTION_TABLE = "transaction";

        public const string TRANSACTION_ID = "TransactionID";
        public const string TRANSACTION_CASHIER_ID = "CashierID";
        public const string TRANSACTION_MACHINE_ID = "MachineID";
        public const string TRANSACTION_DATE = "TransactionDate";
        public const string TRANSACTION_PRODUCT_ID = "ProductID";
        public const string TRANSACTION_BATCH_ID = "BatchID";
        public const string TRANSACTION_COST_PRICE = "CostPrice";
        public const string TRANSACTION_SELLING_PRICE = "SellingPrice";
        public const string TRANSACTION_DISCOUNT = "Discount";
        public const string TRANSACTION_UNIT_SOLD = "UnitSold";
        /*****************End Transaction Table***************************/

        /*****************All About Cashier Register Table**************************/
        public const string CASHIER_REGISTER_TABLE = "cashierregister";

        public const string CASHIER_ID = "ID";
        public const string CASHIER_STATUS = "Status";

        public const string STATUS_ACTIVATED = "Activated";
        public const string STATUS_DEACTIVATED = "Deactivated";

        public const string ERROR_CASHIER_FETCH = "System error: error occurred while fetching cashier register list.";
        public const string ERROR_CASHIER_ADD = "System error: error occurred while adding new cashier register.";
        public const string ERROR_CASHIER_UPDATE = "System error: error occurred while updating the cashier register.";
        public const string ERROR_CASHIER_DELETE = "System error: error occurred while deleting the cashier register ";
        public const string ERROR_NEXT_ACTION = " \nPlease try again later. \nIf error persists, please contact system admin.";
        /*****************End Cashier Register Table********************************/

        /****************All About Price Tag Table********************/
        public const string PRICE_TAG_TABLE = "pricetag";

        public const string PRICE_TAG_ID = "ID";
        public const string PRICE_TAG_STATUS = "Status";
        public const string PRICE_TAG_P_ID = "ProductID";
        public const string PRICE_TAG_B_ID = "BatchID";

        public const string ERROR_PRICE_TAG_FETCH = "System error: error occurred while fetching price tag list.";
        public const string ERROR_PRICE_TAG_ADD = "System error: error occurred while adding new price tag.";
        public const string ERROR_PRICE_TAG_UPDATE = "System error: error occurred while updating the price tag.";
        public const string ERROR_PRICE_TAG_DELETE = "System error: error occurred while deleting the price tag ";
        public const string ERROR_EMPTY_PRODUCT_NAME = "Please select one product from drop down list.";
        public const string ERROR_EMPTY_MANUFACTURER = "Please select one manufacturer from drop down list.";
        public const string ERROR_EMPTY_BATCH = "Please select on batch from drop down list.";
        /****************End Price Tag Table**************************/

        /******************All About Incoming Stock Table********************/
        public const string INCOMING_REQUEST_TABLE = "incomingRequest";

        public const string INCOMING_REQUEST_CONFIRM_DATE = "ConfirmDate";
        public const string INCOMING_REQUEST_RECEIVE_DATE = "ReceiveDate";
        public const string INCOMING_REQUEST_RECEIVE_STATUS = "ReceiveStatus";

        public const string ERROR_UPDATE_INCOMING_STOCK_STATUS = "system error: error occurred while updating incoming stock status";
        public const string ERROR_ADD_PENDING_REQUEST = "system error: error occurred while making new request";
        public const string ERROR_EMPTY_SHOP_ID = "Please indicate shop ID in the text field.";
        public const string ERROR_EMPTY_REQUESTED_QUANTITY = "Please indicate request quantity in the text field.";
        public const string ERROR_NEGATIVE_QUANTITY = "Negative/zero number is not allowed for quantity.";
        /******************End Send Stock Table***************************/

        /******************All About Synchronization***********************/
        public const string ERROR_SYNC_INVENTORY = "System error: error occurred while synchronizing\n inventory list";
        public const string ERROR_SYNC_TRANSACTION = "System error: error occurred while synchronizing transaction";
        public const string ERROR_SYNC_REQUEST = "System error: error occurred while synchronizing request";
        public const string SYNC_SUCCESSFULLY = "Transaction list has been successfully updated!";
        public const string SYNC_REQUEST_SUCCESSFULLY = "Request has been successfully synchronized!";
        /******************End Synchronization*****************************/

        /*****************Select Commands*************************************/
        public const string SHOP_TABLE_SELECT_COMMAND = "SELECT * FROM shop";
        public const string PRODUCT_TABLE_SELECT_COMMAND = "SELECT * FROM product";
        public const string STOCK_TABLE_SELECT_COMMAND = "SELECT * FROM view_stock";
        public const string MANUFACTURER_TABLE_SELECT_COMMAND = "SELECT * FROM manufacturer";
        public const string STAFF_TABLE_SELECT_COMMAND = "SELECT * FROM staff";
        public const string DISCOUNT_TABLE_SELECT_COMMAND = "SELECT * FROM view_discount";
        public const string SEND_STOCK_TABLE_SELECT_COMMAND = "SELECT * FROM view_send_stock";
        public const string TRANSACTION_TABLE_SELECT_COMMAND = "SELECT * FROM view_transactions";
        public const string CASHIER_REGISTER_TABLE_SELECT_COMMAND = "SELECT * FROM CashierRegister";
        public const string PRICE_TAG_TABLE_SELECT_COMMAND = "SELECT * FROM PriceTag";
        public const string REPORT_TABLE_SELECT_COMMAND = "SELECT * FROM view_report";
        public const string PENDING_REQUEST_SELECT_COMMAND = "SELECT * FROM view_shop_pending_request('{0}')";
        public const string APPROVED_REQUEST_SELECT_COMMAND = "SELECT * FROM view_shop_approved_request('{0}')";
        public const string REJECTED_REQUEST_SELECT_COMMAND = "SELECT * FROM view_shop_rejected_request('{0}')";
        public const string INCOMMING_REQUEST_SELECT_COMMAND = "SELECT * FROM view_shop_incoming_stock('{0}')";
        /********************End Select Commands******************************/

        /*****************Update Commands****************************************/
        public const string UPDATE_STOCK_COMMAND = "UPDATE stock SET ImportPrice=@ImportPrice, SellingPrice=@SellingPrice, ExpireDate=@ExpireDate, Quantity=@Quantity WHERE ProductID=@ProductID AND BatchID=@BatchID";
        public const string UPDATE_INCOMING_STOCK_STATUS_COMMAND = "UPDATE SendStock SET ReceiveDate=@ReceiveDate, ReceiveStatus=@ReceiveStatus WHERE ProductID=@ProductID AND BatchID=@BatchID AND ShopID=@ShopID AND ConfirmDate=@ConfirmDate";
        public const string UPDATE_STOCK_QUANTITY = "UPDATE stock SET Quantity=@Quantity WHERE ProductID=@ProductID AND BatchID=@BatchID";
        /******************End Update Commands************************************/

        /******************Add Commands*******************************************/
        public const string ADD_STOCK_COMMAND = "INSERT INTO stock VALUES (@ProductID, @BatchID, @ImportPrice, @SellingPrice, @ExpireDate, @Quantity, @DefaultDiscount)";
        public const string ADD_SEND_STOCK_COMMAND = "INSERT INTO SendStock (ProductID, BatchID, ShopID, ConfirmDate, DeliveryStatus, ReceiveStatus, Quantity) VALUES (@ProductID, @BatchID, @ShopID, @ConfirmDate, @DeliveryStatus, @ReceiveStatus, @Quantity)";
        public const string ADD_PENDING_REQUEST_COMMAND = "INSERT INTO Request VALUES (@ProductID, @ShopID, @RequestDate, @Quantity, @Urgency)";
        /*********************End Add Commands*************************************/

        /*************************Delete Commands***********************************/
        public const string DELETE_DISCOUNT_COMMAND = "DELETE FROM discount WHERE ProductID=@ProductID AND BatchID=@BatchID AND EffectiveDate=@EffectiveDate";
        public const string DELETE_PENDING_REQUEST_COMMAND = "DELETE FROM request WHERE ProductID=@ProductID AND ShopID=@ShopID AND RequestDate=@RequestDate";
        public const string DELETE_REJECTED_REQUEST_COMMAND = "DELETE FROM requestapprovestatus WHERE ProductID=@ProductID AND ShopID=@ShopID AND RequestDate=@RequestDate AND StaffID=@StaffID";
        public const string DELETE_SEND_STOCK_COMMAND = "DELETE FROM send_stock WHERE ProductID=@ProductID AND BatchID=@BatchID AND ShopID=@ShopID";
        public const string REQUEST_STATUS_SWITCH_COMMAND = "UPDATE RequestApproveStatus SET Approved=@Approved, ActionDate=@ActionDate, WHERE Product=@ProductID AND ShopID=@ShopID AND RequestDate=@RequestDate AND StaffID=@StaffID";
        /******************************End Delete Commands*****************************/

        /***********************************Parameters*********************************/
        public const string IMPORT_PRICE = "@ImportPrice";
        public const string SELL_PRICE = "@SellingPrice";
        public const string EXPIRE_DATE = "@ExpireDate";
        public const string PRODUCT_ID = "@ProductID";
        public const string BATCH_ID = "@BatchID";
        public const string A = "@A";
        public const string B = "@B";
        public const string SD = "@SD";
        public const string ED = "@ED";
        public const string APPROVED = "@Approved";
        public const string QUANTITY = "@Quantity";
        public const string APPROVED_QUANTITY = "@ApprovedQuantity";
        public const string DEFAULT_DISCOUNT = "@DefaultDiscount";
        public const string URGENCY = "@Urgency";
        public const string SHOP_ID = "@ShopID";
        public const string CONFIRM_DATE = "@ConfirmDate";
        public const string REQUEST_DATE = "@RequestDate";
        public const string ACTION_DATE = "@ActionDate";
        public const string COMMENTS = "@Comments";
        public const string SEND_DATE = "@SendDate";
        public const string DELIVERY_STATUS = "@DeliveryStatus";
        public const string RECEIVE_STATUS = "@ReceiveStatus";
        public const string RECEIVE_DATE = "@ReceiveDate";
        public const string EFFECTIVE_DATE = "@EffectiveDate";
        /************************************End Parameters*****************************/

        /******************All About AccountManager***************************/
        public const int PASSWORD_VALID_DAYS = 180;
        /************************End AccountManager***************************/

        /******************Error Message**************************************/
        public const string ACTUAL_ERROR = "\n\nActuall Error: ";
        public const string ERROR_FETCH = "system error: error occurred while trying to fetch ";
        public const string ERROR_ADD = "system error: error occurred while trying to add ";
        public const string ERROR_DELETE = "system error: error occurred while trying to delete ";
        public const string ERROR_UPDATE = "system error: error occurred while trying to update ";
        public const string ERROR_CHANGE_STATUS = "system error: error occurred while changing request status ";
        public const string ERROR_VERIFYING_USER = "system error: error occurred while verifying  staff ID ";
        public const string ERROR_VERIFYING_PASSWORD = "system error: error occurred while verifying password ";
        public const string ERROR_CHECK_DEFAULT_PASSWORD = "system error: error occurred while checking default password ";
        public const string ERROR_UPDATE_PASSWORD = "system error: error occurred while updating password ";
        public const string ERROR_GENERATE_SHOP_ID = "system error: error occurred while generating shop ID";
        public const string ERROR_GENERATE_PRODUCT_ID = "system error: error occurred while generating product ID";
        public const string ERROR_SEARCH_VERIFY_CONTENT = "system error: error occurred during verify textbox content";
        public const string ERROR_SEARCH = "system error: error occurred while search the list";
        public const string ERROR_GENERATE_NEXT_STAFF_ID = "system error: error occurred while generating next staff ID. \nPlease try again later. \nIf error persists, please contact system admin.";
        public const string ERROR_GENERATE_NEXT_CASHIER_ID = "system error: error occurred while generating next cashier register ID. \nPlease try again later. \nIf error persists, please contact system admin.";
        public const string ERROR_GENERATE_NEXT_PRICE_TAG_ID = "system error: error occurred while generating next price tag ID. \nPlease try again later. \nIf error persists, please contact system admin.";

        public const string ERROR_UPDATE_DB = "system error: error occurred while updating shop database, Please retry in a minute";
        /******************End Error Message***********************************/


        /********************Login***************************/
        public const string ERROR_INVALID_USER_ID = "Invalid User ID";
        public const string ERROR_WRONG_PASSWORD = "Wrong Password";
        public const string ERROR_EMPTY_USER_ID = "No Empty or white spaces allowed in ID";
        public const string ERROR_EMPTY_PASSWORD = "No Empty or white spaces allowed in psw";
        public const string ERROR_EMPTY_NEW_PASSWORD = "Empty or white spaces are disallowed";
        public const string ERROR_EMPTY_REENTER_PASSWORD = "Please re-enter above new password";
        public const string ERROR_PASSWORD_MISMATCH = "Above passwords are different";
        public const string ERROR_PASSWORD_LENGTH = "Password length must be at least 8,\n with alphanumeric";
        /********************End Login***********************/

        /********************Account*************************/
        public const int RENEW_PASSWORD_DAYS = 180;
        /********************End Account*********************/
        /********************Staff Position******************/
        public const string ADMIN = "Admin";
        public const string PRODUCT_MANAGER = "Product Manager";
        public const string SALES_MANAGER = "Sales Manager";
        public const string WAREHOUSE_MANAGER = "Warehouse Manager";
        public const string PRODUCT_OFFICER = "Product Officer";
        public const string SALES_OFFICER = "Sales Officer";
        public const string WAREHOUSE_OFFICER = "Warehouse Officer";
        /*******************End Staff Postion****************/

        /******************Operation interface****************/
        public const string ERROR_REMOVE_TABPAGE = "system error: error occurred while removing tabpages";
        public const string OPERATION_TAB_PRODUCT = "tbViewProducts";
        public const string OPERATION_TAB_STOCK = "tbManageStock";
        public const string OPERATION_TAB_REPORT = "tbSendReport";
        public const string OPERATION_TAB_ORDER = "tbStockRequest";
        public const string OPERATION_TAB_MANUFACTURER = "tbViewManufacturer";
        public const string OPERATION_TAB_STAFF = "tbViewStaff";
        public const string OPERATION_TAB_DISCOUNT = "tbViewDiscountPolicy";
        public const string OPERATION_TAB_PROFILE = "tbProfile";
        public const string OPERATION_TAB_TRANSACTION = "tbViewTransaction";
        public const string OPERATION_TAB_CASHIER = "tbManageMachines";

        public const string OPERATION_LIKE = " LIKE ";
        public const string OPERATION_OR = " OR ";
        public const string OPERATION_PERCENT = "%";
        public const string OPERATION_EMPTY = "";
        public const string OPERATION_SINGLE_QUOTE = "'";
        public const string OPERATION_DOUBLE_SINGLE_QUOTE = "''";
        public const int OPERATION_INDEX_ZERO = 0;
        public const int OPERATION_INDEX_ONE = 1;
        public const int OPERATION_INDEX_TWO = 2;
        public const int OPERATION_INDEX_THREE = 3;
        public const int OPERATION_INDEX_FOUR = 4;
        public const int OPERATION_INDEX_FIVE = 5;
        public const int OPERATION_INDEX_SIX = 6;
        public const int OPERATION_INDEX_SEVEN = 7;
        public const int OPERATION_INDEX_EIGHT = 8;
        public const int OPERATION_INDEX_NINE = 9;
        public const int OPERATION_INDEX_TEN = 10;
        public const int OPERATION_INDEX_ELEVEN = 11;

        public const string OPERATION_UNSELECT_ALL = "Unselect All";
        public const string OPERATION_SELECT_ALL = "Select All";
        /******************End Operation interface************/

        /******************Error in add shop******************/
        public const string ERROR_ADD_SHOP_EMPTY_COUNTRY = "Country can not be empty";
        public const string ERROR_ADD_SHOP_EMPTY_LOCATION = "Location can not be empty";
        public const string ERROR_ADD_SHOP_EMPTY_CONTACT = "Contact can not be empty";
        public const string ERROR_ADD_SHOP_WRONG_CONTACT = "Contact must be numeric";
        /******************End error in add shop**************/

        /******************Error in add product**************/
        public const string ERROR_ADD_PRODUCT_EMPTY_MANUFACTURER = "Manufacturer can not be empty";
        public const string ERROR_ADD_PRODUCT_EMPTY_CATEGORY = "Category can not be empty";
        public const string ERROR_ADD_PRODUCT_EMPTY_NAME = "Product name can not be empty";
        public const string ERROR_ADD_PRODUCT_EMPTY_MAXSTOCK = "Maximum stock can not be empty";
        public const string ERROR_ADD_PRODUCT_EMPTY_MINSTOCK = "Minimum stock can not be empty";
        public const string ERROR_ADD_PRODCUT_MANUFACTURER_NO_EXIST = "Could not find manufacturer in current database.\nPlease add new manufacturer or select one from list";
        public const string ERROR_ADD_PRODUCT_CATEGORY_NO_EXIST = "Please select one category from dropdown list";
        public const string ERROR_ADD_PRODUCT_SAME_NAME_SAME_MANUFACTURER = "Same product name detected under current maufacturer. \nPlease specify another name!";
        public const string ERROR_ADD_PRODUCT_MAXSTOCK = "Maximum stock must be numeric";
        public const string ERROR_ADD_PRODUCT_MINSTOCK = "Minimum stock must be numeric";
        public const string ERROR_ADD_PRODUCT_MAXSTOCK_COMPARE_MINSTOCK = "Maximum stock must be greater than minimum stock";
        public const string ERROR_ADD_PRODUCT_MINSTOCK_LESS_THAN_ZERO = "Minimum stock must be greater than zero";

        /*******************End add product******************/

        /*******************Error in add manufacturer**********/
        public const string ERROR_ADD_MANUFACTURER_EMPTEY_MANUFACTURER_NAME = "Name can not be empty";
        public const string ERROR_ADD_MANUFACTURER_EMPTEY_ADDRESS = "Address can not be empty";
        public const string ERROR_ADD_MANUFACTURER_EMPTEY_COUNTRY = "Country can not be empty";
        public const string ERROR_ADD_MANUFACTURER_EMPTEY_CONTACT = "Contact can not be empty";
        public const string ERROR_ADD_MANUFACTURER_MISMATCH_COUNTRY = "Please select one country from above list";
        public const string ERROR_ADD_MANUFACTURER_EXIST_NAME = "Name exist in database!\nPlease specify another one.";
        public const string EQUAL = " = ";
        public const string ERROR_ADD_MANUFACTURER_IVALID_CONTACT = "Contact must be numeric";
        /*******************End add manufacturer****************/

        /******************Error in add stock******************/
        public const string ERROR_ADD_STOCK_EMPTY_PRODUCT_NAME = "Please select one product from above list";
        public const string ERROR_ADD_STOCK_EMPTY_MANUFACTURER_NAME = "Please select one manufacturer from above list";
        public const string ERROR_ADD_STOCK_EMPTY_IMPORT_PRICE = "Please specify import price";
        public const string ERROR_ADD_STOCK_EMPTY_SELL_PRICE = "Please specify sell price";
        public const string ERROR_ADD_STOCK_EMPTY_QUANTITY = "Please specify quantity";
        public const string ERROR_ADD_STOCK_INVALID_IMPORT_PRICE = "Import price must be numeric";
        public const string ERROR_ADD_STOCK_INVALID_SELL_PRICE = "Sell price must be numeric";
        public const string ERROR_ADD_STOCK_INVALID_QUANTITY = "Quantity must be integer";
        public const string ERROR_ADD_STOCK_INVALID_DATE = "Batch ID (manufacture date) must\n be earlier than expire date";
        public const string ERROR_ADD_STOCK_INVALID_PRODUCT = "Invalid Product!\n Please re-select from above list";
        public const string ERROR_ADD_STOCK_PRODUCT_BATCH_EXIST = "Same product batch exist in database!\n Please specify another batch or proceed\n to edit stock instead of add";
        public const string ADD_STOCK_PRDUCT_GROUP_BOX_CAPTION = "Product Information";
        /******************End add stock*************************/

        /******************Error in add staff**********************/
        public const string ERROR_ADD_STAFF_EMPTY_NAME = "Please specify name of staff";
        public const string ERROR_ADD_STAFF_EMPTY_GENDER = "Please choose gender of staff";
        public const string ERROR_ADD_STAFF_EMPTY_EMAIL = "Please specify email  of staff";
        public const string ERROR_ADD_STAFF_EMPTY_CONTACT = "Please specify contact of staff";
        public const string ERROR_ADD_STAFF_EMPTY_POSITION = "Please specify position of staff";
        public const string ERROR_ADD_STAFF_WRONG_DATE_OF_BIRTH = "Date of birth must not later than today";
        public const string ERROR_ADD_STAFF_WRONG_CONTACT = "Contact must be numeric";
        public const string ERROR_ADD_STAFF_WRONG_POSITION = "Please specify position from list above";
        /*******************End add staff**************************/

        /******************About Delete********************/
        public const string CONFIRM_DELETE_SHOP = "Do you want to delete selected item(s)?\nAll requests associated with it will be\nremoved from database!";
        public const string CONFIRM_DELETE_PRODUCT = "Do you want to remove selected products?\n All stock and requests associated with it will be\nremoved accordingly!";
        public const string CONFIRM_DELETE_STAFF = "Do you want to remove selected staff from system permanently?";
        public const string CONFIRM_DELETE_CASHIER_REGISTER = "Do you want to remove seleted cashier register(s) from system permanently?";
        public const string CONFIRM_DELETE_PRICE_TAG = "Do you want to remove seleted price tag(s) from system permanently?";
        /********************************************************/

        /*****************All About Message box***************/
        public const string MSG_ERROR = "Error";
        public const string MSG_WARNING = "Warning";
        public const string MSG_AUTO_CLOSE = "Auto";
        /*****************End Message box**********************/

        /*****************All about search*********************/

        public const string SEARCH_PRODUCT = Constant.P_ID + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                            + Constant.OPERATION_OR + Constant.P_NAME + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                            + Constant.OPERATION_OR + Constant.P_M_Name + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                            + Constant.OPERATION_OR + Constant.P_CATEGORY + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                            + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.P_MIN + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE;

        public const string SEARCH_STOCK = Constant.STOCK_P_ID + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                            + Constant.OPERATION_OR + Constant.STOCK_P_NAME + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                            + Constant.OPERATION_OR + Constant.STOCK_M_NAME + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                            + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.STOCK_BATCH_ID + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                            + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.STOCK_IMPORT_PRICE + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                            + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.STOCK_SELL_PRICE + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                            + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.STOCK_EXPIRE_DATE + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                            + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.STOCK_QUANTITY + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE;

        public const string SEARCH_MANUFACTURER = Constant.M_NAME + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.M_ADDRESS + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.M_COUNTRY + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.M_CONTACT + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE;


        public const string SEARCH_STAFF = Constant.STAFF_ID + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.STAFF_NAME + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.STAFF_PASSWORD + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.STAFF_RENEWED_PASSWORD_DATE + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.STAFF_DATE_OF_BIRTH + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.STAFF_JOIN_DATE + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.STAFF_GENDER + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.STAFF_POSITION + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.STAFF_CONTACT + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.STAFF_IS_DEFAULT_PASSWORD+ Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.STAFF_BLOCKED + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE;

        public const string SEARCH_TRANSACTION = Constant.TRANSACTION_ID + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.TRANSACTION_CASHIER_ID + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.TRANSACTION_MACHINE_ID + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.TRANSACTION_DATE + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.TRANSACTION_PRODUCT_ID + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.TRANSACTION_BATCH_ID + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.TRANSACTION_COST_PRICE + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.TRANSACTION_SELLING_PRICE + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.TRANSACTION_DISCOUNT + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.TRANSACTION_UNIT_SOLD + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE;

        public const string SEARCH_DISCOUNT = Constant.D_P_ID + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.D_P_NAME + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.D_M_NAME + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.D_EFFECTIVE_DATE + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.D_BUNDLE_UNIT + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.D_FREE_ITEM_QUANTITY + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.D_DISCOUNT_PERCENTAGE + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE;

        public const string SEARCH_CASHIER_REGISTER = Constant.CASHIER_ID + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                            + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.CASHIER_STATUS + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE;

        public const string SEARCH_PRICE_TAG = Constant.PRICE_TAG_ID + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.PRICE_TAG_P_ID + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.PRICE_TAG_B_ID + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.PRICE_TAG_STATUS + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE;

        public const string SEARCH_PENDING = Constant.RE_P_ID + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.RE_P_NAME + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.RE_M_NAME + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.RE_REQUEST_DATE + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.RE_QUANTITY + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.RE_URGENCY + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE;

        public const string SEARCH_APPROVED = Constant.RE_P_ID + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.RE_P_NAME + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.RE_M_NAME + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.RE_REQUEST_DATE + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.RE_APPROVED_DATE + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.RE_APPROVED_QUANTITY + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.RE_URGENCY + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.RE_COMMENT + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE;

        public const string SEARCH_REJECTED = Constant.RE_P_ID + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.RE_P_NAME + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.RE_M_NAME + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.RE_REQUEST_DATE + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.RE_REJECTED_DATE + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.RE_REQUESTED_QUANTITY + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.RE_URGENCY + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                                    + Constant.OPERATION_OR + Constant.RE_COMMENT + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE;

        public const string SEARCH_INCOMING = Constant.RE_P_ID + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                            + Constant.OPERATION_OR + Constant.RE_P_NAME + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                            + Constant.OPERATION_OR + Constant.RE_M_NAME + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                            + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.RE_BATCH_ID + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                            + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.RE_INCOMING_QUANTITY + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                            + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.RE_SEND_DATE + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                            + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.RE_RECEIVE_DATE + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE
                            + Constant.OPERATION_OR + Constant.CONVERT_TO + Constant.RE_RECEIVE_STATUS + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE;

        public const string FIELTER_REPORT_BY_DATE = Constant.CONVERT_TO + Constant.TRANSACTION_DATE + Constant.SYSTEM_STRING + Constant.OPERATION_LIKE + Constant.OPERATION_SINGLE_QUOTE + Constant.OPERATION_PERCENT + "{0}" + Constant.OPERATION_PERCENT + Constant.OPERATION_SINGLE_QUOTE;

        /******************End search*****************************/

        /*******************Search box****************************/
        public const string SHOP_SEARCH_BOX = "txtKeywordShop";
        public const string PRODUCT_SEARCH_BOX = "txtKeywordProduct";
        public const string STOCK_SEARCH_BOX = "txtKeywordStock";
        public const string PENDING_SEARCH_BOX = "txtKeywordPending";
        public const string APPROVED_SEARCH_BOX = "txtKeywordApproved";
        public const string REJECTED_SEARCH_BOX = "txtKeywordRejected";
        public const string INCOMING_SEARCH_BOX = "txtKeywordIncoming";
        public const string MANUFACTURER_SEARCH_BOX = "txtKeywordManufacturer";
        public const string STAFF_SEARCH_BOX = "txtKeywordStaff";
        public const string TRANSACTION_SEARCH_BOX = "txtKeywordTransaction";
        public const string DISCOUNT_SEARCH_BOX = "txtKeywordDiscount";
        public const string CASHIER_REGISTER_SEARCH_BOX = "txtKeywordCashierRegister";
        public const string PRICE_TAG_SEARCH_BOX = "txtKeywordPriceTag";
        /*******************End Search box************************/

        /********************Approve request************************/
        public const string ERROR_APPROVE_EMPTY_QUANTITY = "Approved quantity can not be empty";
        public const string ERROR_APPROVE_EXCEED_AVAILABLE_QUANTITY = "Approved quantity can not exceed available amount";
        public const string ERROR_APPROVE_WRONG_QUANTITY = "Approved quantity must be numeric";
        public const string ERROR_APPROVE_SMALL_EQUAL_ZERO = "Approve quantity must be at least one";
        /********************End Approve Request********************/
    }
}
