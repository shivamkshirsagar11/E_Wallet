using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_wallet.Models
{
    public class WalletDao : IMiddleWare<Wallet>
    {
        private readonly AppDbContext context;
        static public Wallet currWallet { get; set; }
        public WalletDao(AppDbContext context)
        {
            this.context = context;
        }

        public Wallet AddOne(Wallet addthis)
        {
            context.Wallet.Add(addthis);
            context.SaveChanges();
            return addthis;
        }

        public Wallet GetOneWithId(int id)
        {
            Wallet walle = new Wallet();
            var q = (from m in context.Wallet select m).Where(s=>s.UserI.Equals(id));
            if (q.Count<Wallet>() > 0)
                walle = (Wallet)q.SingleOrDefault();
            else
            {
                walle.UserI = id;
                AddOne(walle);
            }
            currWallet = walle;
            return walle;
        }

        public Wallet Update(Wallet changed)
        {
            context.Wallet.Update(changed);
            context.SaveChanges();
            currWallet = changed;
            return changed;
        }

        public Wallet LoginWithEmailPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Wallet> GetAll(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Wallet> GetAllWithSpecialFeild(string specialId)
        {
            throw new NotImplementedException();
        }

        public Wallet DeleteWithId(int id)
        {
            throw new NotImplementedException();
        }

    }
}
