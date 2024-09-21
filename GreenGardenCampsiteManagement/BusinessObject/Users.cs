using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
     class Users
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
    }
}
