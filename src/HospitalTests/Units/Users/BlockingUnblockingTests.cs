using HospitalLibrary.BloodStorages;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Model.ValueObjects;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Interfaces;
using Moq;
using Shouldly;

namespace HospitalTests.Units.Users;

public class BlockingUnblockingTests
{
    public UserService UserServiceSetup()
    {
        var _unitOfWork = new Mock<IUnitOfWork>();
        var userRepository = new Mock<IUserRepository>();
        
        User user1 = new User("jovica", "sifras123", Role.Patient,1,ActiveStatus.Pending);
        user1.Blocked = true;
        User user2 = new User("jovica2", "sifras123", Role.Patient,2,ActiveStatus.Pending);
        user2.Blocked = false;
        User user3 = new User("jovica2", "sifras123", Role.Manager,3,ActiveStatus.Pending);
        user3.Blocked = false;

        userRepository.Setup(useRep => useRep.GetOne(1)).Returns(user1);
        userRepository.Setup(useRep => useRep.GetOne(2)).Returns(user2);
        userRepository.Setup(useRep => useRep.GetOne(3)).Returns(user3);
        _unitOfWork.Setup((u => u.UserRepository)).Returns(userRepository.Object);
        
        var userService = new UserService(_unitOfWork.Object);
        return userService;
    }

    [Fact]
    public void User_not_patient()
    {
        Should.Throw<Exception>(() => UserServiceSetup()
            .BlockMaliciousUser(3)).Message.ShouldBe("You can only block patients!");
    }

    [Fact]
    public void User_already_blocked()
    {
        Should.Throw<Exception>(() => UserServiceSetup()
            .BlockMaliciousUser(1)).Message.ShouldBe("Patient is already blocked!");
    }

    [Fact]
    public void User_successfuly_blocked()
    {
        UserService userService = UserServiceSetup();
        var user = userService.BlockMaliciousUser(2);
        user.ShouldNotBe(null);
    }

    [Fact]
    public void User_already_unblocked()
    {
        Should.Throw<Exception>(() => UserServiceSetup()
            .UnblockMaliciousUser(2)).Message.ShouldBe("User is not blocked!");
    }

    [Fact]
    public void User_successfuly_unbolcked()
    {
        UserService userService = UserServiceSetup();
        var user = userService.UnblockMaliciousUser(1);
        user.ShouldNotBe(null);
    }
}