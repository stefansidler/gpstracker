using System.Data.Entity;
using ZHAW.GpsTracker.Data.Model;

namespace ZHAW.GpsTracker.Data
{
    public class TrackerContext : DbContext
    {
        public TrackerContext() : base()
        {
            
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
    }
}
