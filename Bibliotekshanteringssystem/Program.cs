using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;

class Program
{
    static void Main(string[] args)
    {
        // Load appsettings.json
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        // Dependency Injection container
        var services = new ServiceCollection();

        services.AddDbContext<LibraryContext>(options =>
            options.UseSqlServer(config.GetConnectionString("LibraryDb"))
        );

        var provider = services.BuildServiceProvider();

        using (var context = provider.GetRequiredService<LibraryContext>())
        {
            Console.WriteLine("Connected to BibliotekDatabase successfully!");
        }
    }
}

