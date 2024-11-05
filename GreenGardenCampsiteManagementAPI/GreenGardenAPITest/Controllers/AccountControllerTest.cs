using Xunit;
using FluentAssertions;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using GreenGardenCampsiteManagementAPI.Controllers;
using BusinessObject.DTOs;
using Repositories;
using System;
using Repositories.Accounts;
using AutoMapper;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
namespace GetAllCategoryTest.Controllers
{
    public class AccountControllerTest
    {
        private readonly AccountController _controller;
        private readonly IAccountRepository _fakeRepo;
        private readonly IMapper _mapper;
        //private readonly Register _validRegister;

        public AccountControllerTest()
        {
            // create fake data for register test
            //    _validRegister = new Register
            //    {
            //        FirstName = "John",
            //        LastName = "Doe",
            //        PhoneNumber = "0123456789",
            //        Email = "test@example.com",
            //        Password = "password123",
            //        IsActive = true,
            //        CreatedAt = DateTime.Now,
            //        RoleId = 2
            //    };

            // Create a fake repository using FakeItEasy
            _fakeRepo = A.Fake<IAccountRepository>();
            _mapper = A.Fake<IMapper>();
            _controller = new AccountController(_fakeRepo, _mapper);
        }

        // test for method Login
        [Fact]
        public void Login_WithEmptyEmailOrPassword_ShouldReturnBadRequest()
        {
            // Arrange
            var accountWithEmptyEmail = new AccountDTO { Email = "", Password = "password123" };
            var accountWithEmptyPassword = new AccountDTO { Email = "haunguyen@gmail.com", Password = "" };
            var accountWithBothEmpty = new AccountDTO { Email = "", Password = "" };

            // Act
            var resultEmptyEmail = _controller.Login(accountWithEmptyEmail);
            var resultEmptyPassword = _controller.Login(accountWithEmptyPassword);
            var resultBothEmpty = _controller.Login(accountWithBothEmpty);

            // Assert
            var badRequestResultEmptyEmail = resultEmptyEmail as BadRequestObjectResult;
            badRequestResultEmptyEmail.Should().NotBeNull();
            badRequestResultEmptyEmail.StatusCode.Should().Be(400);
            badRequestResultEmptyEmail.Value.Should().Be("Email or password cannot be empty.");

            var badRequestResultEmptyPassword = resultEmptyPassword as BadRequestObjectResult;
            badRequestResultEmptyPassword.Should().NotBeNull();
            badRequestResultEmptyPassword.StatusCode.Should().Be(400);
            badRequestResultEmptyPassword.Value.Should().Be("Email or password cannot be empty.");

            var badRequestResultBothEmpty = resultBothEmpty as BadRequestObjectResult;
            badRequestResultBothEmpty.Should().NotBeNull();
            badRequestResultBothEmpty.StatusCode.Should().Be(400);
            badRequestResultBothEmpty.Value.Should().Be("Email or password cannot be empty.");
        }

        [Fact]
        public void Login_WithValidCredentials_ShouldReturnOkResult()
        {
            // Arrange
            var validAccount = new AccountDTO { Email = "haunguyen@gmail.com", Password = "correctpassword" };
            var loginResponse = new LoginResponseDTO
            {

                Token = "mockedToken",
                FullName = "Hau Nguyen",
                UserId = 1,
                ProfilePictureUrl = "https://example.com/profile.jpg",
                Email = validAccount.Email,
                Password = validAccount.Password,
                Phone = "123456789",
                RoleId = 1
            };

            A.CallTo(() => _fakeRepo.Login(validAccount)).Returns(JsonSerializer.Serialize(loginResponse));

            // Act
            var result = _controller.Login(validAccount);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult?.StatusCode.Should().Be(200);
            okResult?.Value.Should().BeEquivalentTo(JsonSerializer.Serialize(loginResponse));
        }

        [Fact]
        public void Login_WithInvalidCredentials_ShouldReturnUnauthorized()
        {
            // Arrange
            var invalidAccount = new AccountDTO { Email = "haunguyen@gmail.com", Password = "wrongpassword" };

            A.CallTo(() => _fakeRepo.Login(invalidAccount)).Returns(null);

            // Act
            var result = _controller.Login(invalidAccount);

            // Assert
            var unauthorizedResult = result as UnauthorizedObjectResult;
            unauthorizedResult.Should().NotBeNull();
            unauthorizedResult.StatusCode.Should().Be(401);
            unauthorizedResult.Value.Should().Be("Invalid email or password.");
        }

        [Fact]
        public void Login_WithException_ShouldReturnStatusCode500()
        {
            // Arrange
            var accountWithError = new AccountDTO { Email = "haunguyen@gmail.com", Password = "errorpassword" };

            A.CallTo(() => _fakeRepo.Login(accountWithError)).Throws(new Exception("An unexpected error occurred."));

            // Act
            var result = _controller.Login(accountWithError);

            // Assert
            var statusCodeResult = result as ObjectResult;
            statusCodeResult.Should().NotBeNull();
            statusCodeResult.StatusCode.Should().Be(500);
            statusCodeResult.Value.Should().Be("An unexpected error occurred.");
        }

