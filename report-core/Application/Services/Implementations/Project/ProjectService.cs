using AutoMapper;
using Microsoft.Extensions.Options;
using report_core.Application.Services.Interfaces;
using report_core.Domain.DTOs.Login;
using report_core.Domain.DTOs.Project;
using report_core.Domain.Entities.Common;
using report_core.Domain.Entities.Project;
using report_core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace report_core.Application.Services.Implementations.Project
{
    public class ProjectService : IProjectService
    {
        public IUnitOfWork _unitOfWork;
        private readonly IMapper _iMapper;
        private readonly AppSetings _appSettings;
        

        public ProjectService(IUnitOfWork unitOfWork, IMapper iMapper, IOptions<AppSetings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _iMapper = iMapper;
            _appSettings = appSettings.Value;

        }
   
        public async Task<Response> GetAllProjects(int id)
        {
            Response response =new Response();
            try
            {
                IEnumerable<ProjectDTO>? dbResponse = null;
                if (id > 0)
                    dbResponse = await _unitOfWork.TBL_PROJECT.GetAllWhereAsync(a => a.PRO_MASTER_ID == id);
                else
                    dbResponse = await _unitOfWork.TBL_PROJECT.GetAll();

                if (dbResponse != null)
                {
                    var mappedDbResponse = _iMapper.Map<IEnumerable<Domain.Entities.Project.Project>>(dbResponse);
                    List<Domain.Entities.Project.Project> modifiedList = new List<Domain.Entities.Project.Project>();
                    foreach (var item in mappedDbResponse)
                    {
                        var proHeadDbresponse = await _unitOfWork.TBL_PROJECT_MASTER.GetById(item.ProjectMasterId);
                        if (proHeadDbresponse != null)
                        {
                            item.ProjectHead = proHeadDbresponse.PROJECT;
                        }
                        modifiedList.Add(item); // Add the modified item to the list
                    }
                    response.ReturnCode = 200;
                    response.Data = modifiedList;
                    response.ReturnMsg = "Project list has been fetch successfully! ";
                }
                else
                {
                    response.ReturnCode = 404;
                    response.ReturnMsg = "Project list not found! ";
                }
            }
            catch (Exception ex)
            {
                response.ReturnCode = 500;
                response.Data = null;
                response.ReturnMsg = "An exception occured while fetching project list";
            }
            return response;
        }
      
        public async Task<Response> CreateorUpdateProject(Domain.Entities.Project.Project project)
        {
            Response response = new Response();
            try
            {
                var request = _iMapper.Map<ProjectDTO>(project);

                if (request.ID == 0)
                {
                    request.CREATED_BY = project.UserId;
                    request.CREATED_ON = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
                    await _unitOfWork.TBL_PROJECT.Add(request);
                }
                else
                {
                    request.MODIFIED_BY = project.UserId;
                    request.MODIFIED_ON = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
                     _unitOfWork.TBL_PROJECT.Update(request);
                }
                var result = _unitOfWork.Save();

                if (result > 0)
                {
                    response.ReturnCode = 200;
                    response.ReturnMsg = request.ID == 0 ? "Details has been added successfully !": "Details has been Updated successfully ";
                }
                else
                {
                    response.ReturnCode = 200;
                    response.ReturnMsg = "Failed to save details !";
                }
            }
            catch (Exception ex)
            {
                response.ReturnCode = 500;
                response.ReturnMsg= "Exception Occured !";
            }
            return response;

        }

        public async Task<Response> GetAllMasterProjects()
        {
            Response response = new Response();
            try
            {
                var dbResponse = await _unitOfWork.TBL_PROJECT_MASTER.GetAll();
                if (dbResponse != null)
                {
                    var mappedResponse = _iMapper.Map<IEnumerable<ProjectMaster>>(dbResponse);
                    response.ReturnCode = 200;
                    response.Data = mappedResponse;
                    response.ReturnMsg = "Master Project List has been fetched successful";
                }
                else
                {
                    response.ReturnCode = 404;
                    response.ReturnMsg = "Master project list not found !";
                }
            }
            catch (Exception ex)
            {
                response.ReturnCode = 404;
                response.ReturnMsg = "Exception occured while fetching pro master list !";
            }
            
            return response;
        }
        public async Task<Response> GetProjectById(int id)
        {
            Response response = new Response();
            Domain.Entities.Project.Project project = new report_core.Domain.Entities.Project.Project();
            try
            {
                var proDbresponse = await _unitOfWork.TBL_PROJECT.GetById(id);
                if (proDbresponse != null)
                {
                    var mappedProDbresponse = _iMapper.Map<Domain.Entities.Project.Project>(proDbresponse);
                    var proMasterDbResponse = await _unitOfWork.TBL_PROJECT_MASTER.GetById(mappedProDbresponse.ProjectMasterId);
                    if(proMasterDbResponse != null)
                    {
                        mappedProDbresponse.ProjectHead = proMasterDbResponse.PROJECT;
                        mappedProDbresponse.Environment = proMasterDbResponse.ENVIRONMENT;
                        response.ReturnCode = 200;
                        response.Data = mappedProDbresponse;
                        response.ReturnMsg = "Project Details fetch successfull;";
                    }
                    else
                    {
                        response.ReturnCode = 404;
                        response.ReturnMsg = "Project master not found with master Id =" + mappedProDbresponse.ProjectMasterId;
                    }
                }
                else
                {
                    response.ReturnCode= 404;
                    response.ReturnMsg = "Project not found with Id =" + id;
                }

            }
            catch (Exception ex)
            {
                response.ReturnCode = 500;
                response.ReturnMsg = "Exception occured  while fetching project by id= " + id;
            }
            return response;
        }
        public async Task<byte[]> ProjectExport(int id,string wwwroot)
        {
       
            int nextId = 1; string htmlTableBody = string.Empty; string tdBody = string.Empty;

            try
            {
                IEnumerable<ProjectDTO>? dbResponse = null;

                if (id > 0)
                    dbResponse = await _unitOfWork.TBL_PROJECT.GetAllWhereAsync(a => a.PRO_MASTER_ID == id && a.IS_ACTIVE > 0);
                else
                    dbResponse= await _unitOfWork.TBL_PROJECT.GetAllWhereAsync(a => a.IS_ACTIVE > 0);

                foreach (var item in dbResponse)
                {
                    tdBody += "<tr><td>" + nextId + "</td>" +
                            "<td>" + item.PROJECT_NAME + "</td>" +
                            "<td>" + item.SPOC + "</td>" +
                            "<td>" + item.IBL_PRIORITY + "</td>" +
                            "<td class='miles'>" + GetConditionalMilestones(item) + "</td>" +
                            "<td>" + item.CURRENT_PHASE + "</td>" +
                            "<td>" + item.STATUS + "</td>" +
                            "<td>" + item.NEXT_PHASE + "</td>" +
                            "<td>" + item.CR_DETAILS + "</td>" +
                            "<td class= '"+ BindRagStatusColor(item.RAG_STATUS)+"'>" + item.RAG_STATUS + "</td>" +
                            "<td>" + item.CURRRENT_PROGRESS + "</td></tr>";

                    nextId++;
                }
                using (StreamReader readerDebitAuthorisation = new StreamReader($"{wwwroot}\\templates\\ReportTable.html"))
                {
                    htmlTableBody = readerDebitAuthorisation.ReadToEnd();
                    htmlTableBody = htmlTableBody.Replace("{htmlBody}", tdBody);
                }

            }
            catch (Exception ex)
            {
            }
            return Encoding.ASCII.GetBytes(htmlTableBody);
        }
        private string GetConditionalMilestones(ProjectDTO item)
        {
            string milestoneHtml = string.Empty;
            milestoneHtml += "<p>";
            if (string.IsNullOrEmpty(item.DEV_START)) { }
            else milestoneHtml += "&nbsp;&nbsp;&nbsp;&nbsp;<b>Dev Start Date:</b>&nbsp;" + item.DEV_START + "<br>";

            if (string.IsNullOrEmpty(item.UAT_RELEASE)) { }
            else milestoneHtml += "&nbsp;&nbsp;&nbsp;&nbsp;<b>Uat Release Date:</b>&nbsp;" + item.UAT_RELEASE + "<br>";

            if (string.IsNullOrEmpty(item.UAT_SIGNOFF)) { }
            else milestoneHtml += "&nbsp;&nbsp;&nbsp;&nbsp;<b>Uat Signoff Date:</b>&nbsp;" + item.UAT_SIGNOFF + "<br>";

            if (string.IsNullOrEmpty(item.PREPROD_RELEASE)) { }
            else milestoneHtml += "&nbsp;&nbsp;&nbsp;&nbsp;<b>PreProd Release Date:</b>&nbsp;" + item.PREPROD_RELEASE + "<br>";

            if (string.IsNullOrEmpty(item.PREPROD_SIGNOFF)) { }
            else milestoneHtml += "&nbsp;&nbsp;&nbsp;&nbsp;<b>PreProd Signoff Date:</b>&nbsp;" + item.PREPROD_SIGNOFF + "<br>";

            if (string.IsNullOrEmpty(item.PROD_RELEASE)) { }
            else milestoneHtml += "&nbsp;&nbsp;&nbsp;&nbsp;<b>Prod Release Date:</b>&nbsp;" + item.PROD_RELEASE;

            milestoneHtml += "</p>";
            return milestoneHtml;

        }
        private string BindRagStatusColor(string status)
        {
            string className = string.Empty;
            switch (status.Trim().ToLower())
            {
                case "green":
                    className = "greenColor";
                    break;
                case "red":
                    className = "redColor";
                    break;
                case "amber":
                    className = "amberColor";
                    break;
              
                default:
                    className = "noneColor";
                    break;
            }
            return className;
        }

    }
}
