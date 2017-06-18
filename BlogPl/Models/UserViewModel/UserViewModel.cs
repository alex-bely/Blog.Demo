using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogPL.Models.UserViewModel
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        [Display(Name = "Email address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorrect Email")]
        public string UserEmail { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int ArticlesCount { get; set; }
        public string Roles { get; set; }
        //public byte[] Avatar { get; set; }
    }
}