using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EMS2;
using EMS2.Models;

namespace EMS2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly EMSContext _context;

        public AppointmentsController(EMSContext context)
        {
            _context = context;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAppointments()
        {
            return await _context.Appointments
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDTO>> GetAppointment(string id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            var appointment = await _context.Appointments
                .Where(x => x.PatientID1 == patient.HCN && x.AppointmentDate>=DateTime.Today)
                .FirstOrDefaultAsync();

            if (appointment == null)
            {
                return NotFound();
            }

            return ItemToDTO(appointment);
        }

        // PUT: api/Appointments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(string id, AppointmentDTO appointment)
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
        public async Task<ActionResult<Appointment>> PostAppointment(AppointmentDTO appointment)
        {
            var val = new Validation(_context);
            await val.IsValidID(appointment.PatientID1);

            if (val.IsWeekend(appointment.AppointmentDate) && appointment.AppointmentSlot > 2)
                ModelState.AddModelError("AppointmentSlot", $" { appointment.AppointmentSlot} is not applicable on week end.");
            
            if (!val.IsValidDay(appointment.AppointmentDate))
                ModelState.AddModelError("AppointmentDate", $" { appointment.AppointmentDate} is earlier than today or later than 3 month from today.");
            
            if (!val.IsValidSlot(appointment))
                ModelState.AddModelError("AppointmentSlot", $"AppointmentSlot { appointment.AppointmentSlot} is occupied.");

            _context.Appointments.Add(CreateAppointment(appointment));
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

        private static AppointmentDTO ItemToDTO(Appointment todoItem) =>
        new AppointmentDTO
        {
            AppointmentID = todoItem.AppointmentID,
            AppointmentDate = todoItem.AppointmentDate,
            AppointmentSlot = todoItem.AppointmentSlot,
            PatientID1=todoItem.PatientID1,
            PatientID2=todoItem.PatientID2,
            NumberOfPatients=todoItem.NumberOfPatients
        };
        private static Appointment CreateAppointment(AppointmentDTO appointmentDTO)=>
        new Appointment
        {
            AppointmentID = Guid.NewGuid().ToString("B").ToUpper(),
            NumberOfPatients = appointmentDTO.NumberOfPatients,
            AppointmentDate = appointmentDTO.AppointmentDate,
            AppointmentSlot = appointmentDTO.AppointmentSlot,
            PatientID1 = appointmentDTO.PatientID1,
            PatientID2 = appointmentDTO.PatientID2,
            Encounter = false,
            NextAppointment = null
        };
    }
}
