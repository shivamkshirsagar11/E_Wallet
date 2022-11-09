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
        private readonly IMiddleWare<Card> cardDao;
        private readonly IMiddleWare<Passbook> passbookDao;
        public WalletController(IMiddleWare<Wallet> walle,IMiddleWare<User>userDao,IMiddleWare<Card>cardDao, IMiddleWare<Passbook> p)
        {
            this.walletDao = walle;
            this.userDao = userDao;
            this.cardDao = cardDao;
            this.passbookDao = p;
        }

        [Route("/user/wallet/show")][HttpGet]
        public IActionResult Index()
        {
            if (WalletDao.currWallet == null)
            {
                walletDao.GetOneWithId(UserDao.CurrUser.Id);
            }
            Wallet walle = WalletDao.currWallet;
            ViewBag.title = "WalletHome - E_Wallet";
            ViewBag.name = "Currently Not Linked";
            ViewBag.no = "Not Available";
            if (walle.BankI != -1)
            {
                Card temp = cardDao.GetOneWithId(walle.BankI);
                ViewBag.name = temp.BankName;
                ViewBag.no = "**** **** **** "+temp.CardNo.Substring(temp.CardNo.Length - 4);
            }
            return View("WalletHome", walle);   
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

            Card card = cardDao.GetOneWithId(cardId);

            Passbook passbook = new Passbook
            {
                UserI = UserDao.CurrUser.Id,
                Action = "Important",
                Date = DateTime.UtcNow.ToString(),
                Message = "Wallet Linked to Card ***** " + card.CardNo[^4..] + ", " + bankName
            };
            passbookDao.AddOne(passbook);
            return View("Home", UserDao.CurrUser);
        }

        [HttpGet][Route("/user/send/money")]
        public IActionResult SendMoney()
        {
            if (WalletDao.currWallet == null)
            {
                walletDao.GetOneWithId(UserDao.CurrUser.Id);
            }
            if (WalletDao.currWallet.BankI == -1)
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
            userW.TakeANote = sendMoney+ "₹ Sent to " + UserDao.ToSenduser.Name;
            userW.LastOperated = DateTime.UtcNow.ToString();
            walletDao.Update(userW);
            //to send user
            Wallet toSendW = walletDao.GetOneWithId(UserDao.ToSenduser.Id);
            toSendW.Balance += sendMoney;
            toSendW.TakeANote = sendMoney + "₹ Recieved From " + UserDao.CurrUser.Name;
            walletDao.Update(toSendW);
            //user side
            Passbook passbook = new Passbook
            {
                UserI = UserDao.CurrUser.Id,
                Action = "Fair",
                Date = DateTime.UtcNow.ToString(),
                Message = sendMoney + "₹ Sent to " + UserDao.ToSenduser.Name + ", Current Balance " + userW.Balance
            };
            passbookDao.AddOne(passbook);
            //to send user side
            Passbook passbook2 = new Passbook
            {
                UserI = UserDao.ToSenduser.Id,
                Action = "Fair",
                Date = DateTime.UtcNow.ToString(),
                Message = sendMoney + "₹ Recieved from " + UserDao.CurrUser.Name + ", Current Balance " + toSendW.Balance
            };
            passbookDao.AddOne(passbook2);

            walletDao.GetOneWithId(UserDao.CurrUser.Id);
            return View("Home", UserDao.CurrUser);
        }

        [HttpGet][Route("/user/add/money/wallet")]
        public IActionResult ToAddMoney()
        {
            if (WalletDao.currWallet == null)
            {
                walletDao.GetOneWithId(UserDao.CurrUser.Id);
            }
            return View("AddMoney",WalletDao.currWallet);
        }

        [HttpPost][Route("/user/add/amount/to/wallet")]
        public IActionResult AddMoneyNow(double money)
        {
            if (WalletDao.currWallet == null)
            {
                walletDao.GetOneWithId(UserDao.CurrUser.Id);
            }
            Wallet w = WalletDao.currWallet;
            w.Balance += money;
            w.TakeANote = money+ "₹ Added To Wallet";
            w.LastOperated = DateTime.UtcNow.ToString();
            walletDao.Update(w);

            Passbook passbook = new Passbook();
            passbook.UserI = UserDao.CurrUser.Id;
            passbook.Action = "Fair";
            passbook.Date = DateTime.UtcNow.ToString();
            passbook.Message = money + "₹ Added to Wallet , Current Balance " + w.Balance;
            passbookDao.AddOne(passbook);

            return View("Home",UserDao.CurrUser);
        }
    }
}
