using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace test1.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Cart_Items> Cart_Items { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<Order_Item> Order_Items { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product_Preview_Image> Product_Preview_Images { get; set; }
        public virtual DbSet<Product_Specification> Product_Specifications { get; set; }
        public virtual DbSet<Product_Images> Product_Images { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Reset_Token> Reset_Tokens { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .HasMany(e => e.Cart_Items)
                .WithRequired(e => e.Cart)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Option>()
                .Property(e => e.price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Option>()
                .HasMany(e => e.Order_Items)
                .WithRequired(e => e.Option)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Order_Items)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Options)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Order_Items)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Product_Preview_Images)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Product_Specifications)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Product_Images)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Reviews)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reset_Token>()
                .Property(e => e.token)
                .IsFixedLength();

            modelBuilder.Entity<Sale>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.role_level)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Carts)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Reset_Tokens)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Reviews)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
