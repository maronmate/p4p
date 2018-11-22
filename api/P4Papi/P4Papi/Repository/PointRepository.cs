using P4Papi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Repository
{
    public class PointRepository : IDisposable
    {
        private PfourPEntities _ctx;

        public PointRepository()
        {
            _ctx = new PfourPEntities();
        }

        public List<PointByUserModel> GetPointByUser(int userId, int numberOfRow)
        {
            //var output = (from point in _ctx.Points
            //              join user in _ctx.Users on point.UserId equals user.UserId
            //              join position in _ctx.Positions on user.PositionId equals position.PositionId
            //              where point.UserId == userId
            //              select point);
            //List<Point> output = null;
            //if (numberOfRow > 0)
            //     output = _ctx.Points.Where(point => point.UserId == userId).Take(numberOfRow).OrderByDescending(point => point.YM).ToList();
            //else
            var output = _ctx.Points
                .Include("User").Include("User.Position").Include("User.Position.Department")
                .Include("User.Subdivision")
                .Where(point => point.UserId == userId).OrderByDescending(point => point.YM).ToList();
            if (numberOfRow > 0)
                output = output.Take(numberOfRow).ToList();

            List<PointByUserModel> pointUserList = new List<PointByUserModel>();
            foreach (var point in output)
            {
                PointByUserModel pointUser = new PointByUserModel(point);
                pointUserList.Add(pointUser);
            }

            return pointUserList;
        }

        //public List<PointByUserModel> SearchPointForInputPoint(int departmentId, int subdivisionId,int positionId, int userId,DateTime? ymStart,DateTime? ymEnd, Dictionary<string,string> sort, int numberOfRow)
        //{
        //    var output = _ctx.Points;
        //    if (departmentId > 0)
        //        output.Where(point => point.User.Position.DepartmentId == departmentId);
        //    if(subdivisionId > 0)
        //        output.Where(point => point.User.SubdivisionId == subdivisionId);
        //    if (positionId > 0)
        //        output.Where(point => point.User.PositionId == positionId);
        //    if (userId > 0)
        //        output.Where(point => point.UserId == userId);
        //    if (ymStart.HasValue)
        //        output.Where(point => point.YM >= ymStart);
        //    if (ymEnd.HasValue)
        //        output.Where(point => point.YM <= ymEnd);
        //}

        public Point GetPointByKeyValues(int userId, DateTime ym)
        {
            var output = _ctx.Points.Where(point => point.UserId == userId && point.YM == ym).ToList();
            if (output.Count > 0)
                return output[0];
            else
                return null;
        }

        public bool HasPointByKeyValues(int userId, DateTime ym)
        {
            var output = _ctx.Points.Where(point => point.UserId == userId && point.YM == ym).ToList();
            if (output.Count > 0)
                return true;
            else
                return false;
        }
        public bool UpdatePoint(int userId, DateTime ym, int point, string remark, out string error)
        {
            try
            {
                error = string.Empty;
                Point updatePoint = GetPointByKeyValues(userId, ym);
                if (updatePoint != null)
                {
                    updatePoint.MonthlyPoint = point;
                    updatePoint.Remark = remark;
                    updatePoint.ModifyDate = DateTime.Now;
                    _ctx.SaveChanges();
                    return true;
                }
                else
                {
                    error = "No Point data in database";
                    return false;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public bool InsertPoint( int userId,  DateTime ym,  int point,  string remark,out string error)
        {
            try
            {
                error = string.Empty;
                Point newPoint = new Point();
                newPoint.UserId = userId;
                newPoint.YM = ym;
                newPoint.MonthlyPoint = point;
                newPoint.Remark = remark;
                newPoint.ModifyDate = DateTime.Now;

                _ctx.Points.Add(newPoint);
                _ctx.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public bool DeletePoint(int userId, DateTime ym, out string error)
        {
            try
            {
                error = string.Empty;
                Point removePoint = GetPointByKeyValues(userId, ym);
                _ctx.Points.Remove(removePoint);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}