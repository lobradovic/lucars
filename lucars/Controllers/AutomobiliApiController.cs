using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using lucars.Models;
using lucars.Data;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;

namespace Strong_eCourses.Controllers;

[Route("api/Automobil")]
[ApiController]
public class AutomobiliApiController : Controller
{
    public readonly ApplicationDbContext context;
    public AutomobiliApiController(ApplicationDbContext context)
    {
        this.context = context;
    }
    [HttpGet]
    public async Task<ActionResult<List<Automobil>>> DohvatiSve()
    {
        var sve = await context.Automobils
            .Include(m => m.Model)
            .ThenInclude(m => m.Markas)
            .Include(k => k.Klasa)
            .ToListAsync();

        return Ok(sve);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Automobil>> DohvatiJedan(int id)
    {
        var auto = await context.Automobils
            .Include(m => m.Model)
            .ThenInclude(m => m.Markas)
            .Include(k => k.Klasa)
            .FirstOrDefaultAsync(a => a.automobilID == id);

        if (auto == null)
            return NotFound();

        return Ok(auto);
    }

    [HttpPost]
    public async Task<ActionResult<Automobil>> KreirajNovi([FromBody] Automobil input)
    {
        var novi = new Automobil
        {
            godinaProizvodnje = input.godinaProizvodnje,
            registrovanDo = DateTime.SpecifyKind(input.registrovanDo, DateTimeKind.Utc),
            cena = input.cena,
            idModel = input.idModel,
            idKlasa = input.idKlasa
        };

        context.Automobils.Add(novi);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(DohvatiJedan), new { id = novi.automobilID }, novi);
    }

    
    [HttpGet("modeli")]
    public IActionResult GetModeli()
    {
        var modeli = context.Models
            .Include(m => m.Markas)
            .Select(m => new
            {
                id = m.modelID,
                naziv = m.Markas.nazivMarke + " " + m.nazivModela
            })
            .ToList();

        return Ok(modeli);
    }
    [HttpGet("klase")]
    public IActionResult GetKlase()
    {
        var klase = context.Klasas
            .Select(k => new
            {
                id = k.klasaID,
                naziv = k.nazivKlase
            }).ToList();

        return Ok(klase);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Obrisi(int id)
    {
        var auto = await context.Automobils
            .Include(m => m.Model)
            .ThenInclude(m => m.Markas)
            .Include(k => k.Klasa)
            .FirstOrDefaultAsync(a => a.automobilID == id);
            
        if (auto == null)
        return NotFound();


        context.Automobils.Remove(auto);
        await context.SaveChangesAsync();
        return NoContent();
    }
}