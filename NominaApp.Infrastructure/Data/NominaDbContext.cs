using Microsoft.EntityFrameworkCore;
using NominaApp.Core.Entities;

namespace NominaApp.Infrastructure.Data;

public class NominaDbContext : DbContext
{
    public NominaDbContext(DbContextOptions<NominaDbContext> options) : base(options) { }

    public DbSet<Empleado> Empleados => Set<Empleado>();
    public DbSet<Departamento> Departamentos => Set<Departamento>();
    public DbSet<Puesto> Puestos => Set<Puesto>();
    public DbSet<Contrato> Contratos => Set<Contrato>();
    public DbSet<Asistencia> Asistencias => Set<Asistencia>();
    public DbSet<Permiso> Permisos => Set<Permiso>();
    public DbSet<ConceptoNomina> ConceptosNomina => Set<ConceptoNomina>();
    public DbSet<Nomina> Nominas => Set<Nomina>();
    public DbSet<DetalleNomina> DetallesNomina => Set<DetalleNomina>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relación circular Departamento <-> Empleado
        modelBuilder.Entity<Departamento>()
            .HasOne(d => d.Responsable)
            .WithMany()
            .HasForeignKey(d => d.ResponsableId)
            .OnDelete(DeleteBehavior.Restrict); // evita cascada ambigua

        modelBuilder.Entity<Empleado>()
            .HasOne(e => e.Departamento)
            .WithMany(d => d.Empleados)
            .HasForeignKey(e => e.DepartamentoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Contrato -> Empleado
        modelBuilder.Entity<Contrato>()
            .HasOne(c => c.Empleado)
            .WithMany(e => e.Contratos)
            .HasForeignKey(c => c.EmpleadoId)
            .OnDelete(DeleteBehavior.Cascade);

        // Usuario -> Empleado 
        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Empleado)
            .WithOne(e => e.Usuario)
            .HasForeignKey<Usuario>(u => u.EmpleadoId)
            .OnDelete(DeleteBehavior.SetNull);

        // Decimal precision 
        modelBuilder.Entity<Contrato>().Property(c => c.SalarioBase).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Puesto>().Property(p => p.SalarioMinimo).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Puesto>().Property(p => p.SalarioMaximo).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Nomina>().Property(n => n.SalarioBruto).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Nomina>().Property(n => n.TotalDeducciones).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Nomina>().Property(n => n.TotalIngresosAdicionales).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Nomina>().Property(n => n.SalarioNeto).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<DetalleNomina>().Property(d => d.Monto).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<ConceptoNomina>().Property(c => c.Valor).HasColumnType("decimal(18,2)");
    }
}