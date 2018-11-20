using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Models
{
    public class UserLoginDisplayModel
    {
        public int LoginId { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
        public string Name { get; set; }
        public List<int> DepartmentIdList { get; set; }
        public string DisplayDepartment { get; set; }

        public UserLoginDisplayModel()
        {

        }
        public UserLoginDisplayModel(LoginUser login)
        {
            if (login != null)
            {
                LoginId = login.LoginId;
                Name = login.Name;
                Username = login.Username;
                IsAdmin = login.IsAdmin;
                List<int> departments = new List<int>();
                List<string> departnameString = new List<string>();
                if (login.UserLoginDepartments != null)
                {
                    foreach (UserLoginDepartment item in login.UserLoginDepartments)
                    {
                        departments.Add(item.DepartmentId);
                        departnameString.Add(item.Department.Name);
                    }
                }
                DepartmentIdList = departments;
                DisplayDepartment = String.Join(", ", departnameString.ToArray());
            }
        }
    }
}