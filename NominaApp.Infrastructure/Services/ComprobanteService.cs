using NominaApp.Core.Entities;
using NominaApp.Core.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace NominaApp.Infrastructure.Services;

public class ComprobanteService : IComprobanteService
{
    private const string CarpetaComprobantes = "Comprobantes";

    public byte[] GenerarPdf(Nomina nomina, string nombreEmpleado)
    {
        var documento = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Header().Text("Comprobante de Pago")
                    .FontSize(18).Bold().AlignCenter();

                page.Content().Column(col =>
                {
                    col.Spacing(8);

                    col.Item().Text($"Empleado: {nombreEmpleado}");
                    col.Item().Text($"Período: {nomina.Periodo:MMMM yyyy}");
                    col.Item().Text($"Fecha de generación: {nomina.FechaGeneracion:dd/MM/yyyy}");

                    col.Item().PaddingTop(10).LineHorizontal(1);

                    col.Item().Text($"Salario Bruto: {nomina.SalarioBruto:C}");
                    col.Item().Text($"Total Deducciones: {nomina.TotalDeducciones:C}").FontColor(Colors.Red.Medium);
                    col.Item().Text($"Ingresos Adicionales: {nomina.TotalIngresosAdicionales:C}").FontColor(Colors.Green.Medium);

                    col.Item().PaddingTop(5).LineHorizontal(1);
                    col.Item().Text($"Salario Neto: {nomina.SalarioNeto:C}").FontSize(14).Bold();

                    col.Item().PaddingTop(15).Text("Detalle:").Bold();

                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Concepto").Bold();
                            header.Cell().Text("Tipo").Bold();
                            header.Cell().Text("Monto").Bold();
                        });

                        foreach (var detalle in nomina.Detalles)
                        {
                            table.Cell().Text(detalle.ConceptoNomina?.Nombre ?? "");
                            table.Cell().Text(detalle.ConceptoNomina?.Tipo.ToString() ?? "");
                            table.Cell().Text($"{detalle.Monto:C}");
                        }
                    });
                });

                page.Footer().AlignCenter().Text("NominaApp — Documento generado automáticamente");
            });
        });

        return documento.GeneratePdf();
    }

    public string GuardarEnDisco(byte[] pdfBytes, int nominaId)
    {
        // Ruta relativa al directorio de ejecución de la API
        var carpeta = Path.Combine(AppContext.BaseDirectory, CarpetaComprobantes);

        if (!Directory.Exists(carpeta))
            Directory.CreateDirectory(carpeta);

        var rutaArchivo = Path.Combine(carpeta, $"comprobante_nomina_{nominaId}.pdf");
        File.WriteAllBytes(rutaArchivo, pdfBytes);

        return rutaArchivo;
    }
}