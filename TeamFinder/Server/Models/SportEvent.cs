﻿using System.ComponentModel.DataAnnotations.Schema;

namespace TeamFinder.Server.Models
{
    public class SportEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sport { get; set; }
        public DateTime Date { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}
