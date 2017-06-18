using MVcPL.Models.AccountViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVcPL.Models.CommentViewModel
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter comment")]
        [Display(Name = "Comment:")]
        public string Text { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }

        public int UserId { get; set; }

        [Required]
        public int ArticleId { get; set; }

        public RegisterViewModel User { get; set; }
    }
}