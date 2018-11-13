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
    [RoutePrefix("api/User")]
    [Authorize]
    public class UserController : ApiController
    {
        // GET api/User
        public List<UserDisplayModel> Get()
        {
            List<UserDisplayModel> userModels = RepositoryFactory.UserRepository.GetAllUser();
            return userModels;
        }
        // GET api/User/5
        public UserDisplayModel Get(int id)
        {
            UserDisplayModel resultUser = RepositoryFactory.UserRepository.GetUserData(id);
            return resultUser;
        }
        // POST api/User/InsertUser
        [HttpPost]
        [Route("InsertUser")]
        public ResultModel InsertUser([FromBody] SaveUserModel saveUserModel)
        {
            try
            {
                ResultModel result = new ResultModel();
                string insertErrorString = string.Empty;
                bool insertResult = RepositoryFactory.UserRepository.InsertUser(saveUserModel.Name, saveUserModel.LastName, saveUserModel.BirthDate, saveUserModel.PositionId, saveUserModel.StartDate, saveUserModel.SubdivisionId, saveUserModel.Enabled, out insertErrorString);
                if (insertResult)
                {
                    return new ResultModel(ResultEnum.OK, string.Empty);
                }
                else
                {
                    return new ResultModel(ResultEnum.FAILED, insertErrorString);
                }
            }
            catch (Exception ex)
            {
                return new ResultModel(ResultEnum.FAILED, ex.Message);
            }
        }
        // PUT api/User/5
        public ResultModel Put(int id, [FromBody] SaveUserModel saveUserModel)
        {
            try
            {
                ResultModel result = new ResultModel();
                bool hasUser = RepositoryFactory.UserRepository.HasUser(id);
                if (!hasUser)
                {
                    return new ResultModel(ResultEnum.FAILED, @"ยังไม่มี User นี้ในระบบ");
                }
                else
                {
                    string updateErrorString = string.Empty;
                    bool updateResult = RepositoryFactory.UserRepository.UpdateUser(id, saveUserModel.Name, saveUserModel.LastName, saveUserModel.BirthDate, saveUserModel.PositionId, saveUserModel.StartDate, saveUserModel.SubdivisionId, saveUserModel.Enabled, out updateErrorString);
                    if (updateResult)
                    {
                        return new ResultModel(ResultEnum.OK, string.Empty);
                    }
                    else
                    {
                        return new ResultModel(ResultEnum.FAILED, updateErrorString);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResultModel(ResultEnum.FAILED, ex.Message);
            }
        }
        //DELETE api/User/5
        public ResultModel Delete(int id)
        {
            try
            {
                ResultModel result = new ResultModel();
                bool hasUser = RepositoryFactory.UserRepository.HasUser(id);
                if (!hasUser)
                {
                    return new ResultModel(ResultEnum.FAILED, @"ยังไม่มี User นี้ในระบบ");
                }
                else
                {
                    string deleteErrorString = string.Empty;
                    bool deleteResult = RepositoryFactory.UserRepository.DeleteUser(id, out deleteErrorString);
                    if (deleteResult)
                    {
                        return new ResultModel(ResultEnum.OK, string.Empty);
                    }
                    else
                    {
                        return new ResultModel(ResultEnum.FAILED, deleteErrorString);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResultModel(ResultEnum.FAILED, ex.Message);
            }
        }
    }
}