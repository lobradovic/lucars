using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using lucars.Models;
using lucars.Data;
using Microsoft.AspNetCore.Components.Forms;

namespace lucars.Controllers;

public class BookingController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public readonly ApplicationDbContext context;

    public BookingController(ApplicationDbContext context)
    {
        this.context = context;
    }
    public IActionResult Index(int id)
    {
        var automobil = context.Automobils
            .Include(a => a.Model)
            .ThenInclude(a => a.Markas)
            .Include(a => a.Klasa)
            .FirstOrDefault(a => a.automobilID == id);

        if (automobil == null)
        {
            return NotFound();
        }

        return View(automobil);
    }
    
}