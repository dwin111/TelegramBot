
namespace TelegramBot.Domain.Interface
{
    public interface IMessage
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public int MessageId { get; set; }
    }
}
