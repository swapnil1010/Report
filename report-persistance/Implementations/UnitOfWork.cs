using Microsoft.EntityFrameworkCore;
using report_core.Domain.Interfaces;
using report_core.Domain.Interfaces.Login;
using report_core.Domain.Interfaces.Project;
using report_persistance.Implementations.Repositories;
using report_persistance.Implementations.Repositories.Login;
using report_persistance.Implementations.Repositories.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace report_persistance.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            TBL_USER= new LoginRepository(_context);
            TBL_PROJECT = new ProjectRepository(_context);
            TBL_PROJECT_MASTER = new ProjectMasterRepository(_context);
        }
        public ILoginRepository TBL_USER { get; private set; }
        public IProjectRepository TBL_PROJECT { get; private set; }

        public IProjectMasterRepository TBL_PROJECT_MASTER { get; private set; }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

    }
}