        //test for method GetAllAccounts
        [Fact]
        public void AccountController_GetAllAccounts_ReturnsOkResult()
        {
            // Arrange
            var accounts = A.Fake<ICollection<AccountDTO>>();
            var accountList = A.Fake<List<AccountDTO>>();
            A.CallTo(() => _mapper.Map<List<AccountDTO>>(accounts)).Returns(accountList);

            // Act
            var result = _controller.GetAllAccounts();

            // Assert
            //result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeEquivalentTo(accountList);
        }

        [Fact]
        public void AccountController_GetAllAccounts_WhenExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            A.CallTo(() => _fakeRepo.GetAllAccount()).Throws(new Exception("Database error"));


            // Act
            var result = _controller.GetAllAccounts();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
            objectResult?.Value.Should().Be("Internal server error: Database error");
        }

        //test for method GetAccountById
        [Fact]
        public void AccountController_GetAccountById_ReturnsOkResult()
        {
            // Arrange
            int accountId = 1;
            var account = A.Fake<ViewUserDTO>();
            A.CallTo(() => _fakeRepo.GetAccountById(accountId)).Returns(account);

            // Act
            var result = _controller.GetAccountById(accountId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeEquivalentTo(account);
        }

        [Fact]
        public void AccountController_GetAccountById_ReturnsInternalServerErrorOnException()
        {
            // Arrange
            int accountId = 1;
            A.CallTo(() => _fakeRepo.GetAccountById(accountId)).Throws(new Exception("Test exception"));

            // Act
            var result = _controller.GetAccountById(accountId);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
            objectResult?.Value.Should().Be("Internal server error: Test exception");
        }

        [Fact]
        public void AccountController_GetAccountById_ReturnsBadRequest()
        {
            // Arrange
            int invalidAccountId = -1;

            // Act
            var result = _controller.GetAccountById(invalidAccountId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            (result as BadRequestObjectResult)?.Value.Should().Be("Invalid account ID.");
        }

        [Fact]
        public void AccountController_GetAccountById_ReturnsNotFound()
        {
            // Arrange
            int accountId = 1;
            A.CallTo(() => _fakeRepo.GetAccountById(accountId)).Returns(null);

            // Act
            var result = _controller.GetAccountById(accountId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            (result as NotFoundObjectResult)?.Value.Should().Be("Account not found.");
        }

        // test for method SendVerificationCode
        [Fact]
        public async Task AccountController_SendVerificationCode_ReturnsOkResult()
        {
            // Arrange
            string email = "hau@gmail.com";
            string verificationCodeMessage = "Verification code";
            A.CallTo(() => _fakeRepo.SendVerificationCode(email)).Returns(Task.FromResult(verificationCodeMessage));

            // Act
            var result = await _controller.SendVerificationCode(email);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult?.Value.Should().Be(verificationCodeMessage);
        }

        [Fact]
        public async Task AccountController_SendVerificationCode_ReturnsInternalServerErrorOnException()
        {
            // Arrange
            string email = "hau@gmail.com";
            A.CallTo(() => _fakeRepo.SendVerificationCode(email)).Throws(new Exception("Test error"));

            // Act
            var result = await _controller.SendVerificationCode(email);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
            objectResult?.Value.Should().Be("Test error");
        }

        // test for method Register
        [Fact]
        public async Task AccountController_Register_ReturnsOkResult()
        {
            // Arrange
            var registerRequest = new Register()
            {
                FirstName = "Test",
                LastName = "1",
                PhoneNumber = "1234567890",
                Email = "test@gmail.com",
                Password = "password"
            };
            string enteredCode = "123456";

            //A.CallTo(() => _fakeRepo.Register(registerRequest, enteredCode)).As<OkObjectResult>();

            // Act
            var result = await _controller.Register(registerRequest, enteredCode);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult?.StatusCode.Should().Be(200);
            //okResult?.Value.Should().Be();
        }

        [Fact]
        public async Task AccountController_NullRegister_ReturnsBadRequest()
        {
            // Arrange
            var registerRequest = A.Fake<Register>();
            string enteredCode = "123456";
            var registerRequestNull = new Register()
            {
                FirstName = "",
                LastName = "",
                PhoneNumber = "",
                Email = "",
                Password = ""
            };
            string enteredCodeNull = null!;

            // Act
            var result = await _controller.Register(registerRequest, enteredCodeNull);
            var result2 = await _controller.Register(registerRequestNull, enteredCode);
            var result3 = await _controller.Register(registerRequestNull, enteredCodeNull);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            (result as BadRequestObjectResult)?.Value.Should().Be("Registration failed.");

            result2.Should().BeOfType<BadRequestObjectResult>();
            (result2 as BadRequestObjectResult)?.Value.Should().Be("Registration failed.");

            result3.Should().BeOfType<BadRequestObjectResult>();
            (result3 as BadRequestObjectResult)?.Value.Should().Be("Registration failed.");
        }

        [Fact]
        public async Task AccountController_Register_ReturnsInternalServerErrorOnException()
        {
            // Arrange
            var registerRequest = new Register()
            {
                FirstName = "Test",
                LastName = "1",
                PhoneNumber = "1234567890",
                Email = "test@gmail.com",
                Password = "password"
            };
            string enteredCode = "123456";

            A.CallTo(() => _fakeRepo.Register(registerRequest, enteredCode)).Throws(new Exception("Test error"));

            // Act
            var result = await _controller.Register(registerRequest, enteredCode);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
            objectResult?.Value.Should().Be("Test error");
        }


        // test for method 

    }
}
