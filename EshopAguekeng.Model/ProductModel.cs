using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EshopAguekeng.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string Photo { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public DateTime CreatedAt { get; set; }
        public ProductModel()
        { 

        }


        public ProductModel(int id,string code, string name,string description,float price,
            string photo,int categoryId, int userId,DateTime created) : this()
        {
            Id = id;
            Code = code;
            Name = name;
            Description = description;
            Price = price;
            Photo = photo;
            CategoryId = categoryId;
            UserId = userId;
            CreatedAt = created;
        }

        public ProductModel(int id,string code, string name, string description, float price,
            string photo, int categoryId, CategoryModel category, int userId, UserModel user,DateTime created)
            :this(id,code,name,description,price,photo,categoryId,userId,created)
        {
            Category = category;
            User = user;
        }
    }
}
