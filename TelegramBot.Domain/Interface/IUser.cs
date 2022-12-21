using TelegramBot.Domain.Models;

namespace TelegramBot.Domain.Interface
{
    public interface IUser
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Roule { get; set; }
        public List<TelegramUserMessage> Messages { get; set; }

    }
}
