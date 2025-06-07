using System.ComponentModel.DataAnnotations;

namespace lucars.Models;

public class Zakup
{
    [Key]
    public int zakupID { get; set; }
    public int idAutomobil { get; set; }
    public Automobil Automobil { get; set; }

    public string idUser { get; set; }
    public ApplicationUser User { get; set; }


    public DateTime zakupljenOd { get; set; }
    public DateTime zakupljenDo { get; set; }
    
}
