using DCA.Data;
using DCA.Models;
using DCA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DCA.Controllers;

[Authorize]
public class OperacionesController : Controller
{
    private readonly PortfolioCalculator _calculator = new();

    public IActionResult Index()
    {
        // aquí se obtienen los datos
        var operaciones = AppStateStore.State.Operaciones.OrderByDescending(o => o.Fecha).ToList();
        return View(operaciones);
    }

    public IActionResult Crear()
    {
        return View(new OperacionDto
        {
            Fecha = DateTime.UtcNow.Date,
            MonedaComprada = "BTC",
            Pago = new PagoDto { MonedaPago = "USDT" }
        });
    }

    [HttpPost]
    public IActionResult Crear(OperacionDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        try
        {
            // aquí se obtienen los datos
            var estado = AppStateStore.State;
            dto.Id = estado.Operaciones.Any() ? estado.Operaciones.Max(o => o.Id) + 1 : 1;
            dto.MonedaComprada = dto.MonedaComprada.ToUpperInvariant();
            dto.Pago.MonedaPago = dto.Pago.MonedaPago.ToUpperInvariant();

            _calculator.AplicarCompraConFifo(estado.Fondos, dto);

            // aquí se guardan los datos
            estado.Operaciones.Add(dto);

            TempData["Success"] = "Operación registrada";
            return RedirectToAction("Index");
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(dto);
        }
    }
}
