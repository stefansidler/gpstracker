using System;
using System.ComponentModel.DataAnnotations;

namespace ZHAW.GpsTracker.Data.Model
{
    public class Position
    {
        [Key]
        public int Id { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public double Speed { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public User User { get; set; }
    }
}
