using Microsoft.AspNetCore.Mvc;
using report_core.Application.Services.Interfaces;
using report_core.Domain.Entities.Login;

namespace report_web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _iLoginService;
        public LoginController(ILoginService ILoginService)
        { 
            _iLoginService = ILoginService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login([FromBody] Login request)
        {
            var response = await _iLoginService.Login(request);
            if (response.ReturnCode == 200)
            {
                var loginData =(Login)response.Data;
                TempData["User"] = loginData.UserName;
                TempData["UserId"] = loginData.Id;
            }
            return Json(new { response });
        }
    }
}
