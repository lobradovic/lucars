using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using lucars.Models;
using lucars.Data;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

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

    public IActionResult AdminBooking()
    {
        var zakupi = context.Zakups.Include(a => a.Automobil)
            .ThenInclude(a => a.Model).ThenInclude(m => m.Markas).Include(k=>k.User).ToList();

        return View(zakupi);        
    }

    public IActionResult Update(int id)
    {
        var z = context.Zakups.Include(z => z.Automobil).FirstOrDefault(z => z.zakupID == id);
        if (z == null) return NotFound();

        var viewModel = new ZakupEditViewModel
        {
            zakupID = z.zakupID,
            automobilID = z.idAutomobil,
            zakupljenOd = z.zakupljenOd,
            zakupljenDo = z.zakupljenDo,
            automobili = context.Automobils
            .Include(a => a.Model).ThenInclude(a => a.Markas).
            Include(a => a.Klasa).Select(a => new SelectListItem
            {
                Value = a.automobilID.ToString(),
                Text = a.automobilID + " " + a.Model.Markas.nazivMarke + " " + a.Model.nazivModela + " " + a.godinaProizvodnje
            }).ToList()
        };

        return View(viewModel);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteZakup(int id)
    {
        var z = await context.Zakups.FindAsync(id);
        if (z == null)
        {
            return NotFound();
        }
        context.Zakups.Remove(z);
        await context.SaveChangesAsync();

        return RedirectToAction("Booking", "Booking");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(ZakupEditViewModel z)
    {
        var zakup = context.Zakups.Find(z.zakupID);
        if (zakup == null) return NotFound();
        if (!ModelState.IsValid)
        {
            return View(zakup);
        }


        zakup.idAutomobil = z.automobilID;
        zakup.zakupljenOd = DateTime.SpecifyKind(z.zakupljenOd,DateTimeKind.Utc);
        zakup.zakupljenDo = DateTime.SpecifyKind(z.zakupljenDo,DateTimeKind.Utc);

        context.SaveChanges();

        return RedirectToAction("Booking", "Booking");

    }
}