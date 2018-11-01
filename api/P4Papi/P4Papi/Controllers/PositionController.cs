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
    [RoutePrefix("api/Position")]
    [Authorize]
    public class PositionController : ApiController
    {
        // GET api/Position
        public List<PositionListModel> Get()
        {
            List<PositionListModel> positionModels = new List<PositionListModel>();
            List<Position> positions = RepositoryFactory.PositionRepository.GetAllPositions();
            foreach (Position position in positions)
            {
                PositionListModel positionModel = new PositionListModel(position);
                positionModels.Add(positionModel);
            }
            return positionModels;
        }

        // GET api/Position/1
        public PositionListModel Get(int id)
        {
            Position position = RepositoryFactory.PositionRepository.GetPositionsById(id);
            if (position != null)
            {
                PositionListModel positionModel = new PositionListModel(position);
                return positionModel;
            }
            else
            {
                return null;
            }
        }

        // GET api/Position/GetPositionByDepartmentId?departmentId=1
        public List<PositionListModel> GetPositionByDepartmentId(int departmentId)
        {
            List<PositionListModel> positionModels = new List<PositionListModel>();
            List<Position> positions = RepositoryFactory.PositionRepository.GetPositionsByDepartmentId(departmentId);
            foreach (Position position in positions)
            {
                PositionListModel positionModel = new PositionListModel(position);
                positionModels.Add(positionModel);
            }
            return positionModels;
        }

        // POST api/Position/InsertPosition
        [HttpPost]
        [Route("InsertPosition")]
        public ResultModel InsertPosition([FromBody] SavePositionModel savePositionModel)
        {
            try
            {
                ResultModel result = new ResultModel();
                string insertErrorString = string.Empty;
                bool insertResult = RepositoryFactory.PositionRepository.InsertPosition(savePositionModel.PositionName, savePositionModel.DepartmentId, savePositionModel.TargetPoint, out insertErrorString);
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

        // PUT api/Position/5
        public ResultModel Put(int id, [FromBody] SavePositionModel savePositionModel)
        {
            try
            {
                ResultModel result = new ResultModel();
                bool hasPosition = RepositoryFactory.PositionRepository.HasPosition(id);
                if (!hasPosition)
                {
                    return new ResultModel(ResultEnum.FAILED, @"ยังไม่มี Position นี้ในระบบ");
                }
                else
                {
                    string updateErrorString = string.Empty;
                    bool updateResult = RepositoryFactory.PositionRepository.UpdatePosition(id, savePositionModel.PositionName, savePositionModel.DepartmentId, savePositionModel.TargetPoint, out updateErrorString);
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

        //DELETE api/Position/5
        public ResultModel Delete(int id)
        {
            try
            {
                ResultModel result = new ResultModel();
                bool hasPosition = RepositoryFactory.PositionRepository.HasPosition(id);
                if (!hasPosition)
                {
                    return new ResultModel(ResultEnum.FAILED, @"ยังไม่มี Position นี้ในระบบ");
                }
                else
                {
                    string deleteErrorString = string.Empty;
                    bool deleteResult = RepositoryFactory.PositionRepository.DeletePosition(id, out deleteErrorString);
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