using E_wallet.Models;
using Microsoft.AspNetCore.Mvc;
using System;

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
            return View();
        }
        [HttpPost][Route("/user/add/newly/create")]
        public IActionResult Create(string useOfApp, string name, string gender, string email, string password, string address, string zip, string phno)
        {
            User newUser = new User();

            newUser.Use = useOfApp;
            newUser.Name = name;
            newUser.Gender = gender;
            newUser.Email = email;
            newUser.Password = password;
            newUser.Address = address;
            newUser.Zipcode = zip;
            newUser.Mobile = phno;

            if (newUser.ValidateEmptyOrBasicError())
            {
                ViewBag.error = "Some Fields Are Empty!";
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
    }
}