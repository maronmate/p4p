using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Repository
{
    public class PagingTable
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }

        public PagingTable()
        {

        }
        public PagingTable(int pageSize, int currentPage)
        {
            PageSize = pageSize;
            CurrentPage = currentPage;
        }
    }
}