using BusinessObject.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Activities
{
    public  interface IActivityRepository
    {
        List<ActivityDTO> GetAllActivities();


    }
}
