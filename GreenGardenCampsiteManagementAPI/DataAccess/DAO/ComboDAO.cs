using BusinessObject.DTOs;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ComboDAO
    {
        public static List<ComboDTO> GetListCombo()
        {
            var listProducts = new List<ComboDTO>();
            try
            {
                using (var context = new GreenGardenContext())
                {
                    listProducts = context.Combos.Where(s => s.Status == true).Select(f => new ComboDTO
                    {
                        ComboId = f.ComboId,
                        ComboName = f.ComboName,
                        Description = f.Description,
                        Price = f.Price,
                        ImgUrl = f.ImgUrl,
                    }).ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listProducts;
        }
        private static GreenGardenContext context = new GreenGardenContext();
        public static void ChangeComboStatus(int comboId, ChangeComboStatus newStatus)
        {
            // Find the food or drink item by ItemId
            var foodAndDrink = context.Combos.FirstOrDefault(f => f.ComboId == comboId);

            // If the item does not exist, throw an exception
            if (foodAndDrink == null)
            {
                throw new Exception($"Food and Drink with ID {comboId} does not exist.");
            }

            // Update the status
            foodAndDrink.Status = newStatus.Status;

            // Save changes to the database
            context.SaveChanges();
        }
        public static ComboDetail GetComboDetail(int id)
        {
            var listProducts = new ComboDetail();
            try
            {
                using (var context = new GreenGardenContext())
                {
                    listProducts = context.Combos.Include(t => t.ComboTicketDetails).ThenInclude(s => s.Ticket)
                        .Include(t => t.ComboCampingGearDetails).ThenInclude(t => t.Gear)
                        .Include(t => t.ComboFootDetails).ThenInclude(s => s.Combo)
                        .Select(s => new ComboDetail()
                        {
                            ComboId = s.ComboId,
                            ComboName = s.ComboName,
                            Description = s.Description,
                            Price = s.Price,
                            ImgUrl = s.ImgUrl,
                            ComboTicketDetails = s.ComboTicketDetails.Select(s => new ComboTicketDetailDTO()
                            {
                                TicketId = s.TicketId,
                                Name = s.Ticket.TicketName,
                                Quantity = s.Quantity,
                                Description = s.Description
                            }).ToList(),
                            ComboCampingGearDetails=s.ComboCampingGearDetails.Select(s=>new ComboCampingGearDetailDTO
                            {
                                GearId= s.GearId,
                                Name=s.Gear.GearName,
                                Quantity=s.Quantity,


                            }).ToList(),
                            ComboFootDetails=s.ComboFootDetails.Select(s=>new ComboFootDetailDTO { 
                                ItemId=s.ItemId,
                                Name=s.Item.ItemName
                               ,Quantity=s.Quantity
                            }).ToList()

                        }).FirstOrDefault(o => o.ComboId == id);


                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listProducts;
        }
        public static void AddNewCombo(AddCombo newCombo)
        {
            try
            {
                using (var context = new GreenGardenContext())
                {
                    // Map AddCombo to Combo entity
                    var combo = new Combo
                    {
                        ComboName = newCombo.ComboName,
                        Description = newCombo.Description,
                        Price = newCombo.Price,
                        ImgUrl = newCombo.ImgUrl
                    };

                    // Map ComboCampingGearDetails, if any
                    if (newCombo.ComboCampingGearDetails != null)
                    {
                        combo.ComboCampingGearDetails = newCombo.ComboCampingGearDetails.Select(detail => new ComboCampingGearDetail
                        {
                            GearId = detail.GearId,
                            Quantity = detail.Quantity
                        }).ToList();
                    }

                    // Map ComboFootDetails, if any
                    if (newCombo.ComboFootDetails != null)
                    {
                        combo.ComboFootDetails = newCombo.ComboFootDetails.Select(detail => new ComboFootDetail
                        {
                            ItemId = detail.ItemId,
                            Quantity = detail.Quantity
                        }).ToList();
                    }

                    // Map ComboTicketDetails, if any
                    if (newCombo.ComboTicketDetails != null)
                    {
                        combo.ComboTicketDetails = newCombo.ComboTicketDetails.Select(detail => new ComboTicketDetail
                        {
                            TicketId = detail.TicketId,
                            Quantity = detail.Quantity,
                            Description = detail.Description
                        }).ToList();
                    }

                    // Add the new combo to the database
                    context.Combos.Add(combo);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding new combo: {ex.Message}");
            }
        }
        public static void UpdateCombo(AddCombo updatedCombo)
        {
            try
            {
                using (var context = new GreenGardenContext())
                {
                    // Tìm combo cần cập nhật trong cơ sở dữ liệu
                    var combo = context.Combos
                        .Include(c => c.ComboCampingGearDetails)
                        .Include(c => c.ComboFootDetails)
                        .Include(c => c.ComboTicketDetails)
                        .FirstOrDefault(c => c.ComboId == updatedCombo.ComboId);

                    if (combo == null)
                    {
                        throw new Exception("Không tìm thấy combo.");
                    }

                    // Cập nhật các thuộc tính chính của Combo
                    combo.ComboName = updatedCombo.ComboName;
                    combo.Description = updatedCombo.Description;
                    combo.Price = updatedCombo.Price;
                    combo.ImgUrl = updatedCombo.ImgUrl;

                    // Cập nhật ComboCampingGearDetails
                    if (updatedCombo.ComboCampingGearDetails != null)
                    {
                        foreach (var updatedDetail in updatedCombo.ComboCampingGearDetails)
                        {
                            // Tìm chi tiết đã tồn tại
                            var existingDetail = combo.ComboCampingGearDetails
                                .FirstOrDefault(d => d.GearId == updatedDetail.GearId);

                            if (existingDetail != null)
                            {
                                // Nếu tồn tại, cập nhật số lượng
                                existingDetail.Quantity = updatedDetail.Quantity;
                            }
                            else
                            {
                                // Nếu không tồn tại, thêm chi tiết mới
                                combo.ComboCampingGearDetails.Add(new ComboCampingGearDetail
                                {
                                    GearId = updatedDetail.GearId,
                                    Quantity = updatedDetail.Quantity
                                });
                            }
                        }
                    }

                    // Cập nhật ComboFootDetails
                    if (updatedCombo.ComboFootDetails != null)
                    {
                        foreach (var updatedDetail in updatedCombo.ComboFootDetails)
                        {
                            var existingDetail = combo.ComboFootDetails
                                .FirstOrDefault(d => d.ItemId == updatedDetail.ItemId);

                            if (existingDetail != null)
                            {
                                existingDetail.Quantity = updatedDetail.Quantity;
                            }
                            else
                            {
                                combo.ComboFootDetails.Add(new ComboFootDetail
                                {
                                    ItemId = updatedDetail.ItemId,
                                    Quantity = updatedDetail.Quantity
                                });
                            }
                        }
                    }

                    // Cập nhật ComboTicketDetails
                    if (updatedCombo.ComboTicketDetails != null)
                    {
                        foreach (var updatedDetail in updatedCombo.ComboTicketDetails)
                        {
                            var existingDetail = combo.ComboTicketDetails
                                .FirstOrDefault(d => d.TicketId == updatedDetail.TicketId);

                            if (existingDetail != null)
                            {
                                existingDetail.Quantity = updatedDetail.Quantity;
                                existingDetail.Description = updatedDetail.Description;
                            }
                            else
                            {
                                combo.ComboTicketDetails.Add(new ComboTicketDetail
                                {
                                    TicketId = updatedDetail.TicketId,
                                    Quantity = updatedDetail.Quantity,
                                    Description = updatedDetail.Description
                                });
                            }
                        }
                    }

                    // Lưu thay đổi vào cơ sở dữ liệu
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật combo: {ex.Message}");
            }
        }

    }
}
