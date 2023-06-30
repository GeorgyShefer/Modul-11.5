using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using BBot.Configures;
using Telegram.Bot.Types.ReplyMarkups;

namespace BBot.Controllers
{
     class DefaultControllers
    {
        private readonly ITelegramBotClient telegramBotClient;

        public DefaultControllers(ITelegramBotClient telegramBotClient)
        {
            this.telegramBotClient = telegramBotClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} обнаружил сообщение");
            telegramBotClient.SendTextMessageAsync(message.Chat.Id, $"Получено сообщение необработанного формата", cancellationToken: ct);
        }
    }
}
