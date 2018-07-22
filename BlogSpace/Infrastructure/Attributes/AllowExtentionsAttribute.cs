using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSpace.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class AllowExtentionsAttribute : ValidationAttribute
    {
        private List<string> AllowedExtensions { get; set; }

        public AllowExtentionsAttribute(string fileExtensions)
        {
            AllowedExtensions = fileExtensions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;

            if (file != null)
            {
                var fileName = file.FileName.ToLower();

                return AllowedExtensions.Any(y => fileName.EndsWith(y.ToLower()));
            }

            throw new InvalidCastException("argument file isn't a type of HttpPostedFileBase");
        }
    }
}