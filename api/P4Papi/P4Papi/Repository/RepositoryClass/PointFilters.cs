using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Repository
{
    public class PointFilters
    {
        public int? DepartmentId { get; set; }
        public int? SubdivisionId { get; set; }
        public int? PositionId { get; set; }
        public int? UserId { get; set; }
        public DateTime? YMStart { get; set; }
        public DateTime? YMEnd { get; set; }

        public PointFilters()
        {

        }
        public PointFilters(int? departmentId, int? subdivisionId, int? positionId, int? userId, DateTime? ymStart, DateTime? ymEnd)
        {
            DepartmentId = departmentId;
            SubdivisionId = subdivisionId;
            PositionId = positionId;
            UserId = userId;
            YMStart = ymStart;
            YMEnd = ymEnd;
        }
    }
}