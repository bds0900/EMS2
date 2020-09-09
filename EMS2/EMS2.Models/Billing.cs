using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS2.Models
{
    public class Billing
    {
        public int BillingID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int PatientID { get; set; }
        public string HCN { get; set; }
        public string Gender { get; set; }
        public string FeeCodes { get; set; }
        public float BillingFee { get; set; }
        public string ResponseCode { get; set; }
    }
}
