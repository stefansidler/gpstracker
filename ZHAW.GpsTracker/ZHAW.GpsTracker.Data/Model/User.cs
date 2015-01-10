using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZHAW.GpsTracker.Data.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Session Session { get; set; }

        public List<Position> Positions { get; set; }
    }
}
