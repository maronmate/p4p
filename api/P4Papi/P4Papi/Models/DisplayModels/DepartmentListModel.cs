using P4Papi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Models
{
    public class DepartmentListModel
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int PositionCount { get; set; }
        public int UserCount { get; set; }
        public int OrderInReport { get; set; }
        public bool ShowInReport { get; set; }
        public int SubDivisionCount { get; set; }

        public DepartmentListModel()
        {
        }

        public DepartmentListModel(Department department,int numberOfUser)
        {
            if (department != null)
            {
                DepartmentId = department.DepartmentId;
                DepartmentName = department.Name;
                OrderInReport = department.ReportOrder;
                ShowInReport = department.ShowInReport;
                 
                if (department.Positions != null)
                {
                    PositionCount = department.Positions.Count();
                    UserCount = numberOfUser;
                }

                if (department.Subdivisions != null)
                    SubDivisionCount = department.Subdivisions.Count();
            }
        }
    }
}