using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Microsoft.VisualBasic;
using System.IO;

/*****************************************
 *Resource online with some editing
 *Used to encrypt user password and connection string
 * 
 * 
 * 
 * 
 * 
 * @Auther: Huang Purong
 * @Date: 08/26/2013
 * ****************************************/

namespace Hypermarket_Shop_Management_Tool._2_Controller
{
    class SecurityManager
    {
        private static SecurityManager SecurityManagerInstance;

        //Hash algorithm used to generate password. Allowed values are: "MD5" and
        // "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
        private string strHashAlg = "MD5";

        // strPassPhrase from which a pseudo-random password will be derived. The
        // derived password will be used to generate the encryption key.
        // strPassPhrase can be any string. 
        private string strPassPhrase = "AutoscanTechnology";

        // Salt value used along with strPassPhrase to generate password. 
        // Salt can be any string. 
        private string strSaltValue = "Autoscan";

        // Number of iterations used to generate password. One or two iterations
        // should be enough.
        private int intPwdIteration = 2;

        // Initialization vector (or IV). This value is required to encrypt the
        // first block of strPlainText data. For RijndaelManaged class IV must be 
        // exactly 16 ASCII characters long.
        private string strInitVector = "1A8u7t5o6s3c2A4n";

        // Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
        // Longer keys are more secure than shorter keys.
        private int strKeySize = 192;

        //Singleton
        private SecurityManager()
        {
        }

        public static SecurityManager getInstance()
        {
            if (SecurityManagerInstance == null)
            {
                SecurityManagerInstance = new SecurityManager();
            }
            return SecurityManagerInstance;
        }

        /// <summary>
        /// Simple encryption that changes character for each alphabet.
        /// </summary>
        /// <remarks>
        /// The letter "d" cannot be used in the password due
        /// to conflicts with database char storage format.
        /// Resolution: Cast all letters to UPPER.
        /// </remarks>
        /// <param name="strPlainText">
        /// Plain text value to be encrypted.
        /// </param>
        /// <returns>
        /// Encrypted value.
        /// </returns>     
        public string Encrypt(string strPlainText)
        {
            strPlainText = strPlainText.ToUpper();

            int i, nCh, nCh2;
            string sReturn, KeyValue;
            char sCh;
            sReturn = "";
            KeyValue = "autoscan";

            if (strPlainText.Length > 0)
            {
                for (i = 1; i < strPlainText.Length; i++)
                {
                    nCh = Convert.ToChar(strPlainText.Substring(i, 1));
                    sCh = Convert.ToChar(nCh);
                    nCh2 = (nCh + GetEncryptionKey(KeyValue)) ^ 255;
                    sCh = Convert.ToChar(nCh2);
                    sReturn = sReturn + Convert.ToChar(253) + sCh;
                }
            }
            return sReturn;
        }

        /// <summary>
        /// Encrypts specified strPlainText using Rijndael symmetric key algorithm
        /// and returns a base64-encoded result.
        /// </summary>
        /// <param name="strPlainText">
        /// Plain text value to be encrypted.
        /// </param>
        /// <returns>
        /// Encrypted value formatted as a base64-encoded string.
        /// </returns>
        public string ComplexEncrypt(string strPlainText)
        {
            // Convert strings into byte arrays.
            // Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
            // encoding.
            byte[] strInitVectorBytes = Encoding.ASCII.GetBytes(strInitVector);
            byte[] strSaltValueBytes = Encoding.ASCII.GetBytes(strSaltValue);

            // Convert our strPlainText into a byte array.
            // Let us assume that strPlainText contains UTF8-encoded characters.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(strPlainText);

            // First, we must create a password, from which the key will be derived.
            // This password will be generated from the specified strPassPhrase and 
            // salt value. The password will be created using the specified hash 
            // algorithm. Password creation can be done in several iterations.
            PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                            strPassPhrase,
                                                            strSaltValueBytes,
                                                            strHashAlg,
                                                            intPwdIteration);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = password.GetBytes(strKeySize / 8);

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;

            // Generate encryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                             keyBytes,
                                                             strInitVectorBytes);

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream();

