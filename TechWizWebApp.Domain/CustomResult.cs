using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechWizWebApp.Domain
{
    public class CustomResult
    {
        public CustomResult(int status, string message, object data)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        public int Status { get; set; }

        public string Message { get; set; }

        public object? Data { get; set; }
    }
}
