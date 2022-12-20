using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingCenter.Models;

    public class ShoppingCenterContext : DbContext
    {
        public ShoppingCenterContext (DbContextOptions<ShoppingCenterContext> options)
            : base(options)
        {
        }

        public DbSet<ShoppingCenter.Models.Shop> Shop { get; set; } = default!;
    }
