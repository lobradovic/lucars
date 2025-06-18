using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace lucars.Models;

public class Automobil
{
    [Key]
    public int automobilID { get; set; }
    public int godinaProizvodnje { get; set; }
    public DateTime registrovanDo { get; set; }
    public decimal cena { get; set; }
    public int idModel { get; set; }

    [ValidateNever]
    public Model Model { get; set; }

    public int idKlasa { get; set; }

    [ValidateNever]
    public Klasa Klasa { get; set; }
    
}