using BusinessObject.DTOs;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace DataAccess.DAO
{
    public class EventDAO
    {
        private static GreenGardenContext context = new GreenGardenContext();

        public static List<EventDTO> GetAllEvents()
        {
            try
            {
                var events = context.Events.Include(user => user.CreateByNavigation)
                    .Select(eventEntity => new EventDTO
                    {
                        EventId = eventEntity.EventId,
                        EventName = eventEntity.EventName,
                        Description = eventEntity.Description,
                        EventDate = eventEntity.EventDate,
                        StartTime = eventEntity.StartTime,
                        EndTime = eventEntity.EndTime,
                        Location = eventEntity.Location,
                        PictureUrl = eventEntity.PictureUrl,
                        IsActive = eventEntity.IsActive,
                        CreatedAt = eventEntity.CreatedAt,
                        NameCreateByUser = eventEntity.CreateByNavigation.FirstName + " " + eventEntity.CreateByNavigation.LastName
                    }).ToList();

                return events;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving events: " + ex.Message);
            }
        }
        public static List<EventDTO> GetEventByCreatedBy(int id)
        {
            try
            {
                var events = context.Events.Include(user => user.CreateByNavigation)
                    .Where(e => e.CreateBy == id)
                    .Select(eventEntity => new EventDTO
                    {
                        EventId = eventEntity.EventId,
                        EventName = eventEntity.EventName,
                        Description = eventEntity.Description,
                        EventDate = eventEntity.EventDate,
                        StartTime = eventEntity.StartTime,
                        EndTime = eventEntity.EndTime,
                        Location = eventEntity.Location,
                        PictureUrl = eventEntity.PictureUrl,
                        IsActive = eventEntity.IsActive,
                        CreatedAt = eventEntity.CreatedAt,
                        NameCreateByUser = eventEntity.CreateByNavigation.FirstName + " " + eventEntity.CreateByNavigation.LastName
                    }).ToList();

                return events;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving events: " + ex.Message);
            }
        }
        public static EventDTO GetEventById(int id)
        {
            try
            {
                // Truy vấn sự kiện theo EventId và bao gồm thông tin người tạo
                var eventEntity = context.Events
                    .Include(user => user.CreateByNavigation) // Bao gồm dữ liệu từ bảng liên kết với người tạo
                    .Where(e => e.EventId == id) // Điều kiện để tìm sự kiện theo EventId
                    .Select(eventEntity => new EventDTO
                    {
                        EventId = eventEntity.EventId,                   // ID của sự kiện
                        EventName = eventEntity.EventName,               // Tên của sự kiện
                        Description = eventEntity.Description,           // Mô tả của sự kiện
                        EventDate = eventEntity.EventDate,               // Ngày diễn ra sự kiện
                        StartTime = eventEntity.StartTime,               // Thời gian bắt đầu sự kiện
                        EndTime = eventEntity.EndTime,                   // Thời gian kết thúc sự kiện
                        Location = eventEntity.Location,                 // Địa điểm tổ chức sự kiện
                        PictureUrl = eventEntity.PictureUrl,             // URL hình ảnh của sự kiện
                        IsActive = eventEntity.IsActive,                 // Trạng thái kích hoạt của sự kiện
                        CreatedAt = eventEntity.CreatedAt,               // Thời điểm tạo sự kiện
                        NameCreateByUser = eventEntity.CreateByNavigation.FirstName + " " + eventEntity.CreateByNavigation.LastName // Tên người tạo sự kiện
                    })
                    .FirstOrDefault(); // Trả về kết quả đầu tiên hoặc null nếu không tìm thấy

                // Kiểm tra nếu không tìm thấy sự kiện
                if (eventEntity == null)
                {
                    throw new Exception($"Event with ID {id} not found.");
                }

                return eventEntity;
            }
            catch (Exception ex)
            {
                // Bắt lỗi và ném ngoại lệ với thông báo chi tiết
                throw new Exception("Error retrieving event: " + ex.Message);
            }
        }

        private static async Task SendEventNotificationEmail(string email, CreateEventDTO eventDTO, IConfiguration configuration)
        {
            // Khai báo địa chỉ email người gửi
            var fromAddress = new MailAddress("CustomerService94321@gmail.com", "SongQue Green Garden");
            var toAddress = new MailAddress(email);
            const string fromPassword = "lwrtmwkgshlqaycp";

            // Tạo tiêu đề cho email
            string subject = $"Sự kiện mới: {eventDTO.EventName}";

            // Tạo nội dung email
            string body = $"Chào bạn,\n\n" +
                          $"Chúng tôi có một sự kiện mới sắp được diễn ra\n\n" +
                          $"Tên sự kiện: {eventDTO.EventName}\n" +
                          $"Mô tả: {eventDTO.Description}\n" +
                          $"Ngày sự kiện: {eventDTO.EventDate.ToShortDateString()}\n" +
                          $"Thời gian bắt đầu: {eventDTO.StartTime}\n" +
                          $"Thời gian kết thúc: {eventDTO.EndTime}\n" +
                          $"Địa điểm: {eventDTO.Location}\n" +
                          $"Hình ảnh: {eventDTO.PictureUrl}\n\n" +
                          $"Chúng tôi rất mong có được sự tham gia của các bạn\n\n" +
                          $"Cảm ơn bạn!";

            // Cấu hình SMTP
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            // Tạo và gửi email
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = false // Không sử dụng HTML
            })
            {
                await smtp.SendMailAsync(message);
            }
        }
        public static async Task<bool> AddEvent(CreateEventDTO eventDTO, IConfiguration configuration)
        {
            try
            {
                using (var context = new GreenGardenContext())
                {
                    // Thêm sự kiện vào cơ sở dữ liệu
                    var newEvent = new Event
                    {
                        EventName = eventDTO.EventName,
                        Description = eventDTO.Description,
                        EventDate = eventDTO.EventDate,
                        StartTime = TimeSpan.Parse(eventDTO.StartTime),
                        EndTime = TimeSpan.Parse(eventDTO.EndTime),
                        Location = eventDTO.Location,
                        PictureUrl = eventDTO.PictureUrl,
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        CreateBy = eventDTO.CreateBy // ID của người tạo sự kiện
                    };

                    context.Events.Add(newEvent);
                    await context.SaveChangesAsync();

                    // Lấy danh sách người dùng có RoleId = 3 để gửi email
                    var usersToNotify = context.Users
                                               .Where(u => u.RoleId == 3)
                                               .Select(u => u.Email)
                                               .ToList();

                    // Tạo và gửi email cho từng người dùng
                    var emailTasks = usersToNotify.Select(userEmail =>
                        Task.Run(async () => await SendEventNotificationEmail(userEmail, eventDTO, configuration))
                    );

                    await Task.WhenAll(emailTasks);

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding event and sending emails: " + ex.Message);
            }
        }

        public static bool UpdateEvent(UpdateEventDTO eventDTO)
        {
            try
            {
                using (var context = new GreenGardenContext())
                {
                    var eventToUpdate = context.Events.FirstOrDefault(e => e.EventId == eventDTO.EventId);

                    if (eventToUpdate == null)
                    {
                        throw new Exception("Event not found.");
                    }

                    eventToUpdate.EventName = eventDTO.EventName;
                    eventToUpdate.Description = eventDTO.Description;
                    eventToUpdate.EventDate = eventDTO.EventDate;
                    eventToUpdate.StartTime = TimeSpan.Parse(eventDTO.StartTime);
                    eventToUpdate.EndTime = TimeSpan.Parse(eventDTO.EndTime);
                    eventToUpdate.Location = eventDTO.Location;
                    eventToUpdate.PictureUrl = eventDTO.PictureUrl;
                    eventToUpdate.IsActive = eventDTO.IsActive;

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating event: " + ex.Message);
            }
        }
        public static bool DeleteEvent(int eventId)
        {
            try
            {
                using (var context = new GreenGardenContext())
                {
                    var eventToDelete = context.Events.FirstOrDefault(e => e.EventId == eventId);

                    if (eventToDelete == null)
                    {
                        throw new Exception("Event not found.");
                    }

                    context.Events.Remove(eventToDelete);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting event: " + ex.Message);
            }
        }

    }
}
