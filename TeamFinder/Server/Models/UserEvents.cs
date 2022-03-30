using System.ComponentModel.DataAnnotations.Schema;

namespace TeamFinder.Server.Models;

public class UserEvents
{
    public string UserId { get; set; }
    public Guid SportEventId { get; set; }
    public ApplicationUser User { get; set; }
    public SportEvent SportEvent { get; set; }
    public RelationshipType Type { get; set; }
}

public enum RelationshipType
{
    Joined=0,
    Owner=1
}