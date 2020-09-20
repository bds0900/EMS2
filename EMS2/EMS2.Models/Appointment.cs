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

        [Required]
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [Range(1,6)]
        public int AppointmentSlot { get; set; }
        public bool Encounter { get; set; }
        public Appointment NextAppointment { get; set; }
    }
}
