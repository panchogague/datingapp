using DateApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DateApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<ValuesModel> Values { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<PhotoModel> Photos {get;set;}
        public DbSet<LikeModel> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<LikeModel>()
                .HasKey(k => new { k.LikerID, k.LikeeID });

            builder.Entity<LikeModel>()
                .HasOne(u => u.Likee)
                .WithMany(u => u.Likers)
                .HasForeignKey(u => u.LikeeID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<LikeModel>()
               .HasOne(u => u.Liker)
               .WithMany(u => u.Likees)
               .HasForeignKey(u => u.LikerID)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
