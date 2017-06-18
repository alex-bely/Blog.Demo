using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVcPL.Models
{
    public class PageInfo
    {
        /// <summary>
        /// The number of the current page
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Amount elements on page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Amount elements
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Total amount of pages
        /// </summary>
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }
}