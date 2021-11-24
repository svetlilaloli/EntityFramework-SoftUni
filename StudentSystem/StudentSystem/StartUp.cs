using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data;
using System;

namespace StudentSystem
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            StudentSystemContext dbContext = new StudentSystemContext();
            dbContext.Database.Migrate();
            //dbContext.Database.EnsureCreated();

            //Console.WriteLine("Database created");
            //Console.WriteLine("Do you want to delete the Database (Y/N)?");
            //string result = Console.ReadLine();

            //if (result == "Y")
            //{
            //    dbContext.Database.EnsureDeleted();
            //}
        }
    }
}
