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
        private readonly IMiddleWare<User> userDao;
        public WalletController(IMiddleWare<Wallet> walle,IMiddleWare<User>userDao)
        {
            this.walletDao = walle;
            this.userDao = userDao;
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

        [HttpPost]
        [Route("/user/cc/link")]
        public IActionResult LinkWallet(int cardId, string bankName)
        {
            if (WalletDao.currWallet == null)
            {
                walletDao.GetOneWithId(UserDao.CurrUser.Id);
            }
            Wallet curr = WalletDao.currWallet;
            curr.BankI = cardId;
            curr.TakeANote = "Link to Wallet change to "+bankName;
            curr.LastOperated = DateTime.UtcNow.ToString();
            walletDao.Update(curr);
            return View("Home", UserDao.CurrUser);
        }

        [HttpGet][Route("/user/send/money")]
        public IActionResult SendMoney()
        {
            if(WalletDao.currWallet == null)
            {
                walletDao.GetOneWithId(UserDao.CurrUser.Id);
            }
            if(WalletDao.currWallet.BankI == -1)
            {
                ViewBag.error = true;
                ViewBag.errorMsg = "Please Link Wallet Firrst before Sending Money";
            }
            return View("SendMoney",WalletDao.currWallet);
        }

        [HttpPost]
        [Route("/user/search/mob")]
        public IActionResult SearchMobile(string mobile)
        {
            if (WalletDao.currWallet == null)
            {
                walletDao.GetOneWithId(UserDao.CurrUser.Id);
            }
            List<User> users = (List<User>)userDao.GetAllWithSpecialFeild(mobile);
            ViewBag.error = false;
            ViewBag.users = users;
            return View("SendMoney", WalletDao.currWallet);
        }

        [HttpPost][Route("/user/wallet/send/money/to/mobile")]
        public IActionResult ProcessSendMoney(int uid)
        {
            if (WalletDao.currWallet == null)
            {
                walletDao.GetOneWithId(UserDao.CurrUser.Id);
            }
            User u = userDao.GetOneWithId(uid);
            UserDao.ToSenduser = u;
            ViewBag.maxAmount = WalletDao.currWallet.Balance;
            return View("ProcessSend",u);
        }

        [HttpPost][Route("/intermidiate/send/to/user")]
        public IActionResult SendNow(double sendMoney)
        {
            if (WalletDao.currWallet == null)
            {
                walletDao.GetOneWithId(UserDao.CurrUser.Id);
            }
            //user side
            Wallet userW = WalletDao.currWallet;
            userW.Balance -= sendMoney;
            userW.TakeANote = "Money Sent to "+UserDao.ToSenduser.Name;
            userW.LastOperated = DateTime.UtcNow.ToString();
            walletDao.Update(userW);
            //to send user
            Wallet toSendW = walletDao.GetOneWithId(UserDao.ToSenduser.Id);
            toSendW.Balance += sendMoney;
            toSendW.TakeANote = "Money Recieved From "+UserDao.CurrUser.Name;
            walletDao.Update(toSendW);

            return View("Home", UserDao.CurrUser);
        }
    }
}
