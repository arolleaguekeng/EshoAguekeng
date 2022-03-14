using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAguekeng.Repository
{
    public partial class User
    {


        public User(int id, string username, string fullname, string role, string password)
            :this()
        {
            Id = id;
            Username = username;
            Fullname = fullname;
            Role = role;
            Password = password;
        }
    }


    public partial class Category
    {
        public Category()
        {
        }

        public Category(int id, string name, int userId)
            : this()
        {
            Id = id;
            Name = name;
            UserId = userId;

        }

        public Category(int id, string name, int userId, User user) 
            : this(id,name,userId)
        {
            User = user;

        }
    }
}
