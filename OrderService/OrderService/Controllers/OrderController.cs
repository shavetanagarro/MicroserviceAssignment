using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderService.DTO;
using OrderService.Model;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;
        private readonly OrderDBContext _dbContext;

        public OrderController(ILogger<OrderController> logger, OrderDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            if (_dbContext.Orders.Count() == 0)
            {
                _dbContext.Orders.Add(new Order { OrderId = 1, UserRefId = 1, OrderAmount = 2500, OrderDate = DateTime.UtcNow.AddDays(-2) });
                _dbContext.Orders.Add(new Order { OrderId = 2, UserRefId = 1, OrderAmount = 2600, OrderDate = DateTime.UtcNow.AddDays(-3) });
                _dbContext.Orders.Add(new Order { OrderId = 3, UserRefId = 1, OrderAmount = 2700, OrderDate = DateTime.UtcNow.AddDays(-4) });
                _dbContext.Orders.Add(new Order { OrderId = 4, UserRefId = 1, OrderAmount = 2800, OrderDate = DateTime.UtcNow.AddDays(-5) });

                _dbContext.Orders.Add(new Order { OrderId = 5, UserRefId = 2, OrderAmount = 2500, OrderDate = DateTime.UtcNow.AddDays(-2) });
                _dbContext.Orders.Add(new Order { OrderId = 6, UserRefId = 2, OrderAmount = 2600, OrderDate = DateTime.UtcNow.AddDays(-3) });
                _dbContext.Orders.Add(new Order { OrderId = 7, UserRefId = 2, OrderAmount = 2700, OrderDate = DateTime.UtcNow.AddDays(-4) });

                _dbContext.Orders.Add(new Order { OrderId = 8, UserRefId = 3, OrderAmount = 2800, OrderDate = DateTime.UtcNow.AddDays(-5) });
                _dbContext.Orders.Add(new Order { OrderId = 9, UserRefId = 3, OrderAmount = 2500, OrderDate = DateTime.UtcNow.AddDays(-2) });

                _dbContext.Orders.Add(new Order { OrderId = 10, UserRefId = 4, OrderAmount = 2600, OrderDate = DateTime.UtcNow.AddDays(-3) });
                _dbContext.Orders.Add(new Order { OrderId = 11, UserRefId = 4, OrderAmount = 2700, OrderDate = DateTime.UtcNow.AddDays(-4) });
                _dbContext.Orders.Add(new Order { OrderId = 12, UserRefId = 4, OrderAmount = 2800, OrderDate = DateTime.UtcNow.AddDays(-5) });
                _dbContext.SaveChanges();

            }
        }

        [HttpGet("{id}")]
        public IEnumerable<OrderDto> Get(int id)
        {
            return _dbContext.Orders.AsNoTracking().Where(or => or.UserRefId == id).Select(o => new OrderDto { OrderId = o.OrderId, OrderAmount = o.OrderAmount, OrderDate = o.OrderDate.ToString("dd-MMM-yyyy") }).ToList();
        }

        [HttpGet]
        public IEnumerable<OrderDto> Get()
        {
            throw new System.Exception("Error Test");
        }

    }
}
