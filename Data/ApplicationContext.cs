using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TripAdvisor.Data
{
    public partial class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<CrewSalary> CrewSalaries { get; set; } = null!;
        public virtual DbSet<Food> Foods { get; set; } = null!;
        public virtual DbSet<Fuel> Fuels { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Ship> Ships { get; set; } = null!;
        public virtual DbSet<Trip> Trips { get; set; } = null!;
        public virtual DbSet<TripCity> TripCities { get; set; } = null!;
        public virtual DbSet<TripMember> TripMembers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=TripAdvisor;User ID=SA;Password=postgreS12");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.Image)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CrewSalary>(entity =>
            {
                entity.ToTable("CrewSalary");

                entity.HasOne(d => d.CrewMember)
                    .WithMany(p => p.CrewSalaries)
                    .HasForeignKey(d => d.CrewMemberId)
                    .HasConstraintName("FK__CrewSalar__CrewM__440B1D61");
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.ToTable("Food");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Fuel>(entity =>
            {
                entity.ToTable("Fuel");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Member__RoleId__3C69FB99");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ship>(entity =>
            {
                entity.ToTable("Ship");

                entity.Property(e => e.Image)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.ToTable("Trip");

                entity.Property(e => e.DateFrom).HasColumnType("date");

                entity.Property(e => e.DateTo).HasColumnType("date");

                entity.Property(e => e.ShipName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TripCity>(entity =>
            {
                entity.ToTable("TripCity");

                entity.Property(e => e.StopDuration)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.City)
                    .WithMany(p => p.TripCities)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK__TripCity__CityId__49C3F6B7");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.TripCities)
                    .HasForeignKey(d => d.TripId)
                    .HasConstraintName("FK__TripCity__TripId__48CFD27E");
            });

            modelBuilder.Entity<TripMember>(entity =>
            {
                entity.ToTable("TripMember");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.TripMembers)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK__TripMembe__Membe__4D94879B");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.TripMembers)
                    .HasForeignKey(d => d.TripId)
                    .HasConstraintName("FK__TripMembe__TripI__4CA06362");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
