using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Models
{
    public class DepartmentDDLModel
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public DepartmentDDLModel()
        {

        }
        public DepartmentDDLModel(Department department)
        {
            DepartmentId = department.DepartmentId;
            DepartmentName = department.Name;
        }
        public DepartmentDDLModel(UserLoginDepartment userLoginDepartment)
        {
            if (userLoginDepartment != null && userLoginDepartment.Department != null)
            {
                DepartmentId = userLoginDepartment.DepartmentId;
                DepartmentName = userLoginDepartment.Department.Name;
            }
        }
    }
}