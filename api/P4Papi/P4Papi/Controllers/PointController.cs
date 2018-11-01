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
    [RoutePrefix("api/point")]
    [Authorize]
    public class PointController : ApiController
    {
        // GET api/Point/GetPointsForUser?userId=1&numberOfRow=0
        public List<PointByUserModel> GetPointsForUser(int userId, int numberOfRow)
        {
            List<PointByUserModel> result = RepositoryFactory.PointRepository.GetPointByUser(userId, numberOfRow);
            return result;
        }

        // POST api/Point/InsertPoint
        [HttpPost]
        [Route("InsertPoint")]
        public ResultModel InsertPoint([FromBody] SavePointRequestModel savePointRequest)
        {
            try
            {
                ResultModel result = new ResultModel();
                bool isInputInLine = InputInLine(savePointRequest.YM);
                if (!isInputInLine)
                {
                    return new ResultModel(ResultEnum.FAILED, @"หมดเวลาลงข้อมูลของเดือนนี้แล้ว กรุณาติดต่อAdmin");
                }
                else
                {
                    // This point has data in database already, better to use put method for update.
                    if (RepositoryFactory.PointRepository.HasPointByKeyValues(savePointRequest.UserId, savePointRequest.YM))
                    {
                        return new ResultModel(ResultEnum.FAILED, @"คุณได้ลงคะแนนของเดือนนี้ไปแล้ว หากต้องการแก้ไขข้อมูลกรุณาเลือกแก้ไข");
                    }
                    else
                    {
                        string insertErrorString = string.Empty;
                        bool insertResult = RepositoryFactory.PointRepository.InsertPoint(savePointRequest.UserId, savePointRequest.YM, savePointRequest.Point, savePointRequest.Remark, out insertErrorString);
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
            }
            catch (Exception ex)
            {
                return new ResultModel(ResultEnum.FAILED, ex.Message);
            }
        }

        // POST api/Point/UpdatePoint
        [HttpPost]
        [Route("UpdatePoint")]
        public ResultModel UpdatePoint([FromBody] SavePointRequestModel savePointRequest)
        {
            try
            {
                ResultModel result = new ResultModel();
                bool isInputInLine = InputInLine(savePointRequest.YM);
                if (!isInputInLine)
                {
                    return new ResultModel(ResultEnum.FAILED, @"หมดเวลาลงข้อมูลของเดือนนี้แล้ว กรุณาติดต่อAdmin");
                }
                else
                {
                    if (RepositoryFactory.PointRepository.HasPointByKeyValues(savePointRequest.UserId, savePointRequest.YM))
                    {
                        string updateErrorString = string.Empty;
                        bool updateResult = RepositoryFactory.PointRepository.UpdatePoint(savePointRequest.UserId, savePointRequest.YM, savePointRequest.Point, savePointRequest.Remark, out updateErrorString);
                        if (updateResult)
                        {
                            return new ResultModel(ResultEnum.OK, string.Empty);
                        }
                        else
                        {
                            return new ResultModel(ResultEnum.FAILED, updateErrorString);
                        }
                    }
                    else
                    {                       
                        return new ResultModel(ResultEnum.FAILED, @"คุณยังไม่ได้ลงคะแนนของเดือนนี้ ไม่สามารถแก้ไขได้");
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResultModel(ResultEnum.FAILED, ex.Message);
            }
        }

        // POST api/Point/DeletePoint
        [HttpPost]
        [Route("DeletePoint")]
        public ResultModel DeletePoint([FromBody] SavePointRequestModel deletePointRequest)
        {
            try
            {
                ResultModel result = new ResultModel();
                bool isInputInLine = InputInLine(deletePointRequest.YM);
                if (!isInputInLine)
                {
                    return new ResultModel(ResultEnum.FAILED, @"หมดเวลาลงข้อมูลของเดือนนี้แล้ว กรุณาติดต่อAdmin");
                }
                else
                {
                    if (RepositoryFactory.PointRepository.HasPointByKeyValues(deletePointRequest.UserId, deletePointRequest.YM))
                    {
                        string deleteErrorString = string.Empty;
                        bool deleteResult = RepositoryFactory.PointRepository.DeletePoint(deletePointRequest.UserId, deletePointRequest.YM, out deleteErrorString);
                        if (deleteResult)
                        {
                            return new ResultModel(ResultEnum.OK, string.Empty);
                        }
                        else
                        {
                            return new ResultModel(ResultEnum.FAILED, deleteErrorString);
                        }
                    }
                    else
                    {
                        return new ResultModel(ResultEnum.FAILED, @"ไม่มีคะแนนนี้ในระบบ");
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResultModel(ResultEnum.FAILED, ex.Message);
            }
        }
        private bool InputInLine(DateTime ym)
        {
            bool inLine = false;
            DateTime currentDate = DateTime.Now.Date;
                        
            if (RepositoryFactory.DeathLineRepository.HasDeathLine(ym))
            {
                inLine = RepositoryFactory.DeathLineRepository.IsNotOverDeathLine(ym, currentDate);
            }
            else
            {
                // No Death line in DB set default date to 10/curent month +1 
                DateTime deathLine = new DateTime(ym.Year, ym.Month + 1,10 );
                if (deathLine >= currentDate)
                    inLine = true;               
            }
            return inLine;
        }
    }
}