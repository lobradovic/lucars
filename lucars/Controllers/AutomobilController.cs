using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using lucars.Models;
using lucars.Data;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Strong_eCourses.Controllers;
public class AutomobilController : Controller
{
    public readonly ApplicationDbContext context;
    public AutomobilController(ApplicationDbContext context)
    {
        this.context = context;
    }

    [Authorize(Roles="Admin")]
    public IActionResult Index()
    {
        return View();
    }
}