
using Microsoft.EntityFrameworkCore;
using report_core.Domain.Interfaces;
using System.Linq.Expressions;

namespace report_persistance.Implementations.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public async Task<IEnumerable<T>> GetAllWhereAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }
        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }
        public async Task<IEnumerable<T>> ExecuteStoredProcedureAsync(string procedureName, params string[] parameters)
        {
            //var parameterNames = string.Join(", ", parameters.Select(p => $"@{p.ParameterName}"));
            var parameterNames = string.Join(", ",  $"@{parameters}");
            var query = $"EXEC {procedureName} {parameterNames}";
            return await _context.Set<T>().FromSqlRaw(query, parameters).ToListAsync();
        }
    }
}
