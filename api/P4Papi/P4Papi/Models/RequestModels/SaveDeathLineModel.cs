using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Models
{
    public class SaveDeathLineModel
    {
        public DateTime YM { get; set; }
        public int UserId { get; set; }
        public DateTime? DeathLineDate { get; set; }

        public SaveDeathLineModel()
        {
        }
        public SaveDeathLineModel(DateTime ym, int userId, DateTime deathLineDate)
        {
            YM = ym;
            UserId = userId;
            DeathLineDate = deathLineDate;
        }
    }
}