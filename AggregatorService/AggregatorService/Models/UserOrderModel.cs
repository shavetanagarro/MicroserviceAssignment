using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AggregatorService.Models
{
    public class UserOrderModel
    {
        public UserModel User { get; set; }
        public IEnumerable<OrderModel> Order { get; set; }
    }
}
