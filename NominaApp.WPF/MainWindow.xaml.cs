using System.Windows;
using NominaApp.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace NominaApp.WPF;

public partial class MainWindow : Window
{
    public MainWindow(EmpleadoListViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    // MainWindow.xaml.cs
    private void AbrirNomina_Click(object sender, RoutedEventArgs e)
    {
        var nominaWindow = ((App)Application.Current).Services.GetRequiredService<Views.NominaWindow>();
        nominaWindow.Show();
    }
}