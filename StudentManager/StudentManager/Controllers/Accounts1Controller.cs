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
    [Route("api/Student")]
    [ApiController]
    public class Accounts1Controller : ControllerBase
    {
        private readonly StudentManagerContext _context;

        public Accounts1Controller(StudentManagerContext context)
        {
            _context = context;
        }

        // GET: api/Accounts1
        [HttpGet]
        public IEnumerable<Account> GetAccount()
        {
            return _context.Account;
        }

        // GET: api/Accounts1/5
        [HttpGet("information")]
        public async Task<IActionResult> GetAccount(string token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existToken = _context.MyCredentials.SingleOrDefault(Tk => Tk.AccessToken == token);
            if (existToken == null)
            {
                return new JsonResult("Ko co token");
            }
            else
            {
                var id = _context.Account.SingleOrDefault(i => i.ID == existToken.OwnId);
                if(id != null)
                {
                    return new JsonResult(id);
                }
            }
            
            return new JsonResult("a");

        }

        // PUT: api/Accounts1/5
        [HttpPut("update")]
        public async Task<IActionResult> PutAccount(int id,[FromBody] Account account)
        {
            var existAccount = _context.Account.Find(id);
            if(existAccount == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return NotFound();
            }
            existAccount.FullName = account.FullName;
            existAccount.Address = account.Address;
            existAccount.PhoneNumber = account.PhoneNumber;
            existAccount.Dob = account.Dob;
            existAccount.Gender = account.Gender;
            existAccount.Password = account.Password;
            _context.Account.Update(existAccount);
            _context.SaveChanges();
            return new JsonResult(existAccount);
        }

        // POST: api/Accounts1
        [HttpPost]
        public async Task<IActionResult> PostAccount([FromBody] Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Account.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { id = account.ID }, account);
        }

        // DELETE: api/Accounts1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Account.Remove(account);
            await _context.SaveChangesAsync();

            return Ok(account);
        }

        private bool AccountExists(int id)
        {
            return _context.Account.Any(e => e.ID == id);
        }
    }
}