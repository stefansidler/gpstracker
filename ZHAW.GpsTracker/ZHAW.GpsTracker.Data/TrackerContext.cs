using System.Data.Entity;
using ZHAW.GpsTracker.Data.Model;

namespace ZHAW.GpsTracker.Data
{
    public class TrackerContext : DbContext
    {
        public TrackerContext() : base("DefaultConnection")
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Session> Sessions { get; set; }
    }
}
