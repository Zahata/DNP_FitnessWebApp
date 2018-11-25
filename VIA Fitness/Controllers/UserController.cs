using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VIA_Fitness.Models;


namespace VIA_Fitness.Controllers
{
    public class UserController : Controller
    {
        // GET: Registration
        [HttpGet]
        public ActionResult Registration()
        {
            User user = new User();
            return View(user);
        }

        // POST : Registration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(User user)
        {
            using(FitnessEntities db = new FitnessEntities())
            {
                db.Users.Add(user);
                db.SaveChanges();
              
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration successful";
            
            return View(user);
            }

         // GET: Login
         [HttpGet]
         public ActionResult Login()
        {
            return View();
        }
        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login, string ReturnUrl)
        {
            string message = "";
            using(FitnessEntities db = new FitnessEntities())
            {
                var v = db.Users.Where(a => a.username == login.username).FirstOrDefault();
                if(v != null)
                {
                    if (string.Compare(login.password,v.password) == 0)
                    {
                        int timeout = login.Remember ? 52600 : 20;
                        var ticket = new FormsAuthenticationTicket(login.username, login.Remember, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "User");
                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    message = "Login failed. Please try again";
                }
            }
            ViewBag.Message = message;
            return View();
        }
        // GET: UserPage
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        //Logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }
    }
}