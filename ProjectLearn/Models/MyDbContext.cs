using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectLearn.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClientTbl> ClientTbls { get; set; }

    public virtual DbSet<FlightBook> FlightBooks { get; set; }

    public virtual DbSet<FlightCheck> FlightChecks { get; set; }

    public virtual DbSet<FlightRecord> FlightRecords { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<UserTbl> UserTbls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientTbl>(entity =>
        {
            entity.HasKey(e => e.Clientid).HasName("PK__ClientTb__E6711E0CD1D948D2");

            entity.ToTable("ClientTbl");

            entity.Property(e => e.ConfirmPassword)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FlightBook>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("PK__FlightBo__8A9E14EEDEB0A060");

            entity.ToTable("FlightBook");

            entity.Property(e => e.ArrivalTime).HasColumnType("datetime");
            entity.Property(e => e.Class).HasMaxLength(50);
            entity.Property(e => e.DepartureTime).HasColumnType("datetime");
            entity.Property(e => e.FlightName).HasMaxLength(100);
            entity.Property(e => e.FlightNumber).HasMaxLength(50);
            entity.Property(e => e.FlightSeat).HasMaxLength(20);
            entity.Property(e => e.FreMeal)
                .HasMaxLength(100)
                .HasColumnName("Fre_Meal");
            entity.Property(e => e.FreeBaggage)
                .HasMaxLength(100)
                .HasColumnName("Free_Baggage");
            entity.Property(e => e.FromPlace).HasMaxLength(100);
            entity.Property(e => e.PassengerAadhaar).HasMaxLength(20);
            entity.Property(e => e.PassengerMobile).HasMaxLength(15);
            entity.Property(e => e.PassengerName).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SeatType).HasMaxLength(50);
            entity.Property(e => e.ToPlace).HasMaxLength(100);
        });

        modelBuilder.Entity<FlightCheck>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FlightCh__3213E83FBB21D74A");

            entity.ToTable("FlightCheck");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FromPlace).HasMaxLength(200);
            entity.Property(e => e.ToPlace).HasMaxLength(200);
        });

        modelBuilder.Entity<FlightRecord>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("PK__FlightRe__8A9E14EE8E8F394B");

            entity.ToTable("FlightRecord");

            entity.Property(e => e.ArrivalTime).HasColumnType("datetime");
            entity.Property(e => e.Class).HasMaxLength(20);
            entity.Property(e => e.DepartureTime).HasColumnType("datetime");
            entity.Property(e => e.FlightName).HasMaxLength(100);
            entity.Property(e => e.FlightNumber).HasMaxLength(20);
            entity.Property(e => e.FlightSeat).HasMaxLength(20);
            entity.Property(e => e.FlightStatus).HasMaxLength(30);
            entity.Property(e => e.FreeBaggage)
                .HasMaxLength(10)
                .HasColumnName("Free_Baggage");
            entity.Property(e => e.FreeMeal)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Free_Meal");
            entity.Property(e => e.FromPlace).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SeatType).HasMaxLength(20);
            entity.Property(e => e.ToPlace).HasMaxLength(100);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.FatherName)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.StudentGender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StudentName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserTbl>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserTbl");

            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
