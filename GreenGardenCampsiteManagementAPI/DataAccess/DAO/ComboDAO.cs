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
                    listProducts = context.Combos.Select(f => new ComboDTO
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
    }
}
