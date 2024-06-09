using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apbd_cw10.Models;

[Table("PrescriptionMedicament")]
[PrimaryKey(nameof(IdMedicament), nameof(IdPrescription))]

public class PrescriptionMedicament
{
    public int Dose { get; set; }

    [MaxLength(100)] 
    public string Details { get; set; }

    public int IdMedicament { get; set; }
    
    public int IdPrescription { get; set; }

    [ForeignKey(nameof(IdMedicament))] 
    public Medicament Medicament { get; set; } = null!;

    [ForeignKey(nameof(IdPrescription))]
    public Prescription Prescription { get; set; }  = null!;
}