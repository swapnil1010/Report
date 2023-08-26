using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using report_core.Application.Services.Interfaces;
using report_core.Domain.Entities.Login;
using report_core.Domain.Entities.Project;
using System.Text;

namespace report_web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IProjectService _iProjectService;
        private readonly string _wwwrootPath;

        public DashboardController(IProjectService IProjectService, IWebHostEnvironment env)
        {
            this._iProjectService = IProjectService;
            _wwwrootPath = env.WebRootPath;
        }
        public IActionResult Index()
        {
            return View();
        }
       
        [HttpGet]
        public async Task<ActionResult> GetProjectList(int id)
        {
            var response = await _iProjectService.GetAllProjects(id);

            return Json(new { response });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddNewReport([FromBody] Project request)
        {
            var response =await _iProjectService.CreateorUpdateProject(request);
            return Json(new { response });
        }
        [HttpGet]
        public async Task<ActionResult> GetMasterProjectList()
        {
            var response = await _iProjectService.GetAllMasterProjects();
            return Json(new { response });
        }
        [HttpGet]
        public async Task<ActionResult> GetProjectById(int id)
        {
            var response = await _iProjectService.GetProjectById(id);
            return Json(new { response });
        }
        [HttpGet]
        public async Task<ActionResult> ProjectExport(int id)
        {
            var response = await _iProjectService.ProjectExport(id,_wwwrootPath);
            //return Json(new { response });
            // return File(response, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ProjectReport.xls");
            return File(response, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
