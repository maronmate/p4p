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
       public List<UserLoginDisplayModel> GetUserLoginDataList()
        {
            var output = _ctx.LoginUsers.Include(t => t.UserLoginDepartments).Include("UserLoginDepartments.Department");
            List<UserLoginDisplayModel> loginModelList = new List<UserLoginDisplayModel>();
            foreach (var login in output)
            {
                loginModelList.Add(new UserLoginDisplayModel(login));
            }
            return loginModelList;
       }

        public bool InsertLoginUser(string username, string password, bool isAdmin, string name, out string error, out int newLoginId)
        {
            try
            {
                error = string.Empty;
                newLoginId = 0;
                if (HasDuplicateUserName(null, username))
                {
                    error = string.Format("username '{0}' นี้มีอยู่ในระบบอยู่แล้ว", username);
                    return false;
                }
                LoginUser newUser = new LoginUser();
                newUser.Name = name;
                newUser.Username = username;
                if (!string.IsNullOrEmpty(password))
                    newUser.Password = password;
                newUser.IsAdmin = isAdmin;
                newUser.Name = name;

                _ctx.LoginUsers.Add(newUser);
                _ctx.SaveChanges();
                newLoginId = newUser.LoginId;
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                newLoginId = 0;
                return false;
            }
        }
        public bool UpdateLoginUser(int userLoginId, string username, string password, bool isAdmin, string name, out string error)
        {
            try
            {
                error = string.Empty;
                if (HasDuplicateUserName(userLoginId, username))
                {
                    error = string.Format("username '{0}' ที่แก้ไขนี้มีอยู่ในระบบอยู่แล้ว", username);
                    return false;
                }
                LoginUser updateUser = GetRealLogin(userLoginId);
                if (updateUser != null)
                {
                    updateUser.Name = name;
                    updateUser.Username = username;
                    if(!string.IsNullOrEmpty(password))
                        updateUser.Password = password;
                    updateUser.IsAdmin = isAdmin;
                    updateUser.Name = name;
                    _ctx.SaveChanges();
                    return true;
                }
                else
                {
                    error = "No LoginUser data in database";
                    return false;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public bool DeleteLoginUser(int userLoginId, out string error)
        {
            try
            {
                error = string.Empty;
                LoginUser removeUser = GetRealLogin(userLoginId);
                _ctx.LoginUsers.Remove(removeUser);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public bool HasDuplicateUserName(int? userLoginId,string username)
        {
            bool duplicated = false;
            
            var output = _ctx.LoginUsers.Where(user => user.Username == username);
            //if userLoginId has value, in updateData
            if (userLoginId.HasValue)
            {
                if (output.Count() > 1)
                {
                    duplicated = true;
                }
                else if (output.Count() == 1)
                {
                    LoginUser duplicatedUsername = output.First();
                    if (duplicatedUsername.LoginId != userLoginId.Value)
                        duplicated = true;
                }
            }
            else
            {
                if (output.Count() > 0)
                    duplicated = true;
            }
            return duplicated;
        }
        public bool HasLoginUser(int loginUserId)
        {
            var output = _ctx.LoginUsers.Where(user => user.LoginId == loginUserId);
            if (output.Count() > 0)
                return true;
            else
                return false;
        }

        private LoginUser GetRealLogin(int loginUserId)
        {
            var output = _ctx.LoginUsers.Where(user => user.LoginId == loginUserId);
            if (output.Count() > 0)
                return output.FirstOrDefault();
            else
                return null;
        }
        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}