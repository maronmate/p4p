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
        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}