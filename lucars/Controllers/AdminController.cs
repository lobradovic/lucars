using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using lucars.Models;
using lucars.Data;

namespace Strong_eCourses.Controllers;

public class AdminController : Controller
{
    public readonly ApplicationDbContext context;
    public AdminController(ApplicationDbContext context)
    {
        this.context = context;
    }

    [Authorize(Roles="Admin")]
    public IActionResult Index()
    {
        return View();
    }
}