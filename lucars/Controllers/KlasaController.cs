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

    public IActionResult Index()
    {
        var klase=context.Klasas.ToList();
        return View(klase);
    }


}