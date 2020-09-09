using EMS2.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS2.Models
{
    public class Validation
    {
        private readonly EMSContext _context;
        public Validation(EMSContext context)
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

            //get patient's birthday
            //DateTime DOB = new DateTime(2009, 09, 15);
            // is the date earlier than today?

            if (DateTime.Compare(apptDate, DateTime.Today) < 0)
            {
                retVal = false;
            }
            if (DateTime.Compare(apptDate, DateTime.Today.AddMonths(3)) > 0)
            {
                retVal = false;
            }

            return retVal;
        }

        public async Task<bool> IsValidID(string patientID)
        {
            return await _context.Patients.AnyAsync(o => o.HCN == patientID); 
        }

        public bool IsValidSlot(AppointmentDTO appointment)
        {
            bool retVal = false;

            // if user selected date is weekend and slot 1-2 || slot 1-6
            if ((IsWeekend(appointment.AppointmentDate) && appointment.AppointmentSlot < 3) || appointment.AppointmentSlot < 7)
            {
                var appt=_context.Appointments.Where(o => 
                    o.AppointmentSlot == appointment.AppointmentSlot && 
                    o.AppointmentDate == appointment.AppointmentDate
                );
                //there is no matched appointment
                if(appt.Count()==0)
                {
                    retVal = true;
                }
            }

            return retVal;
        }

        public bool IsSameSlot(Appointment appointment)
        {
            bool retVal = false;

            var appt = _context.Appointments.Where(o =>
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
