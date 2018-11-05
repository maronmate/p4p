using P4Papi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace P4Papi.Repository
{
    public class LoginUserRepository : IDisposable
    {
        private PfourPEntities _ctx;
        public LoginUserRepository()
        {
            _ctx = new PfourPEntities();
        }
        public LoginModel LoginUser(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return null;
            }
            LoginModel loginModel = null;

            var output = _ctx.LoginUsers
                .Where(user => user.Username == userName && user.Password == password)
                .Include(t => t.UserLoginDepartments);
            if (output.Count() > 0)
            {
                LoginUser login = output.First();
                loginModel = new LoginModel(login);
            }

            return loginModel;
        }
        public LoginModel GetUserLoginData(int userId)
        {
            LoginModel loginModel = null;
            var output = _ctx.LoginUsers
                .Where(login => login.LoginId == userId)
                .Include(t => t.UserLoginDepartments);
            if (output.Count() > 0)
            {
                LoginUser login = output.First();
                loginModel = new LoginModel(login);
            }

            return loginModel;
        }
        //public string EncryptString(string Message)
        //{
        //    byte[] Results;
        //    System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
        //    MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
        //    byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(_tokenModel.Key));
        //    TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
        //    TDESAlgorithm.Key = TDESKey;
        //    TDESAlgorithm.Mode = CipherMode.ECB;
        //    TDESAlgorithm.Padding = PaddingMode.PKCS7;
        //    byte[] DataToEncrypt = UTF8.GetBytes(Message);
        //    try
        //    {
        //        ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
        //        Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
        //    }
        //    finally
        //    {
        //        TDESAlgorithm.Clear();
        //        HashProvider.Clear();
        //    }
        //    return Convert.ToBase64String(Results);
        //}

        //public string DecryptString(string Message)
        //{
        //    byte[] Results;
        //    System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
        //    MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
        //    byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(_tokenModel.Key));
        //    TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
        //    TDESAlgorithm.Key = TDESKey;
        //    TDESAlgorithm.Mode = CipherMode.ECB;
        //    TDESAlgorithm.Padding = PaddingMode.PKCS7;
        //    byte[] DataToDecrypt = Convert.FromBase64String(Message);
        //    try
        //    {
        //        ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
        //        Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
        //    }
        //    finally
        //    {
        //        TDESAlgorithm.Clear();
        //        HashProvider.Clear();
        //    }
        //    return UTF8.GetString(Results);
        //}
        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}