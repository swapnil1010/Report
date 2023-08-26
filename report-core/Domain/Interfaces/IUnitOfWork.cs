using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using report_core.Domain.Interfaces.Login;
using report_core.Domain.Interfaces.Project;

namespace report_core.Domain.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IProjectRepository TBL_PROJECT { get; }
        IProjectMasterRepository TBL_PROJECT_MASTER { get; }
        ILoginRepository TBL_USER { get; }
        int Save();
    }
}
