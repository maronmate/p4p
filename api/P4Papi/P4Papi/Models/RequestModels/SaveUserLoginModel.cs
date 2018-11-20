using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Models
{
    public class SaveUserLoginModel
    {
        public int LoginId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string Name { get; set; }
        public List<int> DepartmentIdList { get; set; }

        public SaveUserLoginModel()
        {
            
        }
        public SaveUserLoginModel(int LoginId, string Username, string Password, bool IsAdmin, string Name, List<int> DepartmentIdList)
        {
            this.LoginId = LoginId;
            this.Username = Username;
            this.Password = Password;
            this.IsAdmin = IsAdmin;
            this.Name = Name;
            this.DepartmentIdList = DepartmentIdList;
        }
    }

}