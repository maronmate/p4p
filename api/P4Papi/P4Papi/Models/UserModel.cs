using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public int PositionId { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public int SubdivisionId { get; set; }

        public DateTime? StartDate { get; set; }

        public UserModel()
        {
        }

        public UserModel(User user)
        {
            if (user != null)
            {
                this.UserId = user.UserId;
                this.Name = user.Name;
                this.LastName = user.Lastname;
                this.PositionId = user.PositionId;
                this.StartDate = user.StartDate;
                this.SubdivisionId = user.SubdivisionId;

                this.DepartmentId = user.Position.DepartmentId;
                this.DepartmentName = user.Position.Department.Name;
            }
        }
    }
}