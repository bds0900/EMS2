using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS2.Models
{
    public class Schedule
    {
        [Key]
        public string ScheduleID { get; set; }
        public string AppointmentID { get; set; }
        public string PatientID { get; set; }
    }
}
