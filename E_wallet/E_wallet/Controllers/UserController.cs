using E_wallet.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace E_wallet.Controllers
{
    public class UserController : Controller
    {
        private readonly IMiddleWare<User> userDao;
        public UserController(IMiddleWare<User> userD)
        {
            this.userDao = userD;
        }

        public IActionResult Index()
        {
            var allUser = userDao.GetAll("all");
            Console.WriteLine(allUser);
            return View(allUser);
        }

        [HttpGet][Route("/user/signup/form")]
        public IActionResult SignupForm(string er)
        {
            ViewBag.action = "/user/add/newly/create";
            ViewBag.read = "";
            return View();
        }
        [HttpGet][Route("/user/signup/form/edit")]
        public IActionResult SignupFormEdit(string er)
        {
            ViewBag.action = "/user/edit/old";
            ViewBag.read = "readOnly";
            ViewBag.user = UserDao.CurrUser;
            return View("SignupForm");
        }

        [HttpPost]
        [Route("/user/edit/old")]
        public IActionResult CreateEdit(string useOfApp, string name, string gender, string email, string password, string address, string zip, string phno)
        {
            User newUser = new User
            {
                Id = UserDao.CurrUser.Id,
                Use = useOfApp,
                Name = name,
                Gender = gender,
                Email = email,
                Password = password,
                Address = address,
                Zipcode = zip,
                Mobile = phno
            };
            userDao.Update(newUser);
            ViewBag.msg = "Details Edited";
            return View("Login");
        }

        [HttpPost][Route("/user/add/newly/create")]
        public IActionResult Create(string useOfApp, string name, string gender, string email, string password, string address, string zip, string phno)
        {
            User newUser = new User
            {
                Use = useOfApp,
                Name = name,
                Gender = gender,
                Email = email,
                Password = password,
                Address = address,
                Zipcode = zip,
                Mobile = phno
            };

            if (newUser.ValidateEmptyOrBasicError())
            {
                ViewBag.error = "Some Fields Are Empty!";
                return View("SignupForm");
            }

            if (!userDao.VerifyEmail(email, phno))
            {
                ViewBag.error = "Change Email or Mobile No, Already in Use";
                return View("SignupForm");
            }

            userDao.AddOne(newUser);
            ViewBag.msg = "User Registered";
            return View("Login");
        }

        [HttpPost][Route("/user/oauth/gateway/purify")]
        public IActionResult LoginPurify(string email, string password)
        {
            User verify = userDao.LoginWithEmailPassword(email, password);
            if (verify != null)
                return View("Home",verify);
            ViewBag.msg = "Error on Login";
            return View("Login");
        }

        [HttpGet][Route("/user/home")]
        public IActionResult Home()
        {
            return View("Home",UserDao.CurrUser);
        }

        [HttpGet][Route("/user/details")]
        public IActionResult Details()
        {
            return View("UserDetail", UserDao.CurrUser);
        }
    }
}