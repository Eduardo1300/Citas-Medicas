using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using wa_citas_medicas.Dtos;
using wa_citas_medicas.Models;

namespace wa_citas_medicas.Data;

public partial class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options,
        IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Citamedica> Citamedicas { get; set; }

    public virtual DbSet<Medico> Medicos { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    public DbSet<QueryPacienteDto> QueryPacientesDto { get; set; }
    public DbSet<QueryMedicoDto> QueryMedicosDto { get; set; }
    public DbSet<QueryCitaMedicaDto> QueryCitaMedicasDto { get; set; }
    public DbSet<QueryUsuarioDto>QueryUsuariosDtos {  get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    /*#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-CRJD420\\SQLEXPRESS;Database=bdclinica;Integrated Security=true;MultipleActiveResultSets=true;TrustServerCertificate=true;");
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Citamedica>(entity =>
        {
            entity.HasKey(e => e.Citaid).HasName("PK__citamedi__A9544A240CBEB4B9");

            entity.ToTable("citamedica");

            entity.HasIndex(e => new { e.Medicoid, e.Fecha }, "uk_medico_fecha").IsUnique();

            entity.Property(e => e.Citaid).HasColumnName("citaid");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Medicoid).HasColumnName("medicoid");
            entity.Property(e => e.Pacienteid).HasColumnName("pacienteid");

            entity.HasOne(d => d.Medico).WithMany(p => p.Citamedicas)
                .HasForeignKey(d => d.Medicoid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_medico");

            entity.HasOne(d => d.Paciente).WithMany(p => p.Citamedicas)
                .HasForeignKey(d => d.Pacienteid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_paciente");
        });

        modelBuilder.Entity<Medico>(entity =>
        {
            entity.HasKey(e => e.Medicoid).HasName("PK__medico__C916EDC6F86A83DD");

            entity.ToTable("medico");

            entity.Property(e => e.Medicoid).HasColumnName("medicoid");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Especialidad)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("especialidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.Pacienteid).HasName("PK__paciente__0AB88F261DDDE34F");

            entity.ToTable("paciente");

            entity.HasIndex(e => e.Dni, "UQ__paciente__D87608A78C9C2160").IsUnique();

            entity.Property(e => e.Pacienteid).HasColumnName("pacienteid");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Dni)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("dni");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("PK__usuario__080A97435E0BA4CE");

            entity.ToTable("usuario");

            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombres");
            entity.Property(e => e.Nomusuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nomusuario");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
        });
        modelBuilder.Entity<QueryPacienteDto>().HasNoKey();
        modelBuilder.Entity<QueryMedicoDto>().HasNoKey();
        modelBuilder.Entity<QueryCitaMedicaDto>().HasNoKey();
        modelBuilder.Entity<QueryUsuarioDto>().HasNoKey();
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
