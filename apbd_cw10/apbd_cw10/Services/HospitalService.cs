using System.Collections;
using apbd_cw10.Data;
using apbd_cw10.DTOs;
using apbd_cw10.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apbd_cw10.Repositories;

public class HospitalService : IHospitalService
{
    private readonly ApbdContext _context;
    public HospitalService(ApbdContext context)
    {
        _context = context;
    }

    public async Task<bool> DoesPatientExists(int id)
    {
        var patient = await _context.Patient
            .AnyAsync(p => p.IdPatient.Equals(id));

        return patient;
    }

    public async Task<bool> DoesMedicamentExists(int id)
    {
        var medicament = await _context.Medicament
            .AnyAsync(m => m.IdMedicament.Equals(id));

        return medicament;
    }

    public async Task<bool> DoesDoctorExists(int id)
    {
        var doctor = await _context.Doctor
            .AnyAsync(p => p.IdDoctor.Equals(id));

        return doctor;
    }

    public async Task<int> AddPatient(PatientDTO patientDto)
    {
        var patientToAdd = new Patient()
        {
            BirthDate = patientDto.BirthDate,
            FirstName = patientDto.FirstName,
            LastName = patientDto.LastName
        };
        var addedPatient = await _context.Patient.AddAsync(patientToAdd);
        await _context.SaveChangesAsync();

        return addedPatient.Entity.IdPatient;
    }

    public async Task AddPrescription(Prescription prescription)
    {
        await _context.Prescription.AddAsync(prescription);
        await _context.SaveChangesAsync();
    }

    public async Task AddPrescriptionMedicament(PrescriptionMedicament prescriptionMedicament)
    {
        await _context.PrescriptionMedicaments.AddAsync(prescriptionMedicament);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<Patient>> GetPatientData(int id)
    {

        var result = await _context.Patient
            .Include(e => e.Prescriptions)
            .ThenInclude(e => e.PrescriptionMedicaments)
            .ThenInclude(e => e.Medicament)
            .Where(e => e.IdPatient.Equals(id)).ToListAsync();
        return result;
    }

    public async Task<GetDoctorDTO> GetDoctorById(int id)
    {
        
        var doctor = await _context.Doctor.FirstOrDefaultAsync(d => d.IdDoctor.Equals(id));
        return new GetDoctorDTO()
        {
            FirstName = doctor.FirstName,
            IdDoctor = doctor.IdDoctor
        };
    }
}