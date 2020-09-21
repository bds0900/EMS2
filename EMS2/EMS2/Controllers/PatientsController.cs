using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EMS2.Models;
using EMS2.Demographics;
using EMS2.Data;

namespace EMS2.Controllers
{
    public class PatientsController : Controller
    {
        private readonly EMSContext _context;
        private PatientManager _manager;

        public PatientsController(EMSContext context)
        {
            _context = context;
            _manager = new PatientManager(context);
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult<Patient>> SearchPatientById(string hcn)
        {
            var patients = await _context.Patients.FindAsync(hcn);
            if (patients == null)
            {
                return NotFound();
            }

            return View("SearchResultDetail", patients);
        }

        [HttpGet]
        public async Task<ActionResult<Patient>> SearchPatientByName(string FirstName,string LastName)
        {
            List<Patient> patients = await _context.Patients.Where(p => p.FirstName == FirstName && p.LastName==LastName).ToListAsync() ;
            if (patients.Count == 0)
            {
                return NotFound();
            }

            return View("SearchResult", patients);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<Patient>> Register(Patient patient)
        {
            if (!String.IsNullOrEmpty(patient.HeadOfHouse))
            {
                var headOfHouse = _context.Patients.Find(patient.HeadOfHouse);
                if (headOfHouse != null)
                {
                    patient.AddressLine1 = headOfHouse.AddressLine1;
                    patient.AddressLine2 = headOfHouse.AddressLine2;
                    patient.City = headOfHouse.City;
                    patient.Province = headOfHouse.Province;
                    patient.PostalCode = headOfHouse.PostalCode;
                    patient.PhoneNumber = headOfHouse.PhoneNumber;

                    ModelState.MarkFieldValid("HeadOfHouse");
                }
                else
                {
                    ModelState.AddModelError("HeadOfHouse", $"can not find the Head of House {patient.HCN}");
                }
            }


            if (!ModelState.IsValid)
            {
                return View(patient);
            }
        
            _context.Patients.Add(patient);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PatientExists(patient.HCN))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPatient", new { id = patient.HCN }, patient);
        }


        [HttpGet]
        public async Task<IActionResult> Update(string hcn)
        {
            return View(await _manager.GetPatient(hcn));
        }

        public async Task<IActionResult> UpdatePatient(Patient patient)
        {
            /*if (id != patient.HCN)
            {
                return BadRequest();
            }*/

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(patient.HCN))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return View("Index");
        }




        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetMembers(string hcn)
        {
            return await _context.Patients.Where(p => p.HeadOfHouse == hcn).ToListAsync();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Patient>> DeletePatient(string id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return patient;
        }

        private bool PatientExists(string id)
        {
            return _context.Patients.Any(e => e.HCN == id);
        }
    }
}
