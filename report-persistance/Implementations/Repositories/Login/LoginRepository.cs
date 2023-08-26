using report_core.Domain.DTOs.Login;
using report_core.Domain.Interfaces.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace report_persistance.Implementations.Repositories.Login
{
    public class LoginRepository : GenericRepository<LoginDTO>, ILoginRepository
    {
        public LoginRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
