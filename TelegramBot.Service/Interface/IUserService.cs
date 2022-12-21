using TelegramBot.Domain.Models;


namespace TelegramBot.Service.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<TelegramUser>> GetAll();
        Task<TelegramUser> GetOne(long id);
        Task<TelegramUser> GetByName(string name);

        Task<TelegramUser> Create(TelegramUser telegramUser);

        Task<bool> Delete(long id);

        Task<TelegramUser> Edit(long id, TelegramUser telegramUser);

        Task<TelegramUser> AddMessage(long id, TelegramUserMessage message);
    }
}
