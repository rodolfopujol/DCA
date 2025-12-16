namespace DCA.Models;

/// <summary>
/// Precios actuales de referencia.
/// </summary>
public class PreciosDto
{
    public decimal TipoCambioUsdtMxn { get; set; }
    public List<PrecioActivoDto> Activos { get; set; } = new();
}

public class PrecioActivoDto
{
    public string Moneda { get; set; } = "BTC";
    public decimal PrecioUsdt { get; set; }
}
