using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Models
{
    public class GetUserDDLModel
    {
        public int? DepartmentId { get; set; }
        public int? SubdivisionId { get; set; }
        public int? PositionId { get; set; }
        public GetUserDDLModel()
        {

        }
        public GetUserDDLModel(int? departmentId,int? subdivisionId, int? positionId)
        {
            DepartmentId = departmentId;
            SubdivisionId = subdivisionId;
            PositionId = positionId;
        }
    }
}