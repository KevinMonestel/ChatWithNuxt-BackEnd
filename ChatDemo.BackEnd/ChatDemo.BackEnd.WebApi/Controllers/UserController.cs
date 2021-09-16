using ChatDemo.BackEnd.WebApi.Hubs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatDemo.BackEnd.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Validate(string username)
        {
            try
            {
                var connections = ChatHub._connections.GetConnections();
                var result = false;

                var validate = connections.Where(x => x.Key.Equals(username)).FirstOrDefault();

                if (validate is null) result = true;

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }       
        }
    }
}
