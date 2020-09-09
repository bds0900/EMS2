using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMS2.Models
{
    public class Appointment
    {
        [Key]
        public string AppointmentID { get; set; }
        public int NumberOfPatients { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int AppointmentSlot { get; set; }
        public string PatientID1 { get; set; }
        public string PatientID2 { get; set; }
        public bool Encounter { get; set; }
        public string NextAppointment { get; set; }
    }
}
