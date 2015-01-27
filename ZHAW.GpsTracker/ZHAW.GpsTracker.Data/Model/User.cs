using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ZHAW.GpsTracker.Data.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [JsonIgnore]
        public Session Session { get; set; }

        [JsonIgnore]
        public List<Position> Positions { get; set; }
    }
}
