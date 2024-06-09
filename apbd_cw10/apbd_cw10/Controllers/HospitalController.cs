using System.Diagnostics;
using apbd_cw10.Data;
using apbd_cw10.DTOs;
using apbd_cw10.Models;
using apbd_cw10.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace apbd_cw10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HospitalController : ControllerBase
{
    private readonly IHospitalService _hospitalService;

    public HospitalController(IHospitalService hospitalService)
    {
        _hospitalService = hospitalService;
    }

    [HttpPost]
    [Route("hospital")]
    public async Task<IActionResult> AddPrescription(AddPrescriptionDTO addPrescriptionDto)
    {

        int idPatient = addPrescriptionDto.PatientDto.IdPatient;
        if (!await _hospitalService.DoesPatientExists(addPrescriptionDto.PatientDto.IdPatient))
        {
            idPatient = await _hospitalService.AddPatient(addPrescriptionDto.PatientDto);
        }
      
        if (!await _hospitalService.DoesDoctorExists(addPrescriptionDto.IdDoctor))
        {
            return NotFound($"Doctor with id: {addPrescriptionDto.IdDoctor} not found");
        }
     
        foreach (var medicament in addPrescriptionDto.Medicaments)
        {
            if (!await _hospitalService.DoesMedicamentExists(medicament.IdMedicament))
            {
                return NotFound($"Medicament with id: {medicament.IdMedicament} not found");
            }
        }
      
        if (addPrescriptionDto.DueDate < addPrescriptionDto.Date)
        {
            return BadRequest("Date cannot be later than DueDate");
        }
    
       
        Prescription prescriptionToAdd = new Prescription()
        {
            Date = addPrescriptionDto.Date,
            DueDate = addPrescriptionDto.DueDate,
            IdDoctor = addPrescriptionDto.IdDoctor,
            IdPatient = idPatient
        };
        await _hospitalService.AddPrescription(prescriptionToAdd);
    
        foreach (var medicament in addPrescriptionDto.Medicaments)
        {
            await _hospitalService.AddPrescriptionMedicament(new PrescriptionMedicament()
            {
                Details = medicament.Details,
                Dose = medicament.Dose,
                IdMedicament = medicament.IdMedicament,
                IdPrescription = prescriptionToAdd.IdPrescription,
            });
        }
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetPatientData(int IdPatient)
    {
        if (!await _hospitalService.DoesPatientExists(IdPatient))
        {
            return NotFound($"Patient with id: {IdPatient} not found");
        }
        var result = await _hospitalService.GetPatientData(IdPatient);
        return Ok(result.Select(e => new GetPatientDataDTO()
        {
            IdPatient = e.IdPatient,
            FirstName = e.FirstName,
            LastName = e.LastName,
            BirthDate = e.BirthDate,
            Prescriptions = e.Prescriptions.Select(p => new GetPrescriptionDTO()
            {
                IdPrescription = p.IdPrescription,
                Date = p.Date,
                DueDate = p.DueDate,
                MedicamentDtos = p.PrescriptionMedicaments.Select(m => new GetMedicamentDTO()
                {
                    Description = m.Medicament.Description,
                    Dose = m.Dose,
                    IdMedicament = m.IdMedicament,
                    Name = m.Medicament.Name
                }).ToList(),
                DoctorDto = _hospitalService.GetDoctorById(p.IdDoctor).Result
            }).ToList()
        }).ToList());
    }
    
    
}