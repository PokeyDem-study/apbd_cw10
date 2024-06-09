using System.ComponentModel.DataAnnotations;
using apbd_cw10.Models;

namespace apbd_cw10.DTOs;

public class AddPrescriptionDTO
{
    [Required] 
    public PatientDTO PatientDto { get; set; }
    
    [Required] 
    public int IdDoctor { get; set; }
    
    [Required]
    [MaxLength(10)]
    public ICollection<MedicamentDTO> Medicaments { get; set; } = new HashSet<MedicamentDTO>();
    [Required] public DateTime Date { get; set; }

    [Required] public DateTime DueDate { get; set; }

}