using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Repository
{
    public class Sorter
    {
        public string FieldName { get; set; }
        public bool OnDesc { get; set; }

        public Sorter()
        {

        }
        public Sorter(string fieldName,bool onDesc)
        {
            FieldName = fieldName;
            OnDesc = onDesc;
        }
    }
}