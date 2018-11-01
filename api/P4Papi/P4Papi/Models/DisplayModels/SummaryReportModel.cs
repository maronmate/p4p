using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Models
{
    public class SummaryReportModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int SubdivisionId { get; set; }
        public string SubdivisionName { get; set; }
        public string Remark { get; set; }
        public double TargetPoint { get; set; }
        public int rowNumber { get; set; } 

        public CloumnYM[] ColumnYm { get; set; }

        public SummaryReportModel()
        {

        }
    }

    public class CloumnYM
    {
        public DateTime YMColumns {get;set;}

        public CloumnYM()
        {
        }
        public CloumnYM(DateTime ym)
        {
            //   YMColumns = ym.OrderBy(y => y).ToArray();
            YMColumns = ym;
        }
    }

    public class RowData
    {
        public int UserId { get; set; }

    }
}