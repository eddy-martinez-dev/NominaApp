using System.Windows;
using NominaApp.WPF.ViewModels;

namespace NominaApp.WPF;

public partial class MainWindow : Window
{
    public MainWindow(EmpleadoListViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}