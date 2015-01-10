using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZHAW.GpsTracker.Data.Model
{
    public class Session
    {
        [Key]
        public string Key { get; set; }

        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
