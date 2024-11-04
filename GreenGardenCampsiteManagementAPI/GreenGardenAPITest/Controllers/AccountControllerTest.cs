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
namespace GetAllCategoryTest.Controllers
{
    public class AccountControllerTest
    {
        private readonly AccountController _controller;
        private readonly IAccountRepository _fakeRepo;
        private readonly IMapper _mapper;

        public AccountControllerTest()
        {
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


    }
}
