using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace lucars.Models;

public class ModelViewModel
{
    [ValidateNever]
    public int idMarka { get; set; }
    [ValidateNever]
    public string nazivModela { get; set; }

    [ValidateNever]
    public Model Model { get; set; }

    [ValidateNever]
    public List<Marka> Marke { get; set; }
}

