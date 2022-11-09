using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_wallet.Models
{
    public class PassbookDao : IMiddleWare<Passbook>
    {
        public readonly AppDbContext context;
        public PassbookDao(AppDbContext c)
        {
            this.context = c;
        }

        public Passbook AddOne(Passbook addthis)
        {
            context.Passbook.Add(addthis);
            context.SaveChanges();
            return addthis;
        }

        public Passbook DeleteWithId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Passbook> GetAll(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Passbook> GetAllWithSpecialFeild(string specialId)
        {
            return (from m in context.Passbook select m).Where(s=> s.UserI.Equals(int.Parse(specialId))).ToList();
            
        }

        public Passbook GetOneWithId(int id)
        {
            throw new NotImplementedException();
        }

        public Passbook LoginWithEmailPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Passbook Update(Passbook changed)
        {
            throw new NotImplementedException();
        }
    }
}
