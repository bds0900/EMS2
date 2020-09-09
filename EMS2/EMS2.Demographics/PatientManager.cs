using EMS2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMS2.Demographics
{
    public class PatientManager
    {
        public EMSContext _context;
        public PatientManager(EMSContext context)
        {
            _context = context;
        }
        public IList<Patient> GetMembers(string HeadOfHouse)
        {
            return _context.Patients.Where(p => p.HeadOfHouse == HeadOfHouse).ToList();
        }
        public Patient GetMember(string HCN)
        {
           return _context.Patients.FindAsync(HCN).Result;
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
            _context.Patients.Add(patient);
            _context.SaveChanges();
        }
        public void Add(string HCN,string lastName,string firstName,string mInitial,DateTime DateOfBirth,string sex,string HOH)
        {
            Patient patient=new Patient { HCN = HCN,LastName=lastName,FirstName=firstName,MInitial=mInitial,DateBirth=DateOfBirth,Sex=sex, }

        }
    }
}
