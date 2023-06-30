using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.ReplyMarkups;
using BBot.Controllers;

namespace BBot
{
     class Bot : BackgroundService
    {
        private ITelegramBotClient _telegramClient;
        private InlineKeyboardControllers _inlineKeyboard;
        private DefaultControllers _defaultControllers;
        private TextMessageControllers _messageControllers;
        private VoiceMessageControllers _voiceMessageControllers;

        public Bot(ITelegramBotClient telegramBotClient, DefaultControllers defaultControllers, TextMessageControllers messageControllers, VoiceMessageControllers voiceMessageControllers, InlineKeyboardControllers inlineKeyboardControllers)
        {
            _telegramClient = telegramBotClient;
            _defaultControllers = defaultControllers;
            _messageControllers = messageControllers;
            _voiceMessageControllers = voiceMessageControllers;
            _inlineKeyboard = inlineKeyboardControllers;
        }

        async Task HandleUpdateAsync(ITelegramBotClient telegramBotClient, Update update, CancellationToken cancellationToken)
        {
            if(update.Type == UpdateType.CallbackQuery)
            {
                await _inlineKeyboard.Handle(update.CallbackQuery, cancellationToken);

            }
            if(update.Type == UpdateType.Message)
            {
                switch (update.Message!.Type)
                {
                    case MessageType.Voice:
                        await _voiceMessageControllers.Handle(update.Message, cancellationToken);
                        return;
                    case MessageType.Text:
                        await _messageControllers.Handle(update.Message, cancellationToken);
                        return;

                        break;

                    default:
                        await _defaultControllers.Handle(update.Message, cancellationToken);
                        return;
                        break;
                }
            }
        }
        Task HandleErrorAsync(ITelegramBotClient telegramBotClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessgae = exception switch
            {
                ApiRequestException apiRequestException
                => $"Telegram API Error: {apiRequestException.Message}\n{apiRequestException.ErrorCode}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessgae);

            Console.WriteLine("Ожидание 10 секунд");

            Thread.Sleep(10000);

            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _telegramClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                new ReceiverOptions { AllowedUpdates = { } },
                cancellationToken: stoppingToken
                );

            Console.WriteLine("Бот запущен");
        }
    }
}
