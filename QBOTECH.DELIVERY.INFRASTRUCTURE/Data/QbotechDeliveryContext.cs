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

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<Deliveries>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("deliveries");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DestinationLat)
                .HasPrecision(10, 6)
                .HasColumnName("destination_lat");
            entity.Property(e => e.DestinationLng)
                .HasPrecision(10, 6)
                .HasColumnName("destination_lng");
            entity.Property(e => e.OriginLat)
                .HasPrecision(10, 6)
                .HasColumnName("origin_lat");
            entity.Property(e => e.OriginLng)
                .HasPrecision(10, 6)
                .HasColumnName("origin_lng");
            entity.Property(e => e.PackageDetails)
                .HasColumnType("text")
                .HasColumnName("package_details");
            entity.Property(e => e.RecipientEmail)
                .HasMaxLength(100)
                .HasColumnName("recipient_email");
            entity.Property(e => e.RecipientName)
                .HasMaxLength(100)
                .HasColumnName("recipient_name");
            entity.Property(e => e.RecipientPhone)
                .HasMaxLength(20)
                .HasColumnName("recipient_phone");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(10)
                .HasColumnName("country_code");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
