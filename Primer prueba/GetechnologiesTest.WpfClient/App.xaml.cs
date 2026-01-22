using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;
using System.Windows;
using GetechnologiesTest.WpfClient.Services;
using GetechnologiesTest.WpfClient.ViewModels;

namespace GetechnologiesTest.WpfClient;

public partial class App : Application
{
    public IServiceProvider Services { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();


        var baseUrl = config["Api:BaseUrl"] ?? throw new InvalidOperationException("Api:BaseUrl no configurado");

        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton(new HttpClient { BaseAddress = new Uri(baseUrl) });
        serviceCollection.AddSingleton<ApiClient>();
        serviceCollection.AddSingleton<MainViewModel>();

        Services = serviceCollection.BuildServiceProvider();

        var mainWindow = new MainWindow
        {
            DataContext = Services.GetRequiredService<MainViewModel>()
        };

        mainWindow.Show();
    }
}
