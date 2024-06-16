using System;

namespace HCA.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; }
        public decimal Price {  get; set; }

        //Constructor : set default value
        public Book()
        {
            Id = Guid.NewGuid();
        }
    }
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

}
