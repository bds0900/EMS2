using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace EMS2.Models
{
    public class PatientContext: DbContext
    {
        public PatientContext(DbContextOptions<PatientContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
    }
}
