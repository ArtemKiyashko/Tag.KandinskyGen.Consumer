using Azure.Data.Tables;
using Azure.Identity;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Tag.KandinskyGen.Repositories;

namespace Tag.KandinskyGen.Managers.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGenerationRequestManager(this IServiceCollection services, GenerationRequestOptions options)
    {
        services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.UseCredential(new ManagedIdentityCredential());
            if (options.TablesServiceUri is not null)
                clientBuilder.AddTableServiceClient(options.TablesServiceUri);
            else
            {
                if (string.IsNullOrEmpty(options.TablesConnectionString))
                    throw new ArgumentException($"{nameof(options.TablesServiceUri)} or {nameof(options.TablesConnectionString)} required");
                clientBuilder.AddTableServiceClient(options.TablesConnectionString);
            }
        });

        services.AddSingleton<IGenerationRequestManager, GenerationRequestManager>();
        services.AddSingleton<IGenerationRequestRepository, GenerationRequestRepository>(builder =>
        {
            var tableServiceClient = builder.GetRequiredService<TableServiceClient>();
            var tableClient = tableServiceClient.GetTableClient(options.GenerationActivityTable);
            tableClient.CreateIfNotExists();
            return new GenerationRequestRepository(tableClient);
        });

        return services;
    }
}
