using apbd_cw10.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_cw10.Data;

public class ApbdContext : DbContext
{
    protected ApbdContext()
    {
    }

    public ApbdContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<Doctor> Doctor { get; set; }
    public virtual DbSet<Medicament> Medicament { get; set; }
    public virtual DbSet<Patient> Patient { get; set; }
    public virtual DbSet<Prescription> Prescription { get; set; }
    public virtual DbSet<PrescriptionMedicament> PrescriptionMedicaments{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>()
        {
            new() {IdDoctor = 1, FirstName = "Soul", LastName = "Hudson", Email = "slash@gmail.com"},
            new() {IdDoctor = 2, FirstName = "James", LastName = "Hetfield", Email = "papahet@gmail.com"},
            new() {IdDoctor = 3, FirstName = "Kirk", LastName = "Hammett", Email = "wah@gmail.com"}
        });

        modelBuilder.Entity<Patient>().HasData(new List<Patient>()
        {
            new() {IdPatient = 1, FirstName = "Matt", LastName = "Tuck", BirthDate = new DateTime(1980,  01, 20)},
            new() {IdPatient = 2, FirstName = "Michael", LastName = "Paget", BirthDate = new DateTime(1978,  09, 12)}
        });

        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>()
        {
            new() {IdMedicament = 1, Name = "Med1", Description = "Maybe it will help, maybe not, idk", Type = "Pills"},
            new() {IdMedicament = 2, Name = "Med2", Description = "Maybe it will help, maybe not, idk", Type = "NotPills"}
        });
    }

}