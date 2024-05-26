using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class CountdContext : DbContext
{
    public CountdContext()
    {
    }

    public CountdContext(DbContextOptions<CountdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Audience> Audiences { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<HowKnown> HowKnowns { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    public virtual DbSet<TypeGame> TypeGames { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=ayala-vardi;Initial Catalog=countd; Trusted_Connection=True;MultipleActiveResultSets=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Audience>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Audience__3213E83F035C71D3");

            entity.ToTable("Audience");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__games__3213E83FCCF50F40");

            entity.ToTable("games");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AudienceId).HasColumnName("audienceId");
            entity.Property(e => e.DetailsId).HasColumnName("detailsId");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("endDate");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.SettingsId).HasColumnName("settingsId");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("startDate");
            entity.Property(e => e.TypeGameId).HasColumnName("typeGameId");

            entity.HasOne(d => d.Audience).WithMany(p => p.Games)
                .HasForeignKey(d => d.AudienceId)
                .HasConstraintName("FK__games__audienceI__4BAC3F29");

            entity.HasOne(d => d.Details).WithMany(p => p.Games)
                .HasForeignKey(d => d.DetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__games__detailsId__49C3F6B7");

            entity.HasOne(d => d.Settings).WithMany(p => p.Games)
                .HasForeignKey(d => d.SettingsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__games__settingsI__4AB81AF0");

            entity.HasOne(d => d.TypeGame).WithMany(p => p.Games)
                .HasForeignKey(d => d.TypeGameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__games__typeGameI__48CFD27E");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__gender__3213E83F1CA28130");

            entity.ToTable("gender");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<HowKnown>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__howKnown__3213E83F7AAE336A");

            entity.ToTable("howKnown");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__settings__3213E83FB7668DC6");

            entity.ToTable("settings");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Background)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("background");
            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.Music)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("music");
        });

        modelBuilder.Entity<TypeGame>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__typeGame__3213E83F3087EE5A");

            entity.ToTable("typeGame");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Picture)
                .HasMaxLength(50)
                .HasColumnName("picture");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F380107B2");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .HasColumnName("firstName");
            entity.Property(e => e.GenderId).HasColumnName("genderId");
            entity.Property(e => e.HowKnownId).HasColumnName("howKnownId");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .HasColumnName("lastName");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .HasColumnName("phoneNumber");

            entity.HasOne(d => d.Gender).WithMany(p => p.Users)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("FK__users__genderId__3F466844");

            entity.HasOne(d => d.HowKnown).WithMany(p => p.Users)
                .HasForeignKey(d => d.HowKnownId)
                .HasConstraintName("FK__users__howKnownI__403A8C7D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
