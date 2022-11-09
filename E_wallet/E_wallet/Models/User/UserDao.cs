using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace E_wallet.Models
{
    public class UserDao : IMiddleWare<User>
    {
        private readonly AppDbContext context;
        static public User CurrUser { get; set; }
        static public User ToSenduser { get; set; }
        public UserDao(AppDbContext context)
        {
            this.context = context;
            
        }

        public User AddOne(User addthis)
        {
            context.Users.Add(addthis);
            context.SaveChanges();
            return addthis;
        }

        public User DeleteWithId(int id)
        {
            User user = context.Users.Find(id);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
            return user;
        }

        public IEnumerable<User> GetAll(String id)
        {
            return context.Users;
        }

        public IEnumerable<User> GetAllWithSpecialFeild(string specialId)
        {
            var q = from m in context.Users select m;
            q = q.Where(s => s.Mobile.Contains(specialId) || s.Name.Contains(specialId)).Where(s=> s.Id != UserDao.CurrUser.Id);
            IEnumerable<User> users = q.ToList();
            return users;
        }

        public User LoginWithEmailPassword(string email, string password)
        {
            var q = (from m in context.Users select m).Where(s => s.Email.Equals(email) && s.Password.Equals(password));
            User user = (User)q.FirstOrDefault();
            CurrUser = user;
            return user;
        }

        public User GetOneWithId(int id)
        {
            return context.Users.FirstOrDefault(m => m.Id == id);
        }

        public User Update(User changed)
        {
            context.Users.Update(changed);
            context.SaveChanges();
            return changed;
        }
        public bool VerifyEmail(string email,string mobile)
        {
            var q = (from m in context.Users select m).Where(s=>s.Email.Equals(email) || s.Mobile.Equals(mobile)).Count<User>();
            if (q > 0) return false;
            return true;
        }
    }
}