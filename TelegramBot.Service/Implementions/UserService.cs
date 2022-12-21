using TelegramBot.DAL.Repositores;
using TelegramBot.DAL;
using TelegramBot.Domain.Models;
using TelegramBot.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace TelegramBot.Service.Implementions
{
    public class UserService : IUserService
    {
        private readonly TelegramUserRepository _userRepository;

        public UserService(ApplicationDbContext db)
        {
            _userRepository = new TelegramUserRepository(db);
        }

        public async Task<TelegramUser> Create(TelegramUser telegramUser)
        {
            try
            {
                await _userRepository.Create(telegramUser);
                return telegramUser;
            }
            catch(Exception ex)
            {
                return new TelegramUser();
            }
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var user = await GetOne(id);
                if (user == null)
                {
                    return true;
                }
                else
                {
                    await _userRepository.Delete(user);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<TelegramUser> Edit(long id, TelegramUser telegramUser)
        {
            try
            {
                var user = await GetOne(id);
                if (telegramUser == null)
                {
                    return telegramUser;
                }
                else
                {
                    if (telegramUser.Roule != null && telegramUser.Roule != "")
                    {
                        user.Roule = telegramUser.Roule;
                    }
                    if (telegramUser.UserName != null && telegramUser.UserName != "")
                    {
                        user.UserName = telegramUser.UserName;
                    }


                    await _userRepository.Update(user);

                    return user;
                }
            }
            catch (Exception ex)
            {
                return new TelegramUser();
            }
        }
        public async Task<TelegramUser> AddMessage(long id, TelegramUserMessage message)
        {
            try
            {
                var user = await GetOne(id);
                if (user == null)
                {
                    return user;
                }
                else
                {
                   if(user.Messages == null)
                    {
                        user.Messages = new() { message };
                    }
                    else
                    {
                        user.Messages.Add(message);
                    }

                    await _userRepository.Update(user);

                    return user;
                }
            }
            catch (Exception ex)
            {
                return new TelegramUser();
            }
        }

        public async Task<IEnumerable<TelegramUser>> GetAll()
        {
            try
            {
                var users = _userRepository.GetAll().ToList();
                return users;
            }
            catch (Exception ex)
            {
                return new List<TelegramUser>();
            }
        }

        public async Task<TelegramUser> GetOne(long id)
        {
            try
            {
                var user = await _userRepository.GetAll().SingleOrDefaultAsync(x => x.Id == id);
                if (user != null)
                    return user;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<TelegramUser> GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
