namespace DCA.Models;

/// <summary>
/// Información para la pantalla principal.
/// </summary>
public class DashboardDto
{
    public List<PosicionDto> Posiciones { get; set; } = new();
    public decimal TotalInvertidoMxn { get; set; }
    public decimal ValorActualMxn { get; set; }
    public decimal PnlMxn { get; set; }
    public decimal PnlPorcentaje { get; set; }
}

public class PosicionDto
{
    public string Moneda { get; set; } = string.Empty;
    public decimal Cantidad { get; set; }
    public decimal CostoPromedioUsdt { get; set; }
    public decimal CostoPromedioMxn { get; set; }
    public decimal TotalInvertidoUsdt { get; set; }
    public decimal TotalInvertidoMxn { get; set; }
    public decimal PrecioActualUsdt { get; set; }
    public decimal ValorActualMxn { get; set; }
    public decimal PnlMxn { get; set; }
    public decimal PnlPorcentaje { get; set; }
}
