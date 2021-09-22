using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageRepo.Models
{
    public class ImageContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        // public DbSet<UserModel> OTHERCLASSES { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
             => options.UseSqlite(@"Data Source=C:\Users\mayank\source\repos\ImageRepo2\Image.db");
    }
}
