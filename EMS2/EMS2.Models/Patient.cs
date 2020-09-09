using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EMS2.Models
{
    public class Patient
    {
        [Key]
        public string HCN { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MInitial { get; set; }
        [Required]
        public DateTime DateBirth { get; set; }
        [Required]
        public string Sex { get; set; }
        public string HeadOfHouse { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        
    }
}
