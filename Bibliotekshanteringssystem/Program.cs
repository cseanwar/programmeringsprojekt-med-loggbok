using LibraryManagement;
using LibraryManagement.Data;
using LibraryManagement.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var services = new ServiceCollection();
        services.AddDbContext<LibraryContext>(options =>
            options.UseSqlServer(config.GetConnectionString("LibraryDb"))
        );
        services.AddSingleton<BookService>();   // Books in memory (List)
        services.AddSingleton<LoanService>();   // Loans in memory
        services.AddScoped<UserService>();      // Users stored in DB
        services.AddTransient<MainMenu>();      // Main Menu

        var provider = services.BuildServiceProvider();

        var menu = provider.GetRequiredService<MainMenu>();
        menu.Show();
    }
}