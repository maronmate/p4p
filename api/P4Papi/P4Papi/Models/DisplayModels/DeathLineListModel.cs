using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Models
{
    public class DeathLineListModel
    {
        public DateTime YM { get; set; }
        public DateTime DeathLineDate { get; set; }
        public string ModifyName { get; set; }
        public DateTime ModifyDate { get; set; }

        public DeathLineListModel()
        {

        }
        public DeathLineListModel(DeathLine deathLine,string nameOfUser)
        {
            if (deathLine != null)
            {
                YM = deathLine.YM;
                if(deathLine.DeathLineDate.HasValue)
                    DeathLineDate = deathLine.DeathLineDate.Value;
                if (deathLine.ModifyDate.HasValue)
                    ModifyDate = deathLine.ModifyDate.Value;

                if (deathLine.ModifyUserId.HasValue)
                {
                    ModifyName = nameOfUser;
                }
            }
        }
    }
}