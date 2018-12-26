using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManager.Models;

namespace StudentManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationApiController : ControllerBase
    {
        private readonly StudentManagerContext _context;

        public AuthenticationApiController(StudentManagerContext context)
        {
            _context = context;
        }
        
        // POST: api/AuthenticationApi
        [HttpPost]
        public async Task<IActionResult> PostLoginInformation([FromBody] LoginInformation loginInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existLogin = _context.Account.SingleOrDefault(lg => lg.Email == loginInformation.Email);
            if(existLogin != null)
            {
                if(loginInformation.PassWord == existLogin.Password)
                {
                    MyCredential myCredential = new MyCredential(existLogin.ID);
                    _context.MyCredentials.Add(myCredential);
                    _context.SaveChanges();
                    return new JsonResult(myCredential);
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return new JsonResult("Forbidden");
                }
            }
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return new JsonResult("Forbidden");
        }
        
    }
}