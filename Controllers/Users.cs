using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimePunchClock.Models;
using TimePunchClock.Data;

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
            string Password = UserReq.Password.Trim();
            string PasswordConfirm = UserReq.PasswordConfirm.Trim();
            PasswordMatchValidation(Password, PasswordConfirm);
            Password = Salting(Password);
            if (ModelState.IsValid)
            {
                var User = new User()
                {
                    Id = Guid.NewGuid(),
                    Email = UserReq.Email,
                    Password = Password,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                };
                UserContext.User.Add(User);
                UserContext.SaveChanges();
                TempData["Notice"] = "User successfully created.";
                return RedirectToAction("Index", "Home");
            };
            TempData["Alert"] = "Something got wrong.";
            return View("Register");
        }

        [HttpPost]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpDelete]
        public IActionResult LogOut()
        {
            return View();
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

        public string Salting(string orgPassword)
        {
            string saltingPassword;
            return SaltingPassword;
        }
    }
}

