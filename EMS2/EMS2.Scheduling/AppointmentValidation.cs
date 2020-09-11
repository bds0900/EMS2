using EMS2.Data;
using EMS2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS2.Scheduling
{
    public class AppointmentValidation
    {
        private readonly DbSet<Appointment> _context;
        public AppointmentValidation(DbSet<Appointment> context)
        {
            _context = context;
        }
        public bool IsWeekend(DateTime apptDate)
        {
            bool retVal = false;
            if (apptDate.DayOfWeek == DayOfWeek.Sunday || apptDate.DayOfWeek == DayOfWeek.Saturday)
            {
                retVal = true;
            }
            return retVal;
        }

        public bool IsValidDay(DateTime apptDate)
        {
            bool retVal = true;

            if (DateTime.Compare(apptDate, DateTime.Today) < 0)
            {
                //apptDate is earlier than today
                retVal = false;
            }
            if (DateTime.Compare(apptDate, DateTime.Today.AddMonths(3)) > 0)
            {
                //apptDate is later than today+3months
                retVal = false;
            }

            return retVal;
        }

        public async Task<bool> IsEncountered(string appointmentID)
        {
            var appointment=await _context.FindAsync(appointmentID);
            if(appointment!=null)
            {
                return appointment.Encounter;    
            }
            return false;
        }
        public async Task<bool> IsEmptySlot(DateTime pickedDate, int pickedSlot)
        {
            if(pickedSlot>7)
            {
                return false;
            }
            if(IsWeekend(pickedDate) && pickedSlot>2)
            {
                return false;
            }
            
            return !await _context.AnyAsync(appointment => appointment.AppointmentDate == pickedDate && appointment.AppointmentSlot == pickedSlot);
        }

        public bool IsSameSlot(Appointment appointment)
        {
            bool retVal = false;

            var appt = _context.Where(o =>
                    o.AppointmentID == appointment.AppointmentID &&
                    o.AppointmentSlot == appointment.AppointmentSlot
            );
            if (appt.Count() > 0)
            {
                retVal = true;
            }

            return retVal;
        }
    }
}
