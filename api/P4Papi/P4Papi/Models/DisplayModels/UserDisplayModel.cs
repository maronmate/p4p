using P4Papi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace P4Papi.Models
{
    public class UserDisplayModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public int SubdivisionId { get; set; }
        public string SubdivisionName { get; set; }
        public bool IsAdmin { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime StartDate { get; set; }
        public double TargetPoint { get; set; }

        public UserDisplayModel()
        {
        }
        public UserDisplayModel(User user)
        {
            if(user != null)
            {
                UserId = user.UserId;
                Name = user.Name;
                LastName = user.Lastname;
                PositionId = user.PositionId;
                
                if (user.Position != null)
                {
                    PositionName = user.Position.Name;
                    DepartmentId = user.Position.DepartmentId;
                    if(user.Position.TargetPoint.HasValue)
                        TargetPoint = user.Position.TargetPoint.Value;
                    if (user.Position.Department != null)
                    {
                        DepartmentName = user.Position.Department.Name;
                    }
                }
                SubdivisionId = user.SubdivisionId;
                if (user.Subdivision != null)
                {
                    SubdivisionName = user.Subdivision.Name;                   
                }
                BirthDate = user.Birthdate;
                StartDate = user.StartDate;
            }
        }

    }
}