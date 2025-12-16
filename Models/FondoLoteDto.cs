namespace DCA.Models;

/// <summary>
/// Representa un lote de fondos (cash) utilizado para aplicar FIFO.
/// </summary>
public class FondoLoteDto
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string MonedaFondo { get; set; } = "USDT";
    public decimal CantidadInicial { get; set; }
    public decimal CantidadDisponible { get; set; }
    public decimal TipoCambioMxn { get; set; }
    public string? Nota { get; set; }

    public decimal CostoUnitarioMxn => TipoCambioMxn;
    public decimal CostoTotalMxn => CantidadInicial * CostoUnitarioMxn;
}
