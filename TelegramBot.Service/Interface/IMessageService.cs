using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Domain.Models;

namespace TelegramBot.Service.Interface
{
    public interface IMessageService
    {
        Task<IEnumerable<TelegramUserMessage>> GetAll();
        Task<TelegramUserMessage> GetOne(long id);
        Task<TelegramUserMessage> GetByName(string name);

        Task<TelegramUserMessage> Create(TelegramUserMessage message);

        Task<bool> Delete(long id);

        Task<TelegramUserMessage> Edit(long id, TelegramUserMessage message);
    }
}

