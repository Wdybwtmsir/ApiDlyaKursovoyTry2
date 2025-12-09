using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiDlyaKursovoyTry2.Models;

public partial class NormalnayaKursovayaContext : DbContext
{
    public NormalnayaKursovayaContext()
    {
    }

    public NormalnayaKursovayaContext(DbContextOptions<NormalnayaKursovayaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Archive> Archives { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<NumbersLuxeAndPoluLuxe> NumbersLuxeAndPoluLuxes { get; set; }

    public virtual DbSet<NumbersOther> NumbersOthers { get; set; }

    public virtual DbSet<RasschetnieCartochki> RasschetnieCartochkis { get; set; }

    public virtual DbSet<RegistrationCard> RegistrationCards { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.ToTable("Admin");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(2000)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Archive>(entity =>
        {
            entity.HasKey(e => e.IdArchive).HasName("Archive_PK");

            entity.ToTable("Archive");

            entity.HasIndex(e => e.IdClient, "IX_Archive_idClient");

            entity.Property(e => e.IdArchive).HasColumnName("idArchive");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.HomeAddress)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IdClient).HasColumnName("idClient");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SerialAndNumberOfPasport)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Sex)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SurName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TypeOfDocument)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Archives)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Archive_Client_FK");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("Client_PK");

            entity.ToTable("Client");

            entity.Property(e => e.IdClient).HasColumnName("idClient");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.HomeAddress)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Sex)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SurName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TypeOfDocument)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<NumbersLuxeAndPoluLuxe>(entity =>
        {
            entity.HasKey(e => e.IdNumbersLuxeAndPoluLuxe).HasName("Numbers_LuxeAndPoluLuxe_PK");

            entity.ToTable("Numbers LuxeAndPoluLuxe");

            entity.HasIndex(e => e.IdClient, "IX_Numbers LuxeAndPoluLuxe_idClient");

            entity.Property(e => e.IdNumbersLuxeAndPoluLuxe).HasColumnName("idNumbersLuxeAndPoluLuxe");
            entity.Property(e => e.CostPerDay).HasColumnType("decimal(38, 0)");
            entity.Property(e => e.FreeOrClose)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IdClient).HasColumnName("idClient");
            entity.Property(e => e.InfoAboutBron)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TypeOfNumber)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.NumbersLuxeAndPoluLuxes)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Numbers_LuxeAndPoluLuxe_Client_FK");
        });

        modelBuilder.Entity<NumbersOther>(entity =>
        {
            entity.HasKey(e => e.IdNumbersOther).HasName("Numbers_other_PK");

            entity.ToTable("Numbers other");

            entity.HasIndex(e => e.IdClient, "IX_Numbers other_idClient");

            entity.Property(e => e.IdNumbersOther).HasColumnName("idNumbersOther");
            entity.Property(e => e.CostPerDay).HasColumnType("decimal(38, 0)");
            entity.Property(e => e.IdClient).HasColumnName("idClient");
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TypeOfNumber)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.NumbersOthers)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Numbers_other_Client_FK");
        });

        modelBuilder.Entity<RasschetnieCartochki>(entity =>
        {
            entity.HasKey(e => e.IdRegistrationCards).HasName("RasschetnieCartochki_PK");

            entity.ToTable("RasschetnieCartochki");

            entity.HasIndex(e => e.IdClient, "IX_RasschetnieCartochki_idClient");

            entity.Property(e => e.IdRegistrationCards).HasColumnName("idRegistrationCards");
            entity.Property(e => e.IdClient).HasColumnName("idClient");
            entity.Property(e => e.OplataBroni)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.RasschetnieCartochkis)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RasschetnieCartochki_Client_FK");
        });

        modelBuilder.Entity<RegistrationCard>(entity =>
        {
            entity.HasKey(e => e.IdRegistrationCards).HasName("RegistrationCards_PK");

            entity.HasIndex(e => e.IdClient, "IX_RegistrationCards_idClient");

            entity.Property(e => e.IdRegistrationCards).HasColumnName("idRegistrationCards");
            entity.Property(e => e.HomeAddress)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IdClient).HasColumnName("idClient");
            entity.Property(e => e.Sex)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TypeOfDocument)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.RegistrationCards)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("RegistrationCards_Client_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
