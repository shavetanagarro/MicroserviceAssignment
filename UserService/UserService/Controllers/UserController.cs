using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserService.Model;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserDBContext _dbContext;

        public UserController(ILogger<UserController> logger, UserDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            if(_dbContext.Users.Count() ==0)
            {
                _dbContext.Users.Add(new User { Name = "Shaveta", Age = 25, Email = "shaveta@nagarro.com" });
                _dbContext.Users.Add(new User { Name = "User2", Age = 26, Email = "user2@gmail.com" });
                _dbContext.Users.Add(new User { Name = "User3", Age = 27, Email = "user3@gmail.com" });
                _dbContext.Users.Add(new User { Name = "User4", Age = 28, Email = "user4@gmail.com" });
                _dbContext.SaveChanges();

            }
        }

        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _dbContext.Users.AsNoTracking().Where(ur => ur.Id == id).Select(ur => new User { Name = ur.Name, Age = ur.Age, Email = ur.Email }).FirstOrDefault();
        }
    }
}