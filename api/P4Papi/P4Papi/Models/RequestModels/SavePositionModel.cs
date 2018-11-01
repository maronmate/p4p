using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Models
{
    public class SavePositionModel
    {
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public int DepartmentId { get; set; }
        public double TargetPoint { get; set; }

        public SavePositionModel()
        {
        }
        public SavePositionModel(int positionId, string positionName, int departmentId, double targetPoint)
        {
            PositionId = positionId;
            PositionName = positionName;
            DepartmentId = departmentId;
            TargetPoint = targetPoint;
        }
    }
}