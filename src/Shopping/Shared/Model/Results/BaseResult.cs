using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Shared.Model.Results
{
    public class BaseResult<T> where T : class
    {
        public bool IsSuccessful { get; set; }
        public List<T> ResultData { get; set; }
        public string Message { get; set; }

        public BaseResult()
        {
            ResultData = new List<T>();
        }
    }
}
