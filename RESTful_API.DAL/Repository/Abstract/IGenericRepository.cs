using RESTful_API.DTO.Entities;

namespace RESTful_API.DAL.Repository.Abstract
{
    public interface IGenericRepository<T> where T : class
    {

        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Add(T item);
        Task<T> Update(T item);
        Task<T> Delete(T item);

    }

}

