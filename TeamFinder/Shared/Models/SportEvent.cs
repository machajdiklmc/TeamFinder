namespace TeamFinder.Shared.Models
{
    public class SportEvent
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public SportEvent(string name, DateTime date)
        {
            Name = name;
            Date = date;
        }
    }
}
