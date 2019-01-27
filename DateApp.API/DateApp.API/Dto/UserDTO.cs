using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DateApp.API.Dto
{
    public class UserDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(8, MinimumLength =4, ErrorMessage ="Arregla el largo de la password")]
        public string Password { get; set; }
    }
}
