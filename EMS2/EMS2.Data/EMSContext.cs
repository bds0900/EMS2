using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS2.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS2.Data
{
    public class EMSContext: DbContext
    {
        public EMSContext(DbContextOptions<EMSContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Billing> Billing { get; set; }
    }
}
