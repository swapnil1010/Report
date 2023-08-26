using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace report_core.Domain.DTOs.Login
{
    public class LoginDTO
    {
        public int ID { get; set; }
        public string? USER_NAME { get; set; }
        public string? PASSWORD { get; set; }
    }
}
