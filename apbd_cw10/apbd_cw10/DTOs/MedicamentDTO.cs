using System.ComponentModel.DataAnnotations;

namespace apbd_cw10.DTOs;

public class MedicamentDTO
{
    public int IdMedicament {get; set; }
    
    public int Dose { get; set; }

    [MaxLength(100)]
    public string Details { get; set; }
}