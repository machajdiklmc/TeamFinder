using TeamFinder.Shared.Models;

namespace TeamFinder.Client.Pages.Models
{
    public class SportEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sport { get; set; }
        public DateTime Date { get; set; }
        public RelationshipType Type { get; set; }

        public SportEvent(string name, DateTime date, string description)
        {
            Name = name;
            Date = date;
            Description = description;
        }
        
        public SportEvent(SportEvent sportEvent)
        {
            Name = sportEvent.Name;
            Date = sportEvent.Date;
            Description = sportEvent.Description;
            Id = sportEvent.Id;
            Sport = sportEvent.Sport;
            Type = sportEvent.Type;
            
        }

        public SportEvent()
        {
            
        }
        public TeamFinder.Shared.Models.SportEvent ToShared(string ownerId)
        {
            return new TeamFinder.Shared.Models.SportEvent(Name,Date,Description)
            {
                Id = this.Id,
                Sport = this.Sport,
                OwnerId = ownerId
            };
        }
    }
}
