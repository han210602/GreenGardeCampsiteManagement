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
        private static GreenGardenContext context = new GreenGardenContext();

        public static List<TicketDTO> GetAllTickets()
        {
            try
            {
                var tickets = context.Tickets
                .Include(ticket => ticket.TicketCategory)
                .Select(ticket => new TicketDTO
                {
                    TicketId = ticket.TicketId,
                    TicketName = ticket.TicketName,
                    Price = ticket.Price,
                    TicketCategoryId = ticket.TicketCategory.TicketCategoryId,
                    TicketCategoryName = ticket.TicketCategory.TicketCategoryName,
                    ImgUrl = ticket.ImgUrl,
                    Status = ticket.Status,
                }).ToList();
                return tickets;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static List<TicketDTO> GetAllCustomerTickets()
        {
            try
            {
                var tickets = context.Tickets
                .Include(ticket => ticket.TicketCategory).Where(s => s.Status == true)
                .Select(ticket => new TicketDTO
                {
                    TicketId = ticket.TicketId,
                    TicketName = ticket.TicketName,
                    Price = ticket.Price,
                    TicketCategoryId = ticket.TicketCategory.TicketCategoryId,
                    TicketCategoryName = ticket.TicketCategory.TicketCategoryName,
                    ImgUrl = ticket.ImgUrl,
                    Status = ticket.Status,
                }).ToList();
                return tickets;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static TicketDTO GetTicketDetail(int ticketId)
        {
            try
            {
                var ticket = context.Tickets
               .Include(t => t.TicketCategory)
               .Where(t => t.TicketId == ticketId)
               .Select(t => new TicketDTO
               {
                   TicketId = t.TicketId,
                   TicketName = t.TicketName,
                   Price = t.Price,
                   TicketCategoryName = t.TicketCategory.TicketCategoryName,
                   TicketCategoryId = t.TicketCategory.TicketCategoryId,
                   ImgUrl = t.ImgUrl,
                   Status = t.Status
               })
               .FirstOrDefault(); // Return the first match or null if not found

                return ticket;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void AddTicket(AddTicket ticketDto)
        {
            try
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
                context.Tickets.Add(ticket);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateTicket(UpdateTicket ticketDto)
        {
            try
            {
                var ticket = context.Tickets.FirstOrDefault(t => t.TicketId == ticketDto.TicketId);
                if (ticket == null)
                {
                    throw new Exception($"Ticket with ID {ticketDto.TicketId} does not exist.");
                }

                ticket.TicketName = ticketDto.TicketName;
                ticket.Price = ticketDto.Price;
                ticket.ImgUrl = ticketDto.ImgUrl;
                ticket.TicketCategoryId = ticketDto.TicketCategoryId;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void ChangeTicketStatus(int ticketId)
        {
            // Find the food or drink item by ItemId
            try
            {
                var foodAndDrink = context.Tickets.FirstOrDefault(f => f.TicketId == ticketId);

                // If the item does not exist, throw an exception
                if (foodAndDrink == null)
                {
                    throw new Exception($"Food and Drink with ID {ticketId} does not exist.");
                }

                // Update the status
                foodAndDrink.Status = !foodAndDrink.Status;

                // Save changes to the database
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<TicketCategoryDTO> GetAllTicketCategories()
        {
            try
            {
                var tickets = context.TicketCategories

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static (List<TicketDTO> tickets, int totalPages) GetTicketsByCategoryIdAndSort(
       int? categoryId,
       int? sortBy,
       int page = 1,
       int pageSize = 3)
        {
            try
            {
                var query = context.Tickets
                    .Include(ticket => ticket.TicketCategory)
                    .Where(s => s.Status == true) // Bao gồm trạng thái vé
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

                // Tính tổng số vé và số trang
                var totalItems = query.Count(); // Tổng số vé
                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize); // Tổng số trang
                var skip = (page - 1) * pageSize;

                // Phân trang
                query = query.Skip(skip).Take(pageSize);

                // Chọn các thuộc tính cần thiết và chuyển đổi sang DTO
                var tickets = query.Select(ticket => new TicketDTO
                {
                    TicketId = ticket.TicketId,
                    TicketName = ticket.TicketName,
                    Price = ticket.Price,
                    TicketCategoryId = ticket.TicketCategory.TicketCategoryId,
                    TicketCategoryName = ticket.TicketCategory.TicketCategoryName,
                    ImgUrl = ticket.ImgUrl,
                    Status = ticket.Status
                }).ToList();

                return (tickets, totalPages);
            }
            catch (Exception ex)
            {
                throw new Exception("Đã xảy ra lỗi khi lấy dữ liệu vé: " + ex.Message);
            }
        }

    }
}
