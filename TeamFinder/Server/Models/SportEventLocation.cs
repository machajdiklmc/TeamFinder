namespace TeamFinder.Server.Models;

public class SportEventLocation
{
    public Guid Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string City { get; set; }
}