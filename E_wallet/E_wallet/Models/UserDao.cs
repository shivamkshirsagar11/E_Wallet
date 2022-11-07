using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace E_wallet.Models
{
    public class UserDao : IMiddleWare<User>
    {
        private readonly AppDbContext context;

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
            q = q.Where(s => s.Mobile.Contains(specialId));
            IEnumerable<User> users = (IEnumerable<User>)q.ToListAsync();
            return users;
        }

        public User LoginWithEmailPassword(string email, string password)
        {
            var q = from m in context.Users select m;
            q = q.Where(s => s.Email.Contains(email) && s.Password.Contains(password));
            User user = (User)q.FirstOrDefault();
            return user;
        }

        public User GetOneWithId(int id)
        {
            return context.Users.FirstOrDefault(m => m.Id == id);
        }

        public User Update(User changed)
        {
            var user = context.Users.Attach(changed);
            user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return changed;
        }
    }
}