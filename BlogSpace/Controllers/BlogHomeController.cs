using BlogSpace.Models;
using BlogSpace.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Z.EntityFramework.Plus;

namespace BlogSpace.Controllers
{
    [RoutePrefix("blog")]
    public class BlogHomeController : BlogSpaceBaseController
    {
        [Route(Name = "blogHome")]
        public async Task<ActionResult> Index(int pageNo = 0)
        {
            var noOfBlogsToShow = int.Parse(ConfigurationManager.AppSettings["NoOfBlogsToShow"].ToString());

            var blogsToDisplay = await _db.Blogs
                .OrderBy(o => o.BlogId)
                .Skip(pageNo * noOfBlogsToShow)
                .Take(noOfBlogsToShow)
                .ToListAsync();

            var selectedblogsIds = blogsToDisplay.Select(s => s.BlogId);

            ViewBag.top10Blogs = await GetPopularBlogs(selectedblogsIds);
            ViewBag.pageNo = pageNo;

            return View(blogsToDisplay);
        }

        [Route("{blogUrlFriendlyName}", Name = "ViewBlogDetails")]
        public async Task<ActionResult> ViewBlogDetails(string blogUrlFriendlyName)
        {
            if (string.IsNullOrEmpty(blogUrlFriendlyName))
            {
                return RedirectToRoute("blogHome");
            }

            var model = await _db.Blogs
                .Select(s => new ReadBlogViewModel
                {
                    BlogId = s.BlogId,
                    BlogContent = s.BlogContent,
                    PageFriendlyUrlTitle = s.PageFriendlyUrlTitle,
                    ImageName = s.ImageName,
                    NoOfViews = s.NoOfViews,
                    PublishedOn = s.PublishedOn,
                    Title = s.Title,
                    Tags = s.Tags
                })
                .FirstOrDefaultAsync(b => b.PageFriendlyUrlTitle == blogUrlFriendlyName);

            if (model != null)
            {
                await _db.Blogs
                    .Where(b => b.BlogId == model.BlogId)
                    .UpdateAsync(b => new Blog { NoOfViews = b.NoOfViews + 1 });

                ViewBag.top10Blogs = await GetPopularBlogs(new int[] { model.BlogId });
            }
            else
            {
                return RedirectToRoute("blogHome");
            }

            return View(model);
        }

        [HttpGet]
        [Route("publish-blog/{authToken}")]
        public ActionResult PublishBlog(string authToken)
        {
            if(string.IsNullOrEmpty(authToken) || authToken != ConfigurationManager.AppSettings["authToken"])
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            return View(new PublishBlogViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("save-and-publish-blog", Name = "SaveBlog")]
        public async Task<ActionResult> PublishBlog(PublishBlogViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pageFriendlyTitle = model.BlogTitle.Replace(' ', '-').ToLower().Trim();
                if (await _db.Blogs.AnyAsync(b => b.PageFriendlyUrlTitle.ToLower().Trim() == pageFriendlyTitle))
                {
                    ModelState.AddModelError("BlogTitle", "Title is already taken.");
                }
                else
                {
                    var blog = new Blog
                    {
                        Title = model.BlogTitle,
                        BlogContent = model.BlogContent,
                        PageFriendlyUrlTitle = model.BlogTitle.Replace(' ', '-'),
                        ShortDescription = model.ShortDescription,
                        NoOfViews = 0,
                        PublishedOn = DateTime.Now,
                        ImageName = DateTime.Now.ToString("ddMMyyyyhhmmssff") + Path.GetExtension(model.BlogImage.FileName),
                        Tags = model.Tags.Select(t => new BlogTag
                        {
                            TagName = t
                        })
                        .ToList()
                    };

                    _db.Entry(blog).State = EntityState.Added;
                    await _db.SaveChangesAsync();
                    model.BlogImage.SaveAs(Path.Combine(Server.MapPath("~/Images/BlogImages"), blog.ImageName));

                    return RedirectToRoutePermanent("ViewBlogDetails", new { blogUrlFriendlyName = blog.PageFriendlyUrlTitle });
                }
            }

            return View(model);
        }

        [NonAction]
        public async Task<List<Blog>> GetPopularBlogs(IEnumerable<int> blogIdsToSkip)
        {
            var noOfTopBlogsToShow = int.Parse(ConfigurationManager.AppSettings["NoOfTopBlogsToShow"].ToString());

            var topBlogs = await _db.Blogs
                .Where(w =>  blogIdsToSkip.Contains(w.BlogId) == false)
                .OrderByDescending(o => o.PublishedOn)
                .Take(noOfTopBlogsToShow)
                .ToListAsync();

            return topBlogs;
        }
    }
}