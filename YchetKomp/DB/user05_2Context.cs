using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using YchetKomp.Models;

namespace YchetKomp.DB
{
    public partial class user05_2Context : DbContext
    {
        public user05_2Context()
        {
        }

        public user05_2Context(DbContextOptions<user05_2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Cabinet> Cabinets { get; set; } = null!;
        public virtual DbSet<Claim> Claims { get; set; } = null!;
        public virtual DbSet<Corps> Corps { get; set; } = null!;
        public virtual DbSet<Device> Devices { get; set; } = null!;
        public virtual DbSet<DeviceType> DeviceTypes { get; set; } = null!;
        public virtual DbSet<History> Histories { get; set; } = null!;
        public virtual DbSet<Manufacturer> Manufacturers { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<StatusClaim> StatusClaims { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=SERVERPC;database=TestDb;user=sa;password=sa");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Cyrillic_General_100_CS_AI_KS_SC_UTF8");

            modelBuilder.Entity<Cabinet>(entity =>
            {
                entity.ToTable("Cabinet");

                entity.Property(e => e.CabinetId).HasColumnName("CabinetID");

                entity.Property(e => e.Cabinet1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Cabinet");

                entity.Property(e => e.CorpsId).HasColumnName("CorpsID");

                entity.HasOne(d => d.Corps)
                    .WithMany(p => p.Cabinets)
                    .HasForeignKey(d => d.CorpsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cabinet_Corps");
            });

            modelBuilder.Entity<Claim>(entity =>
            {
                entity.HasKey(e => e.ClaimsId);

                entity.Property(e => e.ClaimsId).HasColumnName("ClaimsID");

                entity.Property(e => e.DateClose)
                    .HasColumnType("date")
                    .HasColumnName("dateClose");

                entity.Property(e => e.DateOpen)
                    .HasColumnType("date")
                    .HasColumnName("dateOpen");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.StatusClaimId).HasColumnName("StatusClaimID");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.Claims)
                    .HasForeignKey(d => d.DeviceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Claims_Device");

                entity.HasOne(d => d.StatusClaim)
                    .WithMany(p => p.Claims)
                    .HasForeignKey(d => d.StatusClaimId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Claims_StatusClaim");

                entity.HasOne(d => d.UserIdCloseNavigation)
                    .WithMany(p => p.ClaimUserIdCloseNavigations)
                    .HasForeignKey(d => d.UserIdClose)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Claims_User1");

                entity.HasOne(d => d.UserIdOpenNavigation)
                    .WithMany(p => p.ClaimUserIdOpenNavigations)
                    .HasForeignKey(d => d.UserIdOpen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Claims_User");
            });

            modelBuilder.Entity<Corps>(entity =>
            {
                entity.Property(e => e.CorpsId).HasColumnName("CorpsID");

                entity.Property(e => e.CorpsName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("Device");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CabinetId).HasColumnName("CabinetID");

                entity.Property(e => e.Decommissioned).HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeviceTypeId).HasColumnName("DeviceTypeID");

                entity.Property(e => e.InventoryNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");

                entity.Property(e => e.NameDevice)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Cabinet)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.CabinetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Device_Cabinet1");

                entity.HasOne(d => d.DeviceType)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.DeviceTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Device_DeviceType");

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.ManufacturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Device_Manufacturer");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Device_User");
            });

            modelBuilder.Entity<DeviceType>(entity =>
            {
                entity.ToTable("DeviceType");

                entity.Property(e => e.DeviceTypeId).HasColumnName("DeviceTypeID");

                entity.Property(e => e.DeviceTypeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.ToTable("History");

                entity.Property(e => e.HistoryId).HasColumnName("HistoryID");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.OldInventoryNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("oldInventoryNumber");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.Histories)
                    .HasForeignKey(d => d.DeviceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_History_Device");
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.ToTable("Manufacturer");

                entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");

                entity.Property(e => e.Manufacturer1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Manufacturer");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StatusClaim>(entity =>
            {
                entity.ToTable("StatusClaim");

                entity.Property(e => e.StatusClaimId).HasColumnName("StatusClaimID");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserSurname)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
