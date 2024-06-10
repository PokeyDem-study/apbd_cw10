using apbd_cw10.Controllers;
using apbd_cw10.DTOs;
using apbd_cw10.Data;
using apbd_cw10.Models;
using apbd_cw10.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;


namespace apbd_cw10.Tests;


public class HospitalControllerTests
{
    private readonly IHospitalService _hospitalService;

    [Fact]
    public async Task AddPrescription_ReturnsNotFoundWhereDoctorIsNotExists()
    {
        var addPrescriptionDto = new AddPrescriptionDTO
        {
            IdDoctor = 100,
            Date = new DateTime(2022,02,01),
            DueDate = new DateTime(2022,03,01),
            Medicaments = new List<MedicamentDTO>([new MedicamentDTO
            {
                Details = "Test details",
                Dose = 10,
                IdMedicament = 1
            }]),
            PatientDto = new PatientDTO
            {
                IdPatient = 1
            }
        };
        
        var _hospitalServiceMock = new Mock<IHospitalService>();
        var _controller = new HospitalController(_hospitalServiceMock.Object);
        
        var result = await _controller.AddPrescription(addPrescriptionDto);
       
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal($"Doctor with id: {addPrescriptionDto.IdDoctor} not found", notFoundResult.Value);
    }
}