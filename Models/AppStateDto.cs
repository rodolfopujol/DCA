namespace DCA.Models;

/// <summary>
/// Estado de la aplicación en memoria. Simula persistencia.
/// </summary>
public class AppStateDto
{
    public List<FondoLoteDto> Fondos { get; set; } = new();
    public List<OperacionDto> Operaciones { get; set; } = new();
    public PreciosDto Precios { get; set; } = new();
}
