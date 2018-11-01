using P4Papi.Models;
using P4Papi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace P4Papi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/User")]
    [Authorize]
    public class UserController : ApiController
    {
        // GET api/user/5
        public UserModel Get(int id)
        {
            UserModel resultUser = RepositoryFactory.UserRepository.GetUserData(id);
            return resultUser;
        }
        // GET api/user
        //public List<UserDisplayModel> Get()
        //{

        //}
    }
}