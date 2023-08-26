using report_core.Domain.Entities.Common;
using report_core.Domain.Entities.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace report_core.Application.Services.Interfaces
{
    public interface ILoginService
    {
        Task<Response> Login(Login request);
    }
}
