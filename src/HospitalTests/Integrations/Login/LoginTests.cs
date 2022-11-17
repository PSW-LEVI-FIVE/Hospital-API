using HospitalAPI;
using HospitalAPI.Controllers.Public;
using HospitalLibrary.User.Interfaces;
using HospitalLibrary.Users;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.Login;

public class LoginTests : BaseIntegrationTest
{
    public LoginTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }
    
    [Fact]
    public void login_user_successfully()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new UserController(scope.ServiceProvider.GetRequiredService<IUserService>());
        //User user = new User(1,"pas","password",Role.Patient);
        User user = new User(3, "username", "password", Role.Patient);
        var result = ((OkObjectResult)controller.UserExist(user.Username,user.Password)).Value as User;
        result.ShouldNotBeNull();
    }
    [Fact]
    public void login_user_unsuccessfully()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new UserController(scope.ServiceProvider.GetRequiredService<IUserService>());
        User user = new User(1,"pas","password",Role.Patient);
        var result = ((OkObjectResult)controller.UserExist(user.Username,user.Password)).Value as User;
        result.ShouldBeNull();
    }
}