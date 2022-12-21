using Microsoft.EntityFrameworkCore;
using TelegramBot.DAL.Interface;
using TelegramBot.Domain.Models;

namespace TelegramBot.DAL.Repositores
{
    public class TelegramUserRepository : IBaseRepository<TelegramUser>
    {
        private readonly ApplicationDbContext _db;

        public TelegramUserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(TelegramUser model)
        {
            try
            {
                if (model != null)
                {
                    await _db.Users.AddAsync(model);
                    await _db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> Delete(TelegramUser model)
        {

            if (model != null)
            {
                _db.Users.Remove(model);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public IQueryable<TelegramUser> GetAll()
        {
            return _db.Users;
        }

        public async Task<TelegramUser> Update(TelegramUser model)
        {

            _db.Users.Update(model);
            await _db.SaveChangesAsync();
            return model;

        }


    }
}
