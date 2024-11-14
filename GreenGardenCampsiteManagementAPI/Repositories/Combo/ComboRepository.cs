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
        public List<ComboDTO> CustomerCombos()
        {
            return ComboDAO.GetListCustomerCombo();
        }
        public void AddCombo(AddCombo newCombo)
        {
            ComboDAO.AddNewCombo(newCombo);
        }

        public void UpdateCombo(AddCombo updatedCombo)
        {
            ComboDAO.UpdateCombo(updatedCombo);
        }
        public void ChangeComboStatus(int comboId, ChangeComboStatus newStatus)
        {
            ComboDAO.ChangeComboStatus(comboId, newStatus); // Call DAO method
        }
    }
}
