namespace TeamFinder.Shared.Models
{
    public class SportEvent
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sport { get; set; }
        public DateTime Date { get; set; }
        public SportEvent(string name, DateTime date, string description)
        {
            Name = name;
            Date = date;
            Description = description;
        }
    }
}
