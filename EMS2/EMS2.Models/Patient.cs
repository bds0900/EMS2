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
        [DataType(DataType.Date)]
        public DateTime DateBirth { get; set; }

        [Required]
        public SEX Sex { get; set; }

        public string HeadOfHouse { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        
    }
    public enum SEX
    {
        M,
        F,
        I,
        H
    }
}
