using BBot;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using BBot.Controllers;
using BBot.Services;
using BBot.Configures;

class Program
{
    private static async Task Main(string[] args)
    {
        var host = new HostBuilder().ConfigureServices((hostContext, services) => ConfigureServices(services)).UseConsoleLifetime().Build();

        Console.WriteLine("Сервис запущен");

        await host.RunAsync();

        Console.WriteLine("Сервис закрыт");
    }

    static void ConfigureServices(IServiceCollection services)
    {
        AppSettings appSettings = BuildAppSettings();
        services.AddSingleton(BuildAppSettings);
        services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("6076137959:AAEv2Q4z399hTWSmRuTThQls5n4hi0nyJJI"));
        services.AddHostedService<Bot>();
        services.AddSingleton<IStorage, MemoryStorage>();
        services.AddSingleton<IFileHandler, AudioFileHandler>();
        

        services.AddTransient<DefaultControllers>();
        services.AddTransient<InlineKeyboardControllers>();
        services.AddTransient<TextMessageControllers>();
        services.AddTransient<VoiceMessageControllers>();

    }

    static AppSettings BuildAppSettings()
    {

        return new AppSettings()
        {
            DownloadsFolder = "C:\\Users\\evmor\\Downloads",
            BotToken = "5353047760:AAECHVcGyM-cQJIfA4sCStnGDBPimhlIV-g",
            AudioFileName = "audio",
            InputAudioFormat = "ogg",
            OutputAudioFormat = ".wav",
            InputAudioBitrate = 48000
           };
   

    }
}


    
