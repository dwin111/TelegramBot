﻿
namespace TelegramBot.Domain.Interface
{
    public interface IMedia
    {
        public long Id { get; set; }
        public string FileId { get; set; }
        public long UserSendId { get; set; }
        public string FileName { get; set; }
        public string Type { get; set; }


    }
}
