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
        public CardController(IMiddleWare<Card> bank)
        {
            this.cardDao = bank;
        }
        [HttpGet][Route("/user/manage/cc/saved")]
        public IActionResult Index()
        {
            string uid = Convert.ToString(UserDao.CurrUser.Id);
            List<Card> AddedBanks = (List<Card>)cardDao.GetAllWithSpecialFeild(uid);
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
    }
}
