﻿using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using TechTest.Controllers;
using TechTest.DTO;
using TechTest.Services.Interface;
using TechTestData.Models;
using TechTestData.Repositories.Interface;

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

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task LoginFailedInvalidDTO()
        {
            var loginController = new LoginController(_userService);
            var loginDTO = CreateDefaultFilledLoginDTO();
            loginDTO.UserName = null;
            var result = await loginController.Login(loginDTO);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task LoginFailedGenerateJwt()
        {
            _userService.LoginUserAsync(Arg.Any<LoginDTO>()).Returns("");

            var loginController = new LoginController(_userService);
            var loginDTO = CreateDefaultFilledLoginDTO();

            var result = await loginController.Login(loginDTO);

            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public async Task LoginSucess()
        {
            _userService.LoginUserAsync(Arg.Any<LoginDTO>()).Returns("JWT");

            var loginController = new LoginController(_userService);
            var loginDTO = CreateDefaultFilledLoginDTO();
            var result = await loginController.Login(loginDTO);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        private UsersDTO CreateDefaultFilledUserDTO() => new UsersDTO()
        {
            Id = 1,
            UserName = "UserName",
            FullName = "FullName",
            Password = "Password",
            Email = "Email",
            Phone = "Phone",
            RolesId = 1,
        };

        private LoginDTO CreateDefaultFilledLoginDTO() => new LoginDTO()
        {
            UserName = "UserName",
            Password = "Password",
        };
    }
}
