using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using API.DTOs;
using API.Services;
using Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace Tests.APITests.Account
{
    public class AccountControllerTests
    {
        private Mock<UserManager<User>> _userManager;
        private Mock<SignInManager<User>> _signInManager;
        private TokenService _tokenService;
        private AccountController _accountController;
        private List<User> _users;
        
        [SetUp]
        public void Setup() {
            _users = new List<User> {
                new User { Email = "first@gmail.com" }
            };

            _userManager = new Mock<UserManager<User>>(
                new Mock<IUserStore<User>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<User>>().Object,
                new IUserValidator<User>[0],
                new IPasswordValidator<User>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<User>>>().Object);
            _signInManager = new Mock<SignInManager<User>>(
                _userManager.Object, 
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<User>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                new Mock<IUserConfirmation<User>>().Object);

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string> {
                    {"TokenKey", "ramJKeNenRdXmMaG"}
                })
                .Build();
            _tokenService = new TokenService(config);
            _accountController = new AccountController(_userManager.Object, _signInManager.Object, _tokenService);

        }

        [TestCase("second@gmail.com", "sec0nd1234$")]
        public async Task LoginUser_UserNotExists_ReturnsUnauthorized(string email, string password) {
            _userManager.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(_users.FirstOrDefault(u => u.Email == email));

            var result = await _accountController.Login(new LoginDTO { Email = email, Password = password});

            UnauthorizedResult unauthorizedResult = result.Result as UnauthorizedResult;

            Assert.That(unauthorizedResult, Is.TypeOf<UnauthorizedResult>());
        }

        [TestCase("first@gmail.com", "first1234$")]
        public async Task LoginUser_UserExists_ReturnsUserDTO(string email, string password) {

            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<String>()))
                .ReturnsAsync(new User { 
                    Id = new Guid().ToString(), 
                    DisplayName = "Some", 
                    Email = email,
                    IsAdmin = false,
                    UserName = "some"});
            
            _signInManager.Setup(x => x.CheckPasswordSignInAsync(It.IsAny<User>(), It.IsAny<String>(), It.IsAny<Boolean>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            var result = await _accountController.Login(new LoginDTO { Email = email, Password = password});

            UserDTO userDTO = result.Value as UserDTO;

            Assert.That(userDTO, Is.TypeOf<UserDTO>());
        }
    }
}