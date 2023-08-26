
using report_core.Domain.Entities.Common;
using report_core.Domain.Entities.Project;

namespace report_core.Application.Services.Interfaces
{
    public interface IProjectService
    {
        //Task<IEnumerable<Project>> GetAllProjects();
        //Task<bool> CreateorUpdateProject(Project project);
        //Task<IEnumerable<ProjectMaster>> GetAllMasterProjects();
        //Task<Project> GetProjectById(int id);
        Task<Response> GetAllProjects(int id);
        Task<Response> CreateorUpdateProject(Project project);
        Task<Response> GetAllMasterProjects();
        Task<Response> GetProjectById(int id);

        Task<byte[]> ProjectExport(int id,string wwwroot);
       
    }
}
