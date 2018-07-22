using System;
using System.Collections.Generic;

namespace BlogSpace.Models
{
    public class Blog
    {
        public int BlogId { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string BlogContent { get; set; }

        public string PageFriendlyUrlTitle { get; set; }

        public string ImageName { get; set; }

        public int NoOfViews { get; set; }

        public DateTime PublishedOn { get; set; }

        public virtual ICollection<BlogTag> Tags { get; set; }
    }
}