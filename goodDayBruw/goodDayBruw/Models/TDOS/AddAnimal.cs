using System.ComponentModel.DataAnnotations;

namespace goodDayBruw.Models.TDOS;

public class AddAnimal
{
    [Required]
    [MaxLength(3)]
    public string Name { get; set; }
    [Required]
    [MaxLength(200)]
    public string Description { get; set; }
    
    
}