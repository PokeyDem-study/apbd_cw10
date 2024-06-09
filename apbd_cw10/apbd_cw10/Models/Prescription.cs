using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apbd_cw10.Models;

public class Prescription
{
    [Key] 
    public int IdPrescription { get; set; }

    public DateTime Date { get; set; }

    public DateTime DueDate { get; set; }
    
    public int IdDoctor { get; set; }

    public int IdPatient { get; set; }
    
    [ForeignKey(nameof(IdDoctor))] 
    public Doctor Doctor { get; set; }

    [ForeignKey(nameof(IdPatient))] 
    public Patient Patient { get; set; }

    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } =
        new HashSet<PrescriptionMedicament>();

}