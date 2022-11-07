using E_wallet.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        [HttpGet]
        public IActionResult Create(User user)
        {
            if(user != null && ModelState.IsValid)
            {
                User newUser = userDao.AddOne(user);
                return RedirectToAction("New added", new { id = user.Id });
            }
            return View();
        }
    }
}
