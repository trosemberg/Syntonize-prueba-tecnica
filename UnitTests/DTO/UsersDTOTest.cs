using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTest.DTO;

namespace UnitTests.DTO
{
    [TestClass]
    public class UsersDTOTest
    {
        [TestMethod]
        public void UserNotValidEmptyUserName()
        {
            var userDTO = CreateDefaultFilledUserDTO();
            userDTO.UserName = null;
            Assert.IsFalse(userDTO.IsValid());
        }

        [TestMethod]
        public void UserNotValidEmptyEmail()
        {
            var userDTO = CreateDefaultFilledUserDTO();
            userDTO.Email = null;
            Assert.IsFalse(userDTO.IsValid());
        }

        [TestMethod]
        public void UserNotValidEmptyFullName()
        {
            var userDTO = CreateDefaultFilledUserDTO();
            userDTO.FullName = null;
            Assert.IsFalse(userDTO.IsValid());
        }

        [TestMethod]
        public void UserNotValidEmptyPassword()
        {
            var userDTO = CreateDefaultFilledUserDTO();
            userDTO.Password = null;
            Assert.IsFalse(userDTO.IsValid());
        }

        [TestMethod]
        public void UserNotValidLongUserName()
        {
            var userDTO = CreateDefaultFilledUserDTO();
            userDTO.UserName = "REALLY_LONG_USERNAME_NOT_VALID";
            Assert.IsFalse(userDTO.IsValid());
        }

        [TestMethod]
        public void UserValid()
        {
            var userDTO = CreateDefaultFilledUserDTO();
            Assert.IsTrue(userDTO.IsValid());
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
    }
}
