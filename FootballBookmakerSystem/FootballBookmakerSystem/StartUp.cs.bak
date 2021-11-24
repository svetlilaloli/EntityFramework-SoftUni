using Microsoft.EntityFrameworkCore;
using P03_FootballBetting.Data;
using System;

namespace FootballBookmakerSystem
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            FootballBettingContext dbContext = new FootballBettingContext();
            dbContext.Database.Migrate();
            Console.WriteLine("Database created successfully");
            //dbContext.Database.EnsureCreated();
            //Console.WriteLine("Created");
            //Console.WriteLine("Delete database? (Y/N)");
            //if (Console.ReadLine() == "Y")
            //{
            //    dbContext.Database.EnsureDeleted();
            //}
        }
    }
}
