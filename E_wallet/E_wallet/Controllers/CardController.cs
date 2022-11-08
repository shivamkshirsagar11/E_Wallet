using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_wallet.Models;
namespace E_wallet.Controllers
{
    public class CardController : Controller
    {
        private readonly IMiddleWare<Card> cardDao;
        private readonly IMiddleWare<Wallet> walletDao;
        public CardController(IMiddleWare<Card> bank,IMiddleWare<Wallet> wallet)
        {
            this.cardDao = bank;
            this.walletDao = wallet;
        }
        [HttpGet][Route("/user/manage/cc/saved")]
        public IActionResult Index()
        {
            string uid = Convert.ToString(UserDao.CurrUser.Id);
            List<Card> AddedBanks = (List<Card>)cardDao.GetAllWithSpecialFeild(uid);
            ViewBag.action = "/user/cc/delete";
            ViewBag.buttonVal = "Delete";
            ViewBag.head = "Manage your Saved Card";
            return View("CardManager",AddedBanks);
        }
        [Route("/user/add/cc/new")][HttpGet]
        public IActionResult AddNew()
        {
            return View("AddCard");
        }
        [HttpPost][Route("/user/add/cc/process")]
       public IActionResult ProcessCard(string accNo,string exp, string cvv, string bankName, string ifsc, string holder)
        {
            Card addcard = new Card();
            addcard.CardNo = accNo;
            addcard.Expire = exp;
            addcard.Cvv = cvv;
            addcard.BankName = bankName;
            addcard.Ifsc = ifsc;
            addcard.Holdername = holder;
            addcard.UserI = UserDao.CurrUser.Id;
            cardDao.AddOne(addcard);
            return View("Home",UserDao.CurrUser);
        }
        [HttpPost][Route("/user/cc/delete")]
        public IActionResult DeleteCard(int cardId)
        {
            if (WalletDao.currWallet == null)
            {
                walletDao.GetOneWithId(UserDao.CurrUser.Id);
            }
            cardDao.DeleteWithId(cardId);
            if(WalletDao.currWallet.BankI == cardId)
            {
                Wallet temp = WalletDao.currWallet;
                temp.BankI = -1;
                temp.TakeANote = "Linked Card Removed";
                walletDao.Update(temp);
            }
            return View("Home", UserDao.CurrUser);
        }

        [HttpGet][Route("/user/link/wallet")]
        public IActionResult ToLink()
        {
            string uid = Convert.ToString(UserDao.CurrUser.Id);
            List<Card> AddedBanks = (List<Card>)cardDao.GetAllWithSpecialFeild(uid);
            ViewBag.action = "/user/cc/link";
            ViewBag.buttonVal = "Link Wallet with this Card";
            ViewBag.head = "Link Card to Wallet";
            if (WalletDao.currWallet == null)
            {
                walletDao.GetOneWithId(UserDao.CurrUser.Id);
            }
            return View("CardManager",AddedBanks);
        }
    }
}
