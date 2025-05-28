using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace wa_citas_medicas.Models;

public partial class BancoDbContext : DbContext
{
    public BancoDbContext()
    {
    }

    public BancoDbContext(DbContextOptions<BancoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cuentum> Cuenta { get; set; }

    public virtual DbSet<Transaccion> Transaccions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-CRJD420\\SQLEXPRESS;Database=BancoDB;User=sa;Password=sql;MultipleActiveResultSets=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cuentum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cuenta__3214EC07318C761A");

            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Saldo).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transacc__3214EC075E1F2AEB");

            entity.ToTable("Transaccion");

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CuentaDestino).WithMany(p => p.TransaccionCuentaDestinos)
                .HasForeignKey(d => d.CuentaDestinoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacci__Cuent__3A81B327");

            entity.HasOne(d => d.CuentaOrigen).WithMany(p => p.TransaccionCuentaOrigens)
                .HasForeignKey(d => d.CuentaOrigenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacci__Cuent__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
