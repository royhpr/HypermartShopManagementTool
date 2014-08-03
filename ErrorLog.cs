using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

/**
 * This class is used to record message from exception occured in program
 * User can refer to the file in application directory
 * Basic Format:

 * Index was out of range. Must be non-negative and less than the size of the collection.
 * Parameter name: index - AssignIndex in CommandHandler.cs
 * Index was out of range. Must be non-negative and less than the size of the collection.
 * Parameter name: index - AssignIndex in CommandHandler.cs
 * 
 * 
 * @Author: Huang Purong
 * @Date: 01/10/2012
 */

namespace Hypermarket_Shop_Management_Tool
{
    class ErrorLog
    {
        private const string LOG_FILE = "ErrorLog.txt";

        public static void Log(string ErrorString)
        {
            // Create a writer and open the file:
            StreamWriter LogError;

            if (!File.Exists(LOG_FILE))
            {
                LogError = new StreamWriter(LOG_FILE);
            }
            else
            {
                LogError = File.AppendText(LOG_FILE);
            }

            // Write the error message to the file:
            LogError.WriteLine(ErrorString);

            // Close the stream:
            LogError.Close();
        }
    }
}
