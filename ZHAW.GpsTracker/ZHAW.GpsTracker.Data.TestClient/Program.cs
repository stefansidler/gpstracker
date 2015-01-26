using System;
using System.Linq;

namespace ZHAW.GpsTracker.Data.TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new TrackerContext())
            {
                Console.WriteLine("Sessions: " + dbContext.Sessions.Count());
                Console.WriteLine("Users: " + dbContext.Users.Count());
                Console.WriteLine("Positions: " + dbContext.Positions.Count());
            }

            Console.ReadLine();
        }
    }
}
