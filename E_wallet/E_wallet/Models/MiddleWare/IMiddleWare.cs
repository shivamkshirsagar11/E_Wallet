using System;
using System.Collections.Generic;

namespace E_wallet.Models
{
    public interface IMiddleWare<T>
    {
        T GetOneWithId(int id);

        IEnumerable<T> GetAll(String id);

        IEnumerable<T> GetAllWithSpecialFeild(string specialId);

        T Update(T changed);

        T DeleteWithId(int id);

        T AddOne(T addthis);

        T LoginWithEmailPassword(string email, string password);

        bool VerifyEmail(string email, string mobile);
    }
}