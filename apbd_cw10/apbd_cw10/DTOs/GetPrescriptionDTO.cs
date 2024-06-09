namespace apbd_cw10.DTOs;

public class GetPrescriptionDTO
{
    public int IdPrescription { get; set; }

    public DateTime Date { get; set; }

    public DateTime DueDate { get; set; }

    public ICollection<GetMedicamentDTO> MedicamentDtos { get; set; }
    
    public GetDoctorDTO DoctorDto { get; set; }
}