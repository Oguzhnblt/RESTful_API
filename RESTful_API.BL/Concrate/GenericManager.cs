using RESTful_API.BL.Abstract;
using RESTful_API.DAL.Repository.Abstract;

namespace RESTful_API.BL.Concrate
{
    public class GenericManager<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;
        public GenericManager(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<T> AddAsync(T item)
        {
            return await _repository.AddAsync(item);
        }

        public async Task<T> DeleteAsync(T item)
        {
            return await _repository.DeleteAsync(item);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<T> UpdateAsync(T item)
        {

            return await _repository.UpdateAsync(item);

        }
    }
}
