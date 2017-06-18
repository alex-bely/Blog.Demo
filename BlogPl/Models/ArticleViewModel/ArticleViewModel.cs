using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BlogPL.Models.AccountViewModel;


namespace BlogPL.Models.ArticleViewModel
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Title:")]
        [MaxLength(400, ErrorMessage = "Too long title")]
        [Required(ErrorMessage = "Enter title")]
        public string Title { get; set; }
        [Display(Name = "Text:")]
        [Required(ErrorMessage = "Enter some text")]
        public string Content { get; set; }
        public DateTime PublicationDate { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public RegisterViewModel User { get; set; }
        [Display(Name = "Tags:")]
        public string Tags { get; set; }
        public IEnumerable<string> Comments { get; set; }
    }
}