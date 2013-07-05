using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Security.Cryptography;
using System.Text;

public class SSTCryptographer
{
        
	public SSTCryptographer()
	{ 
	}
    #region[Encrypt]
    public static string Encrypt(string strToEncrypt, string strKey)
    {
        try
        {
            TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();

            byte[] byteHash, byteBuff;
            string strTempKey = strKey;

            byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
            objHashMD5 = null;
            objDESCrypto.Key = byteHash;
            objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB

            byteBuff = ASCIIEncoding.ASCII.GetBytes(strToEncrypt);
            return Convert.ToBase64String(objDESCrypto.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
        }
        catch (Exception ex)
        {
            return "Wrong Input. " + ex.Message;
        }
    }
    #endregion
    #region[Decrypt]
    public static string Decrypt(string strEncrypted, string strKey)
    {
        try
        {
            TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();

            byte[] byteHash, byteBuff;
            string strTempKey = strKey;

            byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
            objHashMD5 = null;
            objDESCrypto.Key = byteHash;
            objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB

            byteBuff = Convert.FromBase64String(strEncrypted);
            string strDecrypted = ASCIIEncoding.ASCII.GetString(objDESCrypto.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            objDESCrypto = null;

            return strDecrypted;
        }
        catch (Exception ex)
        {
            return "Wrong Input. " + ex.Message;
        }
    }
    #endregion
}
