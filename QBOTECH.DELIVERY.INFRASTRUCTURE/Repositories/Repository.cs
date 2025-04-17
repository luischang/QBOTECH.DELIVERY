using Microsoft.EntityFrameworkCore;
using QBOTECH.DELIVERY.CORE.Interfaces;
using QBOTECH.DELIVERY.INFRASTRUCTURE.Data;

namespace QBOTECH.DELIVERY.INFRASTRUCTURE.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly QbotechDeliveryContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(QbotechDeliveryContext context)
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
            return await _dbSet.FindAsync(id);
        }
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
