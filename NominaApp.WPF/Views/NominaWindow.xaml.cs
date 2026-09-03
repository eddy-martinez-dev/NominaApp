

using System.Windows;
using NominaApp.WPF.ViewModels;

namespace NominaApp.WPF.Views;

public partial class NominaWindow : Window
{
    public NominaWindow(NominaViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private async void ComboBox_Loaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is NominaViewModel vm)
            await vm.CargarEmpleadosCommand.ExecuteAsync(null);
    }
}