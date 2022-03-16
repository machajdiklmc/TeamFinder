namespace TeamFinder.Shared.Models;

public class UserEventsRequest
{
    public string UserId { get; set; }
    public RelationshipType? Type { get; set; }

    public UserEventsRequest(string userId, RelationshipType? type)
    {
        this.UserId = userId;
        this.Type = type;
    }
}