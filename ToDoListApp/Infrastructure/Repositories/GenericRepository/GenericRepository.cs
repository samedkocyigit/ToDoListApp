using Microsoft.EntityFrameworkCore;
using ToDoListApp.Data;
using ToDoListApp.Exceptions;
using static ToDoListApp.Infrastructure.Repositories.GenericRepository.IGenericRepository;

namespace ToDoListApp.Infrastructure.Repositories.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ToDoAppContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ToDoAppContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null)
                throw new ErrorExceptions("There is no field with that Id");
            return result;
        }

        public async Task<T> AddAsync(T entity)
        {
            var result =await _dbSet.AddAsync(entity);
            if (result == null)
                throw new ErrorExceptions("Creation process has been failed");
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var result = _dbSet.Update(entity);
            if (result == null)
                throw new ErrorExceptions("Update process has been failed");
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
                throw new ErrorExceptions("There is no field with that Id");
            else
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }

}
