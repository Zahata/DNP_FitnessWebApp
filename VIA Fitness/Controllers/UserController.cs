﻿using System;
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
            using(VIAFitnessEntities1 db = new VIAFitnessEntities1())
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
            using(VIAFitnessEntities db = new VIAFitnessEntities())
            {
                var v = db.Users.Where(a => a.username == login.username).FirstOrDefault();

                string user = login.username;
                TempData["username"] = user;

                if (v != null)
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
            ViewBag.ShowTopBar = true;
            return View();
        }
        // GET: Workout page
        [HttpGet]
        public ActionResult getWorkout()
        {
            ViewBag.ShowTopBar = true;
            return View();
        }

        // POST : Create workout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult createWorkout(Workout workout)
        {
            try {
                using (VIAFitnessEntities db = new VIAFitnessEntities())
                {
                    string user = Convert.ToString(TempData["username"]);

                    Workout newWorkout = new Workout();
                    newWorkout.type = workout.type;
                    newWorkout.duration = workout.duration;
                    newWorkout.caloriesBurned = workout.caloriesBurned;
                    newWorkout.date = DateTime.Today;
                    newWorkout.username = user;

                    db.Workouts.Add(newWorkout);
                    db.SaveChanges();

                    int latestWorkoutID = newWorkout.workoutID;
                }
            }
            catch(Exception ex) { throw ex; }
            return RedirectToAction("getWorkout");
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