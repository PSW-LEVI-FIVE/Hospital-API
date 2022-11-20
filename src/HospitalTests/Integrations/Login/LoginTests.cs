﻿using HospitalAPI;
using HospitalAPI.Controllers.Public;
using HospitalLibrary.Auth.Interfaces;
using HospitalLibrary.User.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Dtos;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.Login;

[Collection("Test")]
public class LoginTests : BaseIntegrationTest
{
    public LoginTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }
    
    [Fact]
    public void Login_user_successfully()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new AuthController(scope.ServiceProvider.GetRequiredService<IAuthService>());
        //User user = new User(1,"pas","password",Role.Patient);
        User user = new User(1, "Mika", "plsradi", Role.Patient);
        var result = ((OkObjectResult)controller.UserExist(new UserDTO(user.Username,user.Password,user.Role))).Value as string;
        result.ShouldNotBeNull();
    }
    [Fact]
    public void Login_user_unsuccessfully()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new AuthController(scope.ServiceProvider.GetRequiredService<IAuthService>());
        User user = new User(1,"pas","password",Role.Patient);
        var result = ((NotFoundObjectResult)controller.UserExist(new UserDTO(user.Username,user.Password,user.Role))).Value as string;
        result.ShouldNotBeNull();
    }
}