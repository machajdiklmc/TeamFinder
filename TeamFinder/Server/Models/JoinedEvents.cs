using System.ComponentModel.DataAnnotations.Schema;

namespace TeamFinder.Server.Models;

public class JoinedEvents
{
    //public Guid Id { get; set; }
    public string UserId { get; set; }
    public Guid SportEventId { get; set; }
    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; }
    [ForeignKey(nameof(SportEventId))]
    public SportEvent SportEvent { get; set; }
}