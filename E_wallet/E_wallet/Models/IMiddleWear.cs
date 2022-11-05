using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_wallet.Models
{
    interface  IMiddleWear <T>
    {
        T GetOneWithId(int id);
        IEnumerable<T> GetAll(int id);
        IEnumerable<T> GetAllWithSpecialFeild(string specialId);
        T Update(T changed);
        T DeleteWithId(int id);
        T AddOne(T addthis);
    }
}
