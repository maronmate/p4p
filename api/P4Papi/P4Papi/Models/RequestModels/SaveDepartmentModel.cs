using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Models
{
    public class SaveDepartmentModel
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public int OrderInReport { get; set; }
        public bool ShowInReport { get; set; }

        public SaveDepartmentModel()
        {
        }
        public SaveDepartmentModel(int departmentId,string name, int orderInReport, bool showInReport)
        {
            DepartmentId = departmentId;
            Name = name;
            OrderInReport = orderInReport;
            ShowInReport = showInReport;
        }
    }
}