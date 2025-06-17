namespace lucars.Models;

public class AutoPageModel
{
    public List<Automobil> automobili { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public string searchQuery { get; set; }
    
}
