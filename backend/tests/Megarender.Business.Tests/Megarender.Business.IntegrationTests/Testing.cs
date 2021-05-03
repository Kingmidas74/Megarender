using Megarender.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Respawn.Postgres;
using System.IO;
using System.Threading.Tasks;
using Megarender.Business.IntegrationTests;
using Megarender.ManagementService;

[SetUpFixture]
public class Testing
{   
    private static IConfigurationRoot _configuration;
    private static IServiceScopeFactory _scopeFactory;
    private static PostgresCheckpoint _checkpoint;

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        DotNetEnv.Env.TraversePath().Load();

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, false)
            .AddEnvironmentVariables();

        _configuration = builder.Build();

        var startup = new Startup(_configuration);

        var services = new ServiceCollection();

        services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
            w.EnvironmentName == "Development" &&
            w.ApplicationName == "Megarender.ManagementService"));

        services.AddLogging();

        startup.ConfigureServices(services);

        _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
        
        _checkpoint = new PostgresCheckpoint
        {
            TablesToIgnore = new [] { "__EFMigrationsHistory" },
            AutoCreateExtensions = true,
            //DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = new[]
            {
                "public"
            }
        };

        EnsureDatabase();
    }

    private static void EnsureDatabase()
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<APIContext>();

        context.Database.Migrate();
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetService<ISender>();

        return await mediator.Send(request);
    }


    public static async Task ResetState()
    {
        var connection = string.Format(_configuration.GetConnectionString("DefaultConnection"),
            System.Environment.GetEnvironmentVariable(nameof(EnvironmentVariables.DB_HOST)),
            System.Environment.GetEnvironmentVariable(nameof(EnvironmentVariables.DB_PORT)),
            File.ReadAllText(System.Environment.GetEnvironmentVariable(nameof(EnvironmentVariables.DB_USER_FILE))),
            File.ReadAllText(System.Environment.GetEnvironmentVariable(nameof(EnvironmentVariables.DB_PWD_FILE))));
        await _checkpoint.Reset(connection);
    }

    public static async Task<TEntity> FindAsync<TEntity>(int id)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<APIContext>();

        return await context.FindAsync<TEntity>(id);
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<APIContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
    }
}