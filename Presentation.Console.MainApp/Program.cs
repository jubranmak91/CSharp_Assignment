using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using MainApp.Dialogs;
using Business.Interfaces;

class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IFileService, FileService>()
            .AddSingleton<IContactService, ContactService>()
            .AddTransient<MenuDialog>()
            .BuildServiceProvider();

        var menuDialog = serviceProvider.GetRequiredService<MenuDialog>();
        menuDialog.ShowMainMenu();
    }

}
