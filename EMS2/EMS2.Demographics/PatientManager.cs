using EMS2.Data;
using EMS2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS2.Demographics
{
    public class PatientManager
    {
        private EMSContext _context;
        private PatientValidation validator;
        public PatientManager(EMSContext context)
        {
            _context = context;
            validator = new PatientValidation(context.Patients);

        }
        public Task<List<Patient>> GetMembers(string HeadOfHouse)
        {
            return _context.Patients.Where(p => p.HeadOfHouse == HeadOfHouse).ToListAsync();
        }
        public async Task<Patient> GetPatient(string HCN)
        {
           return await _context.Patients.FindAsync(HCN);
        }
        public bool IsExist(string HCN)
        {
            return _context.Patients.FindAsync(HCN).Result != null ? true : false;
        }
        public void Update(Patient patient)
        {
            _context.Patients.Update(patient);
            _context.SaveChanges();
        }
        public void Add(Patient patient)
        {
            validator.IsValidName(patient.FirstName, patient.LastName);
            validator.IsValidDOB(patient.DateBirth);
            validator.IsValidAdditionalInfo(patient);
            validator.IsValidSex(patient.Sex);
            if(patient.HeadOfHouse!=null)
            {
                validator.IsValidHeadOfHous(patient.HeadOfHouse);
            }
            
            _context.Patients.Add(patient);
            _context.SaveChanges();
        }
        public void Add(string HCN,string lastName,string firstName,string mInitial,DateTime DateOfBirth,SEX sex,string HOH)
        {
            var headOfHouse=_context.Patients.Find(HOH);
            if (headOfHouse != null)
            {
                Patient patient = new Patient
                {
                    HCN = HCN,
                    LastName = lastName,
                    FirstName = firstName,
                    MInitial = mInitial,
                    DateBirth = DateOfBirth,
                    Sex = sex,
                    HeadOfHouse = HOH,
                    AddressLine1 = headOfHouse.AddressLine1,
                    AddressLine2 = headOfHouse.AddressLine2,
                    City = headOfHouse.City,
                    Province = headOfHouse.Province,
                    PostalCode = headOfHouse.PostalCode,
                    PhoneNumber = headOfHouse.PhoneNumber
                };

                Add(patient);
            }
        }
       
    }
}
