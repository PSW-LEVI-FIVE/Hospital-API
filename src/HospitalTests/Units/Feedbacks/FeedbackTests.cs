using HospitalLibrary.Auth;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Feedbacks;
using HospitalLibrary.Feedbacks.Interfaces;
using HospitalLibrary.Feedbacks.ValueObjects;
using HospitalLibrary.Patients;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Model.ValueObjects;
using HospitalLibrary.Users;
using Moq;
using Shouldly;

namespace HospitalTests.Units.Feedbacks;

public class FeedbackTests
{
    public FeedbackService FeedbackServiceSetup()
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        var feedbackRepository = new Mock<IFeedbackRepository>();
        unitOfWork.Setup(unit => unit.FeedbackRepository).Returns(feedbackRepository.Object);
        Feedback feedback1 = new Feedback(1,0,"neki kontent1", new FeedbackStatus(true,false,true));
        Feedback feedback2 = new Feedback(2,0,"neki kontent2", new FeedbackStatus(false,false,true));
        Feedback feedback3 = new Feedback(3,0,"neki kontent3", new FeedbackStatus(true,true,true));
        feedbackRepository.Setup(unit => unit.GetOne(1)).Returns(feedback1);
        feedbackRepository.Setup(unit => unit.GetOne(2)).Returns(feedback2);

        FeedbackService feedbackService = new FeedbackService(
            unitOfWork.Object
        );
        return feedbackService;
    }
    [Fact]
    public void Feedback_publish_success()
    {
        var feedback = FeedbackServiceSetup().ChangePublishmentStatus(1); ;
        feedback.ShouldNotBe(null);
    }
    [Fact]
    public void Feedback_withdraw_success()
    {
        var feedback = FeedbackServiceSetup().ChangePublishmentStatus(3); ;
        feedback.ShouldNotBe(null);
    }
}