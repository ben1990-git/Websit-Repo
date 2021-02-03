using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace DAL.DataBase
{
    public class WebSiteDBContext : DbContext
    {
     public   DbSet<Product> Products { get; set; }
      public  DbSet<User> Users { get; set; }

        public WebSiteDBContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(x => x.Id).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.LastName).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.BirthDate).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.Email).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.UserName).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<User>().HasIndex(x => x.UserName).IsUnique();
            modelBuilder.Entity<User>().Property(x => x.Password).HasMaxLength(50).IsRequired();

            modelBuilder.Entity<Product>().Property(x => x.Title).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Product>().Property(x => x.ShortDiscription).HasMaxLength(500).IsRequired();
            modelBuilder.Entity<Product>().Property(x => x.LongDiscription).HasMaxLength(4000).IsRequired();
            modelBuilder.Entity<Product>().Property(x => x.Date).IsRequired();

            modelBuilder.Entity<Product>().Property(p => p.Picture1).HasColumnType("image");
            modelBuilder.Entity<Product>().Property(p => p.Picture2).HasColumnType("image");
            modelBuilder.Entity<Product>().Property(p => p.Picture3).HasColumnType("image");

            modelBuilder.Entity<Product>().Property(x => x.Price).HasColumnType("decimal(18,0)");
            modelBuilder.Entity<Product>().Property(x => x.Price).IsRequired();
            modelBuilder.Entity<Product>().Property(x => x.State).IsRequired();



            modelBuilder.Entity<Product>().HasOne(pro => pro.Owner).WithMany(user => user.ProductsToSell).HasForeignKey(s => s.OwnerId);
            modelBuilder.Entity<Product>().HasOne(pro => pro.Buyer).WithMany(user => user.ProductsToBuy).HasForeignKey(s => s.UserId);
            
        }
    }
}
