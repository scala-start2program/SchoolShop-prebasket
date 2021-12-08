using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolShop.Models;

namespace SchoolShop.Data
{
    public class SchoolShopContext : DbContext
    {
        public SchoolShopContext (DbContextOptions<SchoolShopContext> options)
            : base(options)
        {
        }

        public DbSet<SchoolShop.Models.Article> Article { get; set; }

        public DbSet<SchoolShop.Models.Basket> Basket { get; set; }

        public DbSet<SchoolShop.Models.Brand> Brand { get; set; }

        public DbSet<SchoolShop.Models.Category> Category { get; set; }

        public DbSet<SchoolShop.Models.Order> Order { get; set; }
        public DbSet<SchoolShop.Models.OrderDetail> OrderDetail { get; set; }

        public DbSet<SchoolShop.Models.User> User { get; set; }
        public DbSet<SchoolShop.Models.Score> Scores { get; set; }
    }
}
