using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Results;
using TechTest.Controllers;
using TechTest.DTO;
using TechTest.Models;
using TechTest.Repositories.Interface;
using TechTest.Services.Interface;

namespace UnitTests.Controllers
{
    [TestClass]
    public class LoginControllerTests
    {
        private readonly IUserService _userService;
        private readonly IRepository<Users> _userRepository;
        private readonly IRepository<Roles> _rolesRepository;

        public LoginControllerTests()
        {
            _userRepository = Substitute.For<IRepository<Users>>();
            _rolesRepository = Substitute.For<IRepository<Roles>>();
            _userService = Substitute.For<IUserService>();
        }

        [TestMethod]
        public async Task RegisterUserValidDTO()
        {
            var loginController = new LoginController(_userService);
            var userDTO = CreateDefaultFilledUserDTO();

            var result = await loginController.RegisterUsers(userDTO);

            Assert.IsInstanceOfType(result, typeof(CreatedResult));
        }

        [TestMethod]
        public async Task RegisterUserInvalidDTO()
        {
            var loginController = new LoginController(_userService);
            var userDTO = CreateDefaultFilledUserDTO();
            userDTO.UserName = null;
            var result = await loginController.RegisterUsers(userDTO);

            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public async Task LoginFailedInvalidDTO()
        {
            var loginController = new LoginController(_userService);
            var loginDTO = CreateDefaultFilledLoginDTO();
            loginDTO.UserName = null;
            var result = await loginController.Login(loginDTO);

            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public async Task LoginFailedGenerateJwt()
        {
            _userService.LoginUserAsync(Arg.Any<LoginDTO>()).Returns("");

            var loginController = new LoginController(_userService);
            var loginDTO = CreateDefaultFilledLoginDTO();

            var result = await loginController.Login(loginDTO);

            Assert.IsInstanceOfType(result, typeof(CreatedResult));
        }

        [TestMethod]
        public async Task LoginSucess()
        {
            _userService.LoginUserAsync(Arg.Any<LoginDTO>()).Returns("JWT");

            var loginController = new LoginController(_userService);
            var loginDTO = CreateDefaultFilledLoginDTO();
            var result = await loginController.Login(loginDTO);

            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<string>));
        }

        private UsersDTO CreateDefaultFilledUserDTO() => new UsersDTO()
        {
            Id = 1,
            UserName = "UserName",
            FullName = "FullName",
            Password = "Password",
            Email = "Email",
            Phone = "Phone",
            Role = "1",
        };

        private LoginDTO CreateDefaultFilledLoginDTO() => new LoginDTO()
        {
            UserName = "UserName",
            Password = "Password",
        };
    }
}
