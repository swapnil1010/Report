using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace report_core.Domain.Entities.Project
{
    public class ProjectMaster
    {
        public int Id { get; set; }
        public string Project { get; set; }
        public string Environment { get; set; }
    }
}
