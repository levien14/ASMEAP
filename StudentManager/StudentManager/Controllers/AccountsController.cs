using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManager.Models;

namespace StudentManager.Controllers
{
    public class AccountsController : Controller
    {
        
        private readonly StudentManagerContext _context;

        public AccountsController(StudentManagerContext context)
        {
            _context = context;
        }
        public bool checkSession()
        {
            var check = HttpContext.Session.GetString("EmailLogin");
            if(check == null)
            {
                return false;
            }
            return true;
        }
        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            if (!this.checkSession())
            {
                return Redirect("https://studentmanager20181225060217.azurewebsites.net/login/authentication");
            }
            return View(await _context.Account
                .Include(cl => cl.ClassRoom)
                .ToListAsync());
        }
        //public async Task<IActionResult> AddMark(int id, Mark mark)
        //{
        //    var account = await _context.Account
        //                      .Include(a => a.ClassRoom)
        //                      .Include(m => m.Marks)
        //                      .ThenInclude(s => s.Subject)
        //                      .FirstOrDefaultAsync(m => m.ID == id);
        //    if(account == null)
        //    {
        //        return NotFound();
        //    }
        //    List<Subject> ListSubject = _context.Subjects.ToList();
        //    return
        //}
        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!this.checkSession())
            {
                return Redirect("https://studentmanager20181225060217.azurewebsites.net/login/authentication");
            }
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .Include(a => a.ClassRoom)
                .Include(m => m.Marks)
                .ThenInclude(s => s.Subject)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            if (!this.checkSession())
            {
                return Redirect("https://studentmanager20181225060217.azurewebsites.net/login/authentication");
            }
            //    var checkLogin = HttpContext.Session.GetString("EmailLogin");
            //    if(checkLogin == null)
            //    {
            //        return Redirect("");
            //    }
            ViewData["ClassRoom"] = _context.ClassRooms.ToList();
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Email,Password,ConfirmPassword,FullName,Dob,Gender,PhoneNumber,Address,Description,ClassRoomId")] Account account)
        {
            
            if (ModelState.IsValid)
            {
                
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!this.checkSession())
            {
                return Redirect("https://studentmanager20181225060217.azurewebsites.net/login/authentication");
            }
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Email,Password,FullName,Dob,Gender,PhoneNumber,Address,Description,CreatedAt,UpdatedAt,Status")] Account account)
        {
            if (id != account.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!this.checkSession())
            {
                return Redirect("https://studentmanager20181225060217.azurewebsites.net/login/authentication");
            }
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .FirstOrDefaultAsync(m => m.ID == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Account.FindAsync(id);
            _context.Account.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Account.Any(e => e.ID == id);
        }
    }
}
