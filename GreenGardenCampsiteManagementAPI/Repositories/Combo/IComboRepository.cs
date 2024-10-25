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


    }
}
