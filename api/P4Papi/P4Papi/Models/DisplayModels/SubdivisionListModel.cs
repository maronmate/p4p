using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Models
{
    public class SubdivisionListModel
    {
        public int SubdivisionId { get; set; }
        public string SubdivisionName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int UserInSubdivisionCount { get; set; }
        public int OrderInDepartment { get; set; }

        public SubdivisionListModel()
        {
        }
        public SubdivisionListModel(Subdivision subdivision)
        {
            if (subdivision != null)
            {
                SubdivisionId = subdivision.SubdivisionId;
                SubdivisionName = subdivision.Name;
                DepartmentId = subdivision.DepartmentId;
                if(subdivision.OrderInDepartment.HasValue)
                    OrderInDepartment = subdivision.OrderInDepartment.Value;
                if (subdivision.Department != null)
                {
                    DepartmentName = subdivision.Department.Name;
                }
                if (subdivision.Users != null)
                {
                    UserInSubdivisionCount = subdivision.Users.Count();
                }
            }
        }
    }
}