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
        List<CampingGearDTO> GetAllCustomerCampingGears();
        List<CampingCategoryDTO> GetAllCampingGearCategories();
        CampingGearDTO GetCampingGearDetail(int gearId);
        void AddCampingGear(AddCampingGearDTO gearDto);
        void UpdateCampingGear(UpdateCampingGearDTO gearDto);
        public (List<CampingGearDTO> campingGears, int TotalPages) GetCampingGearsBySort(int? categoryId, int? sortBy, int? priceRange, int page, int pageSize);
        void ChangeGearStatus(int gearId);
    }

}
