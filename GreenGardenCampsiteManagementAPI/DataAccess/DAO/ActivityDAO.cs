using BusinessObject.DTOs;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ActivityDAO
    {
        private static GreenGardenContext _context;
        public static void InitializeContext(GreenGardenContext context)
        {
            _context = context;
        }
        public static List<ActivityDTO> getAllActivity()
        {
            var listActivities = new List<ActivityDTO>();
            try
            {
                
                    listActivities = _context.Activities.Select(a => new ActivityDTO()
                    { 
                        ActivityId =a.ActivityId,
                        ActivityName =a.ActivityName,
                    })
                    .ToList();
                

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listActivities;
        }
    }
}
