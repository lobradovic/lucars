using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using lucars.Models;
using lucars.Data;

namespace lucars.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public readonly ApplicationDbContext context;

    public HomeController(ILogger<HomeController> logger,ApplicationDbContext context)
    {
        this.context=context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        var automobili = context.Automobils
            .Include(a => a.Model)
                .ThenInclude(m => m.Markas)  // ili kako se zove kolekcija/entitet Marka u modelu
        .Include(a => a.Klasa)
        .ToList();

        return View(automobili);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
