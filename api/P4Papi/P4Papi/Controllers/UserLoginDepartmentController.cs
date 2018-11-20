using P4Papi.Models;
using P4Papi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using static P4Papi.Models.ResultModel;
namespace P4Papi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/UserLoginDepartment")]
    [Authorize]
    public class UserLoginDepartmentController : ApiController
    {
    }
}