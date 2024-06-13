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
    public class LoginDTOTest
    {
        [TestMethod]
        public void LoginNotValidEmptyUserName()
        {
            var loginDTO = CreateDefaultFilledLoginDTO();
            loginDTO.UserName = null;
            Assert.IsFalse(loginDTO.IsValid());
        }

        [TestMethod]
        public void LoginNotValidEmptyPassword()
        {
            var loginDTO = CreateDefaultFilledLoginDTO();
            loginDTO.Password = null;
            Assert.IsFalse(loginDTO.IsValid());
        }

        [TestMethod]
        public void LoginNotValidLongUserName()
        {
            var loginDTO = CreateDefaultFilledLoginDTO();
            loginDTO.UserName = "REALLY_LONG_USERNAME_NOT_VALID";
            Assert.IsFalse(loginDTO.IsValid());
        }

        [TestMethod]
        public void UserValid()
        {
            var userDTO = CreateDefaultFilledLoginDTO();
            Assert.IsTrue(userDTO.IsValid());
        }

        private LoginDTO CreateDefaultFilledLoginDTO() => new LoginDTO() 
        {
            UserName = "UserName",
            Password = "Password",
        };
    }
}
