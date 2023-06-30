using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using BBot.Configures;
using Telegram.Bot.Types.ReplyMarkups;

using Telegram.Bot.Types.Enums;

namespace BBot.Controllers
{
    class TextMessageControllers
    {
        private readonly ITelegramBotClient telegramBotClient;

        public TextMessageControllers(ITelegramBotClient telegramBotClient)
        {
            this.telegramBotClient = telegramBotClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":

                    var buttons = new List<InlineKeyboardButton[]>();

                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"🇷🇺 Русский",$"ru"),
                        InlineKeyboardButton.WithCallbackData($"🇬🇧 English",$"en")
                    });

                    telegramBotClient.SendTextMessageAsync(message.Chat.Id, $"<b>Наш превращает аудио в текст </b> {Environment.NewLine}" +
                        $"{Environment.NewLine} Можно записать голосовое сообщение если лень печатать {Environment.NewLine}", cancellationToken: ct,
                        parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;

                default:
                    telegramBotClient.SendTextMessageAsync(message.Chat.Id, "Отправте аудио для превращения в текст", cancellationToken: ct);
                    break;
            }
        }
    }
}
