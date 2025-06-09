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

    public virtual DbSet<DeliveryLocation> DeliveryLocations { get; set; }

    public virtual DbSet<DeliveryStatusHistory> DeliveryStatusHistories { get; set; }

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

            entity.Property(e => e.TrackingNumber)
                .HasMaxLength(50)
                .HasColumnName("tracking_number");

            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasColumnName("status")
                .HasDefaultValue("P");

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
            entity.Property(e => e.OriginDescription)
                .HasMaxLength(255)
                .HasColumnName("origin_description");

            entity.Property(e => e.DestinationDescription)
                .HasMaxLength(255)
                .HasColumnName("destination_description");

            entity.Property(e => e.EstimatedDeliveryDate)
                .HasColumnName("estimated_delivery_date");

            entity.Property(e => e.EstimatedDeliveryTime)
                .HasColumnName("estimated_delivery_time");

            entity.Property(e => e.EstimatedTimeFrom)
                .HasColumnName("estimated_time_from");

            entity.Property(e => e.EstimatedTimeTo)
                .HasColumnName("estimated_time_to");

        });

        modelBuilder.Entity<DeliveryLocation>(entity =>
        {
            entity.ToTable("deliveryLocations");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DeliveryId).HasColumnName("delivery_id");
            entity.Property(e => e.Latitude).HasColumnName("latitude").HasPrecision(10, 7);
            entity.Property(e => e.Longitude).HasColumnName("longitude").HasPrecision(10, 7);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("current_timestamp()");
            entity.HasIndex(e => new { e.DeliveryId, e.CreatedAt }, "idx_deliveryid_createdat");
            entity.HasOne(e => e.Delivery)
                  .WithMany(d => d.DeliveryLocations)
                  .HasForeignKey(e => e.DeliveryId)
                  .HasConstraintName("FK_DeliveryLocation_Deliveries")
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DeliveryStatusHistory>(entity =>
        {
            entity.ToTable("deliveryStatusHistory");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DeliveryId).HasColumnName("delivery_id");
            entity.Property(e => e.PreviousStatus).HasColumnName("previous_status").HasMaxLength(1);
            entity.Property(e => e.NewStatus).HasColumnName("new_status").HasMaxLength(1).IsRequired();
            entity.Property(e => e.ChangedAt).HasColumnName("changed_at");
            entity.Property(e => e.ChangedBy).HasColumnName("changed_by");
            entity.Property(e => e.Comment).HasColumnName("comment").HasMaxLength(255);
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
