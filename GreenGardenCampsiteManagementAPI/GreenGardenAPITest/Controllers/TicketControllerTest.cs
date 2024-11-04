using AutoMapper;
using BusinessObject.DTOs;
using DataAccess.DAO;
using FakeItEasy;
using FluentAssertions;
using GreenGardenCampsiteManagementAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Repositories.Tickets;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GetAllCategoryTest.Controllers
{

    public class TicketControllerTest
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly TicketController _controller;

        public TicketControllerTest()
        {
            _ticketRepository = A.Fake<ITicketRepository>();
            _mapper = A.Fake<IMapper>();
            _controller = new TicketController(_ticketRepository, _mapper);
        }

        // test for method GetAllTickets
        [Fact]
        public void TicketController_GetAllTickets_ReturnsOkResult()
        {
            // Arrange
            var tickets = A.Fake<ICollection<TicketDTO>>();
            var ticketList = A.Fake<List<TicketDTO>>();
            A.CallTo(() => _mapper.Map<List<TicketDTO>>(tickets)).Returns(ticketList);
            var controller = new TicketController(_ticketRepository, _mapper);

            // Act
            var result = controller.GetAllTickets();

            // Assert
            //result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeEquivalentTo(ticketList);
        }

        [Fact]
        public void TicketController_GetAllTickets_WhenExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            A.CallTo(() => _ticketRepository.GetAllTickets()).Throws(new Exception("Database error"));
            var controller = new TicketController(_ticketRepository, _mapper);

            // Act
            var result = controller.GetAllTickets();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
            objectResult?.Value.Should().Be("Internal server error: Database error");
        }

        // test for method AddTicket
        [Fact]
        public void AddTicket_NullTicketDto_ReturnsBadRequest()
        {
            // Act
            var result = _controller.AddTicket(null);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            (result as BadRequestObjectResult)?.Value.Should().Be("Invalid input.");
        }

        [Fact]

        public void AddTicket_ValidTicketDto_ReturnsOk()
        {
            // Arrange
            //var ticketDto = A.Fake<AddTicket>();
            var ticketDto = new AddTicket
            {
                TicketId = 15,
                TicketName = "Concert Ticket",
                Price = 100,
                CreatedAt = DateTime.Now,
                TicketCategoryId = 2,
                ImgUrl = "http://example.com/image.jpg"
            };

            A.CallTo(() => _ticketRepository.AddTicket(ticketDto)).DoesNothing();

            // Act
            var result = _controller.AddTicket(ticketDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().Be("Ticket added successfully.");
        }

        [Fact]
        public void AddTicket_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var ticketDto = new AddTicket
            {
                TicketId = 15,
                TicketName = "Concert Ticket",
                Price = 100,
                CreatedAt = DateTime.Now,
                TicketCategoryId = 2,
                ImgUrl = "http://example.com/image.jpg"
            };
            var exceptionMessage = "Database connection failed.";

            A.CallTo(() => _ticketRepository.AddTicket(ticketDto))
                .Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.AddTicket(ticketDto);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
            objectResult?.Value.Should().Be($"Internal server error: {exceptionMessage}");
        }

        //test for method GetTicketDetail
        [Fact]
        public void GetTicketDetail_ValidId_ReturnsOkResult()
        {
            // Arrange
            int ticketId = 1;
            var ticketDetail = new TicketDTO
            {
                TicketId = ticketId,
                TicketName = "Concert Ticket",
                Price = 100,
                TicketCategoryName = "Ve nguoi lon",
                ImgUrl = "http://example.com/image.jpg"
            };

            A.CallTo(() => _ticketRepository.GetTicketDetail(ticketId)).Returns(ticketDetail);

            // Act
            var result = _controller.GetTicketDetail(ticketId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult?.Value.Should().BeEquivalentTo(ticketDetail);
        }

        [Fact]
        public void GetTicketDetail_InvalidId_ReturnsInternalServerError()
        {
            // Arrange
            int ticketId = -1;
            var exceptionMessage = "Ticket not found";
            A.CallTo(() => _ticketRepository.GetTicketDetail(ticketId)).Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.GetTicketDetail(ticketId);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
            objectResult?.Value.Should().Be($"Internal server error: {exceptionMessage}");
        }

        // test for method GetAllTicketCategories
        [Fact]
        public void GetAllTicketCategories_ReturnsOkResult()
        {
            // Arrange
            var ticketCategories = A.Fake<ICollection<TicketCategoryDTO>>();
            var ticketCategoryList = A.Fake<List<TicketCategoryDTO>>();
            A.CallTo(() => _mapper.Map<List<TicketCategoryDTO>>(ticketCategories)).Returns(ticketCategoryList);
            var controller = new TicketController(_ticketRepository, _mapper);

            // Act
            var result = controller.GetAllTicketCategories();

            // Assert
            //result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeEquivalentTo(ticketCategoryList);
        }

        [Fact]
        public void GetAllTicketCategories_WhenExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var exceptionMessage = "Database error";
            A.CallTo(() => _ticketRepository.GetAllTicketCategories()).Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.GetAllTicketCategories();

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
            objectResult?.Value.Should().Be($"Internal server error: {exceptionMessage}");
        }

        //test for method GetTicketsByCategory
        [Fact]
        public void GetTicketsByCategory_ValidCategoryId_ReturnsOkResult()
        {
            // Arrange
            int categoryId = 1;
            var tickets = new List<TicketDTO>
            {
                new TicketDTO
                {
                    TicketId = 1,
                    TicketName = "Concert Ticket",
                    Price = 100,
                    TicketCategoryName = "Ve nguoi lon"
                },
            };

            A.CallTo(() => _ticketRepository.GetTicketsByCategoryId(categoryId)).Returns(tickets);

            // Act
            var result = _controller.GetTicketsByCategory(categoryId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult?.Value.Should().BeEquivalentTo(tickets);
        }

        [Fact]
        public void GetTicketsByCategory_NoTicketsFound_ReturnsNotFoundResult()
        {
            // Arrange
            int categoryId = 20;
            A.CallTo(() => _ticketRepository.GetTicketsByCategoryId(categoryId)).Returns(new List<TicketDTO>());

            // Act
            var result = _controller.GetTicketsByCategory(categoryId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult?.Value.Should().Be("No tickets found for the specified category ID.");
        }

        [Fact]
        public void GetTicketsByCategory_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            int categoryId = 3;
            var exceptionMessage = "Database error";
            A.CallTo(() => _ticketRepository.GetTicketsByCategoryId(categoryId)).Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.GetTicketsByCategory(categoryId);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
            objectResult?.Value.Should().Be($"Internal server error: {exceptionMessage}");
        }

        // test for method UpdateTicket

        [Fact]
        public void UpdateTicket_NullTicketDto_ReturnsBadRequest()
        {
            // Act
            var result = _controller.UpdateTicket(null);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            (result as BadRequestObjectResult)?.Value.Should().Be("Invalid input.");
        }

        [Fact]
        public void UpdateTicket_ValidTicketDto_ReturnsOk()
        {
            // Arrange
            var ticketDto = new UpdateTicket
            {
                TicketId = 1,
                TicketName = "Updated Concert Ticket",
                Price = 120
            };

            A.CallTo(() => _ticketRepository.UpdateTicket(ticketDto)).DoesNothing();

            // Act
            var result = _controller.UpdateTicket(ticketDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().Be("Ticket updated successfully.");
        }

        [Fact]
        public void UpdateTicket_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var ticketDto = new UpdateTicket
            {
                TicketId = 1,
                TicketName = "Updated Concert Ticket",
                Price = 120
            };
            var exceptionMessage = "Database update error";

            A.CallTo(() => _ticketRepository.UpdateTicket(ticketDto))
                .Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.UpdateTicket(ticketDto);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
            objectResult?.Value.Should().Be($"Internal server error: {exceptionMessage}");
        }

        // test for method GetTicketsByCategoryAndSort
        [Fact]
        public void GetTicketsByCategoryAndSort_ValidParameters_ReturnsOkResult()
        {
            // Arrange
            int? categoryId = 1;
            int? sort = 1; // TicketDAO
            var tickets = new List<TicketDTO>
            {
                new TicketDTO
                {
                    TicketId = 1,
                    TicketName = "Concert Ticket",
                    Price = 100,
                    TicketCategoryName = "Music",
                    ImgUrl = "http://example.com/image1.jpg"
                },
                new TicketDTO
                {
                    TicketId = 2,
                    TicketName = "Festival Ticket",
                    Price = 150,
                    TicketCategoryName = "Music",
                    ImgUrl = "http://example.com/image2.jpg"
                }
            };

            A.CallTo(() => _ticketRepository.GetTicketsByCategoryIdAndSort(categoryId, sort)).Returns(tickets);

            // Act
            var result = _controller.GetTicketsByCategoryAndSort(categoryId, sort);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult?.Value.Should().BeEquivalentTo(tickets);
        }

        [Fact]
        public void GetTicketsByCategoryAndSort_NoTicketsFound_ReturnsNotFoundResult()
        {
            // Arrange
            int? categoryId = 1; // Category with no tickets
            int? sort = 1; // Any sort option
            A.CallTo(() => _ticketRepository.GetTicketsByCategoryIdAndSort(categoryId, sort)).Returns(new List<TicketDTO>());

            // Act
            var result = _controller.GetTicketsByCategoryAndSort(categoryId, sort);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult?.Value.Should().Be("No tickets found for the specified category ID.");
        }

        [Fact]
        public void GetTicketsByCategoryAndSort_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            int? categoryId = 1; // Any category ID
            int? sort = 1; // Any sort option
            var exceptionMessage = "Database error";
            A.CallTo(() => _ticketRepository.GetTicketsByCategoryIdAndSort(categoryId, sort))
                .Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.GetTicketsByCategoryAndSort(categoryId, sort);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
            objectResult?.Value.Should().Be($"Internal server error: {exceptionMessage}");
        }
    }
}