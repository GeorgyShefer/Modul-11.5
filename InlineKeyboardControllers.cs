using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using BBot.Configures;
using BBot.Services;
using Telegram.Bot.Types.Enums;

namespace BBot.Controllers
{
    internal class InlineKeyboardControllers
    {
        private readonly ITelegramBotClient telegramBotClient;
        private readonly IStorage _memoryStorage;

        public InlineKeyboardControllers(ITelegramBotClient telegramBotClient, IStorage storage)
        {
            _memoryStorage = storage;
            this.telegramBotClient = telegramBotClient;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            _memoryStorage.GetSession(callbackQuery.From.Id).LanguageCode = callbackQuery.Data;

            string languageCode = callbackQuery.Data switch
            {
                "ru" => "Русский",
                "en" => "Английский",
                _ => String.Empty
            }; 

            telegramBotClient.SendTextMessageAsync(callbackQuery.From.Id, $"<b> Язык аудио - {languageCode}.{Environment.NewLine}</b>" + 
                $"{Environment.NewLine} Можно поменять в главном меню", cancellationToken: ct, parseMode: ParseMode.Html);

        }
    }
}
