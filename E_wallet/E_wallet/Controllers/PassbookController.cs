using E_wallet.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_wallet.Controllers
{
    public class PassbookController : Controller
    {
        public readonly IMiddleWare<Passbook> passbookDao;
        public PassbookController(IMiddleWare<Passbook> p)
        {
            this.passbookDao = p;
        }
        [HttpGet][Route("/user/wallet/history/passbook")]
        public IActionResult Index()
        {
            List<Passbook> history = (List<Passbook>)passbookDao.GetAllWithSpecialFeild(Convert.ToString(UserDao.CurrUser.Id));
            history.Reverse();
            return View("History",history);
        }
    }
}
