using TelegramBot.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TelegramBot.Domain.Models
{
    public class TelegramUserMessage : IMessage
    {
        [Key]
        public long Id { get; set; }
        public string Text { get; set; }
        public int MessageId { get; set; }
    }
}
