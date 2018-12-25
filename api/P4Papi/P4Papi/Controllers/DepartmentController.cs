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
    [RoutePrefix("api/Department")]
    [Authorize]
    public class DepartmentController : ApiController
    {

        // GET api/Department
        public List<DepartmentListModel> Get()
        {
            List<DepartmentListModel> departmentModels = new List<DepartmentListModel>();
            List<Department> departments = RepositoryFactory.DepartmentRepository.GetAllDepartment();
            foreach (Department department in departments)
            {
                List<int> positionList = department.Positions.Select(p => p.PositionId).ToList();
                int number = RepositoryFactory.UserRepository.CountUserInPositions(positionList);
                DepartmentListModel departmentModel = new DepartmentListModel(department, number);
                departmentModels.Add(departmentModel);
            }
            return departmentModels;
        }
        // GET api/Department/1
        public DepartmentListModel Get(int id)
        {
            Department department = RepositoryFactory.DepartmentRepository.GetDepartmentByid(id);
            if (department != null)
            {
                List<int> positionList = department.Positions.Select(p => p.PositionId).ToList();
                int number = RepositoryFactory.UserRepository.CountUserInPositions(positionList);
                DepartmentListModel departmentModel = new DepartmentListModel(department, number);
                return departmentModel;
            }
            else
            {
                return null;
            }
        }
        // GET api/Department/GetDepartmentDDL
        [HttpGet]
        [Route("GetDepartmentDDL")]
        public List<DepartmentDDLModel> GetDepartmentDDL()
        {
            List<DepartmentDDLModel> departmentModels = new List<DepartmentDDLModel>();
            List<Department> departments = RepositoryFactory.DepartmentRepository.GetAllDepartmentOrderByName();
            foreach (Department department in departments)
            {
                DepartmentDDLModel departmentDDLModel = new DepartmentDDLModel(department);
                departmentModels.Add(departmentDDLModel);
            }
            return departmentModels;
        }
        // GET api/Department/GetDepartmentDDLByLoginId/1
        [HttpGet]
        [Route("GetDepartmentDDLByLoginId/{loginId}")]
        public List<DepartmentDDLModel> GetDepartmentDDLByLoginId(int loginId)
        {
            List<DepartmentDDLModel> departmentModels = new List<DepartmentDDLModel>();
            List<UserLoginDepartment> userLoginDepartment = RepositoryFactory.UserLoginDepartmentRepository.GetUserLoginDepartmentForDdl(loginId);
            foreach (UserLoginDepartment uld in userLoginDepartment)
            {
                DepartmentDDLModel departmentDDLModel = new DepartmentDDLModel(uld);
                departmentModels.Add(departmentDDLModel);
            }
            return departmentModels;
        }


        // POST api/Department/InsertDepartment
        [HttpPost]
        [Route("InsertDepartment")]
        public ResultModel InsertDepartment([FromBody] SaveDepartmentModel saveDepartmentModel)
        {
            try
            {
                ResultModel result = new ResultModel();
                string insertErrorString = string.Empty;
                bool insertResult = RepositoryFactory.DepartmentRepository.InsertDepartment(saveDepartmentModel.Name, saveDepartmentModel.OrderInReport, saveDepartmentModel.ShowInReport, out insertErrorString);
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

        // PUT api/Department/5

        public ResultModel Put(int id,[FromBody] SaveDepartmentModel saveDepartmentModel)
        {
            try
            {
                ResultModel result = new ResultModel();
                bool hasDepartment = RepositoryFactory.DepartmentRepository.HasDepartment(id);
                if (!hasDepartment)
                {
                    return new ResultModel(ResultEnum.FAILED, @"ยังไม่มี Department นี้ในระบบ");
                }
                else
                {
                    string updateErrorString = string.Empty;
                    bool updateResult = RepositoryFactory.DepartmentRepository.UpdateDepartment(id, saveDepartmentModel.Name, saveDepartmentModel.OrderInReport, saveDepartmentModel.ShowInReport, out updateErrorString);
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

        //DELETE api/Department/5
        public ResultModel Delete(int id)
        {
            try
            {
                ResultModel result = new ResultModel();
                bool hasDepartment = RepositoryFactory.DepartmentRepository.HasDepartment(id);
                if (!hasDepartment)
                {
                    return new ResultModel(ResultEnum.FAILED, @"ยังไม่มี Department นี้ในระบบ");
                }
                else
                {
                    string deleteErrorString = string.Empty;
                    bool deleteResult = RepositoryFactory.DepartmentRepository.DeleteDepartment(id, out deleteErrorString);
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