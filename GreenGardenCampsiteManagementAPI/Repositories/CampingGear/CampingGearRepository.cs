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
        public List<CampingGearDTO> GetAllCustomerCampingGears()
        {
            return CampingGearDAO.GetAllCustomerCampingGears();
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
        public (List<CampingGearDTO> campingGears, int TotalPages) GetCampingGearsBySort(int? categoryId, int? sortBy, int? priceRange, int page, int pageSize)
        {
            return CampingGearDAO.GetCampingGears(categoryId, sortBy, priceRange, page, pageSize);
        }
        public void ChangeGearStatus(int gearId)
        {
            CampingGearDAO.ChangeGearStatus(gearId); // Call DAO method
        }
    }
}
