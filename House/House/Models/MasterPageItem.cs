using House.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Android.Provider;
using House.Helpers;

namespace House.Models
{
    public class MasterPageItem
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string IconSource { get; set; }      
    }

    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string IconSource { get; set; }
    }

    public class Product
    {
        public int ProductID { get; set; }
        public string Title { get; set; }
        public string IconSource { get; set; }
        public string Type { get; set; }
    }
    public class CategoryList
    {
        public List<Category> Categories { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ProductList
    {
        public List<Product> Productes { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ProductDataRequest
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }

    public class User
    {
        public int Id { get; set; }

        //[Required]
        public string Name { get; set; }

        //[Required]
        //[Email]
        public string Email { get; set; }

        //[Required]
        public string Mobile { get; set; }
        public string UserDP { get; set; }

        //[Required]
        public string Password { get; set; }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public User User { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class SaveResponse
    {
        public int ID { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
