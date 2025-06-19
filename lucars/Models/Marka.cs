using System.ComponentModel.DataAnnotations;

namespace lucars.Models;

public class Marka
{
    [Key]
    public int markaID { get; set; }
    public string nazivMarke { get; set;}
}
