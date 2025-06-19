using System.ComponentModel.DataAnnotations;

namespace lucars.Models;

public class Klasa
{
    [Key]
    public int klasaID { get; set; }
    public string nazivKlase { get; set;}
}