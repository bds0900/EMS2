using EMS2.Data;
using EMS2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS2.Scheduling
{
    public class AppointmentManager
    {
        public EMSContext _context;
        public AppointmentManager(EMSContext context)
        {
            _context = context;
        }
        public async Task<List<Appointment>> GetAllAppointments(string patientID)
        {
            return await (from appt in _context.Appointments
                         join schedule in _context.Schedules on appt.AppointmentID equals schedule.AppointmentID
                         select appt)
                         .ToListAsync();
        }
        public async Task<List<Appointment>> GetAppointments(string patientID)
        {
            return await (from appt in _context.Appointments
                          join schedule in _context.Schedules on appt.AppointmentID equals schedule.AppointmentID
                          where DateTime.Compare(appt.AppointmentDate,DateTime.Today)>=0
                          select appt)
                         .ToListAsync();
        }
        public async Task Add(Appointment appt)
        {
            await _context.Appointments.AddAsync(appt);
            await _context.SaveChangesAsync();
        }
        public void Update(Appointment appt)
        {
            _context.Appointments.Update(appt);
            _context.SaveChangesAsync();
        }

    }
}
