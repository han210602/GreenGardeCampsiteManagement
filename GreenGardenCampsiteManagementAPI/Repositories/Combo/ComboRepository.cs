using BusinessObject.DTOs;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Combo
{
    public class ComboRepository : IComboRepository
    {
        public ComboDetail ComboDetail(int id)
        {
            return ComboDAO.GetComboDetail(id);

        }

        public List<ComboDTO> Combos()
        {
            return ComboDAO.GetListCombo();
        }
    }
}
