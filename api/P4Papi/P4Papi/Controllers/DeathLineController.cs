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
    [RoutePrefix("api/deathline")]
    [Authorize]
    public class DeathLineController : ApiController
    {
        // GET api/DeathLine/GetDeathLineList?numberOfRow=0
        public List<DeathLineListModel> GetDeathLineList(int numberOfRow)
        {
            List<DeathLineListModel> result = RepositoryFactory.DeathLineRepository.GetDeathLineList( numberOfRow);
            return result;
        }

        // POST api/DeathLine/InsertDeathLine
        [HttpPost]
        [Route("InsertDeathLine")]
        public ResultModel InsertDeathLine([FromBody] SaveDeathLineModel saveDeathLineModel)
        {
            try
            {
                ResultModel result = new ResultModel();
                bool hasDeathLine = RepositoryFactory.DeathLineRepository.HasDeathLine(saveDeathLineModel.YM);
                if (hasDeathLine)
                {
                    return new ResultModel(ResultEnum.FAILED, @"มี Death line ของเดือนนี้ในระบบแล้ว");
                }
                else
                {
                    string insertErrorString = string.Empty;
                    bool insertResult = RepositoryFactory.DeathLineRepository.InsertDeathLine(saveDeathLineModel.YM, saveDeathLineModel.DeathLineDate.Value, saveDeathLineModel.UserId, out insertErrorString);
                    if (insertResult)
                    {
                        return new ResultModel(ResultEnum.OK, string.Empty);
                    }
                    else
                    {
                        return new ResultModel(ResultEnum.FAILED, insertErrorString);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResultModel(ResultEnum.FAILED, ex.Message);
            }
        }

        // POST api/DeathLine/UpdateDeathLine
        [HttpPost]
        [Route("UpdateDeathLine")]
        public ResultModel UpdateDeathLine([FromBody] SaveDeathLineModel saveDeathLineModel)
        {
            try
            {
                ResultModel result = new ResultModel();
                bool hasDeathLine = RepositoryFactory.DeathLineRepository.HasDeathLine(saveDeathLineModel.YM);
                if (!hasDeathLine)
                {
                    return new ResultModel(ResultEnum.FAILED, @"ยังไม่มี Death line เดือนนี้ในระบบ");
                }
                else
                {
                    string updateErrorString = string.Empty;
                    bool updateResult = RepositoryFactory.DeathLineRepository.UpdateDeathLine(saveDeathLineModel.YM, saveDeathLineModel.DeathLineDate.Value, saveDeathLineModel.UserId, out updateErrorString);
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

        // POST api/DeathLine/DeleteDeathLine
        [HttpPost]
        [Route("DeleteDeathLine")]
        public ResultModel DeleteDeathLine([FromBody] SaveDeathLineModel saveDeathLineModel)
        {
            try
            {
                ResultModel result = new ResultModel();
                bool hasDeathLine = RepositoryFactory.DeathLineRepository.HasDeathLine(saveDeathLineModel.YM);
                if (!hasDeathLine)
                {
                    return new ResultModel(ResultEnum.FAILED, @"ยังไม่มี Death line เดือนนี้ในระบบ");
                }
                else
                {
                    string deleteErrorString = string.Empty;
                    bool deleteResult = RepositoryFactory.DeathLineRepository.DeleteDeathLine(saveDeathLineModel.YM, out deleteErrorString);
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