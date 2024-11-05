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
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
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


        // test for method SendResetPasswordEmail
        [Fact]
        public async Task AccountController_SendResetPasswordEmail_ReturnsContentResult_WhenSuccessful()
        {
            // Arrange
            string email = "test@gmail.com";
            string responseMessage = "{ \"message\": \"Email đặt lại mật khẩu đã được gửi đến bạn.\" }";

            // Setup the fake repository to return a specific response
            A.CallTo(() => _fakeRepo.SendResetPassword(email)).Returns(Task.FromResult(responseMessage));

            // Act
            var result = await _controller.SendResetPasswordEmail(email);

            // Assert
            result.Should().BeOfType<ContentResult>(); // The result should be a ContentResult
            var contentResult = result as ContentResult;
            contentResult.Should().NotBeNull();
            contentResult?.Content.Should().Be(responseMessage); // Ensure the content matches the expected response
            contentResult?.ContentType.Should().Be("application/json"); // Check the content type
        }

        [Fact]
        public async Task AccountController_SendResetPasswordEmail_ReturnsBadRequest_WhenEmailDoesNotExist()
        {
            // Arrange
            string email = "nonexistent@gmail.com"; // An email that does not exist
            string responseMessage = "Email không tồn tại."; // Expected response message

            // Setup the fake repository to return null or throw an exception when the email does not exist
            A.CallTo(() => _fakeRepo.SendResetPassword(email)).Returns(Task.FromResult<string>(null)); // Assuming null means the email doesn't exist

            // Act
            var result = await _controller.SendResetPasswordEmail(email);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>(); // The result should be a BadRequestObjectResult
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult?.Value.Should().Be(responseMessage); // Ensure the error message matches what we expect
        }


        [Fact]
        public async Task AccountController_SendResetPasswordEmail_Returns500_WhenExceptionThrown()
        {
            // Arrange
            string email = "test@gmail.com";
            string errorMessage = "Gửi email thất bại.";

            // Setup the fake repository to throw an exception
            A.CallTo(() => _fakeRepo.SendResetPassword(email)).Throws(new Exception(errorMessage));

            // Act
            var result = await _controller.SendResetPasswordEmail(email);

            // Assert
            result.Should().BeOfType<ObjectResult>(); // The result should be an ObjectResult for the 500 response
            var objectResult = result as ObjectResult;
            objectResult.Should().NotBeNull();
            objectResult?.StatusCode.Should().Be(500); // Ensure the status code is 500
            objectResult?.Value.Should().BeEquivalentTo(new { Message = errorMessage }); // Check the error message returned
        }

        // test for method UpdateProfile 
        [Fact]
        public async Task AccountController_UpdateProfile_ReturnsUnauthorized_WhenUserIsNotAuthenticated()
        {
            // Arrange
            var updateProfile = new UpdateProfile
            {
                UserId = 1,
                FirstName = "UpdateFirstName",
                LastName = "UpdateLastName",
                Email = "update@gmail.com",
                PhoneNumber = "1234567899"
            };

            // Simulate an unauthenticated user by mocking the controller's user context
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(); // No authenticated user

            // Act
            var result = await _controller.UpdateProfile(updateProfile);

            // Assert
            result.Should().BeOfType<UnauthorizedResult>(); // Should return Unauthorized when user is not authenticated
        }

        [Fact]
        public async Task AccountController_UpdateProfile_ReturnsBadRequest_WhenUpdateProfileIsNull()
        {
            // Arrange
            UpdateProfile? updateProfile = null;

            // Act
            var result = await _controller.UpdateProfile(updateProfile);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>(); // Should return BadRequest when input is null
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult?.Value.Should().Be("Invalid data."); // Ensure the message is "Invalid data."
        }

        [Fact]
        public async Task AccountController_UpdateProfile_ReturnsOk_WhenProfileIsUpdatedSuccessfully()
        {
            // Arrange
            var updateProfile = new UpdateProfile
            {
                UserId = 1,
                FirstName = "UpdateFirstName",
                LastName = "UpdateLastName",
                Email = "update@gmail.com",
                PhoneNumber = "1234567899"
            };
            // Set up repository
            A.CallTo(() => _fakeRepo.UpdateProfile(updateProfile)).Returns(Task.FromResult("Profile updated successfully"));

            // Act
            var result = await _controller.UpdateProfile(updateProfile);

            // Assert
            result.Should().BeOfType<OkObjectResult>(); // The result should be Ok
            var okResult = result as OkObjectResult;
            okResult?.Value.Should().Be("Profile updated successfully."); // Success message should match
        }

        [Fact]
        public async Task AccountController_UpdateProfile_ReturnsBadRequest_WhenUserIdIsInvalid()
        {
            // Arrange
            var updateProfile = new UpdateProfile { UserId = 0 };
            var updateProfile2 = new UpdateProfile { UserId = -1 };

            // Act
            var result = await _controller.UpdateProfile(updateProfile);
            var result2 = await _controller.UpdateProfile(updateProfile2);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult?.Value.Should().Be("Invalid user ID.");

            result2.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult2 = result as BadRequestObjectResult;
            badRequestResult2?.Value.Should().Be("Invalid user ID.");
        }

        [Fact]
        public async Task AccountController_UpdateProfile_ReturnsStatusCode500_WhenExceptionIsThrown()
        {
            // Arrange
            var updateProfile = new UpdateProfile
            {
                UserId = 1,
                FirstName = "UpdateFirstName",
                LastName = "UpdateLastName",
                Email = "update@gmail.com",
                PhoneNumber = "1234567899"
            };
            var errorMessage = "An error occurred while updating the profile.";
            A.CallTo(() => _fakeRepo.UpdateProfile(updateProfile)).Throws(new Exception(errorMessage)); // Simulate an exception

            // Act
            var result = await _controller.UpdateProfile(updateProfile);

            // Assert
            result.Should().BeOfType<ObjectResult>(); // The result should be ObjectResult for error
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500); // Status code should be 500
            objectResult?.Value.Should().Be($"Internal server error: {errorMessage}"); // Error message should match
        }

        // test for method ChangePassword
        [Fact]
        public async Task AccountController_ChangePassword_ReturnsOk_WhenPasswordUpdatedSuccessfully()
        {
            // Arrange
            var changePasswordDto = new ChangePassword
            {
                OldPassword = "oldPassword123",
                NewPassword = "newPassword123",
                ConformPassword = "newPassword123"
            };

            string successMessage = "Password updated successfully.";
            A.CallTo(() => _fakeRepo.ChangePassword(changePasswordDto)).Returns(Task.FromResult(successMessage));

            // Act
            var result = await _controller.ChangePassword(changePasswordDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult?.Value.Should().Be(successMessage); // Verify the success message
        }

        [Fact]
        public async Task AccountController_ChangePassword_ReturnsBadRequest_WhenPasswordChangeFails_IncorrectOldPassword()
        {
            // Arrange
            var changePasswordDto = new ChangePassword
            {
                OldPassword = "oldPassword123",
                NewPassword = "newPassword123",
                ConformPassword = "newPassword123"
            };

            string failureMessage = "Password change failed due to incorrect old password.";
            A.CallTo(() => _fakeRepo.ChangePassword(changePasswordDto)).Returns(Task.FromResult(failureMessage));

            // Act
            var result = await _controller.ChangePassword(changePasswordDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult?.Value.Should().Be(failureMessage); // Verify failure message
        }

        [Fact]
        public async Task AccountController_ChangePassword_ReturnsUnauthorized_WhenNotAuthorized()
        {
            // Arrange
            var changePasswordDto = new ChangePassword
            {
                OldPassword = "oldPassword123",
                NewPassword = "newPassword123",
                ConformPassword = "newPassword123"
            };

            // Simulate that the user is not authorized (assuming some authorization logic or middleware is set)
            // This might involve mocking or configuring a mock user identity if using a specific authentication scheme
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.User = null; // Simulate no user

            // Act
            var result = await _controller.ChangePassword(changePasswordDto);

            // Assert
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public async Task AccountController_ChangePassword_ReturnsBadRequest_WhenUserIdDoesNotExist()
        {
            // Arrange
            var changePasswordDto = new ChangePassword
            {
                UserId = 1, // Non-existing UserId
                OldPassword = "oldPassword123",
                NewPassword = "newPassword123",
                ConformPassword = "newPassword123"
            };

            // Mock repository to return a message indicating user not found
            A.CallTo(() => _fakeRepo.ChangePassword(changePasswordDto))
                .Returns(Task.FromResult("User does not exist."));

            // Act
            var result = await _controller.ChangePassword(changePasswordDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult?.Value.Should().Be("User does not exist."); // Check for specific message
        }

        [Fact]
        public async Task AccountController_ChangePassword_ReturnsBadRequest_WhenNewPasswordDoesNotMatchConfirmPassword()
        {
            // Arrange
            var changePasswordDto = new ChangePassword
            {
                UserId = 1, // Assuming this UserId exists
                OldPassword = "oldPassword123",
                NewPassword = "newPassword123",
                ConformPassword = "differentConfirmPassword"
            };

            // Act
            var result = await _controller.ChangePassword(changePasswordDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult?.Value.Should().Be("New password and confirm password do not match."); // Check for specific message
        }

        [Fact]
        public async Task AccountController_ChangePassword_ReturnsBadRequest_WhenInvalidData()
        {
            // Arrange
            ChangePassword changePasswordDto = null;

            // Act
            var result = await _controller.ChangePassword(changePasswordDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult?.Value.Should().Be("Invalid data.");
        }

        [Fact]
        public async Task AccountController_ChangePassword_ReturnsStatusCode500_WhenExceptionOccurs()
        {
            // Arrange
            var changePasswordDto = new ChangePassword
            {
                UserId = 1, // Assuming this UserId exists
                OldPassword = "oldPassword123",
                NewPassword = "newPassword123",
                ConformPassword = "newPassword123"
            };

            // Simulate an exception being thrown by the repository method
            A.CallTo(() => _fakeRepo.ChangePassword(changePasswordDto))
                .Throws(new Exception("An unexpected error occurred"));

            // Act
            var result = await _controller.ChangePassword(changePasswordDto);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500); // Check for 500 status code
            objectResult?.Value.Should().Be("An unexpected error occurred");
        }
    }
}
