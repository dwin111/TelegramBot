using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Domain.Interface;

namespace TelegramBot.Domain.Models
{
    public class Media : IMedia
    {
        public long Id { get; set; }
        public string FileId { get; set; }
        public long UserSendId { get; set; }
        public string FileName { get; set; }
        public string Type { get; set; }
    }
}
