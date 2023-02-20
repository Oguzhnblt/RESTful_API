namespace RESTful_API.BL.Abstract
{
    public interface IGenericService<T>
    {

        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Add(T item);
        Task<T> Update(T item);
        Task<T> Delete(T item);
    }
}
