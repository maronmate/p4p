using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Models
{
    public class SearchPointRequestModel
    {
        public int? DepartmentId { get; set; }
        public int? SubdivisionId { get; set; }
        public int? PositionId { get; set; }
        public int? UserId { get; set; }
        public DateTime? YMStart { get; set; }
        public DateTime? YMEnd { get; set; }

        public string SortFieldName { get; set; }
        public bool SortOnDesc { get; set; }

        public int PageSize { get; set; }
        public int CurrentPage { get; set; }

        public SearchPointRequestModel()
        {

        }
    }
}