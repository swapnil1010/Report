using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace report_core.Domain.Entities.Common
{
    public class Response
    {
        public int ReturnCode { get; set; }
        public string ReturnMsg { get; set; }
        public object Data { get; set; }
    }
}
