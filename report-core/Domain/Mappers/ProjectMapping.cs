using AutoMapper;
using report_core.Domain.DTOs.Project;
using report_core.Domain.Entities.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace report_core.Domain.Mappers
{
    public class ProjectMapping : Profile
    {
        public ProjectMapping()
        {
            CreateMap<Project, ProjectDTO>() //Map from Developer Object to DeveloperDTO Object
                .ForMember(dest => dest.PRO_MASTER_ID, source => source.MapFrom(source => source.ProjectMasterId))
                .ForMember(dest => dest.IS_ACTIVE, source => source.MapFrom(source => source.IsActive))
                 .ForMember(dest => dest.PROJECT_NAME, source => source.MapFrom(source => source.ProjectName)) //Specific Mapping
                 .ForMember(dest => dest.SPOC, source => source.MapFrom(source => source.Spoc))
                 .ForMember(dest => dest.IBL_PRIORITY, source => source.MapFrom(source => source.IblPriority))
                 //.ForMember(dest => dest.MILESTONES, source => source.MapFrom(source => source.Milestones))
                 .ForMember(dest => dest.CURRENT_PHASE, source => source.MapFrom(source => source.CurrentPhase))
                 .ForMember(dest => dest.STATUS, source => source.MapFrom(source => source.Status))
                 .ForMember(dest => dest.NEXT_PHASE, source => source.MapFrom(source => source.NextPhase))
                 .ForMember(dest => dest.CR_DETAILS, source => source.MapFrom(source => source.CrDetails))
                 .ForMember(dest => dest.RAG_STATUS, source => source.MapFrom(source => source.RagStatus))
                 .ForMember(dest => dest.CURRRENT_PROGRESS, source => source.MapFrom(source => source.CurrentProgress))
                 .ForMember(dest => dest.DEV_START, source => source.MapFrom(source => source.DevStartDate))
                 .ForMember(dest => dest.UAT_RELEASE, source => source.MapFrom(source => source.UatReleaseDate))
                 .ForMember(dest => dest.UAT_SIGNOFF, source => source.MapFrom(source => source.UatSignoffDate))
                 .ForMember(dest => dest.PREPROD_RELEASE, source => source.MapFrom(source => source.PreProdReleaseDate))
                 .ForMember(dest => dest.PREPROD_SIGNOFF, source => source.MapFrom(source => source.PreProdSignoffDate))
                 .ForMember(dest => dest.PROD_RELEASE, source => source.MapFrom(source => source.ProdReleaseDate));

            CreateMap<ProjectDTO, Project>() //Map from Developer Object to DeveloperDTO Object
               .ForMember(dest => dest.ProjectMasterId, source => source.MapFrom(source => source.PRO_MASTER_ID))
               .ForMember(dest => dest.ProjectName, source => source.MapFrom(source => source.PROJECT_NAME))
               .ForMember(dest => dest.IsActive, source => source.MapFrom(source => source.IS_ACTIVE))
               .ForMember(dest => dest.Spoc, source => source.MapFrom(source => source.SPOC))
               .ForMember(dest => dest.IblPriority, source => source.MapFrom(source => source.IBL_PRIORITY))
               .ForMember(dest => dest.CurrentPhase, source => source.MapFrom(source => source.CURRENT_PHASE))
               .ForMember(dest => dest.Status, source => source.MapFrom(source => source.STATUS))
               .ForMember(dest => dest.NextPhase, source => source.MapFrom(source => source.NEXT_PHASE))
               .ForMember(dest => dest.CrDetails, source => source.MapFrom(source => source.CR_DETAILS))
               .ForMember(dest => dest.RagStatus, source => source.MapFrom(source => source.RAG_STATUS))
               .ForMember(dest => dest.CurrentProgress, source => source.MapFrom(source => source.CURRRENT_PROGRESS))
               .ForMember(dest => dest.DevStartDate, source => source.MapFrom(source => source.DEV_START))
               .ForMember(dest => dest.UatReleaseDate, source => source.MapFrom(source => source.UAT_RELEASE))
               .ForMember(dest => dest.UatSignoffDate, source => source.MapFrom(source => source.UAT_SIGNOFF))
               .ForMember(dest => dest.PreProdReleaseDate, source => source.MapFrom(source => source.PREPROD_RELEASE))
               .ForMember(dest => dest.PreProdSignoffDate, source => source.MapFrom(source => source.PREPROD_SIGNOFF))
               .ForMember(dest => dest.ProdReleaseDate, source => source.MapFrom(source => source.PROD_RELEASE));

            CreateMap<ProjectMasterDTO, ProjectMaster>()
              .ForMember(dest => dest.Id, source => source.MapFrom(source => source.ID))
              .ForMember(dest => dest.Project, source => source.MapFrom(source => source.PROJECT))
              .ForMember(dest => dest.Environment, source => source.MapFrom(source => source.ENVIRONMENT));
        }
    }
}

