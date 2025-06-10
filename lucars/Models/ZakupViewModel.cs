using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace lucars.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
public class ZakupViewModel
{
    public int automobilID { get; set; }

    [ValidateNever]
    public Automobil automobil { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime zakupljenOd { get; set; } = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Utc);

    [Required]
    [DataType(DataType.Date)]
    public DateTime zakupljenDo { get; set; } = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Utc);
}
