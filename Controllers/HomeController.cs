using DCA.Data;
using DCA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DCA.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly PortfolioCalculator _calculator = new();

    public IActionResult Index()
    {
        // aquí se obtienen los datos
        var estado = AppStateStore.State;
        var dashboard = _calculator.ConstruirDashboard(estado);
        return View(dashboard);
    }
}
