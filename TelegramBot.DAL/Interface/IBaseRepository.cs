
namespace TelegramBot.DAL.Interface
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create(T model);
        IQueryable<T> GetAll();
        Task<bool> Delete(T model);
        Task<T> Update(T model);
    }
}
