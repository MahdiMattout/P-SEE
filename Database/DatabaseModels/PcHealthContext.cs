﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Database.DatabaseModels
{
    public partial class PcHealthContext : DbContext
    {
        public PcHealthContext(DbContextOptions<PcHealthContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<AdminHasPc> AdminHasPcs { get; set; }
        public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<Pc> Pcs { get; set; }
        public virtual DbSet<Service> Services { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Server=bsvhzy1r6yrrhqucx9o9-mysql.services.clever-cloud.com;port=3306;Database=bsvhzy1r6yrrhqucx9o9;username=uclrhckmdjsm76tx;password=pUd7Fb8karg1EjPc6hVd");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.AdminCredentialsUsername)
                    .HasName("PRIMARY");

                entity.ToTable("Admin");

                entity.Property(e => e.AdminCredentialsUsername).HasMaxLength(45);

                entity.Property(e => e.AdminFirstName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.AdminLastName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.HasOne(d => d.AdminCredentialsUsernameNavigation)
                    .WithOne(p => p.Admin)
                    .HasForeignKey<Admin>(d => d.AdminCredentialsUsername)
                    .HasConstraintName("fk_Admin_Credentials1");
            });

            modelBuilder.Entity<AdminHasPc>(entity =>
            {
                entity.HasKey(e => new { e.AdminCredentialsUsername, e.PcId })
                    .HasName("PRIMARY");

                entity.ToTable("Admin_has_Pc");

                entity.HasIndex(e => e.AdminCredentialsUsername, "fk_Admin_has_Pc_Admin1_idx");

                entity.HasIndex(e => e.PcId, "fk_Admin_has_Pc_Pc1_idx");

                entity.Property(e => e.AdminCredentialsUsername).HasMaxLength(45);

                entity.Property(e => e.PcId).HasMaxLength(150);

                entity.HasOne(d => d.AdminCredentialsUsernameNavigation)
                    .WithMany(p => p.AdminHasPcs)
                    .HasForeignKey(d => d.AdminCredentialsUsername)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Admin_has_Pc_Admin1");

                entity.HasOne(d => d.Pc)
                    .WithMany(p => p.AdminHasPcs)
                    .HasForeignKey(d => d.PcId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Admin_has_Pc_Pc1");
            });

            modelBuilder.Entity<Credential>(entity =>
            {
                entity.HasKey(e => e.CredentialsUsername)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.CredentialsUsername, "CredentialsUsername_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.CredentialsUsername).HasMaxLength(45);

                entity.Property(e => e.CredentialsPassword)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CredentialsSalt)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Pc>(entity =>
            {
                entity.ToTable("Pc");

                entity.HasIndex(e => e.PcId, "idPc_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.PcId).HasMaxLength(150);

                entity.Property(e => e.PcFirewallStatus)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.PcOs)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("PcOS");

                entity.Property(e => e.PcUsername)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => new { e.ServiceName, e.PcId })
                    .HasName("PRIMARY");

                entity.ToTable("Service");

                entity.HasIndex(e => e.PcId, "PcId_idx");

                entity.Property(e => e.ServiceName).HasMaxLength(45);

                entity.Property(e => e.PcId).HasMaxLength(150);

                entity.HasOne(d => d.Pc)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.PcId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PcId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}