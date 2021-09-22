using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageRepo.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter a name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter an email")]
        [EmailAddress]
        public string Email { get; set; }
        [Range(16, 99, ErrorMessage ="Must be 16 or older")]
        public int Age { get; set; }
    }
}
