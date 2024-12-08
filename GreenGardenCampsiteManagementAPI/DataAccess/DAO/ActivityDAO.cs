using BusinessObject.DTOs;
using BusinessObject.Models;

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
                    listActivities = context.Activities
                        .Where(a => a.ActivityId != 1003) // Lọc bỏ ActivityId là 1003
                        .Select(a => new ActivityDTO()
                        {
                            ActivityId = a.ActivityId,
                            ActivityName = a.ActivityName,
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
