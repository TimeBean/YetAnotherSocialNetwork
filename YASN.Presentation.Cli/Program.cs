using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YASN.Application.Users.Profile.Create;
using YASN.Application.Users.Profile.Remove;
using YASN.Application.Users.Profile.Update;
using YASN.Application.Users.UserCredential.Create;
using YASN.Application.Users.UserCredential.VerifyUserCredential;
using YASN.Domain.Repository;
using YASN.Domain.Service;
using YASN.Infrastructure.Repository;
using YASN.Infrastructure.Service;

namespace YASN.Presentation.Cli
{
    internal class Program
    {
        private static IMediator? _mediator;

        private static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            Console.Write("username: ");
            var username = Console.ReadLine();
            Console.Write("password: ");
            var password = Console.ReadLine();
            /*var saltBytes = RandomNumberGenerator.GetBytes(32);
            var salt = Convert.ToBase64String(saltBytes);

            await _mediator.Send(new CreateAccountCommand(username!, password!, salt));*/
            var verify = await _mediator.Send(new VerifyUserCredentialCommand(username!, password!));

            Console.WriteLine(verify);
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
                    services.AddMediatR(cfg =>
                        cfg.RegisterServicesFromAssembly(typeof(UpdateProfileHandler).Assembly));
                    services.AddMediatR(cfg =>
                        cfg.RegisterServicesFromAssembly(typeof(CreateUserCredentialHandler).Assembly));

                    services.AddScoped<IProfileRepository>(_ =>
                        new DatabaseProfileRepository(connectionString));
                    services.AddScoped<IUserCredentialRepository>(_ =>
                        new DatabaseUserCredentialRepository(connectionString));
                    services.AddSingleton<IPasswordService>(_ => new PasswordService());
                });
    }
}