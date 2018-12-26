using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManager.Models;

namespace StudentManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyCredentialsController : ControllerBase
    {
        private readonly StudentManagerContext _context;

        public MyCredentialsController(StudentManagerContext context)
        {
            _context = context;
        }

        // GET: api/MyCredentials
        [HttpGet]
        public IEnumerable<MyCredential> GetMyCredentials()
        {
            return _context.MyCredentials;
        }

        // GET: api/MyCredentials/5
        [HttpGet("check-token")]
        public async Task<IActionResult> GetMyCredential(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var myCrendentials = await _context.MyCredentials.FindAsync(id);

            if (myCrendentials == null)
            {
                return NotFound();
            }

            return Ok(myCrendentials);
        }
    }
}