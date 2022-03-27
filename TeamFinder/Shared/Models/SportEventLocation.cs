using System.ComponentModel.DataAnnotations;

namespace TeamFinder.Shared.Models;

public class SportEventLocation
{
    public Guid Id { get; set; }
    [Required]
    public double Latitude { get; set; }
    [Required]
    public double Longitude { get; set; }
    [Required]
    [MinLength(3)]
    public string City { get; set; }
}