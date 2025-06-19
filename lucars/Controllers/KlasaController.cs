using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using lucars.Models;
using lucars.Data;
using System.Net.Mime;
using AspNetCoreGeneratedDocument;
using Microsoft.EntityFrameworkCore;

namespace Strong_eCourses.Controllers;

public class KlasaController : Controller
{
    public readonly ApplicationDbContext context;
    public KlasaController(ApplicationDbContext context)
    {
        this.context = context;
    }

    [Authorize(Roles ="Admin")]
    public IActionResult Index()
    {
        var klase = context.Klasas.ToList();
        return View(klase);
    }

    [Authorize(Roles ="Admin")]
    public IActionResult InsertKlasa()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> InsertKlasa(Klasa k)
    {
        if (!ModelState.IsValid)
        {
            return View(k);
        }

        var klasa = new Klasa();
        klasa.nazivKlase = k.nazivKlase;

        await context.Klasas.AddAsync(klasa);
        await context.SaveChangesAsync();

        return RedirectToAction("Index", "Klasa");
    }
    
    [Authorize(Roles = "Admin")]
    public IActionResult UpdateKlasa(int id)
    {
        var klasa = context.Klasas.Where(k => k.klasaID == id).FirstOrDefault();
        if (klasa == null)
        {
            return NotFound();
        }

        return View(klasa);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> UpdateKlasa(Klasa klasa)
    {
        if (!ModelState.IsValid)
        {
            return View(klasa);
        }
        var k = context.Klasas.FirstOrDefault(k => k.klasaID == klasa.klasaID);
        if (k == null)
        {
            return NotFound();
        }


        k.nazivKlase = klasa.nazivKlase;

        await context.SaveChangesAsync();

        return RedirectToAction("Index", "Klasa");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> DeleteKlasa(int id)
    {
        var klasa = context.Klasas.Find(id);
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index", "Klasa");
        }

        context.Klasas.Remove(klasa);
        await context.SaveChangesAsync();
        return RedirectToAction("Index", "Klasa");
    }


}