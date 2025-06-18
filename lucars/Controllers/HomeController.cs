using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using lucars.Models;
using lucars.Data;
using Microsoft.AspNetCore.Components.Forms;

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

    public IActionResult Index(string searchQuery = "", int page = 1, int pageSize = 6)
    {
        // var automobili = context.Automobils
        //     .Include(a => a.Model)
        //         .ThenInclude(m => m.Markas)  // ili kako se zove kolekcija/entitet Marka u modelu
        // .Include(a => a.Klasa)
        // .ToList();
        var query = context.Automobils
            .Include(a => a.Model)
                .ThenInclude(m => m.Markas)
            .Include(a => a.Klasa)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchQuery))
        {
            query = query.Where(z => z.Model.nazivModela.Contains(searchQuery) ||
            z.Model.Markas.nazivMarke.Contains(searchQuery)
            || z.Klasa.nazivKlase.Contains(searchQuery));
        }

        int totalCount = query.Count();
        var a = query
            .OrderBy(z => z.Model.Markas.nazivMarke)
            .ThenBy(z => z.Model.nazivModela)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var kola = new AutoPageModel
        {
            automobili=a,
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
            searchQuery = searchQuery
        };
        return View(kola);
        
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
