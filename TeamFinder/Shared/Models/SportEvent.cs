using System.Collections.Immutable;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace TeamFinder.Shared.Models
{
    public class SportEvent
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(150)]
        public string Description { get; set; }
        
        [Required]
        [MinLength(3)]
        public string Sport { get; set; }
        
        public DateTime Date { get; set; }
        public string OwnerId { get; set; }
        public RelationshipType Type { get; set; }
        public SportEventLocation Location { get; set; }
        private int? _countId = null;

        public int? CountId
        {
            get => _countId;
            set
            {
                if (_countId != value)
                {
                    _countId = value;
                    OnCountIdChanged?.Invoke();
                }
            }
        }

        public delegate void CountIdChange();  // delegate

        public event CountIdChange OnCountIdChanged;

        public SportEvent()
        {
            
        }
        public SportEvent(string name, DateTime date, string description)
        {
            Name = name;
            Date = date;
            Description = description;
        }
    }
}