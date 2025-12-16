namespace DCA.Models;

/// <summary>
/// Representa una operación de compra (BUY) o venta (SELL) de cripto.
/// </summary>
public class OperacionDto
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string Tipo { get; set; } = "BUY"; // BUY/SELL, solo BUY implementado
    public string MonedaComprada { get; set; } = "BTC";
    public PagoDto Pago { get; set; } = new();
    public decimal CantidadComprada { get; set; }
    public FeeDto? Fee { get; set; }

    public decimal CostoMxnCalculado { get; set; }
    public List<FifoConsumoDto> DetalleFifo { get; set; } = new();
}

public class PagoDto
{
    public string MonedaPago { get; set; } = "USDT";
    public decimal CostoEnMonedaPago { get; set; }
}

public class FeeDto
{
    public string Moneda { get; set; } = "USDT";
    public decimal Monto { get; set; }
}

public class FifoConsumoDto
{
    public int LoteId { get; set; }
    public decimal CantidadConsumida { get; set; }
    public decimal CostoUnitarioMxn { get; set; }
    public decimal SubtotalMxn { get; set; }
}
