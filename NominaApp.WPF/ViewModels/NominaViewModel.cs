using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NominaApp.Core.Entities;
using NominaApp.WPF.Models;
using NominaApp.WPF.Services;
using System.Collections.ObjectModel;
using System.IO;

namespace NominaApp.WPF.ViewModels;

public partial class NominaViewModel : ObservableObject
{
    private readonly INominaApiService _nominaApiService;
    private readonly IEmpleadoApiService _empleadoApiService;

    [ObservableProperty]
    private ObservableCollection<EmpleadoModel> empleados = new();

    [ObservableProperty]
    private EmpleadoModel? empleadoSeleccionado;

    [ObservableProperty]
    private DateTime periodo = new(DateTime.Today.Year, DateTime.Today.Month, 1);

    [ObservableProperty]
    private NominaModel? nominaGenerada;

    [ObservableProperty]
    private bool estaCargando;

    [ObservableProperty]
    private string? mensajeError;

    public NominaViewModel(INominaApiService nominaApiService, IEmpleadoApiService empleadoApiService)
    {
        _nominaApiService = nominaApiService;
        _empleadoApiService = empleadoApiService;
    }

    [RelayCommand]
    private async Task CargarEmpleadosAsync()
    {
        var lista = await _empleadoApiService.GetAllAsync();
        Empleados = new ObservableCollection<EmpleadoModel>(lista);
    }

    [RelayCommand]
    private async Task GenerarNominaAsync()
    {
        if (EmpleadoSeleccionado is null)
        {
            MensajeError = "Selecciona un empleado primero.";
            return;
        }

        EstaCargando = true;
        MensajeError = null;
        NominaGenerada = null;

        var request = new GenerarNominaModel
        {
            EmpleadoId = EmpleadoSeleccionado.Id,
            Periodo = Periodo
        };

        var (esExitoso, nomina, error) = await _nominaApiService.GenerarAsync(request);

        if (esExitoso)
            NominaGenerada = nomina;
        else
            MensajeError = error;

        EstaCargando = false;
    }

    [RelayCommand]
    private async Task ExportarComprobanteAsync()
    {
        if (NominaGenerada is null) return;

        var pdfBytes = await _nominaApiService.DescargarComprobanteAsync(NominaGenerada.Id);
        if (pdfBytes is null)
        {
            MensajeError = "No se pudo descargar el comprobante.";
            return;
        }

        var dialog = new Microsoft.Win32.SaveFileDialog
        {
            FileName = $"comprobante_nomina_{NominaGenerada.Id}.pdf",
            Filter = "Archivos PDF (*.pdf)|*.pdf"
        };

        if (dialog.ShowDialog() == true)
        {
            await File.WriteAllBytesAsync(dialog.FileName, pdfBytes);
            MensajeError = null;
        }
    }
}