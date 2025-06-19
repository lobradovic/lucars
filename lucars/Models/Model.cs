using System.ComponentModel.DataAnnotations;

namespace lucars.Models;

public class Model
{
    [Key]
    public int modelID { get; set; }
    public string nazivModela { get; set; }

    public int idMarka { get; set; }
    public Marka Markas { get; set; }
    
}