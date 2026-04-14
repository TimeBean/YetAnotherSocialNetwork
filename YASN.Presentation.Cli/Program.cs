using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YASN.Application.Users.Profile.Create;
using YASN.Domain.Repository;
using YASN.Infrastructure;

namespace YASN.Presentation.Cli
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
    
            Console.Write("Enter username: ");
            var username = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(username))
            {
                var profileId = await mediator.Send(new CreateProfileCommand(username));
                Console.WriteLine($"Created profile: {profileId}");
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    var connectionString = context.Configuration.GetConnectionString("DefaultConnection")
                                           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                        
                    Console.WriteLine(connectionString);
                    
                    services.AddMediatR(cfg =>
                        cfg.RegisterServicesFromAssembly(typeof(CreateProfileHandler).Assembly));

                    services.AddScoped<IProfileRepository>(sp =>
                        new DatabaseProfileRepository(connectionString));
                });
    }
}