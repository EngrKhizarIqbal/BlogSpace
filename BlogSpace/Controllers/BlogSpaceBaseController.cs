using BlogSpace.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BlogSpace.Controllers
{
    public abstract class BlogSpaceBaseController : Controller
    {
        protected ApplicationDbContext _db = new ApplicationDbContext();
        protected Dictionary<string, object> ResponseBody = new Dictionary<string, object>();
        protected ICollection<IDisposable> DisposableObjects { set; get; } = new HashSet<IDisposable>();

        public BlogSpaceBaseController()
        {
            DisposableObjects.Add(_db);
        }

        [NonAction]
        protected virtual List<string> GetModelStateErrors()
        {
            var errorList = new List<string>();

            foreach (var key in ModelState.Keys)
                foreach (var errors in ModelState[key].Errors)
                    if (!string.IsNullOrEmpty(errors.ErrorMessage))
                        errorList.Add(errors.ErrorMessage);

            return errorList;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var obj in DisposableObjects)
                    obj?.Dispose();
            }

            base.Dispose(disposing);
        }

    }
}