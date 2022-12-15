using HospitalLibrary.Appointments;
using HospitalLibrary.Examination;
using HospitalLibrary.Examination.Dtos;
using HospitalLibrary.Examination.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Repository;
using Moq;
using Shouldly;

namespace HospitalTests.Units.Examinations;

public class CreateExaminationReport
{


    public Mock<IUnitOfWork> SetupUOW()
    {
        var uow = new Mock<IUnitOfWork>();
        var examinationReportRepository = new Mock<IExaminationReportRepository>();

        uow.Setup(u => u.ExaminationReportRepository).Returns(examinationReportRepository.Object);
        return uow;
    }
    [Fact]
    public void Examination_report_already_exists()
    {
        var uow = SetupUOW();
        ExaminationReport report = new ExaminationReport()
        {
            ExaminationId = 1
        };
        Appointment appointment = new Appointment()
        {
            Id = 1,
            Type = AppointmentType.EXAMINATION
        };

        uow.Setup(u => u.ExaminationReportRepository.GetByExamination(It.IsAny<int>())).Returns(report);
        uow.Setup(u => u.AppointmentRepository.GetOne(It.IsAny<int>())).Returns(appointment);
        var validator = new ExaminationReportValidator(uow.Object);
        var service = new ExaminationReportService(uow.Object, validator, null, null);

        Should.Throw<BadRequestException>(() => service.Create(report));
    }
}