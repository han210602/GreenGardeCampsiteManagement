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
        public static List<ActivityDTO> getAllActivity()
        {
            var listActivities = new List<ActivityDTO>();
            try
            {
                using (var context = new GreenGardenContext())
                {
                    listActivities = context.Activities.Select(a => new ActivityDTO()
                    { 
                        ActivityId =a.ActivityId,
                        ActivityName =a.ActivityName,
                    })
                    .ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listActivities;
        }
    }
}
