using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EMS2.Models;
using EMS2.Data;
using EMS2.Scheduling;
using EMS2.Demographics;

namespace EMS2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly EMSContext _context;
        private AppointmentManager manager;
        private AppointmentValidation validator;
        private PatientValidation patientValidator;

        public AppointmentsController(EMSContext context)
        {
            _context = context;
            manager = new AppointmentManager(context);
            validator = new AppointmentValidation(context.Appointments);
            patientValidator = new PatientValidation(context.Patients);
        }

        // GET: api/Appointments
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments(string patientID)
        {
            return await manager.GetAllAppointments(patientID);
        }


        // PUT: api/Appointments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(string id, Appointment appointment)
        {
            if (id != appointment.AppointmentID)
            {
                return BadRequest();
            }

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Appointments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
            await patientValidator.IsValidID(appointment.PatientID);

            if (validator.IsWeekend(appointment.AppointmentDate) && appointment.AppointmentSlot > 2)
                ModelState.AddModelError("AppointmentSlot", $" { appointment.AppointmentSlot} is not applicable on week end.");
            
            if (!validator.IsValidDay(appointment.AppointmentDate))
                ModelState.AddModelError("AppointmentDate", $" { appointment.AppointmentDate} is earlier than today or later than 3 month from today.");
            
            if (!validator.IsEmptySlot(appointment.AppointmentDate,appointment.AppointmentSlot).Result)
                ModelState.AddModelError("AppointmentSlot", $"AppointmentSlot { appointment.AppointmentSlot} is occupied.");

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppointment", new { id = appointment.AppointmentID }, appointment);
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Appointment>> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return appointment;
        }

        private bool AppointmentExists(string id)
        {
            return _context.Appointments.Any(e => e.AppointmentID == id);
        }

    }
}
