using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace lucars.Models;

public class Automobil
{
    [Key]
    public int automobilID { get; set; }
    public int godinaProizvodnje { get; set; }
    public DateTime registrovanDo { get; set; }
    public decimal cena { get; set; }
    public int idModel { get; set; }
    public Model Model { get; set; }

    public int idKlasa { get; set; }
    public Klasa Klasa { get; set; }
    
}