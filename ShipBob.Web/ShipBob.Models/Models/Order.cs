using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBob.Models.Models
{
    public class Order
    {
        public int ID { get; set; }

        [Required]
        [Display(Name="User")]
        public int ApplicationUserID { get; set; }
        
        [Display(Name = "Tracking Number")]
        [Required]
        public string TrackingNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Street Address")]
        [Required]
        public string StreetAddress { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Zip { get; set; }

        public virtual ApplicationUser UserForOrder { get; set; }
    }
}
