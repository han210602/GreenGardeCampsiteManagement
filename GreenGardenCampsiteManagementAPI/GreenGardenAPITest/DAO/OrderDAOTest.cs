using Xunit;
using FluentAssertions;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using GreenGardenCampsiteManagementAPI.Controllers;
using BusinessObject.DTOs;
using Repositories;
using System;
using Repositories.Orders;
using AutoMapper;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using BusinessObject.Models;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Repositories.Accounts;
using DataAccess.DAO;
using Moq;

namespace GreenGardenAPITest.DAO
{
    public class OrderDAOTest
    {
        private readonly OrderDAO _orderDAO;
        private readonly Mock<IOrderManagementRepository> _mockRepo;
        
        public OrderDAOTest()
        {
           _mockRepo = new Mock<IOrderManagementRepository>();  
            // Khởi tạo DAO với context giả lập
            _orderDAO = new OrderDAO();
        }

        [Fact]
        public void GetAllOrder_ShouldReturnListOfOrderDTO()
        {
            // Arrange: Giả lập dữ liệu trong DbSet
            var orders = new List<OrderDTO>
        {
            new OrderDTO
            {
                OrderId = 1,
                CustomerId = 1,
                EmployeeId = 1,
                EmployeeName = "JohnDoe",
                CustomerName = "JaneSmith",
                PhoneCustomer = "123456789",
                OrderDate = DateTime.Now,
                OrderUsageDate = DateTime.Now.AddDays(1),
                Deposit = 100,
                TotalAmount = 1000,
                AmountPayable = 900,
                StatusOrder = true,
                ActivityId = 1
            }
        };

            _mockRepo.Setup(repo => repo.GetAllOrders()).Returns(orders);

            // Act: Gọi phương thức getAllOrder
            var result = OrderDAO.getAllOrder();

            // Assert: Kiểm tra kết quả trả về
            result.Should().NotBeNull();
            result.Should().HaveCount(1);  // Kiểm tra có 1 order DTO được trả về

            var orderDTO = result[0];
            orderDTO.OrderId.Should().Be(1);
            orderDTO.CustomerName.Should().Be("JohnDoe");
            orderDTO.EmployeeName.Should().Be("JaneSmith");
            orderDTO.PhoneCustomer.Should().Be("123456789");
            orderDTO.StatusOrder.Should().Be(true);
            orderDTO.ActivityName.Should().Be("Gardening");
        }

        [Fact]
        public void GetAllOrder_ShouldReturnEmptyList_WhenNoOrders()
        {
            // Arrange: Thiết lập mock trả về danh sách rỗng
            _mockRepo.Setup(repo => repo.GetAllOrders()).Returns(new List<OrderDTO>());

            // Act: Gọi phương thức GetAllOrder
            var result = OrderDAO.getAllOrder();

            // Assert: Kiểm tra kết quả trả về là danh sách rỗng
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetAllOrder_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange: Thiết lập mock để ném ra ngoại lệ khi gọi GetAllOrders
            _mockRepo.Setup(repo => repo.GetAllOrders()).Throws(new Exception("Database connection error"));

            // Act & Assert: Kiểm tra xem ngoại lệ có được ném ra đúng cách không
            var exception = Assert.Throws<Exception>(() => OrderDAO.getAllOrder());
            exception.Message.Should().Be("Error retrieving orders");
            exception.InnerException.Message.Should().Be("Database connection error");
        }
    }

}
