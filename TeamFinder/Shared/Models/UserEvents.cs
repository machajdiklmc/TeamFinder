namespace TeamFinder.Shared.Models;

public class UserEvents
{
    //public Guid Id { get; set; }
    public string UserId { get; set; }
    public Guid SportEventId { get; set; }
    public User User { get; set; }
    public SportEvent SportEvent { get; set; }
    public RelationshipType Type { get; set; }
}

public enum RelationshipType
{
    None=0,
    Joined=2,
    Owner=1
}