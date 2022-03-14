namespace TeamFinder.Shared.Models;

public class JoinedEvents
{
    //public Guid Id { get; set; }
    public string? UserId { get; set; }
    public Guid SportEventId { get; set; }
    public SportEvent SportEvent { get; set; }
}