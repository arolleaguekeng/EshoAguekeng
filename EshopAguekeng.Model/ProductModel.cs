using EshopAguekeng.Model;
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
        private string priceToString;
        public string PriceToString 
        {
            get => Price.ToString("NO") + "FCFA" ;
            set => priceToString = value; 
        }
        public string Photo { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
        public int UserId { get; set; }

        public UserModel User { get; set; }
        public DateTime CreatedAt { get; set; }
        private string createdAtTostring;
        public string CreatedAtTostring
        {
            get => CreatedAt.ToRelativeFormat() ;
            set => createdAtTostring = value; 
        }

        public double Like { get; set; }

        private string likeToString;
        public string LikeToString
        {
            get => Like.ToRelativeFormat();
            set => likeToString = value;
        }
        public double Hangry { get; set; }


        private string hangryToString;
        public string HangryToString
        {
            get => Hangry.ToRelativeFormat();
            set => hangryToString = value;
        }
        public double Shared { get; set; }
        private string sharedToString;
        public string SharedToString
        {
            get => Shared.ToRelativeFormat();
            set => sharedToString = value;
        }
        public double Buy { get; set; }
        private string buyToString;
        public string BuyToString
        {
            get => Buy.ToRelativeFormat();
            set => buyToString = value;
        }

        public double Comment { get; set; }
        private string commentToString;
        public string CommentToString
        {
            get => Comment.ToRelativeFormat();
            set => commentToString = value;
        }


        public ProductModel()
        {
            var rand = new Random();
            Like = rand.Next(0, int.MaxValue);
            Hangry = rand.Next(0, 10);
            Shared = rand.Next(0, int.MaxValue);
            Shared = rand.Next(0, 20000);
            Buy = rand.Next(0, 10);
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
