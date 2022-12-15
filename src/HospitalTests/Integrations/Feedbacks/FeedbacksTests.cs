using System.Security.Claims;
using HospitalAPI;
using HospitalAPI.Controllers.Public;
using HospitalLibrary.Feedbacks;
using HospitalLibrary.Feedbacks.Dtos;
using HospitalLibrary.Feedbacks.Interfaces;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.Feedbacks;

[Collection("Test")]
public class FeedbacksTests: BaseIntegrationTest
{
    public FeedbacksTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
        using var scope = Factory.Services.CreateScope();
    }
    private FeedbacksController CreateFakeControllerWithIdentity(IFeedbackService feedbackService) {
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "Somebody"),
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Role, "Patient"),
        }, "mock"));
        
        var controller = new FeedbacksController(feedbackService);
        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };
        return controller;
    }
    [Fact]
    public void Feedback_post_success()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreateFakeControllerWithIdentity(scope.ServiceProvider.GetRequiredService<IFeedbackService>());
        CreateFeedbackDTO feedbackToCreate = new CreateFeedbackDTO
        {
            FeedbackContent = "Neki Kontent",
            FeedbackStatus = new FeedbackStatusDTO(true,false,true),
            PatientId = 2
        };
        var result = ((OkObjectResult)controller.Create(feedbackToCreate).Result).Value as Feedback;
        result.ShouldNotBeNull();
    }
    [Fact]
    public void Feedback_post_fail()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreateFakeControllerWithIdentity(scope.ServiceProvider.GetRequiredService<IFeedbackService>());
        CreateFeedbackDTO feedbackToCreate = new CreateFeedbackDTO
        {
            FeedbackContent = "",
            FeedbackStatus = new FeedbackStatusDTO(true,false,true),
            PatientId = 2
        };
        Should.Throw<AggregateException>(() => ((OkObjectResult)controller.Create(feedbackToCreate).Result).Value)
            .Message.ShouldBe("One or more errors occurred. (Feedback content is empty!)");
    }
}