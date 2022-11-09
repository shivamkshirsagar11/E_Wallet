using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_wallet.Models
{
    public class CardDao : IMiddleWare<Card>
    {
        private readonly AppDbContext context;
        public CardDao(AppDbContext context)
        {
            this.context = context;
        }
        
        public bool VerifyEmail(string email, string mobile)
        {
            throw new NotImplementedException();
        }

        public Card AddOne(Card addthis)
        {
            context.Card.Add(addthis);
            context.SaveChanges();
            return addthis;
        }

        public Card DeleteWithId(int id)
        {
            Card del = GetOneWithId(id);
            context.Card.Remove(del);
            context.SaveChanges();
            return del;
        }

        public IEnumerable<Card> GetAll(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Card> GetAllWithSpecialFeild(string specialId)
        {
            int uid = int.Parse(specialId);
            var q = (from m in context.Card select m).Where(s=>s.UserI.Equals(uid)).ToList();
            IEnumerable<Card> banksOfUser =  q;
            return banksOfUser;
        }

        public Card GetOneWithId(int id)
        {
            return context.Card.Find(id);
        }

        public Card LoginWithEmailPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Card Update(Card changed)
        {
           context.Card.Update(changed);
            context.SaveChanges();
            return changed;
        }
    }
}
