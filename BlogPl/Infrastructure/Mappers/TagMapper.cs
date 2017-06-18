using BLL.Interfacies.Entities;
using BlogPL.Models.TagViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogPL.Infrastructure.Mappers
{
    public static class TagMapper
    {
        public static TagEntity ToBllTag(this TagViewModel mvcTag)
        {
            if (mvcTag == null)
                throw new ArgumentNullException(nameof(mvcTag));

            return new TagEntity
            {
                Name = mvcTag.Name,
            };
        }

        public static TagViewModel ToMvcTag(this TagEntity bllTag)
        {
            if (bllTag == null)
                return null;

            return new TagViewModel
            {
                Id = bllTag.Id,
                Name = bllTag.Name
            };
        }
    }
}