using BusinessObject.DTOs;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.CampingGear
{
    public class CampingGearRepository : ICampingGearRepository
    {
        public List<CampingGearDTO> GetAllCampingGears()
        {
            return CampingGearDAO.GetAllCampingGears();
        }
        public CampingGearDTO GetCampingGearDetail(int id)
        {
            return CampingGearDAO.GetCampingGearDetail(id);
        }
        public void AddCampingGear(AddCampingGearDTO gearDto)
        {
            CampingGearDAO.AddCampingGear(gearDto);
        }

        public void UpdateCampingGear(UpdateCampingGearDTO gearDto)
        {
            CampingGearDAO.UpdateCampingGear(gearDto);
        }



        public List<CampingCategoryDTO> GetAllCampingGearCategories()
        {
            return CampingGearDAO.GetAllCampingGearCategories();
        }
        public List<CampingGearDTO> GetCampingGearsBySort(int? categoryId, int? sortBy, int? priceRange, int? popularity)
        {
            return CampingGearDAO.GetCampingGears(categoryId, sortBy, priceRange, popularity);
        }

    }
}
