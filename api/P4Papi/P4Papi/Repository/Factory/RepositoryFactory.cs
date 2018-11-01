using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Repository
{
    public static class RepositoryFactory
    {
        public static DeathLineRepository DeathLineRepository
        {
            get { return new DeathLineRepository(); }
        }

        public static PointRepository PointRepository
        {
            get { return new PointRepository(); }
        }

        public static UserRepository UserRepository
        {
            get { return new UserRepository(); }
        }
        public static PositionRepository PositionRepository
        {
            get { return new PositionRepository(); }
        }
        public static DepartmentRepository DepartmentRepository
        {
            get { return new DepartmentRepository(); }
        }
        public static SubdivisionRepository SubdivisionRepository
        {
            get { return new SubdivisionRepository(); }
        }
        public static LoginUserRepository LoginUserRepository
        {
            get { return new LoginUserRepository(); }
        }
        public static UserLoginDepartmentRepository UserLoginDepartmentRepository
        {
            get { return new UserLoginDepartmentRepository(); }
        }
    }
}