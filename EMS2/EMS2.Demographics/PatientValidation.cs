using EMS2;
using EMS2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            if(DateTime.Today<DOB)
            {
                return false;
            }
            if((new DateTime(1900,1,1))>DOB)
            {
                return false;
            }
            return true;
        }
    }
}
