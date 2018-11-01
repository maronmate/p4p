using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Models
{
    public class SaveSubdivisionModel
    {
        public int SubdivisionId { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public int OrderInDepartment { get; set; }

        public SaveSubdivisionModel()
        {
        }
        public SaveSubdivisionModel(int subdivisionId, string name, int departmentId, int orderInDepartment)
        {
            SubdivisionId = subdivisionId;
            Name = name;
            DepartmentId = departmentId;
            OrderInDepartment = orderInDepartment;
        }
    }
}