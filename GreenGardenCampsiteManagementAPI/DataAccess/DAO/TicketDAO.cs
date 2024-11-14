﻿using BusinessObject.DTOs;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.DAO
{
    public class TicketDAO
    {
        //private static GreenGardenContext context = new GreenGardenContext();

        private static GreenGardenContext _context;
        public static void InitializeContext(GreenGardenContext context)
        {
            _context = context;
        }

        public static List<TicketDTO> GetAllTickets()
        {
            var tickets = _context.Tickets
                .Include(ticket => ticket.TicketCategory)
                .Select(ticket => new TicketDTO
                {
                    TicketId = ticket.TicketId,
                    TicketName = ticket.TicketName,
                    Price = ticket.Price,
                    TicketCategoryName = ticket.TicketCategory.TicketCategoryName
                }).ToList();
            return tickets;
        }
        public static List<TicketDTO> GetAllCustomerTickets()
        {
            var tickets = _context.Tickets
                .Include(ticket => ticket.TicketCategory).Where(s => s.Status == true)
                .Select(ticket => new TicketDTO
                {
                    TicketId = ticket.TicketId,
                    TicketName = ticket.TicketName,
                    Price = ticket.Price,
                    TicketCategoryName = ticket.TicketCategory.TicketCategoryName
                }).ToList();
            return tickets;
        }
        public static TicketDTO GetTicketDetail(int ticketId)
        {
            var ticket = _context.Tickets
                .Include(t => t.TicketCategory)
                .Where(t => t.TicketId == ticketId)
                .Select(t => new TicketDTO
                {
                    TicketId = t.TicketId,
                    TicketName = t.TicketName,
                    Price = t.Price,
                    TicketCategoryName = t.TicketCategory.TicketCategoryName
                })
                .FirstOrDefault(); // Return the first match or null if not found

            return ticket;
        }
        public static void AddTicket(AddTicket ticketDto)
        {
            var ticket = new Ticket
            {
                TicketId = ticketDto.TicketId,
                TicketName = ticketDto.TicketName,
                Price = ticketDto.Price,
                CreatedAt = DateTime.Now,
                TicketCategoryId = ticketDto.TicketCategoryId,
                ImgUrl = ticketDto.ImgUrl,
                Status = true

            };
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
        }

        public static void UpdateTicket(UpdateTicket ticketDto)
        {
            var ticket = _context.Tickets.FirstOrDefault(t => t.TicketId == ticketDto.TicketId);
            if (ticket == null)
            {
                throw new Exception($"Ticket with ID {ticketDto.TicketId} does not exist.");
            }

            ticket.TicketName = ticketDto.TicketName;
            ticket.Price = ticketDto.Price;
            _context.SaveChanges();
        }
        public static void ChangeTicketStatus(int ticketId, ChangeTicketStatus newStatus)
        {
            // Find the food or drink item by ItemId
            var foodAndDrink = _context.Tickets.FirstOrDefault(f => f.TicketId == ticketId);

            // If the item does not exist, throw an exception
            if (foodAndDrink == null)
            {
                throw new Exception($"Food and Drink with ID {ticketId} does not exist.");
            }

            // Update the status
            foodAndDrink.Status = newStatus.Status;

            // Save changes to the database
            _context.SaveChanges();
        }

        public static List<TicketCategoryDTO> GetAllTicketCategories()
        {
            var tickets = _context.TicketCategories

                .Select(ticket => new TicketCategoryDTO
                {
                    TicketCategoryId = ticket.TicketCategoryId,
                    TicketCategoryName = ticket.TicketCategoryName
                    ,
                    Description = ticket.Description,
                    CreatedAt = ticket.CreatedAt,

                }).ToList();
            return tickets;
        }
        public static List<TicketDTO> GetTicketsByCategoryIdAndSort(int? categoryId, int? sortBy)
        {
            var query = _context.Tickets
                .Include(ticket => ticket.TicketCategory).Where(s => s.Status == true) // Sửa tên biến từ gear thành ticket
                .AsNoTracking() // Không theo dõi thực thể để cải thiện hiệu suất
                .AsQueryable();

            // Lọc theo danh mục vé nếu có categoryId
            if (categoryId.HasValue)
            {
                query = query.Where(ticket => ticket.TicketCategoryId == categoryId.Value);
            }

            // Sắp xếp theo tiêu chí sortBy
            switch (sortBy)
            {
                case 1: // Sắp xếp theo giá từ thấp đến cao
                    query = query.OrderBy(ticket => ticket.Price);
                    break;
                case 2: // Sắp xếp theo giá từ cao đến thấp
                    query = query.OrderByDescending(ticket => ticket.Price);
                    break;
                default:
                    // Mặc định sắp xếp theo tên vé nếu không có tiêu chí sắp xếp nào
                    query = query;
                    break;
            }

            // Chọn các thuộc tính cần thiết và chuyển đổi sang DTO
            var tickets = query.Select(ticket => new TicketDTO
            {
                TicketId = ticket.TicketId, // Sửa tên thuộc tính từ GearId thành TicketId
                TicketName = ticket.TicketName,
                Price = ticket.Price,
                TicketCategoryName = ticket.TicketCategory.TicketCategoryName,
                ImgUrl = ticket.ImgUrl
            }).ToList();

            return tickets;
        }
    }
}
