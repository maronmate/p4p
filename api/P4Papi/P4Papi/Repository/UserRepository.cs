using P4Papi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Repository
{
    public class UserRepository : IDisposable
    {
        private PfourPEntities _ctx;

        public UserRepository()
        {
            _ctx = new PfourPEntities();
        }

        public UserModel GetUserData(int userId)
        {
            UserModel userModel = null;
            var output = _ctx.Users.Where(user => user.UserId == userId);
            if (output.Count() > 0)
            {
                User user = output.First();
                userModel = new UserModel(user);
            }

            return userModel;
        }
        public List<User> GetWithFilter(string name,string lastName,int? departmentId,int? positionId,int? subdivisionId)
        {
            var output = _ctx.Users;
            if (!string.IsNullOrEmpty(name))
            {
                output.Where(user => user.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                output.Where(user => user.Lastname.Contains(lastName));
            }
            if(departmentId.HasValue)
            {
                output.Where(user => user.Position.DepartmentId == departmentId);
            }
            if (positionId.HasValue)
            {
                output.Where(user => user.PositionId == positionId);
            }
            if (subdivisionId.HasValue)
            {
                output.Where(user => user.SubdivisionId == subdivisionId);
            }
            return output.ToList();
        }
        private User GetUserById(int userId)
        {
            var output = _ctx.Users.Where(user => user.UserId == userId);
            if (output.Count() > 0)
            {
                User user = output.First();
                return user;
            }
            else
                return null;
        }
        public bool InsertUser(string name,string lastName,DateTime? birthDate, int positionId,DateTime startdate, int subDivisionId, out string error)
        {
            try
            {
                error = string.Empty;
                User newUser = new User();
                newUser.Name = name;
                newUser.Lastname = lastName;
                newUser.Birthdate = birthDate;
                newUser.PositionId = positionId;
                newUser.StartDate = startdate;
                newUser.SubdivisionId = subDivisionId;

                _ctx.Users.Add(newUser);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool UpdateUser(int userId,string name, string lastName, DateTime? birthDate, int positionId, DateTime startdate,int subDivisionId, out string error)
        {
            try
            {
                error = string.Empty;
                User updateUser = GetUserById(userId);
                if (updateUser != null)
                {
                    updateUser.Name = name;
                    updateUser.Name = name;
                    updateUser.Lastname = lastName;
                    updateUser.Birthdate = birthDate;
                    updateUser.PositionId = positionId;
                    updateUser.StartDate = startdate;
                    updateUser.SubdivisionId = subDivisionId;
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
        public bool DeleteUser(int userId, out string error)
        {
            try
            {
                error = string.Empty;
                User removeUser = GetUserById(userId);
                _ctx.Users.Remove(removeUser);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public int CountUserInPosition(int positionId)
        {
            var output = _ctx.Users.Where(user => user.PositionId == positionId).Count();
            return output;
        }
        public int CountUserInPositions(List<int> positionIds)
        {
            int output = 0;
            if (positionIds != null)
            {
                output = _ctx.Users.Where(user => positionIds.Contains(user.PositionId)).Count();               
            }
            return output;
        }
        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}