namespace lucars.Models;

public class Automobil
{
    public int automobilID { get; set; }
    public int godinaProizvodnje { get; set; }
    public DateTime registrovanDo { get; set; }
    public double cena { get; set; }
    public int idModel { get; set; }
    public Model model { get; set; }

    public int idKlasa { get; set; }
    public Klasa klasa { get; set; }
    
}