using BusinessObject.DTOs;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Activities
{
    public class ActiviyRepository : IActivityRepository
    {
        public List<ActivityDTO> GetAllActivities()
        {
            return ActivityDAO.getAllActivity();
        }
    }
}
