using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TelegramBot.Domain.Interface;

namespace TelegramBot.Domain.Models
{
    public class TelegramUser : IUser
    {
        [Key]
        public long Id { get; set; }
        public string UserName { get; set; } = default!;
        public string Roule { get; set; } = default!;

        public List<TelegramUserMessage> Messages { get; set; }
    }
}
