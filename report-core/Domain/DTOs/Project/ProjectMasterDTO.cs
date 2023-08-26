using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace report_core.Domain.DTOs.Project
{
    public class ProjectMasterDTO
    {
        public int ID { get; set; }
        public string PROJECT { get; set; }
        public string ENVIRONMENT { get; set; }
    }
}
