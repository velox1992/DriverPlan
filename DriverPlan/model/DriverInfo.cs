using System;
using System.Collections.Generic;
using System.Text;

namespace DriverPlan.model
{
    class DriverInfo
    {
        // ToDo: Internal Key

        public string Driver { get; set; }

        public DateTime DeliveryTime { get; set; }

        public string DeliveryLocation { get; set; }

        public string Note { get; set; }
    }
}
