namespace TeamFinder.Shared.Models;

public class UserEventsRequest
{
    public string UserId { get; set; }
    public Guid SportEventId { get; set; }
    public RelationshipType? Type { get; set; }

    public UserEventsRequest(string userId, Guid sportEventId, RelationshipType? type=null)
    {
        this.UserId = userId;
        this.Type = type;
        SportEventId = sportEventId;
    }
}