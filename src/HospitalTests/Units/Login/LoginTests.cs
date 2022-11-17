using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Interfaces;
using Moq;
using Shouldly;

namespace HospitalTests.Units.Login;

public class LoginTests
{
    private Mock<IUnitOfWork> SetupUOW()
    {
        var userRepository = new Mock<IUserRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        unitOfWork.Setup(u => u.UserRepository).Returns(userRepository.Object);
        
        return unitOfWork;
    }
    
    [Fact]
    public void username_exist()
    {
        var unitOfWork = SetupUOW();
        unitOfWork.Setup(u => u.UserRepository.UsernameExist(It.IsAny<string>())).Returns(true);
        var userService = new UserService(unitOfWork.Object);

        User userObject = new User(2, "username", "password", Role.Manager);

        bool result = userService.UsernameExist(userObject.Username);

        result.ShouldBeTrue();


    }
    [Fact]
    public void username_doesnt_exist()
    {
        var unitOfWork = SetupUOW();
        unitOfWork.Setup(u => u.UserRepository.UsernameExist(It.IsAny<string>())).Returns(false);
        var userService = new UserService(unitOfWork.Object);

        var userObject = new User(1, "Jova", "Jova",Role.Secretary);

        bool result = userService.UsernameExist(userObject.Username);

        result.ShouldBeFalse();


    }
    [Fact]
    public void password_exist()
    {
        var unitOfWork = SetupUOW();
        unitOfWork.Setup(u => u.UserRepository.PasswordExist(It.IsAny<string>())).Returns(true);
        var userService = new UserService(unitOfWork.Object);

        User userObject = new User(2, "username", "password", Role.Manager);

        bool result = userService.PasswordExist(userObject.Password);

        result.ShouldBeTrue();


    }
    [Fact]
    public void password_doesnt_exist()
    {
        var unitOfWork = SetupUOW();
        unitOfWork.Setup(u => u.UserRepository.PasswordExist(It.IsAny<string>())).Returns(false);
        var userService = new UserService(unitOfWork.Object);

        var userObject = new User(1, "Jova", "Jova",Role.Secretary);

        bool result = userService.PasswordExist(userObject.Password);

        result.ShouldBeFalse();


    }
}