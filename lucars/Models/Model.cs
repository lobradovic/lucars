namespace lucars.Models;

public class Model
{
    public int modelID { get; set; }
    public string nazivModela { get; set; }

    public int idMarka { get; set; }
    public Marka marka { get; set; }
    
}