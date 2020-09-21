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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EMS2.Controllers
{
    [Route("[controller]")]
    public class AppointmentsController : Controller
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
        [HttpGet]
        public IActionResult Index()
        {
            return View("CreateAppointment");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments(string patientID)
        {
            return await manager.GetAllAppointments(patientID);
        }


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

        [HttpPost]
        public async Task<ActionResult<Appointment>> CreateAppointment(AppointmentViewModel vm)
        {
            if (validator.IsWeekend(vm.AppointmentDate) && vm.AppointmentSlot > 2)
                ModelState.AddModelError("AppointmentSlot", $" { vm.AppointmentSlot} is not applicable on week end.");

            if (!validator.IsValidDay(vm.AppointmentDate))
                ModelState.AddModelError("AppointmentDate", $" { vm.AppointmentDate} is earlier than today or later than 3 month from today.");
                

            if (!validator.IsEmptySlot(vm.AppointmentDate, vm.AppointmentSlot).Result)
                ModelState.AddModelError("AppointmentSlot", $"AppointmentSlot { vm.AppointmentSlot} is occupied.");
                       
            if(!await patientValidator.IsValidID(vm.PatientID1))
            {
                ModelState.AddModelError("PatientID1", $" PatientID1 is not valid.");
            }
            else if (vm.Double && !await patientValidator.IsValidID(vm.PatientID2))
            {
                ModelState.AddModelError("PatientID2", $" PatientID2 is not valid.");
            }
            else if(vm.Double)
            {
                return View("AppointmentResult",await manager.ScheduleAppointment(vm.AppointmentDate, vm.AppointmentSlot, vm.PatientID1,vm.PatientID2));
            }
            else
            {
                return View("AppointmentResult",await manager.ScheduleAppointment(vm.AppointmentDate, vm.AppointmentSlot, vm.PatientID1));
            }

            return View();
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
        private void BuildSchedule(AppointmentViewModel vm)
        {


        }

    }
}
