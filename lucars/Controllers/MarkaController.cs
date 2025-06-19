using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using lucars.Models;
using lucars.Data;
using System.Net.Mime;
using AspNetCoreGeneratedDocument;

namespace Strong_eCourses.Controllers;

public class MarkaController : Controller
{
    public readonly ApplicationDbContext context;
    public MarkaController(ApplicationDbContext context)
    {
        this.context = context;
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Index()
    {
        var marke = context.Markas.ToList();
        return View(marke);
    }

    [Authorize(Roles ="Admin")]
    public IActionResult InsertMarka()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> InsertMarka(Marka marka)
    {
        if (!ModelState.IsValid)
        {
            return View(marka);
        }
        await context.Markas.AddAsync(marka);
        await context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [Authorize(Roles ="Admin")]
    public IActionResult UpdateMarka(int id)
    {
        var marka = context.Markas
    .FirstOrDefault(m => m.markaID == id);

        if (marka == null)
            return NotFound();

        return View(marka);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> UpdateMarka(Marka marka)
    {
        if (!ModelState.IsValid)
        {
            return View(marka);
        }
        var m = context.Markas.FirstOrDefault(m => m.markaID == marka.markaID);
        if (m == null)
        {
            return NotFound();
        }


        m.nazivMarke = marka.nazivMarke;

        await context.SaveChangesAsync();

        return RedirectToAction("Index", "Marka");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> DeleteMarka(int id)
    {
        var m = await context.Markas.FindAsync(id);
        if (m == null)
        {
            return NotFound();
        }
        context.Markas.Remove(m);
        await context.SaveChangesAsync();

        return RedirectToAction("Index", "Marka");
    }
}