using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Models
{
    public class UserDDLModel
    {
        public int UserId { get; set; }

        public string UserFullName { get; set; }

        public UserDDLModel()
        {

        }
        public UserDDLModel(User user)
        {
            if(user != null)
            {
                UserId = user.UserId;
                UserFullName = string.Format("{0} {1}", user.Name, user.Lastname);
            }
        }
    }


}