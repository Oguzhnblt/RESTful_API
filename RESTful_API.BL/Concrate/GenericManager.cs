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

        public Task<T> Add(T item)
        {
            return _repository.Add(item);
        }

        public Task<T> Delete(T item)
        {
            return _repository.Delete(item);
        }
        public Task<IEnumerable<T>> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<T> GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Task<T> Update(T item)
        {

            return _repository.Update(item);

        }
    }
}
