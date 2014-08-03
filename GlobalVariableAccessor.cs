using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypermarket_Shop_Management_Tool
{
    static class GlobalVariableAccessor
    {
        private static string _currentStaffName = string.Empty;
        public static string CurrentStaffName
        {
            get { return _currentStaffName;}
            set { _currentStaffName = value; }
        }

        private static string _currentStaffID = string.Empty;
        public static string CurrentStaffID
        {
            get { return _currentStaffID; }
            set { _currentStaffID = value; }
        }

        private static string _currentStaffEmail = string.Empty;
        public static string CurrentStaffEmail
        {
            get { return _currentStaffEmail; }
            set { _currentStaffEmail = value; }
        }

        private static string _passwordValidDays = string.Empty;
        public static string PasswordValidDays
        {
            get { return _passwordValidDays; }
            set { _passwordValidDays = value; }
        }

        private static string _position = string.Empty;
        public static string Position
        {
            get { return _position; }
            set { _position = value; }
        }

        private static string _errorMessage = string.Empty;
        public static string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        private static string _salution = string.Empty;
        public static string Salution
        {
            get { return _salution; }
            set { _salution = value; }
        }
    }
}
