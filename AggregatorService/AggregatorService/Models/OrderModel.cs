using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AggregatorService.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int OrderAmount { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
