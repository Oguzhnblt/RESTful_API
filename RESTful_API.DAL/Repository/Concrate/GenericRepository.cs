using Microsoft.EntityFrameworkCore;
using RESTful_API.DAL.Context;
using RESTful_API.DAL.Repository.Abstract;
using RESTful_API.DTO.Entities;

namespace RESTful_API.DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly RESTful_Api_Context _context;

        public GenericRepository(RESTful_Api_Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {

            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T item)
        {
            _context.Set<T>().Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<T> UpdateAsync(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }


        public async Task<T> DeleteAsync(T item)
        {
            _context.Set<T>().Remove(item);

            _context.Entry(item).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return item;
        }

    }

}



