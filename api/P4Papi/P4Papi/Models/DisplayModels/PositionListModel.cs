using P4Papi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Models
{
    public class PositionListModel
    {
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int UserInPositionCount { get; set; }
        public double TargetPoint { get; set; }

        public PositionListModel()
        {
        }
        public PositionListModel(Position position)
        {
            if (position != null)
            {
                PositionId = position.PositionId;
                PositionName = position.Name;
                DepartmentId = position.DepartmentId;
                if(position.TargetPoint.HasValue)
                    TargetPoint = position.TargetPoint.Value;
                if (position.Department != null)
                    DepartmentName = position.Department.Name;
                if (position.Users != null)
                {
                    UserInPositionCount = position.Users.Count();
                }

            }
        }
    }
}