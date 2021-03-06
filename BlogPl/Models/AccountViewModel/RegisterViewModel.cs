﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogPL.Models.AccountViewModel
{
    public class RegisterViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Enter login")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        [Display(Name = "Email address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorrect Email")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Enter password")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("UserPassword", ErrorMessage = "The password and confirmation password do not match")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Field for captcha code can't be empty!")]
        [StringLength(4, ErrorMessage="Code length must be 4 charactes long", MinimumLength =4)]
        [Display(Name = "Enter the code from the image")]
        public string Captcha { get; set; }

        
        [Display(Name = "Enter the role")]
        
        public string Roles { get; set; }

    }
}