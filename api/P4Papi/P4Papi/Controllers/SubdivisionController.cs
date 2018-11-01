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
    [RoutePrefix("api/Subdivision")]
    [Authorize]
    public class SubdivisionController : ApiController
    {
        // GET api/Subdivision
        public List<SubdivisionListModel> Get()
        {
            List<SubdivisionListModel> subdivisionModels = new List<SubdivisionListModel>();
            List<Subdivision> subdivisions = RepositoryFactory.SubdivisionRepository.GetAllSubdivision();
            foreach (Subdivision subdivision in subdivisions)
            {
                SubdivisionListModel subdivisionModel = new SubdivisionListModel(subdivision);
                subdivisionModels.Add(subdivisionModel);
            }
            return subdivisionModels;
        }

        // GET api/Subdivision/1
        public SubdivisionListModel Get(int id)
        {
            Subdivision subdivision = RepositoryFactory.SubdivisionRepository.GetSubdivisionById(id);
            if (subdivision != null)
            {
                SubdivisionListModel subdivisionModel = new SubdivisionListModel(subdivision);
                return subdivisionModel;
            }
            else
            {
                return null;
            }
        }

        // GET api/Subdivision/GetSubdivisionBySubdivisionId?subdivisionId=1
        public List<SubdivisionListModel> GetSubdivisionBySubdivisionId(int subdivisionId)
        {
            List<SubdivisionListModel> subdivisionModels = new List<SubdivisionListModel>();
            List<Subdivision> subdivisions = RepositoryFactory.SubdivisionRepository.GetSubdivisionByDepartmentId(subdivisionId);
            foreach (Subdivision subdivision in subdivisions)
            {
                SubdivisionListModel positionModel = new SubdivisionListModel(subdivision);
                subdivisionModels.Add(positionModel);
            }
            return subdivisionModels;
        }

        // POST api/Subdivision/InsertSubdivision
        [HttpPost]
        [Route("InsertSubdivision")]
        public ResultModel InsertSubdivision([FromBody] SaveSubdivisionModel saveSubdivisionModel)
        {
            try
            {
                ResultModel result = new ResultModel();
                string insertErrorString = string.Empty;
                bool insertResult = RepositoryFactory.SubdivisionRepository.InsertSubdivision(saveSubdivisionModel.Name, saveSubdivisionModel.DepartmentId, saveSubdivisionModel.OrderInDepartment, out insertErrorString);
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

        // PUT api/Subdivision/5
        public ResultModel Put(int id, [FromBody] SaveSubdivisionModel saveSubdivisionModel)
        {
            try
            {
                ResultModel result = new ResultModel();
                bool hasSubdivision = RepositoryFactory.SubdivisionRepository.HasSubdivision(id);
                if (!hasSubdivision)
                {
                    return new ResultModel(ResultEnum.FAILED, @"ยังไม่มี Subdivision นี้ในระบบ");
                }
                else
                {
                    string updateErrorString = string.Empty;
                    bool updateResult = RepositoryFactory.SubdivisionRepository.UpdateSubdivision(id, saveSubdivisionModel.Name, saveSubdivisionModel.DepartmentId, saveSubdivisionModel.OrderInDepartment, out updateErrorString);
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

        //DELETE api/Subdivision/5
        public ResultModel Delete(int id)
        {
            try
            {
                ResultModel result = new ResultModel();
                bool hasSubdivision = RepositoryFactory.SubdivisionRepository.HasSubdivision(id);
                if (!hasSubdivision)
                {
                    return new ResultModel(ResultEnum.FAILED, @"ยังไม่มี Subdivision นี้ในระบบ");
                }
                else
                {
                    string deleteErrorString = string.Empty;
                    bool deleteResult = RepositoryFactory.SubdivisionRepository.DeleteSubdivision(id, out deleteErrorString);
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