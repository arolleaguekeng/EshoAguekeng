using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EshoAguekeng.Repository
{
    public class CategoryRepository
    {
        private readonly EshopAguekengEntities db;
        public CategoryRepository()
        {
            db = new EshopAguekengEntities();
        }
        public Category Get(int id)
        {
            return db.Categories.FirstOrDefault(x => x.Id == id);
        }
        public Category Get(string name)
        {
            return db.Categories.FirstOrDefault(x => x.Name == name);
        }
        public Category Get(string username,string password)
        {
            var user = Get(username);
            if (user?.Password == CreateMD5Hash(password))
                return user;
            return null;
        }

        public Category Add(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            var u = Get(user.Username);
            if (u != null)
                throw new DuplicateWaitObjectException($"Username{user.Username} already exist ! ");
            user.Password = CreateMD5Hash(user.Password);
            db.Categories.Add(user);
            db.SaveChanges();
            return user;
        }
        public Category Set(User user)
        {
            if (user == null)
                throw new KeyNotFoundException(nameof(user));
            var oldUser = new EshopAguekengEntities().Categories.Find(user.Id);
            if (oldUser != null)
                throw new DuplicateWaitObjectException($"User not exist ! ");

             var u = Get(user.Username);
            if (u != null && u.Id != oldUser.Id)
                throw new DuplicateWaitObjectException($"Username{user.Username} already exist ! ");

            user.Password = oldUser.Password != user.Password ? CreateMD5Hash(user.Password) : oldUser.Password;
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return user;
        }


        public string CreateMD5Hash(string input)
        {
            // Step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
