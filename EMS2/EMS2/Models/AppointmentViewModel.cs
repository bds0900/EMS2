using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS2.Models
{
    public class AppointmentViewModel : Appointment
    {
        public string PatientID1 { get; set; }
        public string PatientID2 { get; set; }
        public bool Double { get; set; }
    }
}
