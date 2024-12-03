using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using QBOTECH.DELIVERY.CORE.Entities;

namespace QBOTECH.DELIVERY.INFRASTRUCTURE.Data;

public partial class QbotechDeliveryContext : DbContext
{
    public QbotechDeliveryContext()
    {
    }

    public QbotechDeliveryContext(DbContextOptions<QbotechDeliveryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Deliveries> Deliveries { get; set; }

    public virtual DbSet<SubscriptionPlans> SubscriptionPlans { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=qbotech.net;port=3306;uid=qbotech_userdelivery;pwd=BNy8FN25H&@h;database=qbotech_delivery", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.39-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<Deliveries>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.UserId, "UserId");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.DeliveryAddress).HasMaxLength(255);
            entity.Property(e => e.PickupAddress).HasMaxLength(255);
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'Pendiente'")
                .HasColumnType("enum('Pendiente','En camino','Entregado')");
            entity.Property(e => e.UserId).HasColumnType("int(11)");
        });

        modelBuilder.Entity<SubscriptionPlans>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.DeliveryLimit).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price).HasPrecision(10, 2);
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.HasIndex(e => e.SubscriptionPlanId, "SubscriptionPlanId");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.SubscriptionPlanId).HasColumnType("int(11)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
