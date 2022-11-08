using E_wallet.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace E_wallet.Controllers
{
    public class WalletController : Controller
    {
        private readonly IMiddleWare<Wallet> walletDao;

        public WalletController(IMiddleWare<Wallet> walle)
        {
            this.walletDao = walle;
        }

        [Route("/user/wallet/show")][HttpGet]
        public IActionResult Index()
        {
            Wallet walle = walletDao.GetOneWithId(UserDao.CurrUser.Id);
            ViewBag.title = "WalletHome - E_Wallet";
            //ViewBag.id = UserDao.CurrUser.Id;
            return View("WalletHome", walle);
            //return View("WalletHome", new Wallet());
        }

        [HttpGet][Route("/user/manage/bank/saved")]
        public IActionResult ManageBankHome()
        {
            return View("ManageBankHome");
        }
    }
}
