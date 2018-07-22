using BlogSpace.Infrastructure.Attributes;
using BlogSpace.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace BlogSpace.ViewModels
{
    public class PublishBlogViewModel
    {
        public PublishBlogViewModel()
        {
            this.Tags = new List<string>();
        }

        [MinLength(1)]
        [Display(Name = "Blog Title")]
        [Required(AllowEmptyStrings = false)]
        public string BlogTitle { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [AllowExtentions("jpg,jpeg,png,gif")]
        public HttpPostedFileBase BlogImage { get; set; }

        [Display(Name = "Short Description")]
        [Required(AllowEmptyStrings = false)]
        [StringLength(130, MinimumLength = 5)]
        public string ShortDescription { get; set; }

        [AllowHtml]
        [MinLength(1)]
        [Display(Name = "Blog Conntent")]
        [DataType(DataType.MultilineText)]
        [Required(AllowEmptyStrings = false)]
        public string BlogContent { get; set; }

        [Required]
        public List<string> Tags { set; get; }
    }

    public class ReadBlogViewModel
    {
        public int BlogId { get; set; }

        public string Title { get; set; }

        public string BlogContent { get; set; }

        public string PageFriendlyUrlTitle { get; set; }

        public string ImageName { get; set; }

        public int NoOfViews { get; set; }

        public DateTime PublishedOn { get; set; }

        public ICollection<BlogTag> Tags { get; set; }
    }
}