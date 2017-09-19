using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBob.Models.Models
{
    public class ApplicationUser
    {
        public int ID { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
