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
        [Display(Name = "Health Card Number")]
        [RegularExpression(@"^\d{10}[A-Z]{2}")]
        public string HCN { get; set; }

        [Required]
        [RegularExpression(@"\w+", ErrorMessage ="regular express(indvalid)")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"\w+", ErrorMessage = "regular express(indvalid)")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [RegularExpression(@"\w+", ErrorMessage = "regular express(indvalid)")]
        [Display(Name = "Middle Name Initial")]
        public string MInitial { get; set; }

        [Required]
        [DOB]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DateBirth { get; set; }

        [Required]
        public SEX Sex { get; set; }

        [Display(Name = "Head of House")]
        public string HeadOfHouse { get; set; }

        [RegularExpression(@"^\d+\s[A-z]+\s[A-z]+",ErrorMessage = "regular express(indvalid)")]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [RegularExpression(@"(?:[A-Z][a-z.-]+[ ]?)+",ErrorMessage = "regular express(indvalid)")]
        public string City { get; set; }

        [RegularExpression(@"^(?:AB|BC|MB|N[BLTSU]|ON|PE|QC|SK|YT)*$",ErrorMessage = "regular express(indvalid)")]
        public string Province { get; set; }

        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^(?!.*[DFIOQU])[A-VXY][0-9][A-Z]●?[0-9][A-Z][0-9]$",ErrorMessage = "regular express(indvalid)")]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})",ErrorMessage = "regular express(indvalid)")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        
    }
    public enum SEX
    {
        M,
        F,
        I,
        H
    }
    public class DOBAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            if(value!=null)
            {
                var DOB = (DateTime)value;
                if (DateTime.Compare(DateTime.Today, DOB)<=0)
                {
                    return new ValidationResult("Date of Birth can not be later than today");
                }
                if (DateTime.Compare(new DateTime(1900, 1, 1), DOB) >0)
                {
                    return new ValidationResult("Year of Date of Birth can not be earlier than 1900");
                }
                return ValidationResult.Success;
            }

            return new ValidationResult("Value is null");
        }
    }
}
