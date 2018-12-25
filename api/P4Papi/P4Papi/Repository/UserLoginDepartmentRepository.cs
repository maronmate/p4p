using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Repository
{
    public class UserLoginDepartmentRepository : IDisposable
    {
        private PfourPEntities _ctx;
        public UserLoginDepartmentRepository()
        {
            _ctx = new PfourPEntities();
        }
        public UserLoginDepartment GetUserLoginDepartment(int userLoginDepartmentId)
        {
            var output = _ctx.UserLoginDepartments.Where(ULD => ULD.UserLoginDepartmentId == userLoginDepartmentId);
            if (output.Count() > 0)
                return output.FirstOrDefault();
            else
                return null;
        }
        public List<UserLoginDepartment> GetUserLoginDepartments()
        {
            var output = _ctx.UserLoginDepartments.ToList();
            return output;
        }
        public List<UserLoginDepartment> GetUserLoginDepartmentByUserLogin(int userLoginId)
        {
            var output = _ctx.UserLoginDepartments.Where(ULD => ULD.UserLoginId == userLoginId).OrderBy(a => a.DepartmentId).ToList();
            return output;
        }
        public List<UserLoginDepartment> GetUserLoginDepartmentForDdl(int userLoginId)
        {
            var output = _ctx.UserLoginDepartments
                .Include("Department")
                .Where(ULD => ULD.UserLoginId == userLoginId).OrderBy(a => a.Department.Name).ToList();
            return output;
        }
        public List<UserLoginDepartment> GetUserLoginDepartmentByDepartment(int departmentId)
        {
            var output = _ctx.UserLoginDepartments.Where(ULD => ULD.DepartmentId == departmentId).OrderBy(a => a.UserLoginId).ToList();
            return output;
        }
        public bool InsertUserLoginDepartment(int loginId, int departmentId, out string error)
        {
            try
            {
                error = string.Empty;
                UserLoginDepartment newULD = new UserLoginDepartment();
                newULD.DepartmentId = departmentId;
                newULD.UserLoginId = loginId;

                _ctx.UserLoginDepartments.Add(newULD);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public bool UpdateUserLoginDepartment(int userLoginDepartmentId, int loginId, int departmentId, out string error)
        {
            try
            {
                error = string.Empty;
                UserLoginDepartment updateULD = GetUserLoginDepartment(userLoginDepartmentId);
                if (updateULD != null)
                {
                    updateULD.DepartmentId = departmentId;
                    updateULD.UserLoginId = loginId;
                    _ctx.SaveChanges();
                    return true;
                }
                else
                {
                    error = "No UserLoginDepartment in database";
                    return false;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public bool DeleteUserLoginDepartment(int userLoginDepartmentId, out string error)
        {
            try
            {
                error = string.Empty;
                UserLoginDepartment removeULD = GetUserLoginDepartment(userLoginDepartmentId);
                _ctx.UserLoginDepartments.Remove(removeULD);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public bool DeleteUserLoginDepartmentByUserLoginId(int userLoginId, out string error)
        {
            try
            {
                error = string.Empty;
                List<UserLoginDepartment> removeULD = GetUserLoginDepartmentByUserLogin(userLoginId);
                if (removeULD != null && removeULD.Count > 0)
                {
                    _ctx.UserLoginDepartments.RemoveRange(removeULD);
                    _ctx.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public bool InsertUserLoginDepartmentByUserLoginId(int userLoginId, List<int> departmentIds, out string error)
        {
            try
            {
                error = string.Empty;
                if (departmentIds != null && departmentIds.Count > 0)
                {
                    List<UserLoginDepartment> newULDs = new List<UserLoginDepartment>();
                    foreach (int departmentId in departmentIds)
                    {
                        UserLoginDepartment newULD = new UserLoginDepartment();
                        newULD.DepartmentId = departmentId;
                        newULD.UserLoginId = userLoginId;
                        newULDs.Add(newULD);
                    }
                    _ctx.UserLoginDepartments.AddRange(newULDs);
                    _ctx.SaveChanges();                 
                }
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool ManageUserLoginDepartment(int userLoginId, List<int> departmentIds,out string error)
        {
            error = string.Empty;
            if (HasEditDepartment(userLoginId, departmentIds))
            {              
                bool deletePass = DeleteUserLoginDepartmentByUserLoginId(userLoginId, out error);
                if(deletePass)
                {
                    bool addPass = InsertUserLoginDepartmentByUserLoginId(userLoginId, departmentIds, out error);
                    return addPass;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        public bool HasEditDepartment(int userLoginId,List<int> departmentIds)
        {        
            List<UserLoginDepartment> oldUserLoginDepartment = GetUserLoginDepartmentByUserLogin(userLoginId);
            if (oldUserLoginDepartment.Count != departmentIds.Count)
                return true;
            else
            {
                foreach (int departmentId in departmentIds)
                {
                    bool isInUserLoginDepartment = IsInUserLoginDepartment(oldUserLoginDepartment, departmentId);
                    if(!isInUserLoginDepartment)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        private bool IsInUserLoginDepartment(List<UserLoginDepartment> oldUserLoginDepartments,int departmentId)
        {
            foreach (UserLoginDepartment oldUserLoginDepartment in oldUserLoginDepartments)
            {
                if (oldUserLoginDepartment.DepartmentId == departmentId)
                    return true;
            }
            return false;
        }


        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}