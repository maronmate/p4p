using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Models
{
    public class SaveUserModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int PositionId { get; set; }
        public int SubdivisionId { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime StartDate { get; set; }

        public SaveUserModel()
        {
        }
    }
}