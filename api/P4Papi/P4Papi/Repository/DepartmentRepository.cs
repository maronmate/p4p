using P4Papi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Repository
{
    public class DepartmentRepository : IDisposable
    {
        private PfourPEntities _ctx;

        public DepartmentRepository()
        {
            _ctx = new PfourPEntities();
        }

        public List<Department> GetAllDepartment()
        {
            List<Department> departments = _ctx.Departments
                .Include("Positions")
                .Include("Subdivisions")
                .OrderBy(a => a.ReportOrder).ToList();
            return departments;
        }
        public List<Department> GetAllDepartmentOrderByName()
        {
            List<Department> departments = _ctx.Departments.OrderBy(a => a.Name).ToList();
            return departments;
        }
        public Department GetDepartmentByid(int departmentId)
        {
            var departments = _ctx.Departments
                .Include("Positions")
                .Include("Subdivisions")
                .Where(d => d.DepartmentId == departmentId); ;
            if (departments.Count() > 0)           
                  return departments.First();
            else
                return null;
        }
        public bool HasDepartment(int departmentId)
        {
            var departments = _ctx.Departments.Where(d => d.DepartmentId == departmentId); ;
            if (departments.Count() > 0)
                return true;
            else
                return false;
        }
        public bool InsertDepartment(string name,int reportOrder,bool showInreport, out string error)
        {
            try
            {
                error = string.Empty;
                Department newDepartment = new Department();
                newDepartment.Name = name;
                newDepartment.ReportOrder = reportOrder;
                newDepartment.ShowInReport = showInreport;

                _ctx.Departments.Add(newDepartment);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public bool UpdateDepartment(int departmentId, string name, int reportOrder, bool showInreport, out string error)
        {
            try
            {
                error = string.Empty;
                Department updateDepartment = GetDepartmentByid(departmentId);
                if (updateDepartment != null)
                {
                    updateDepartment.Name = name;
                    updateDepartment.ReportOrder = reportOrder;
                    updateDepartment.ShowInReport = showInreport;
                    _ctx.SaveChanges();
                    return true;
                }
                else
                {
                    error = "No Department data in database";
                    return false;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public bool DeleteDepartment(int departmentId, out string error)
        {
            try
            {
                error = string.Empty;
                Department removeDepartment = GetDepartmentByid(departmentId);
                _ctx.Departments.Remove(removeDepartment);
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