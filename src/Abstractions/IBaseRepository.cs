namespace Abstractions;

public interface IBaseRepository<T> where T : BaseEntity
{
    T Add(T entity);
    void Delete(T entity);
}
