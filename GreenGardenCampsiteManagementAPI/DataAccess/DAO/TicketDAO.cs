using BusinessObject.DTOs;
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
            var tickets = context.Tickets
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
            };
            context.Tickets.Add(ticket);
            context.SaveChanges();
        }

        public static void UpdateTicket(UpdateTicket ticketDto)
        {
            var ticket = context.Tickets.FirstOrDefault(t => t.TicketId == ticketDto.TicketId);
            if (ticket == null)
            {
                throw new Exception($"Ticket with ID {ticketDto.TicketId} does not exist.");
            }

            ticket.TicketName = ticketDto.TicketName;
            ticket.Price = ticketDto.Price;
            context.SaveChanges();
        }
        public static List<TicketDTO> GetTicketsByCategoryId(int categoryId)
        {
            var tickets = context.Tickets
                .Include(ticket => ticket.TicketCategory)
                .Where(ticket => ticket.TicketCategoryId == categoryId)
                .Select(ticket => new TicketDTO
                {
                    TicketId = ticket.TicketId,
                    TicketName = ticket.TicketName,
                    Price = ticket.Price,
                    TicketCategoryName = ticket.TicketCategory.TicketCategoryName
                }).ToList();
            return tickets;
        }

        public static List<TicketCategoryDTO> GetAllTicketCategories()
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
    }
}
