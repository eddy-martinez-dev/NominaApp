using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NominaApp.Core.Entities;
using NominaApp.WPF.Models;
using NominaApp.WPF.Services;
using System.Collections.ObjectModel;

namespace NominaApp.WPF.ViewModels;

public partial class EmpleadoListViewModel : ObservableObject
{
    private readonly IEmpleadoApiService _empleadoApiService;

    [ObservableProperty]
    private ObservableCollection<EmpleadoModel> empleados = new();

    [ObservableProperty]
    private bool estaCargando;

    [ObservableProperty]
    private string? mensajeError;

    public EmpleadoListViewModel(IEmpleadoApiService empleadoApiService)
    {
        _empleadoApiService = empleadoApiService;
    }

    [RelayCommand]
    private async Task CargarEmpleadosAsync()
    {
        EstaCargando = true;
        MensajeError = null;

        try
        {
            var lista = await _empleadoApiService.GetAllAsync();
            Empleados = new ObservableCollection<EmpleadoModel>(lista);
        }
        catch (Exception ex)
        {
            MensajeError = $"Error al cargar empleados: {ex.Message}";
        }
        finally
        {
            EstaCargando = false;
        }
    }
}