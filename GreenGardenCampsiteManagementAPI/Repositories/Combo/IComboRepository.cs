using BusinessObject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Combo
{
    public interface IComboRepository
    {
        List<ComboDTO> Combos();
        ComboDetail ComboDetail(int id);
        void AddCombo(AddCombo newCombo);

        void UpdateCombo(AddCombo updatedCombo);
    }
}
