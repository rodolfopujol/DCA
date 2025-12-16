using DCA.Data;
using DCA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DCA.Controllers;

[Authorize]
public class PreciosController : Controller
{
    public IActionResult Index()
    {
        // aquí se obtienen los datos
        var precios = AppStateStore.State.Precios;
        return View(precios);
    }

    [HttpPost]
    public IActionResult Guardar(PreciosDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", dto);
        }

        // aquí se guardan los datos
        AppStateStore.State.Precios = dto;
        TempData["Success"] = "Precios actualizados";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult SimularActualizacion()
    {
        // aquí se obtienen los datos
        var precios = AppStateStore.State.Precios;
        var rand = new Random();
        foreach (var activo in precios.Activos)
        {
            var delta = (decimal)(rand.NextDouble() - 0.5) * 1000;
            activo.PrecioUsdt = Math.Max(1, activo.PrecioUsdt + delta);
        }

        precios.TipoCambioUsdtMxn += (decimal)(rand.NextDouble() - 0.5);
        // aquí se guardan los datos
        AppStateStore.State.Precios = precios;
        TempData["Success"] = "Precios simulados";
        return RedirectToAction("Index");
    }
}
