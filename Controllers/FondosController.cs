using DCA.Data;
using DCA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DCA.Controllers;

[Authorize]
public class FondosController : Controller
{
    public IActionResult Index()
    {
        // aquí se obtienen los datos
        var fondos = AppStateStore.State.Fondos.OrderBy(f => f.Fecha).ToList();
        return View(fondos);
    }

    public IActionResult Crear()
    {
        return View(new FondoLoteDto { Fecha = DateTime.UtcNow.Date });
    }

    [HttpPost]
    public IActionResult Crear(FondoLoteDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        // aquí se obtienen los datos
        var lista = AppStateStore.State.Fondos;
        dto.Id = lista.Any() ? lista.Max(f => f.Id) + 1 : 1;
        dto.CantidadDisponible = dto.CantidadInicial;
        dto.MonedaFondo = string.IsNullOrWhiteSpace(dto.MonedaFondo) ? "USDT" : dto.MonedaFondo.ToUpperInvariant();

        // aquí se guardan los datos
        lista.Add(dto);

        TempData["Success"] = "Lote registrado";
        return RedirectToAction("Index");
    }
}
