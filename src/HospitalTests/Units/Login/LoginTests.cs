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
    public void Username_exist()
    {
        var unitOfWork = SetupUOW();
        unitOfWork.Setup(u => u.UserRepository.UsernameExist(It.IsAny<string>())).Returns(true);
        var userService = new UserService(unitOfWork.Object);
        User userObject = new User("username", "password123", Role.Manager, 2,ActiveStatus.Active);
        bool result = userService.UsernameExist(userObject.Username);

        result.ShouldBeTrue();
    }
    [Fact]
    public void Username_doesnt_exist()
    {
        var unitOfWork = SetupUOW();
        unitOfWork.Setup(u => u.UserRepository.UsernameExist(It.IsAny<string>())).Returns(false);
        var userService = new UserService(unitOfWork.Object);

        var userObject = new User("Jova", "Jovaasd123",Role.Secretary,1,ActiveStatus.Active);

        bool result = userService.UsernameExist(userObject.Username);

        result.ShouldBeFalse();
    }
}