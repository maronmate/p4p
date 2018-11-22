using P4Papi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Repository
{
    public class PositionRepository : IDisposable
    {
        private PfourPEntities _ctx;
        public PositionRepository()
        {
            _ctx = new PfourPEntities();
        }
        public List<Position> GetAllPositions()
        {
            List<Position> positions = _ctx.Positions
                .Include("Department")
                .Include("Users")
                .OrderBy(a => a.Name).ToList();
            return positions;
        }
        public Position GetPositionsById(int positionId)
        {
            var positions = _ctx.Positions
                .Include("Department")
                .Include("Users")
                .Where(p => p.PositionId == positionId); ;
            if (positions.Count() > 0)
                return positions.First();
            else
                return null;
        }
        public bool HasPosition(int positionId)
        {
            var position = _ctx.Positions.Where(p => p.PositionId == positionId); ;
            if (position.Count() > 0)
                return true;
            else
                return false;
        }
        public List<Position> GetPositionsByDepartmentId(int departmentId)
        {
            var positions = _ctx.Positions.Where(d => d.DepartmentId == departmentId).OrderBy(a => a.Name).ToList() ;
            return positions;
        }
        public bool InsertPosition(string name, int departmentId,double tragetPoint, out string error)
        {
            try
            {
                error = string.Empty;
                Position newPosition = new Position();
                newPosition.Name = name;
                newPosition.DepartmentId = departmentId;
                newPosition.TargetPoint = tragetPoint;

                _ctx.Positions.Add(newPosition);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public bool UpdatePosition(int positionId, string name, int departmentId, double tragetPoint, out string error)
        {
            try
            {
                error = string.Empty;
                Position updatePosition = GetPositionsById(positionId);
                if (updatePosition != null)
                {
                    updatePosition.Name = name;
                    updatePosition.DepartmentId = departmentId;
                    updatePosition.TargetPoint = tragetPoint;
                    _ctx.SaveChanges();
                    return true;
                }
                else
                {
                    error = "No Position data in database";
                    return false;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public bool DeletePosition(int positionId, out string error)
        {
            try
            {
                error = string.Empty;
                Position removePosition = GetPositionsById(positionId);
                _ctx.Positions.Remove(removePosition);
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