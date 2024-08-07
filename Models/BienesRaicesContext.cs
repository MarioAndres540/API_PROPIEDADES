﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API_PROPIEDADES.Models;

public partial class BienesRaicesContext : DbContext
{
    public BienesRaicesContext()
    {
    }

    public BienesRaicesContext(DbContextOptions<BienesRaicesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<PropertyImage> PropertyImages { get; set; }

    public virtual DbSet<PropertyTrace> PropertyTraces { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost; Database=BienesRaices; Integrated Security=true; TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Owner>(entity =>
        {
            entity.HasKey(e => e.IdOwner);

            entity.ToTable("Owner");

            entity.Property(e => e.Addres).HasMaxLength(50);
            entity.Property(e => e.Birthday).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Photo).HasColumnType("image");
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.IdProperty);

            entity.ToTable("Property");

            entity.Property(e => e.IdProperty).ValueGeneratedNever();
            entity.Property(e => e.Addres)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdOwnerNavigation).WithMany(p => p.Properties)
                .HasForeignKey(d => d.IdOwner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Property_Owner");
        });

        modelBuilder.Entity<PropertyImage>(entity =>
        {
            entity.HasKey(e => e.IdPropertyImage);

            entity.ToTable("PropertyImage");

            entity.Property(e => e.Enabled)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.File)
                .HasMaxLength(5000)
                .IsFixedLength()
                .HasColumnName("file");

            entity.HasOne(d => d.IdPropertyNavigation).WithMany(p => p.PropertyImages)
                .HasForeignKey(d => d.IdProperty)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PropertyImage_Property");
        });

        modelBuilder.Entity<PropertyTrace>(entity =>
        {
            entity.HasKey(e => e.IdPropertyTrace);

            entity.ToTable("PropertyTrace");

            entity.Property(e => e.DateSale).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Tax).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Value).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdPropertyNavigation).WithMany(p => p.PropertyTraces)
                .HasForeignKey(d => d.IdProperty)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PropertyTrace_Property");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
