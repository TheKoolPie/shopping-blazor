using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Shared.Results
{
    public class BaseResult<T> where T : class
    {
        public bool IsSuccessful { get; set; }
        public List<T> ResultData { get; set; }
        public List<string> ErrorMessages { get; set; }

        public BaseResult()
        {
            ResultData = new List<T>();
            ErrorMessages = new List<string>();
        }

        public string CompleteErrorMessage
        {
            get
            {
                string output = "";
                foreach(var error in ErrorMessages)
                {
                    output += error + Environment.NewLine;
                }
                return output;
            }
        }
    }
}
