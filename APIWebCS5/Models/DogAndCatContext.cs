using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIWebCS5.Models;

public partial class DogAndCatContext : DbContext
{
    public DogAndCatContext()
    {
    }

    public DogAndCatContext(DbContextOptions<DogAndCatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<DetailOrder> DetailOrders { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Medium> Media { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=DogAndCat;Integrated Security=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(25);
            entity.Property(e => e.PhoneNumber).HasMaxLength(10);
            entity.Property(e => e.UserName).HasMaxLength(25);
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.ToTable("Blog");

            entity.HasIndex(e => e.IdAccount, "IX_Blog_IdAccount");

            entity.Property(e => e.Headline).HasMaxLength(100);

            entity.HasOne(d => d.IdAccountNavigation).WithMany(p => p.Blogs).HasForeignKey(d => d.IdAccount);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<DetailOrder>(entity =>
        {
            entity.ToTable("DetailOrder");

            entity.HasIndex(e => e.OrderId, "IX_DetailOrder_OrderId");

            entity.HasIndex(e => e.ProductId, "IX_DetailOrder_ProductId");

            entity.HasOne(d => d.Order).WithMany(p => p.DetailOrders).HasForeignKey(d => d.OrderId);

            entity.HasOne(d => d.Product).WithMany(p => p.DetailOrders).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.ToTable("Image");
        });

        modelBuilder.Entity<Medium>(entity =>
        {
            entity.HasIndex(e => e.IdImage, "IX_Media_IdImage");

            entity.HasIndex(e => e.IdProduct, "IX_Media_IdProduct");

            entity.HasOne(d => d.IdImageNavigation).WithMany(p => p.Media).HasForeignKey(d => d.IdImage);

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Media).HasForeignKey(d => d.IdProduct);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.HasIndex(e => e.AccountId, "IX_Order_AccountId");

            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Account).WithMany(p => p.Orders).HasForeignKey(d => d.AccountId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.HasIndex(e => e.CategoryId, "IX_Product_CategoryId");

            entity.Property(e => e.Color)
                .HasMaxLength(10)
                .HasDefaultValue("");
            entity.Property(e => e.Hair)
                .HasMaxLength(10)
                .HasDefaultValue("");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Popular)
                .HasMaxLength(10)
                .HasDefaultValue("");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Size)
                .HasMaxLength(5)
                .HasDefaultValue("");
            entity.Property(e => e.Status).HasMaxLength(15);
            entity.Property(e => e.StatusHair)
                .HasMaxLength(10)
                .HasDefaultValue("");

            entity.HasOne(d => d.Category).WithMany(p => p.Products).HasForeignKey(d => d.CategoryId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
