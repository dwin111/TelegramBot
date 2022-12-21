using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.DAL;
using TelegramBot.Domain.Models;
using TelegramBot.Service.Implementions;
using TelegramBot.Service.Interface;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace TelegramBot.App
{
    public partial class Form1 : Form
    {

        private TelegramBotClient botClient;
        private readonly ApplicationDbContext _db;
        private IUserService _userService;

        public Form1()
        {
            InitializeComponent();

            _db = new ApplicationDbContext(@"host=localhost;port=5432;database=Terock_Db;username=postgres;password=gD9g)hEwZs");
            _userService = new UserService(_db);
            // _messageService = new MessageService(_db);

            StartBot("5881182146:AAEBgm3Tu5AcqKMhSwavNd1OrbHujoh1NcM");
        }


        #region TelegramBot
        public async Task StartBot(string token)
        {
            botClient = new TelegramBotClient(token);
            botClient.StartReceiving(HandleUpdateAsync, HandlePollingErrorAsync);
        }
        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken token)
        {
            if (update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                await HandleMessage(botClient, update.Message);
                return;
            }
            if (update.Type == UpdateType.CallbackQuery)
            {
                await HandleCallbackQuery(botClient, update.CallbackQuery);
                return;
            }


            if(update.Type == UpdateType.EditedMessage)
            {
                await botClient.SendTextMessageAsync(update.EditedMessage.Chat.Id, $"You Edit text {update.EditedMessage.Text}");
                return;
            }
            if(update.Message.Type == MessageType.Video)
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, $"This Video");
                return;
            }
            if (update.Message.Type == MessageType.Photo)
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, $"This Photo");
                await botClient.SendPhotoAsync(update.Message.Chat.Id, "https://lh3.googleusercontent.com/IALGyI3i7Qb9uWcDuwRadPgPJIYWSlDBsHBjbjxoj4KFbUahMiL2oU9JkQ3DYJGxYhQiy3zPhwoMKPLiPtECygIJltA07tvEvUAgftyQq7z421_0XA=w1200", caption: "Hello");
                await botClient.SendPhotoAsync(update.Message.Chat.Id, update.Message.Photo[0].FileId , caption: "Hello");
                return;
            }
            if (update.Message.Type == MessageType.VideoNote)
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, $"This Video Note");
                return;
            }
            if (update.Message.Animation != null)
            {
                await botClient.SendAnimationAsync(update.Message.Chat.Id, update.Message.Animation.FileId);
                return;
            }



        }
        private async Task HandleMessage(ITelegramBotClient botClient, Telegram.Bot.Types.Message message)
        {
            var chatId = message.Chat.Id;
            var Nick = message.Chat.FirstName == null ? message.Chat.LastName : message.Chat.FirstName;
            string? messageText = message.Text;

            TelegramUser user = await _userService.GetOne(chatId);
            long id = 0;
            if (user == null)
            {

                user = await _userService.Create(new TelegramUser
                {
                    Id = chatId,
                    UserName = Nick == null ? "chat: " + message.Chat.Title : Nick,
                    Roule = "Admin",
                    Messages = new(){
                        new TelegramUserMessage
                        {
                            Id = id,
                            MessageId = message.MessageId,
                            Text = message.Text

                        }
                    }

                });
            }
            else
            {
                //user.Messages.Add(new TelegramUserMessage
                //{
                //    Id = id,
                //    MessageId = message.MessageId,
                //    Text = message.Text

                //}) ;
                await _userService.AddMessage(chatId, new TelegramUserMessage
                {
                    Id = id,
                    MessageId = message.MessageId,
                    Text = message.Text

                });
            }

            //TelegramUserViewModel userViewModel = new() { TelegramUser = await _userService.GetOneUser(chatId) };

            //sendButton.Click += delegate { SendMessage(chatId); };

            //userBox.BeginInvoke(new Action(() =>
            //{
            //    if (userBox.FindString(userViewModel.ToString()) == -1)
            //    {
            //        userBox.Items.Add(userViewModel);
            //    }

            //}));
            //MessageBox.BeginInvoke(new Action(() =>
            //{
            //    if (userBox.SelectedItem != null)
            //    {
            //        string curItem = userBox.SelectedItem.ToString();
            //        int index = userBox.FindString(curItem);
            //        TelegramUserViewModel user = (TelegramUserViewModel)userBox.Items[index];
            //        TelegramUserMessageViewModel messageViewModel = new TelegramUserMessageViewModel();

            //        messageViewModel.UserMessage = new TelegramUserMessage
            //        {
            //            Id = message.MessageId,
            //            Message = messageText,
            //            MessageId = message.MessageId
            //        };
            //        messageViewModel.TelegramUser = user.TelegramUser;

            //        MessageBox.Items.Add(messageViewModel);
            //    }
            //}));

            // userBox.Click += delegate { SelectedUser(); };

            bool isAdmin = true;
            //foreach (var item in await _userService.GetAllUsers())
            //{
            //    if (item.Roule == "Admin")
            //    {
            //        isAdmin = true;
            //        continue;
            //    }
            //}
            if (message.Text.ToLower().Equals("/start"))
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "/randomgif");
                return;
            }
            if (message.Text.ToLower().Equals("/randomgif"))
            {
                await botClient.SendAnimationAsync(message.Chat.Id, @$"https://cdn.theatlantic.com/thumbor/SsB2su49vz2cvHJW5TNMDOqaVt8=/0x43:2000x1085/1200x625/media/img/mt/2022/10/the_end/original.gif");
                return;
            }
            if (!isAdmin)
            {
                await botClient.SendTextMessageAsync(chatId, $"You said:\n{messageText}", replyMarkup: CreateButtonDefault());
                return;
            }
            else
            {
                await botClient.SendTextMessageAsync(chatId, $"{messageText}", replyMarkup: CreateButtonAdmin());
                return;
            }



        }

        private async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery? callbackQuery)
        {
            if (callbackQuery.Data.StartsWith("OK"))
            {
                await botClient.SendTextMessageAsync
                    (
                    callbackQuery.Message.Chat.Id,
                    $"OK"
                    );
                foreach (var item in await _userService.GetAll())
                {
                    if (item.Roule == "Admin" && item.Id != 342345235)
                    {
                        await botClient.SendTextMessageAsync
                        (
                        item.Id,
                        $"Задание {callbackQuery.Message.Text} принял {callbackQuery.Message.Chat.FirstName}_{callbackQuery.Message.Chat.Id}"
                        );
                    }
                }

                await botClient.DeleteMessageAsync(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId);
                //LogBox.BeginInvoke(new Action(() => { LogBox.Text += $"{}"});
                return;
            }
            if (callbackQuery.Data.StartsWith("NO"))
            {
                await botClient.SendTextMessageAsync
                    (
                    callbackQuery.Message.Chat.Id,
                    $"NO"
                    );
                return;
            }
            if (callbackQuery.Data.StartsWith("AllSends"))
            {
                foreach (var item in await _userService.GetAll())
                {
                    if (item.Id != callbackQuery.Message.Chat.Id && item.Id != 342345235)
                    {
                        await botClient.SendTextMessageAsync
                            (
                            item.Id,
                            callbackQuery.Message.Text,
                            replyMarkup: CreateButtonDefault()
                            );
                    }
                }
                return;
            }
            if (callbackQuery.Data.StartsWith("RedactRoule"))
            {
                List<InlineKeyboardButton> buttonList = new List<InlineKeyboardButton>();
                foreach (var item in await _userService.GetAll())
                {

                    buttonList.Add(InlineKeyboardButton.WithCallbackData("OK", "OK"));
                }

                InlineKeyboardMarkup keybourd = new(new[]
                {
                    buttonList
                });
                foreach (var item in await _userService.GetAll())
                {
                    if (item.Id != callbackQuery.Message.Chat.Id && item.Id != 342345235 && item.Id != 530570938)
                    {
                        await botClient.SendTextMessageAsync
                            (
                            item.Id,
                            callbackQuery.Message.Text,
                            replyMarkup: keybourd
                            );
                    }
                }
                return;
            }
            if (callbackQuery.Data.StartsWith("SendRandomGif"))
            {
                await botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "OGO", showAlert: true);
                await botClient.SendAnimationAsync(callbackQuery.Message.Chat.Id, @$"https://cdn.theatlantic.com/thumbor/SsB2su49vz2cvHJW5TNMDOqaVt8=/0x43:2000x1085/1200x625/media/img/mt/2022/10/the_end/original.gif");
                return;
            }

        }


        private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken token)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private InlineKeyboardMarkup CreateButtonDefault()
        {
            return new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("OK","OK"),
                    InlineKeyboardButton.WithCallbackData("NO","NO"),
                }
            });
        }
        private InlineKeyboardMarkup CreateButtonAdmin()
        {
            return new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("OK","OK"),
                    InlineKeyboardButton.WithCallbackData("NO","NO"),
                },
                new[]
                {

                    InlineKeyboardButton.WithCallbackData("Отправить всем","AllSends"),
                    InlineKeyboardButton.WithCallbackData("Редаутиривать Роль юзера","RedactRoule"),
                },
                new[]
                {

                    InlineKeyboardButton.WithCallbackData("AllUserMessage","AllUserMessage"),
                    InlineKeyboardButton.WithCallbackData("RedactRoule","RedactRoule"),
                },
                new[]
                {

                    InlineKeyboardButton.WithCallbackData("SendRandomGif","SendRandomGif"),

                }
            });
        }


        //private void SendMessage(long id)
        //{
        //    string text = textToSend.Text;
        //    string sendText = textToSend.Text;
        //    if (text != null && text.Replace(" ", "") != "")
        //    {
        //        botClient.SendTextMessageAsync(id, sendText);
        //        Users.GetAllUsers().FirstOrDefault(x => x.Id == id).Message.LastOrDefault().Answer = text;
        //        MessageBox.Items.Add($"Support: {Users.GetAllUsers().FirstOrDefault(x => x.Id == id).Message.LastOrDefault().Answer}");
        //        textToSend.Text = string.Empty;
        //    }
        //    else
        //    {
        //        LogBox.Text = $"[{id}]: Текст небил отправлен поскольку он пустой\n";
        //    }
        //}

        //private void SelectedUser()
        //{
        //    if (userBox.SelectedItem != null)
        //    {
        //        string curItem = userBox.SelectedItem.ToString();
        //        int index = userBox.FindString(curItem);
        //        TelegramUserViewModel user = (TelegramUserViewModel)userBox.Items[index];
        //        List<TelegramUserMessageViewModel> message = new List<TelegramUserMessageViewModel>();

        //        foreach (var item in user.TelegramUser.Message)
        //        {
        //            message.Add(new TelegramUserMessageViewModel { UserMessage = item, TelegramUser = user.TelegramUser });
        //        }

        //        MessageBox.Items.Clear();

        //        MessageBox.Items.AddRange(message.ToArray());
        //    }
        //}


        #endregion

    }
}