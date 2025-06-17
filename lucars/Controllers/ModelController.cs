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

public class ModelController : Controller
{
    public readonly ApplicationDbContext context;
    public ModelController(ApplicationDbContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        var modeli = context.Models.Include(m => m.Markas).ToList();

        return View(modeli);
    }

    public IActionResult UpdateModel(int id)
    {
        var model = context.Models.Include(m => m.Markas)
                 .FirstOrDefault(m => m.modelID == id);

        // if (modeli == null)
        //     return NotFound();
        var modeli = new ModelViewModel
        {
            Model = model,
            Marke = context.Markas.ToList()
        };
        return View(modeli);

        //return View(modeli);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateModel(ModelViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var m = context.Models.FirstOrDefault(m => m.modelID == model.Model.modelID);
        if (m == null)
        {
            return NotFound();
        }

        m.idMarka=model.idMarka;
        m.nazivModela = model.Model.nazivModela;

        await context.SaveChangesAsync();

        return RedirectToAction("Index", "Model");
    }

    public IActionResult InsertModel()
    {
        var modeli = new ModelViewModel
        {
            Model = new Model(),
            Marke = context.Markas.ToList()
        };


        return View(modeli);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> InsertModel(ModelViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        Model m = new Model();
        m.idMarka = model.idMarka;
        m.nazivModela = model.nazivModela;

        await context.Models.AddAsync(m);
        await context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

}