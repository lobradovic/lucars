using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using lucars.Models;
using lucars.Data;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;

namespace lucars.Controllers;

public class BookingController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public readonly ApplicationDbContext context;
    private UserManager<ApplicationUser> userManager;
    public BookingController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        this.context = context;
        this.userManager = userManager;
    }
    public IActionResult Index(int id)
    {
        var automobil = context.Automobils
            .Include(a => a.Model).ThenInclude(m => m.Markas)
            .Include(a => a.Klasa)
            .FirstOrDefault(a => a.automobilID == id);

        if (automobil == null)
            return NotFound();

        var viewModel = new ZakupViewModel
        {
            automobilID = id,
            automobil = automobil
        };
        return View(viewModel);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ZakupViewModel z)
    {
        if (!ModelState.IsValid)
        {
            var error = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            var allErrors = string.Join("; ", error);
            ViewBag.Message = allErrors;

            z.automobil = await context.Automobils
                .Include(a => a.Model).ThenInclude(m => m.Markas)
                .Include(a => a.Klasa)
                .FirstOrDefaultAsync(a => a.automobilID == z.automobilID);

            return View(z);
        }

        var korisnik = userManager.GetUserId(User);

        var zakup = new Zakup
        {
            idAutomobil = z.automobilID,
            idUser = korisnik,
            zakupljenOd = DateTime.SpecifyKind(z.zakupljenOd, DateTimeKind.Utc),
            zakupljenDo = DateTime.SpecifyKind(z.zakupljenDo, DateTimeKind.Utc)
        };

        context.Zakups.Add(zakup);
        await context.SaveChangesAsync();

        return RedirectToAction("Booking", "Booking");
    }

    public IActionResult Booking()
    {
        var korisnik = userManager.GetUserId(User);
        var zakupi = context.Zakups.Include(a => a.Automobil)
            .ThenInclude(a => a.Model).ThenInclude(m => m.Markas)
            .Where(z => z.idUser == korisnik).ToList();

        return View(zakupi);
    }
    
}