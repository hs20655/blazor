using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Authentication
{
    public class CreateRoleModel
    {
        [Required(ErrorMessage = "Role is required")]
        public string? Role { get; set; }
    }
}
