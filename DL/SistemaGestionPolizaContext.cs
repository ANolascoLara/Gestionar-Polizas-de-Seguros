using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class SistemaGestionPolizaContext : DbContext
{
    public SistemaGestionPolizaContext()
    {
    }

    public SistemaGestionPolizaContext(DbContextOptions<SistemaGestionPolizaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Colonium> Colonia { get; set; }

    public virtual DbSet<Direccion> Direccions { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Estatus> Estatuses { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<Pai> Pais { get; set; }

    public virtual DbSet<Poliza> Polizas { get; set; }

    public virtual DbSet<PolizaUsuario> PolizaUsuarios { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<TipoPoliza> TipoPolizas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<GetAllUsuario> GetAllUsuarios { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GetAllUsuario>(entity => { entity.HasNoKey(); });

        modelBuilder.Entity<Colonium>(entity =>
        {
            entity.HasKey(e => e.IdColonia).HasName("PK__Colonia__A1580F668D73C3E0");

            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.Colonia)
                .HasForeignKey(d => d.IdMunicipio)
                .HasConstraintName("FK__Colonia__IdMunic__32E0915F");
        });

        modelBuilder.Entity<Direccion>(entity =>
        {
            entity.HasKey(e => e.IdDireccion).HasName("PK__Direccio__1F8E0C76A5EB7DB9");

            entity.ToTable("Direccion");

            entity.Property(e => e.Calle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroExterior)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NumeroInterior)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdColoniaNavigation).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.IdColonia)
                .HasConstraintName("FK__Direccion__IdCol__33D4B598");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Direccion__IdUsu__2F10007B");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__Estado__FBB0EDC1D5D2D3CB");

            entity.ToTable("Estado");

            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Estados)
                .HasForeignKey(d => d.IdPais)
                .HasConstraintName("FK__Estado__IdPais__1273C1CD");
        });

        modelBuilder.Entity<Estatus>(entity =>
        {
            entity.HasKey(e => e.IdEstatus).HasName("PK__Estatus__B32BA1C795FDA435");

            entity.ToTable("Estatus");

            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.IdGenero).HasName("PK__Genero__0F834988F10B1A49");

            entity.ToTable("Genero");

            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.HasKey(e => e.IdMunicipio).HasName("PK__Municipi__6100597833AB6F47");

            entity.ToTable("Municipio");

            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Municipios)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK__Municipio__IdEst__15502E78");
        });

        modelBuilder.Entity<Pai>(entity =>
        {
            entity.HasKey(e => e.IdPais).HasName("PK__Pais__FC850A7B1352134E");

            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Poliza>(entity =>
        {
            entity.HasKey(e => e.NumeroPoliza).HasName("PK__Poliza__38B3102058C84094");

            entity.ToTable("Poliza");

            entity.Property(e => e.NumeroPoliza).ValueGeneratedNever();
            entity.Property(e => e.Montoprima).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdEstatusNavigation).WithMany(p => p.Polizas)
                .HasForeignKey(d => d.IdEstatus)
                .HasConstraintName("FK__Poliza__IdEstatu__2E1BDC42");

            entity.HasOne(d => d.IdTipoPolizaNavigation).WithMany(p => p.Polizas)
                .HasForeignKey(d => d.IdTipoPoliza)
                .HasConstraintName("FK__Poliza__IdTipoPo__24927208");
        });

        modelBuilder.Entity<PolizaUsuario>(entity =>
        {
            entity.HasKey(e => e.IdPolizaUsuario).HasName("PK__PolizaUs__2FE969A54AFF1540");

            entity.ToTable("PolizaUsuario");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.PolizaUsuarios)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__PolizaUsu__IdUsu__440B1D61");

            entity.HasOne(d => d.NumeroPolizaNavigation).WithMany(p => p.PolizaUsuarios)
                .HasForeignKey(d => d.NumeroPoliza)
                .HasConstraintName("FK__PolizaUsu__Numer__44FF419A");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584CB674B123");

            entity.ToTable("Rol");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoPoliza>(entity =>
        {
            entity.HasKey(e => e.IdTipoPoliza).HasName("PK__TipoPoli__1BF2E5A76E5A4AA9");

            entity.ToTable("TipoPoliza");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF970A30A44B");

            entity.ToTable("Usuario");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contraseña)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(12)
                .IsUnicode(false);

            entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdGenero)
                .HasConstraintName("FK__Usuario__IdGener__2D27B809");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuario__IdRol__2B3F6F97");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
