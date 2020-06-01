using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS2
{
    public class Appointment
    {

        public int AppointmentID { get; set; }
        public int NumberOfPatients { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int AppointmentSlot { get; set; }
        public int PatientID1 { get; set; }
        public int PatientID2 { get; set; }
        public bool Encounter { get; set; }
        public string Chain { get; set; }
    }
}
