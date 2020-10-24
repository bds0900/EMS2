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
                          where schedule.PatientID == patientID
                          select appt)
                         .ToListAsync();
        }
        public async Task<List<Appointment>> GetAppointments(string patientID)
        {
            return await (from appt in _context.Appointments
                          join schedule in _context.Schedules on appt.AppointmentID equals schedule.AppointmentID
                          where DateTime.Compare(appt.AppointmentDate, DateTime.Today) >= 0
                          where schedule.PatientID == patientID
                          select appt)
                         .ToListAsync();
        }
        public async Task<List<Appointment>> SearchAppointmentByName(string firstName, string lastName)
        {
            return await (from appt in _context.Appointments
                          join schedule in _context.Schedules on appt.AppointmentID equals schedule.AppointmentID
                          join patient in _context.Patients on schedule.PatientID equals patient.HCN
                          where patient.FirstName == firstName && patient.LastName == lastName
                          select appt)
                        .ToListAsync();
        }
        public async Task<Appointment> ScheduleAppointment(DateTime date, int slot, string patientID)
        {
            Appointment appointment = new Appointment
            {
                AppointmentID = Guid.NewGuid().ToString(),
                AppointmentDate = date,
                AppointmentSlot = slot,
                Encounter = false
            };

            Schedule schedule = new Schedule
            {
                ScheduleID = Guid.NewGuid().ToString(),
                AppointmentID = appointment.AppointmentID,
                PatientID = patientID
            };
            await _context.Appointments.AddAsync(appointment);
            await _context.Schedules.AddAsync(schedule);
            await _context.SaveChangesAsync();
            return appointment;

        }
        public async Task<Appointment> ScheduleAppointment(DateTime date, int slot, string patientID, string patientID2)
        {

            var appointment = await ScheduleAppointment(date, slot, patientID);
            Schedule schedule = new Schedule
            {
                ScheduleID = Guid.NewGuid().ToString(),
                AppointmentID = appointment.AppointmentID,
                PatientID = patientID2
            };
            await _context.Schedules.AddAsync(schedule);
            await _context.SaveChangesAsync();
            return appointment;
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
