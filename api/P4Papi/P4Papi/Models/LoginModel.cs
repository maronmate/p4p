using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Models
{
    public class LoginModel
    {
        public int LoginId { get; set; }

        public string Name { get; set; }

        public bool IsAdmin { get; set; }   

        public List<int> DepartmentList { get; set; }
        
        public LoginModel()
        {

        }
        public LoginModel(LoginUser login)
        {
            if(login != null)
            {
                LoginId = login.LoginId;
                Name = login.Name;
                IsAdmin = login.IsAdmin;
                List<int> departments = new List<int>();
                if(login.UserLoginDepartments !=null)
                {
                    foreach (UserLoginDepartment item in login.UserLoginDepartments)
                    {
                        departments.Add(item.DepartmentId);
                    }
                }
                DepartmentList = departments;
            }
        }
    }
}