using BusinessObject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.CampingGear
{
    public interface ICampingGearRepository
    {
        List<CampingGearDTO> GetAllCampingGears();
        void AddCampingGear(AddCampingGearDTO gearDto);
        void UpdateCampingGear(UpdateCampingGearDTO gearDto);
        List<CampingGearDTO> GetCampingGearsByCategoryId(int categoryId);
    }
}
