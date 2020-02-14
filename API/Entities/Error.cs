using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevItUp.Grain.API.Entities
{
    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public Error(int code, string message)
        {
            this.Code = code;
            this.Message = message;
        }
    }
}
