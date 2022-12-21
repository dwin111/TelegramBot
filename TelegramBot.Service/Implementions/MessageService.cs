using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.DAL;
using TelegramBot.Domain.Models;
using TelegramBot.Service.Interface;
using TelegramBot.DAL.Repositores;
using Microsoft.EntityFrameworkCore;

namespace TelegramBot.Service.Implementions
{
    public class MessageService : IMessageService
    {
        private readonly TelegramUserMessageRepository _messageRepository;

        public MessageService(ApplicationDbContext db)
        {
            _messageRepository = new TelegramUserMessageRepository(db);
        }

        public async Task<TelegramUserMessage> Create(TelegramUserMessage message)
        {
            try
            {
                await _messageRepository.Create(message);
                return message;
            }
            catch (Exception ex)
            {
                return new TelegramUserMessage();
            }
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var message = await GetOne(id);
                if (message == null)
                {
                    return true;
                }
                else
                {
                    await _messageRepository.Delete(message);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<TelegramUserMessage> Edit(long id, TelegramUserMessage message)
        {
            try
            {
                var user = await GetOne(id);
                if (message == null)
                {
                    return message;
                }
                else
                {
                    if (message.Text != null && message.Text != "")
                    {
                        user.Text = message.Text;
                    }

                    await _messageRepository.Update(user);

                    return message;
                }
            }
            catch (Exception ex)
            {
                return new TelegramUserMessage();
            }
        }

        public async Task<IEnumerable<TelegramUserMessage>> GetAll()
        {
            try
            {
                var messages = _messageRepository.GetAll().ToList();
                if (messages.Count > 0)
                {
                    return messages;
                }
                else
                {
                    return new List<TelegramUserMessage>();
                }
            }
            catch (Exception ex)
            {
                return new List<TelegramUserMessage>();
            }
        }

        public async Task<TelegramUserMessage> GetOne(long id)
        {
            try
            {
                var message = await _messageRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (message != null)
                    return message;
                else
                    return new TelegramUserMessage();
            }
            catch (Exception ex)
            {
                return new TelegramUserMessage();
            }
        }

        public async Task<TelegramUserMessage> GetByName(string name)
        {
            throw new NotImplementedException();
        }
   
    }
}
