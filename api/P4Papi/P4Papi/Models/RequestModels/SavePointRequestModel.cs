using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Models
{
    public class SavePointRequestModel
    {
        public DateTime YM { get; set; }
        public int UserId { get; set; }
        public int Point { get; set; }
        public string Remark { get; set; }

        public SavePointRequestModel()
        {
        }
    }
}