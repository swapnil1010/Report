using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace report_core.Domain.DTOs.Project
{
    public class ProjectDTO
    {
        public int ID { get; set; }
        public int PRO_MASTER_ID { get; set; }
        public int IS_ACTIVE { get; set; }
        public string PROJECT_NAME { get; set; }
        public string SPOC { get; set; }
        public int IBL_PRIORITY { get; set; }
        //public string MILESTONES  { get; set; }
        public string CURRENT_PHASE { get; set; }
        public string STATUS { get; set; }
        public string NEXT_PHASE { get; set; }
        public string CR_DETAILS { get; set; }
        public string RAG_STATUS { get; set; }
        public string CURRRENT_PROGRESS { get; set; }
        public string? DEV_START { get; set; }
        public string? UAT_RELEASE { get; set; }
        public string? UAT_SIGNOFF { get; set; }
        public string? PREPROD_RELEASE { get; set; }
        public string? PREPROD_SIGNOFF { get; set; }
        public string? PROD_RELEASE { get; set; }
        public string? CREATED_ON { get; set; }
        public int? CREATED_BY { get; set; }
        public string? MODIFIED_ON { get; set; }
        public int? MODIFIED_BY { get; set; }
    }
}
