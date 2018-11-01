using P4Papi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Repository
{
    public class SubdivisionRepository : IDisposable
    {
        private PfourPEntities _ctx;

        public SubdivisionRepository()
        {
            _ctx = new PfourPEntities();
        }
        public List<Subdivision> GetAllSubdivision()
        {
            List<Subdivision> subdivision = _ctx.Subdivisions.OrderBy(a => a.Name).ToList();
            return subdivision;
        }
        public Subdivision GetSubdivisionById(int subdivisionId)
        {
            var subdivision = _ctx.Subdivisions.Where(d => d.SubdivisionId == subdivisionId); ;
            if (subdivision.Count() > 0)
                return subdivision.First();
            else
                return null;
        }
        public bool HasSubdivision(int subdivisionId)
        {
            var subdivision = _ctx.Subdivisions.Where(d => d.SubdivisionId == subdivisionId); ;
            if (subdivision.Count() > 0)
                return true;
            else
                return false;
        }
        public List<Subdivision> GetSubdivisionByDepartmentId(int departmentId)
        {
            var subdivisions = _ctx.Subdivisions.Where(d => d.DepartmentId == departmentId).OrderBy(a => a.Name).ToList();
            return subdivisions;
        }
        public bool InsertSubdivision(string name, int departmentId, int orderInDepartment, out string error)
        {
            try
            {
                error = string.Empty;
                Subdivision newSubdivision = new Subdivision();
                newSubdivision.Name = name;
                newSubdivision.DepartmentId = departmentId;
                newSubdivision.OrderInDepartment = orderInDepartment;


                _ctx.Subdivisions.Add(newSubdivision);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public bool UpdateSubdivision(int subdivisionId, string name, int departmentId, int orderInDepartment, out string error)
        {
            try
            {
                error = string.Empty;
                Subdivision updateSubdivision = GetSubdivisionById(subdivisionId);
                if (updateSubdivision != null)
                {
                    updateSubdivision.Name = name;
                    updateSubdivision.DepartmentId = departmentId;
                    updateSubdivision.OrderInDepartment = orderInDepartment;
                    _ctx.SaveChanges();
                    return true;
                }
                else
                {
                    error = "No Subdivision data in database";
                    return false;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public bool DeleteSubdivision(int subdivisionId, out string error)
        {
            try
            {
                error = string.Empty;
                Subdivision removeSubdivision = GetSubdivisionById(subdivisionId);
                _ctx.Subdivisions.Remove(removeSubdivision);
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