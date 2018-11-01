using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace P4Papi.Models
{
    
    public class ResultModel
    {
        public enum ResultEnum
        {
            [Description("OK")]
            OK,
            [Description("FAILED")]
            FAILED
        };
        public string Result { get; set; }
        public string ResultDescription { get; set; }

        public ResultModel(ResultEnum result,string description)
        {
            SetResult(result, description);
        }
        public ResultModel()
        {
        }
        public void SetResult(ResultEnum result, string description)
        {
            Result = result.ToString();
            ResultDescription = description;
        }
    }
}