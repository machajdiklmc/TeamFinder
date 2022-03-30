namespace TeamFinder.Client;

public class GeocodePosition
{
    public int id { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public GeocodeCoords coords { get; set; }
}