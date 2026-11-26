using System;
using System.Collections.Generic;
using ImportadorMovimientos.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImportadorMovimientos.Data;

public partial class BancoDbContext : DbContext
{
    public BancoDbContext(DbContextOptions<BancoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<TiposMovimiento> TiposMovimientos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clientes__3214EC074331BCD1");

            entity.HasIndex(e => e.Dni, "UQ__Clientes__C030857530931145").IsUnique();

            entity.Property(e => e.Dni)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Movimien__3214EC0794B4CBDA");

            entity.Property(e => e.Importe).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movimientos_Clientes");

            entity.HasOne(d => d.TipoMovimiento).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.TipoMovimientoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movimientos_TiposMovimiento");
        });

        modelBuilder.Entity<TiposMovimiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TiposMov__3214EC0745EC3FB0");

            entity.ToTable("TiposMovimiento");

            entity.HasIndex(e => e.Codigo, "UQ__TiposMov__06370DACDEBF28E0").IsUnique();

            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
