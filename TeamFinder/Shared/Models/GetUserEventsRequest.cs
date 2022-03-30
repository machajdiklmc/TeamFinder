namespace TeamFinder.Shared.Models;

public class GetUserEventsRequest
{
    public string UserId { get; set; }
    public RelationshipType? Type { get; set; }

    public GetUserEventsRequest(string userId, RelationshipType? type)
    {
        UserId = userId;
        Type = type;
    }
}