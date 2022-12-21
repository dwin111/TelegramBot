using TelegramBot.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Domain.ViewModel
{
    public class TelegramUserMessageViewModel
    {
        public IUser TelegramUser { get; set; }
        public IMessage UserMessage { get; set; }
        public override string ToString()
        {
            return $"{TelegramUser.UserName}: {UserMessage.Text}";
        }
    }
}
