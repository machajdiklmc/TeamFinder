using System.Collections.Immutable;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TeamFinder.Shared.Models
{
    public class SportEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sport { get; set; }
        public DateTime Date { get; set; }
        public string OwnerId { get; set; }
        public RelationshipType Type { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public SportEvent(string name, DateTime date, string description)
        {
            Name = name;
            Date = date;
            Description = description;
        }
    }
}