            // Define cryptographic stream (always use Write mode for encryption).
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                         encryptor,
                                                         CryptoStreamMode.Write);
            // Start encrypting.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

            // Finish encrypting.
            cryptoStream.FlushFinalBlock();

            // Convert our encrypted data from a memory stream into a byte array.
            byte[] cipherTextBytes = memoryStream.ToArray();

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert encrypted data into a base64-encoded string.
            string strCipherText = Convert.ToBase64String(cipherTextBytes);

            // Return encrypted string.
            return strCipherText;

        }

        private string genNewString(string strValue)
        {
            int i, j, k;
            string strNewStringValue = "";
            char[] chrValue = strValue.ToCharArray();

            for (i = 0; i <= chrValue.Length - 1; i++)
            {
                // take the next character in the string
                j = (int)chrValue[i];

                // find out the character code
                k = (int)j;

                if (k >= 97 && k <= 109)
                {
                    // a ... m inclusive become n ... z
                    k = k + 13;
                }
                else if (k >= 110 && k <= 122)
                {
                    // n ... z inclusive become a ... m
                    k = k - 13;
                }
                else if (k >= 65 && k <= 77)
                {
                    // A ... m inclusive become n ... z
                    k = k + 13;
                }
                else if (k >= 78 && k <= 90)
                {
                    // N ... Z inclusive become A ... M
                    k = k - 13;
                }

                //add the current character to the string returned by the function
                strNewStringValue = strNewStringValue + (char)k;
            }

            return strNewStringValue;
        }

        private int GetEncryptionKey(string sEncryptionKey)
        {
            int i, nAve, nAsc;
            int nKey = 0, n = 0;
            if (sEncryptionKey.Length > 0)
            {
                for (i = 1; i < sEncryptionKey.Length; i++)
                {
                    nAsc = Convert.ToChar(sEncryptionKey.Substring(i, 1));
                    nKey = nKey + nAsc;
                    n++;
                }
                nAve = Convert.ToInt32(nKey / n);
                nKey = nKey + (nKey / nAve);
            }
            while (nKey > n)
            {
                nKey = nKey / 7;
            }
            while (nKey < 0)
            {
                nKey = nKey + 7;
            }
            return nKey;
        }

        /// <summary>
        /// Decrypts specified strCipherText using Rijndael symmetric key algorithm.
        /// </summary>
        /// <param name="strCipherText">
        /// Base64-formatted Cipher Text value.
        /// </param>
        /// <returns>
        /// Decrypted string value.
        /// </returns>
        public string Decrypt(string strCipherText)
        {
            // Convert strings defining encryption key characteristics into byte
            // arrays. Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8
            // encoding.
            byte[] strInitVectorBytes = Encoding.ASCII.GetBytes(strInitVector);
            byte[] strSaltValueBytes = Encoding.ASCII.GetBytes(strSaltValue);

            // Convert our strCipherText into a byte array.
            byte[] cipherTextBytes = Convert.FromBase64String(strCipherText);

            // First, we must create a password, from which the key will be 
            // derived. This password will be generated from the specified 
            // strPassPhrase and salt value. The password will be created using
            // the specified hash algorithm. Password creation can be done in
            // several iterations.
            PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                            strPassPhrase,
                                                            strSaltValueBytes,
                                                            strHashAlg,
                                                            intPwdIteration);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = password.GetBytes(strKeySize / 8);

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;

            // Generate decryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                             keyBytes,
                                                             strInitVectorBytes);

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

            // Define cryptographic stream (always use Read mode for encryption).
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                          decryptor,
                                                          CryptoStreamMode.Read);

            // Since at this point we don't know what the size of decrypted data
            // will be, allocate the buffer long enough to hold strCipherText;
            // strPlainText is never longer than strCipherText.
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            // Start decrypting.
            int decryptedByteCount = cryptoStream.Read(plainTextBytes,
                                                       0,
                                                       plainTextBytes.Length);

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert decrypted data into a string. 
            // Let us assume that the original strPlainText string was UTF8-encoded.
            string strPlainText = Encoding.UTF8.GetString(plainTextBytes,
                                                       0,
                                                       decryptedByteCount);

            // Return decrypted string.   
            return strPlainText;
        }
    }
}
