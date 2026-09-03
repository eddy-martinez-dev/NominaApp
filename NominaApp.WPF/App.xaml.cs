using System.Net.Http;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NominaApp.WPF.Services;
using NominaApp.WPF.ViewModels;

namespace NominaApp.WPF;

public partial class App : Application
{
    private readonly IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddHttpClient<IEmpleadoApiService, EmpleadoApiService>(client =>
                {
                    client.BaseAddress = new Uri("https://localhost:7174/"); // puerto real
                })
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                });

                services.AddTransient<EmpleadoListViewModel>();
                services.AddTransient<MainWindow>();

                services.AddHttpClient<INominaApiService, NominaApiService>(client =>
                {
                    client.BaseAddress = new Uri("https://localhost:7174/");
                })
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                });

                services.AddTransient<NominaViewModel>();
                services.AddTransient<Views.NominaWindow>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host.StartAsync();

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();
        base.OnExit(e);
    }

    public IServiceProvider Services => _host.Services;
}