﻿using System;
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
        public DateTime AppointmentDate { get; set; }
        public int AppointmentSlot { get; set; }
        public bool Encounter { get; set; }
        public Appointment NextAppointment { get; set; }
    }
}