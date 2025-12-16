using DCA.Models;

namespace DCA.Services;

/// <summary>
/// Lógica de negocio y cálculos financieros (FIFO, dashboard).
/// </summary>
public class PortfolioCalculator
{
    /// <summary>
    /// Aplica FIFO para una compra y actualiza los lotes consumidos.
    /// </summary>
    public void AplicarCompraConFifo(List<FondoLoteDto> lotes, OperacionDto operacion)
    {
        var disponibles = lotes
            .Where(l => l.MonedaFondo.Equals(operacion.Pago.MonedaPago, StringComparison.OrdinalIgnoreCase))
            .OrderBy(l => l.Fecha)
            .ThenBy(l => l.Id)
            .ToList();

        var restante = operacion.Pago.CostoEnMonedaPago;
        if (disponibles.Sum(l => l.CantidadDisponible) < restante)
        {
            throw new InvalidOperationException("Fondos insuficientes para cubrir la operación");
        }

        var consumos = new List<FifoConsumoDto>();
        foreach (var lote in disponibles)
        {
            if (restante <= 0)
            {
                break;
            }

            var aConsumir = Math.Min(restante, lote.CantidadDisponible);
            lote.CantidadDisponible -= aConsumir;
            restante -= aConsumir;

            consumos.Add(new FifoConsumoDto
            {
                LoteId = lote.Id,
                CantidadConsumida = aConsumir,
                CostoUnitarioMxn = lote.CostoUnitarioMxn,
                SubtotalMxn = aConsumir * lote.CostoUnitarioMxn
            });
        }

        operacion.DetalleFifo = consumos;
        operacion.CostoMxnCalculado = consumos.Sum(c => c.SubtotalMxn);
    }

    /// <summary>
    /// Construye el dashboard con posiciones y PnL.
    /// </summary>
    public DashboardDto ConstruirDashboard(AppStateDto estado)
    {
        var resultado = new DashboardDto();
        var agrupado = estado.Operaciones
            .Where(o => o.Tipo.Equals("BUY", StringComparison.OrdinalIgnoreCase))
            .GroupBy(o => o.MonedaComprada);

        foreach (var grupo in agrupado)
        {
            var cantidadTotal = grupo.Sum(o => o.CantidadComprada);
            var invertidoMxn = grupo.Sum(o => o.CostoMxnCalculado);
            var invertidoUsdt = grupo.Sum(o => o.Pago.CostoEnMonedaPago);
            var costoPromedioUsdt = cantidadTotal == 0 ? 0 : invertidoUsdt / cantidadTotal;
            var costoPromedioMxn = cantidadTotal == 0 ? 0 : invertidoMxn / cantidadTotal;

            var precio = estado.Precios.Activos.FirstOrDefault(a => a.Moneda.Equals(grupo.Key, StringComparison.OrdinalIgnoreCase));
            var precioUsdt = precio?.PrecioUsdt ?? 0;
            var valorActualMxn = cantidadTotal * precioUsdt * estado.Precios.TipoCambioUsdtMxn;
            var pnlMxn = valorActualMxn - invertidoMxn;
            var pnlPct = invertidoMxn == 0 ? 0 : pnlMxn / invertidoMxn * 100;

            resultado.Posiciones.Add(new PosicionDto
            {
                Moneda = grupo.Key,
                Cantidad = cantidadTotal,
                CostoPromedioUsdt = Math.Round(costoPromedioUsdt, 4),
                CostoPromedioMxn = Math.Round(costoPromedioMxn, 4),
                TotalInvertidoUsdt = invertidoUsdt,
                TotalInvertidoMxn = invertidoMxn,
                PrecioActualUsdt = precioUsdt,
                ValorActualMxn = valorActualMxn,
                PnlMxn = pnlMxn,
                PnlPorcentaje = pnlPct
            });
        }

        resultado.TotalInvertidoMxn = resultado.Posiciones.Sum(p => p.TotalInvertidoMxn);
        resultado.ValorActualMxn = resultado.Posiciones.Sum(p => p.ValorActualMxn);
        resultado.PnlMxn = resultado.ValorActualMxn - resultado.TotalInvertidoMxn;
        resultado.PnlPorcentaje = resultado.TotalInvertidoMxn == 0 ? 0 : resultado.PnlMxn / resultado.TotalInvertidoMxn * 100;

        return resultado;
    }
}
