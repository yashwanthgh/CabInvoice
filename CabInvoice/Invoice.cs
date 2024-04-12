using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoice
{
    public class Invoice
    {
        public int TotalRides { get; set; }
        public double TotalFare { get; set; }
        public double AverageFarePerRide { get; set; }
    }
}
