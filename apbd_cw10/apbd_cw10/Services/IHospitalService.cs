using System.Collections;
using apbd_cw10.Data;
using apbd_cw10.DTOs;
using apbd_cw10.Models;
using Microsoft.AspNetCore.Mvc;

namespace apbd_cw10.Repositories;

public interface IHospitalService
{
    Task<bool> DoesPatientExists(int id);
    Task<bool> DoesDoctorExists(int id);
    Task<bool> DoesMedicamentExists(int id);
    Task<int> AddPatient(PatientDTO patientDto);

    Task AddPrescription(Prescription prescriptionDto);
    
    Task AddPrescriptionMedicament(PrescriptionMedicament prescriptionMedicament);

    Task<ICollection<Patient>> GetPatientData(int id);

    Task<GetDoctorDTO> GetDoctorById(int id);
}