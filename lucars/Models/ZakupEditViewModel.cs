using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace lucars.Models;

public class ZakupEditViewModel
{
    public int zakupID { get; set; }

    public int automobilID { get; set; }

    [ValidateNever]
    public Automobil automobil { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime zakupljenOd { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

    [Required]
    [DataType(DataType.Date)]
    public DateTime zakupljenDo { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

    [ValidateNever]
    public List<SelectListItem> automobili { get; set; } = new();
}