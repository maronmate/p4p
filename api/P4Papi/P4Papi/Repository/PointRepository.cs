using P4Papi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Data.Entity.Core.Objects;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

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

        public int CountSearchPointForInputPoint(PointFilters pointFilters)
        {
            var output = _ctx.Points
                .Include("User").Include("User.Position").Include("User.Position.Department")
                .Include("User.Subdivision");
            //filter
            if (pointFilters != null)
            {
                if (pointFilters.DepartmentId.HasValue && pointFilters.DepartmentId > 0)
                    output.Where(point => point.User.Position.DepartmentId == pointFilters.DepartmentId);
                if (pointFilters.SubdivisionId.HasValue && pointFilters.SubdivisionId > 0)
                    output.Where(point => point.User.SubdivisionId == pointFilters.SubdivisionId);
                if (pointFilters.PositionId.HasValue && pointFilters.PositionId > 0)
                    output.Where(point => point.User.PositionId == pointFilters.PositionId);
                if (pointFilters.UserId.HasValue && pointFilters.UserId > 0)
                    output.Where(point => point.UserId == pointFilters.UserId);
                if (pointFilters.YMStart.HasValue)
                    output.Where(point => point.YM >= pointFilters.YMStart);
                if (pointFilters.YMEnd.HasValue)
                    output.Where(point => point.YM <= pointFilters.YMEnd);
            }
            return output.Count();
        }
        public List<PointByUserModel> SearchPointForInputPoint(PointFilters pointFilters, Sorter sorter, PagingTable pagingTable)
        {
            var output = _ctx.Points
                .Include("User").Include("User.Position").Include("User.Position.Department")
                .Include("User.Subdivision");

            //filter
            if (pointFilters != null)
            {
                if (pointFilters.DepartmentId.HasValue && pointFilters.DepartmentId > 0)
                    output.Where(point => point.User.Position.DepartmentId == pointFilters.DepartmentId);
                if (pointFilters.SubdivisionId.HasValue && pointFilters.SubdivisionId > 0)
                    output.Where(point => point.User.SubdivisionId == pointFilters.SubdivisionId);
                if (pointFilters.PositionId.HasValue && pointFilters.PositionId > 0)
                    output.Where(point => point.User.PositionId == pointFilters.PositionId);
                if (pointFilters.UserId.HasValue && pointFilters.UserId > 0)
                    output.Where(point => point.UserId == pointFilters.UserId);
                if (pointFilters.YMStart.HasValue)
                    output.Where(point => point.YM >= pointFilters.YMStart);
                if (pointFilters.YMEnd.HasValue)
                    output.Where(point => point.YM <= pointFilters.YMEnd);
            }

            //sorter
            if (sorter != null)
            {
                if(!string.IsNullOrEmpty(sorter.FieldName))
                {
                    Type pointModelType = typeof(PointByUserModel);
                    var property = pointModelType.GetProperty(sorter.FieldName);
                    if(property != null)
                    {
                        string pointModelPropertyName = property.Name;
                        if (pointModelPropertyName == "YM")
                        {
                            if (sorter.OnDesc)
                                output.OrderByDescending(p => p.YM);
                            else
                                output.OrderBy(p => p.YM);
                        }
                        else if (pointModelPropertyName == "UserId")
                        {
                            if (sorter.OnDesc)
                                output.OrderByDescending(p => p.UserId);
                            else
                                output.OrderBy(p => p.UserId);
                        }
                        else if (pointModelPropertyName == "Name")
                        {
                            if (sorter.OnDesc)
                                output.OrderByDescending(p => p.User.Name);
                            else
                                output.OrderBy(p => p.User.Name);
                        }
                        else if (pointModelPropertyName == "Lastname")
                        {
                            if (sorter.OnDesc)
                                output.OrderByDescending(p => p.User.Lastname);
                            else
                                output.OrderBy(p => p.User.Lastname);
                        }
                        else if (pointModelPropertyName == "PositionName")
                        {
                            if (sorter.OnDesc)
                                output.OrderByDescending(p => p.User.Position.Name);
                            else
                                output.OrderBy(p => p.User.Position.Name);
                        }
                        else if (pointModelPropertyName == "DepartmentName")
                        {
                            if (sorter.OnDesc)
                                output.OrderByDescending(p => p.User.Position.Department.Name);
                            else
                                output.OrderBy(p => p.User.Position.Department.Name);
                        }
                        else if (pointModelPropertyName == "SubdivisionName")
                        {
                            if (sorter.OnDesc)
                                output.OrderByDescending(p => p.User.Subdivision.Name);
                            else
                                output.OrderBy(p => p.User.Subdivision.Name);
                        }
                        else if (pointModelPropertyName == "Point")
                        {
                            if (sorter.OnDesc)
                                output.OrderByDescending(p => p.MonthlyPoint);
                            else
                                output.OrderBy(p => p.MonthlyPoint);
                        }
                        else if (pointModelPropertyName == "Remark")
                        {
                            if (sorter.OnDesc)
                                output.OrderByDescending(p => p.Remark);
                            else
                                output.OrderBy(p => p.Remark);
                        }
                        else if (pointModelPropertyName == "TargetPoint")
                        {
                            if (sorter.OnDesc)
                                output.OrderByDescending(p => p.User.Position.TargetPoint);
                            else
                                output.OrderBy(p => p.User.Position.TargetPoint);
                        }
                        else if (pointModelPropertyName == "ModifyDate")
                        {
                            if (sorter.OnDesc)
                                output.OrderByDescending(p => p.ModifyDate);
                            else
                                output.OrderBy(p => p.ModifyDate);
                        }
                    }                  
                }
            }

            //paging
            if(pagingTable != null)
            {
                if (pagingTable.CurrentPage < 1)
                    pagingTable.CurrentPage = 1;
                if (pagingTable.PageSize > 0)
                    output.ToPagedList(pagingTable.CurrentPage, pagingTable.PageSize);
            }

            List<Point> rawPoint = output.ToList();
            List<PointByUserModel> points = new List<PointByUserModel>();
            foreach (Point point in rawPoint)
            {
                PointByUserModel pm = new PointByUserModel(point);
                points.Add(pm);
            }
            return points;
        }

        public ObjectResult<UserPoint> GetUserPointA(PointFilters pointFilters, Sorter sorter)
        {
            string procedureType = "REQUEST_POINT_LIST";
            string ymStart = "";
            string ymEnd = "";

            if (pointFilters != null)
            {
                if (pointFilters.YMStart.HasValue)
                    ymStart = pointFilters.YMStart.Value.ToString("yyyy-MM-dd");
                if (pointFilters.YMEnd.HasValue)
                    ymEnd = pointFilters.YMEnd.Value.ToString("yyyy-MM-dd");
                if (sorter == null)
                    sorter = new Sorter();

                ObjectResult<UserPoint> output = _ctx.UserPointProcedure(procedureType, pointFilters.DepartmentId, pointFilters.PositionId, pointFilters.SubdivisionId, pointFilters.UserId, ymStart, ymEnd, sorter.FieldName, sorter.OnDesc);
                return output;
            }
            return null;
        }

        public DataTable GetUserPoint(PointFilters pointFilters, Sorter sorter)
        {
            string ymStart = "";
            string ymEnd = "";

            if (pointFilters != null)
            {
                if (pointFilters.YMStart.HasValue)
                    ymStart = pointFilters.YMStart.Value.ToString("yyyy-MM-dd");
                if (pointFilters.YMEnd.HasValue)
                    ymEnd = pointFilters.YMEnd.Value.ToString("yyyy-MM-dd");
                if (sorter == null)
                    sorter = new Sorter();

                DataSet ds = new StoredProcedures.GetDataStoredProcedures().GetDataUserPoint(pointFilters.DepartmentId, pointFilters.PositionId, pointFilters.SubdivisionId, pointFilters.UserId, ymStart, ymEnd);
                if(ds != null && ds.Tables.Count >0)
                {
                    DataTable dt = ds.Tables[0];
                    
                    return dt;
                }
            }
            return null;
        }

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