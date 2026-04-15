using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YASN.Application.Users.Profile.Create;
using YASN.Application.Users.Profile.Remove;
using YASN.Application.Users.Profile.Update;
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

            var profileId = await mediator.Send(new CreateProfileCommand(username!));
            Console.WriteLine($"Created profile: {profileId}");

            Console.Write("Ready change username?");
            Console.ReadLine();

            Console.Write("Enter username: ");
            var newUsername = Console.ReadLine();

            await mediator.Send(new UpdateProfileUsernameCommand(profileId, newUsername!));
            Console.WriteLine($"Username changed for {profileId}");
            
            Console.Write("Ready delete profile?");
            Console.ReadLine();

            await mediator.Send(new RemoveProfileByIdCommand(profileId));
            Console.WriteLine($"Deleted profile: {profileId}");
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    var connectionString = context.Configuration.GetConnectionString("DefaultConnection")
                                           ?? throw new InvalidOperationException(
                                               "Connection string 'DefaultConnection' not found.");

                    services.AddMediatR(cfg =>
                        cfg.RegisterServicesFromAssembly(typeof(CreateProfileHandler).Assembly));
                    services.AddMediatR(cfg =>
                        cfg.RegisterServicesFromAssembly(typeof(RemoveProfileHandler).Assembly));

                    services.AddScoped<IProfileRepository>(_ =>
                        new DatabaseProfileRepository(connectionString));
                });
    }
}