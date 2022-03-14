using System.Collections.Immutable;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TeamFinder.Shared.Models
{
    public class SportEvent : INotifyPropertyChanged
    {
        private bool _userIsJoined;
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sport { get; set; }
        public DateTime Date { get; set; }

        public bool UserIsJoined
        {
            get => _userIsJoined;
            set => SetField(ref _userIsJoined, value);
        }

        public SportEvent(string name, DateTime date, string description)
        {
            Name = name;
            Date = date;
            Description = description;
        }
    }
}
