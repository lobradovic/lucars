namespace lucars.Models;

public class Zakup
{
    public int zakupID { get; set; }
    public int idAutomobil { get; set; }
    public Automobil automobil { get; set; }

    public string idUser { get; set; }
    public ApplicationUser user { get; set; }


    public DateTime zakupljenOd { get; set; }
    public DateTime zakupljenDo { get; set; }
    
}
