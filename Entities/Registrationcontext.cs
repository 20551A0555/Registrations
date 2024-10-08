﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Registration_server_project.Entities;

public partial class Registrationcontext : DbContext
{
    public Registrationcontext(DbContextOptions<Registrationcontext> options)
        : base(options)
    {
    }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<LoginDetail> LoginDetails { get; set; }

    public virtual DbSet<Plant> Plants { get; set; }

    public virtual DbSet<RegistrationDetail> RegistrationDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.BranchId).HasName("PK__branch__E55E37DEF8C0E48D");

            entity.ToTable("branch");

            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.BranchAddress)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("branch_address");
            entity.Property(e => e.BranchName)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("branch_name");
            entity.Property(e => e.PlantId).HasColumnName("plant_id");

            entity.HasOne(d => d.Plant).WithMany(p => p.Branches)
                .HasForeignKey(d => d.PlantId)
                .HasConstraintName("FK__branch__plant_id__04E4BC85");
        });

        modelBuilder.Entity<LoginDetail>(entity =>
        {
            entity.HasKey(e => e.LoginId).HasName("PK__Login_de__D78B57AFDF507913");

            entity.ToTable("Login_details");

            entity.Property(e => e.LoginId).HasColumnName("Login_id");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("First_name");
            entity.Property(e => e.RegisterId).HasColumnName("Register_id");

            entity.HasOne(d => d.Register).WithMany(p => p.LoginDetails)
                .HasForeignKey(d => d.RegisterId)
                .HasConstraintName("FK__Login_det__Regis__3F115E1A");
        });

        modelBuilder.Entity<Plant>(entity =>
        {
            entity.HasKey(e => e.PlantId).HasName("PK__plant__A576B3B4F5AE4DF7");

            entity.ToTable("plant");

            entity.Property(e => e.PlantId).HasColumnName("plant_id");
            entity.Property(e => e.PlantAddress)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("plant_address");
            entity.Property(e => e.PlantName)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("plant_name");
        });

        modelBuilder.Entity<RegistrationDetail>(entity =>
        {
            entity.ToTable("Registration_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}