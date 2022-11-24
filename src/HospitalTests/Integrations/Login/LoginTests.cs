﻿using HospitalAPI;
using HospitalAPI.Controllers.Public;
using HospitalLibrary.Auth.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Dtos;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.Login;

[Collection("Test")]
public class LoginTests : BaseIntegrationTest {
    public LoginTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }
    
    [Fact]
    public void Login_user_successfully()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new AuthController(scope.ServiceProvider.GetRequiredService<IAuthService>(),
                                    scope.ServiceProvider.GetRequiredService<IEmailService>());
        User user = new User("Mika", "plsradi", Role.Patient,1,ActiveStatus.Active);
        var result = ((OkObjectResult)controller.UserExist(new UserDTO(user.Username,user.Password,user.Role))).Value as string;
        result.ShouldNotBeNull();
    }
    [Fact]
    public void Login_user_unsuccessfully()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new AuthController(scope.ServiceProvider.GetRequiredService<IAuthService>(),
                                    scope.ServiceProvider.GetRequiredService<IEmailService>());
        User user = new User("pas","password",Role.Patient,1,ActiveStatus.Active);
        var result = ((NotFoundObjectResult)controller.UserExist(new UserDTO(user.Username,user.Password,user.Role))).Value as string;
        result.ShouldNotBeNull();
    }
    [Fact]
    public void Login_user_successfully_intra()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new HospitalAPI.Controllers.Intranet.AuthController(scope.ServiceProvider.GetRequiredService<IAuthService>());
        User user = new User("Mika1", "plsradi", Role.Doctor,10,ActiveStatus.Active);
        var result = ((OkObjectResult)controller.UserExist(new UserDTO(user.Username,user.Password,user.Role))).Value as string;
        result.ShouldNotBeNull();
    }
    [Fact]
    public void Login_user_unsuccessfully_intra()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new HospitalAPI.Controllers.Intranet.AuthController(scope.ServiceProvider.GetRequiredService<IAuthService>());
        User user = new User("nema","usera",Role.Doctor,1,ActiveStatus.Active);
        var result = ((NotFoundObjectResult)controller.UserExist(new UserDTO(user.Username,user.Password,user.Role))).Value as string;
        result.ShouldNotBeNull();
    }


}