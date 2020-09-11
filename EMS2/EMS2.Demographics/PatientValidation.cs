using EMS2;
using EMS2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EMS2.Demographics
{
    public class PatientValidation
    {
        private readonly DbSet<Patient> _context;
        public PatientValidation(DbSet<Patient> context)
        {
            _context = context;
        }
        public bool IsValidDOB(DateTime DOB)
        {
            if (DateTime.Today < DOB)
            {
                return false;
            }
            if ((new DateTime(1900, 1, 1)) > DOB)
            {
                return false;
            }
            return true;
        }
        public bool IsValidHeadOfHous(string HOH)
        {
            return _context.AnyAsync(p=>p.HCN==HOH).Result;
        }
        public async Task<bool> IsValidID(string patientID)
        {
            return await _context.AnyAsync(a => a.HCN == patientID);
        }
        public bool IsValidAdditionalInfo(Patient patient)
        {
            bool retVal = true;

            string pattern;

            pattern = @"^\d+\s[A-z]+\s[A-z]+";
            if (!Regex.IsMatch(patient.AddressLine1,pattern))
            {
                retVal = false;
            }
            
            pattern = @"(?:[A-Z][a-z.-]+[ ]?)+";
            if (!Regex.IsMatch(patient.City, pattern))
            {
               retVal = false; 
            }

            pattern = @"^(?:AB|BC|MB|N[BLTSU]|ON|PE|QC|SK|YT)*$";
            if (!Regex.IsMatch(patient.Province, pattern))
            {
                retVal = false;
            }

            pattern = @"^(?!.*[DFIOQU])[A-VXY][0-9][A-Z]●?[0-9][A-Z][0-9]$";
            if (!Regex.IsMatch(patient.PostalCode, pattern))
            {
                retVal = false;
            }

            pattern = @"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})";
            if (!Regex.IsMatch(patient.PhoneNumber, pattern))
            {
                retVal = false;
            }
            return retVal;
        }

        public bool IsValidName(string firstName,string lastName)
        {
            string pattern = @"^[[:alpha:]]";
            if(!Regex.IsMatch(firstName,pattern)||!Regex.IsMatch(lastName,pattern))
            {
                return false;
            }
            return true;
        }

        public bool IsValidSex(SEX sex)
        {
            /*if(sex.GetType() == typeof(SEX))
            {
                return true;
            }
            return false;*/

            return sex is SEX;
        }
    }
}
