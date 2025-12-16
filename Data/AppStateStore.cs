using DCA.Models;

namespace DCA.Data;

/// <summary>
/// Contenedor estático simple para simular persistencia en memoria.
/// </summary>
public static class AppStateStore
{
    public static AppStateDto State { get; } = CrearEstadoInicial();

    private static AppStateDto CrearEstadoInicial()
    {
        return new AppStateDto
        {
            Precios = new PreciosDto
            {
                TipoCambioUsdtMxn = 18.50m,
                Activos = new List<PrecioActivoDto>
                {
                    new PrecioActivoDto { Moneda = "BTC", PrecioUsdt = 60000 },
                    new PrecioActivoDto { Moneda = "ETH", PrecioUsdt = 3000 }
                }
            }
        };
    }
}
