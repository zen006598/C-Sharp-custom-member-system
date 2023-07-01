using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimePunchClock.Models;
using TimePunchClock.Data;
using TimePunchClock.ControllersHelper;

namespace TimePunchClock.Controllers
{
    public class Users : Controller
    {
        private readonly UserContext UserContext;
        public Users(UserContext user)
        {
            this.UserContext = user;
        }
         
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(CreateUser UserReq)
        {
            //Declare
            string Email = UserReq.Email.Trim();
            string Password = UserReq.Password.Trim();
            string PasswordConfirm = UserReq.PasswordConfirm.Trim();
            byte[] salt;
            //validation
            PasswordMatchValidation(Password, PasswordConfirm);
            IsRepeat(Email);
            //salting
            string saltedPassword = PasswordHelper.HashPassword(Password, out salt);
            //save
            if (ModelState.IsValid)
            {
                var User = new User()
                {
                    Id = Guid.NewGuid(),
                    Email = Email,
                    Password = saltedPassword,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Salt = Convert.ToBase64String(salt),
                };
                UserContext.User.Add(User);
                UserContext.SaveChanges();
                TempData["Notice"] = "User successfully created.";
                return RedirectToAction("Index", "Home");
            };
            TempData["Alert"] = "Something got wrong.";
            return View("Register");
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(CreateSession SessionReq)
        {
            string Email = SessionReq.Email;
            string Password = SessionReq.Password;
            //Look for User
            var User = UserContext.User.FirstOrDefault(user => user.Email == Email);
            //Is exist?
            if (User == null)
            {
                ModelState.AddModelError("Password", "Email or password is wrong.");
                return View("LogIn");
            }

            string UserPassword = User.Password;
            byte[] Salt = Convert.FromBase64String(User.Salt);
            //Compare the HashPassword
            if (!PasswordHelper.VerifyPassword(Password, UserPassword, Salt))
            {
                ModelState.AddModelError("Password", "Password is wrong.");
                return View("LogIn");
            }
            //successfully Log in
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("UserEmail", Email);
                TempData["Notice"] = $"Welcome {Email}";
                 return RedirectToAction("Index", "Home");
            }
            TempData["Alert"] = "Something wrong.";

            return View("LogIn");
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserEmail");
            return RedirectToAction("Index", "Home");
        }

        public bool PasswordMatchValidation(string password, string passwordConfirm)
        {
            if (password != passwordConfirm)
            {
                ModelState.AddModelError("PasswordConfirm", "Passwords do not match");
                return false;
            };
            return true;
        }

        private bool IsRepeat(string email)
        {
            var user = UserContext.User.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                ModelState.AddModelError("Email", "Email already exists.");
                return true;
            }
            return false;
        }
    }
}

