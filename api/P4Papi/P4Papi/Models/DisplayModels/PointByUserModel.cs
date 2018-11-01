using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Models
{
    public class PointByUserModel
    {
        public DateTime YM { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int SubdivisionId { get; set; }
        public string SubdivisionName { get; set; }
        public double Point { get; set; }
        public string Remark { get; set; }
        public double TargetPoint { get; set; }
        public DateTime ModifyDate { get; set; }
        public PointByUserModel()
        {
        }

        public PointByUserModel(Point point)
        {
            if (point != null)
            {
                YM = point.YM;
                UserId = point.UserId;
                Remark = point.Remark;
                Point = point.MonthlyPoint;
                
                if (point.User != null)
                {
                    Name = point.User.Name;
                    Lastname = point.User.Lastname;
                    PositionId = point.User.PositionId;
                    if (point.User.Position != null)
                    {                       
                        PositionName = point.User.Position.Name;
                        DepartmentId = point.User.Position.DepartmentId;
                        DepartmentName = point.User.Position.Department.Name;
                        if(point.User.Position.TargetPoint.HasValue)
                            TargetPoint = point.User.Position.TargetPoint.Value;
                    }
                    if(point.User.Subdivision != null)
                    {
                        SubdivisionId = point.User.Subdivision.SubdivisionId;
                        SubdivisionName = point.User.Subdivision.Name;
                    }
                }      
                if (point.ModifyDate.HasValue)
                    ModifyDate = point.ModifyDate.Value;
            }
        }

    }
}