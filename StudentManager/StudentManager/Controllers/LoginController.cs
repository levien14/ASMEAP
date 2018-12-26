using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManager.Models;

namespace StudentManager.Controllers
{
    public class LoginController : Controller
    {
        private readonly StudentManagerContext _context;
        public LoginController(StudentManagerContext context)
        {
            _context = context;
        }
        public IActionResult Authentication(string redirectUrl)
        {
            string login = HttpContext.Session.GetString("EmailLogin");
            if(login != null)
            {
                return Redirect("/Home");
            }
            ViewData["redirectUrl"] = redirectUrl;
            return View(redirectUrl);
        }
        public IActionResult PostLogin(LoginInformation loginInformation, string redirectUrl)
        {
            var existLogin = _context.Account.SingleOrDefault(lg => lg.Email == loginInformation.Email);
            if(existLogin != null)
            {
                if(loginInformation.PassWord == existLogin.Password)
                {
                    HttpContext.Session.SetString("EmailLogin", existLogin.Email);
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    if(redirectUrl != null)
                    {
                        Redirect(redirectUrl);
                    }
                    return Redirect("/Home");
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return Json("Forbidden");
                }
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;

                return Json("Forbidden");
            }
            
           
        }
    }
}