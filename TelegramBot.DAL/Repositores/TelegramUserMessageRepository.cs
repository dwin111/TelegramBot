using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.DAL;
using TelegramBot.DAL.Interface;
using TelegramBot.Domain.Models;

namespace TelegramBot.DAL.Repositores
{
    public class TelegramUserMessageRepository : IBaseRepository<TelegramUserMessage>
    {
        private readonly ApplicationDbContext _db;

        public TelegramUserMessageRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(TelegramUserMessage model)
        {
            if (model != null)
            {
                await _db.Messages.AddAsync(model);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<bool> Delete(TelegramUserMessage model)
        {
            if (model != null)
            {
                _db.Messages.Remove(model);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public IQueryable<TelegramUserMessage> GetAll()
        {
            return _db.Messages;
        }

        public async Task<TelegramUserMessage> Update(TelegramUserMessage model)
        {
            _db.Messages.Update(model);
            await _db.SaveChangesAsync();
            return model;
        }
    }
}
