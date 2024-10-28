using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Tag.KandinskyGen.Managers;
using Tag.KandinskyGen.Managers.Extensions;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Tag.KandinskyGen.Consumer;

IConfiguration _functionConfig;
GenerationRequestOptions _generationRequestOptions = new();
KandinskyOptions _kandinskyOptions = new();

_functionConfig = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .Build();

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services => {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        _functionConfig.GetSection(nameof(GenerationRequestOptions)).Bind(_generationRequestOptions);
        _functionConfig.GetSection(nameof(KandinskyOptions)).Bind(_kandinskyOptions);
        
        services.AddGenerationRequestManager(_generationRequestOptions);
        services.AddKandinskyManager(_kandinskyOptions);

        services.AddSingleton<ITelegramBotClient>(factory => {
            var botToken = _functionConfig.GetValue<string>("TELEGRAM_BOT_TOKEN") ?? throw new ArgumentException("Bot token required", "TELEGRAM_BOT_TOKEN");
            return new TelegramBotClient(botToken);
        });

        services.AddSingleton<IMessageValidator, MessageValidator>();
    })
    .Build();

host.Run();
