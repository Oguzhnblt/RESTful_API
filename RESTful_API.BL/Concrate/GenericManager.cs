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

        public async Task<T> Add(T item)
        {
            return await _repository.Add(item);
        }

        public async Task<T> Delete(T item)
        {
            return await _repository.Delete(item);
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<T> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<T> Update(T item)
        {

            return await _repository.Update(item);

        }
    }
}
