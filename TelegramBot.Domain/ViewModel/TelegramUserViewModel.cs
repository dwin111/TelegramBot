using TelegramBot.Domain.Interface;


namespace TelegramBot.Domain.ViewModel
{
    public class TelegramUserViewModel
    {
        public IUser TelegramUser { get; set; }
        public override string ToString()
        {
            return $"@{TelegramUser.UserName}) ({TelegramUser.Id})";
        }
    }
}
