using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace report_core.Domain.Entities.Project
{
    public class Project
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectMasterId { get; set; }
        public string ProjectHead { get; set; }
        public string Environment { get; set; }
        public int IsActive { get; set; }

        public string ProjectName { get; set; }
        public string Spoc { get; set; }
        public int IblPriority { get; set; }
        public string Milestones { get; set; }
        public string CurrentPhase { get; set; }
        public string Status { get; set; }
        public string NextPhase { get; set; }
        public string CrDetails { get; set; }
        public string RagStatus { get; set; }
        public string CurrentProgress { get; set; }

        public string DevStartDate { get; set; }
        public string UatReleaseDate { get; set; }
        public string UatSignoffDate { get; set; }
        public string PreProdReleaseDate { get; set; }
        public string PreProdSignoffDate { get; set; }
        public string ProdReleaseDate { get; set; }
    }
}
