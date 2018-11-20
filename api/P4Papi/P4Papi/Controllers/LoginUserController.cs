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
    [RoutePrefix("api/LoginUser")]
    [Authorize]
    public class LoginUserController : ApiController
    {
        // GET api/LoginUser
        public List<UserLoginDisplayModel> Get()
        {
            List<UserLoginDisplayModel> loginModels = RepositoryFactory.LoginUserRepository.GetUserLoginDataList();
            return loginModels;
        }

        [HttpPost]
        [Route("InsertLoginUser")]
        public ResultModel InsertLoginUser([FromBody] SaveUserLoginModel saveUserLoginModel)
        {
            try
            {
                ResultModel result = new ResultModel();
                string insertErrorString = string.Empty;
                int loginId = 0;
                bool insertResult = RepositoryFactory.LoginUserRepository.InsertLoginUser(saveUserLoginModel.Username, saveUserLoginModel.Password, saveUserLoginModel.IsAdmin, saveUserLoginModel.Name, out insertErrorString, out loginId);
                if (insertResult && loginId != 0)
                {
                    bool saveULDResult = RepositoryFactory.UserLoginDepartmentRepository.ManageUserLoginDepartment(loginId, saveUserLoginModel.DepartmentIdList,out insertErrorString);
                    if(saveULDResult)
                        return new ResultModel(ResultEnum.OK, string.Empty);
                    else
                        return new ResultModel(ResultEnum.FAILED, insertErrorString);
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

        // PUT api/LoginUser/5
        public ResultModel Put(int id, [FromBody] SaveUserLoginModel saveUserLoginModel)
        {
            try
            {
                ResultModel result = new ResultModel();
                bool hasLogin = RepositoryFactory.LoginUserRepository.HasLoginUser(id);
                if (!hasLogin)
                {
                    return new ResultModel(ResultEnum.FAILED, @"ยังไม่มี Login นี้ในระบบ");
                }
                else
                {
                    string updateErrorString = string.Empty;
                    bool updateResult = RepositoryFactory.LoginUserRepository.UpdateLoginUser(id, saveUserLoginModel.Username, saveUserLoginModel.Password, saveUserLoginModel.IsAdmin, saveUserLoginModel.Name, out updateErrorString);
                    if (updateResult)
                    {
                        bool saveULDResult = RepositoryFactory.UserLoginDepartmentRepository.ManageUserLoginDepartment(id, saveUserLoginModel.DepartmentIdList, out updateErrorString);
                        if (saveULDResult)
                            return new ResultModel(ResultEnum.OK, string.Empty);
                        else
                            return new ResultModel(ResultEnum.FAILED, updateErrorString);

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

        //DELETE api/LoginUser/5
        public ResultModel Delete(int id)
        {
            try
            {
                ResultModel result = new ResultModel();
                bool hasLogin = RepositoryFactory.LoginUserRepository.HasLoginUser(id);
                if (!hasLogin)
                {
                    return new ResultModel(ResultEnum.FAILED, @"ยังไม่มี Login นี้ในระบบ");
                }
                else
                {
                    string deleteErrorString = string.Empty;

                    bool deleteResult = RepositoryFactory.UserLoginDepartmentRepository.DeleteUserLoginDepartmentByUserLoginId(id, out deleteErrorString);
                    if (deleteResult)
                    {
                        bool deleteLogin = RepositoryFactory.LoginUserRepository.DeleteLoginUser(id, out deleteErrorString);
                        if (deleteLogin)
                            return new ResultModel(ResultEnum.OK, string.Empty);
                        else
                            return new ResultModel(ResultEnum.FAILED, deleteErrorString);
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