using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Routing;

namespace System.Web.Mvc.Html
{
    /// <summary>
    /// Html helper extension for an Input file upload element
    /// </summary>
    public static class FileBoxHtmlHelperExtension
    {
        /// <summary>
        /// Returns a file input element by using the specified HTML helper and the name of the form field.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the <see cref="System.Web.Mvc.ViewDataDictionary" /> key that is used to look up the validation errors.</param>
        /// <returns>
        /// An input element that has its type attribute set to "file".
        /// </returns>
        public static MvcHtmlString FileBox(this HtmlHelper htmlHelper, string name)
        {
            return htmlHelper.FileBox(name, (object)null);
        }
        /// <summary>
        /// Returns a file input element by using the specified HTML helper, the name of the form field, and the HTML attributes.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the <see cref="System.Web.Mvc.ViewDataDictionary" /> key that is used to look up the validation errors.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes for the element. The attributes are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
        /// <returns>
        /// An input element that has its type attribute set to "file".
        /// </returns>
        public static MvcHtmlString FileBox(this HtmlHelper htmlHelper, string name, object htmlAttributes)
        {
            return htmlHelper.FileBox(name, new RouteValueDictionary(htmlAttributes));
        }
        /// <summary>
        /// Returns a file input element by using the specified HTML helper, the name of the form field, and the HTML attributes.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the <see cref="System.Web.Mvc.ViewDataDictionary" /> key that is used to look up the validation errors.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes for the element. The attributes are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
        /// <returns>
        /// An input element that has its type attribute set to "file".
        /// </returns>
        public static MvcHtmlString FileBox(this HtmlHelper htmlHelper, string name, IDictionary<String, Object> htmlAttributes)
        {
            var tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("type", "file", true);
            tagBuilder.MergeAttribute("name", name, true);
            tagBuilder.GenerateId(name);


            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(name, out modelState))
            {
                if (modelState.Errors.Count > 0)
                {
                    tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                }
            }


            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }
        /// <summary>
        /// Returns a file input element by using the specified HTML helper and the name of the form field as an expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">The expression that resolves to the name of the form field and the <see cref="System.Web.Mvc.ViewDataDictionary" /> key that is used to look up the validation errors.</param>
        /// <returns>
        /// An input element that has its type attribute set to "file".
        /// </returns>
        public static MvcHtmlString FileBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
        {
            return htmlHelper.FileBoxFor<TModel, TValue>(expression, (object)null);
        }
        /// <summary>
        /// Returns a file input element by using the specified HTML helper and the name of the form field as an expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">The expression that resolves to the name of the form field and the <see cref="System.Web.Mvc.ViewDataDictionary" /> key that is used to look up the validation errors.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes for the element. The attributes are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
        /// <returns>
        /// An input element that has its type attribute set to "file".
        /// </returns>
        public static MvcHtmlString FileBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return htmlHelper.FileBoxFor<TModel, TValue>(expression, new RouteValueDictionary(htmlAttributes));
        }
        /// <summary>
        /// Returns a file input element by using the specified HTML helper and the name of the form field as an expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">The expression that resolves to the name of the form field and the <see cref="System.Web.Mvc.ViewDataDictionary" /> key that is used to look up the validation errors.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes for the element. The attributes are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
        /// <returns>
        /// An input element that has its type attribute set to "file".
        /// </returns>
        public static MvcHtmlString FileBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IDictionary<String, Object> htmlAttributes)
        {
            var name = ExpressionHelper.GetExpressionText(expression);


            return htmlHelper.FileBox(name, htmlAttributes);
        }
    }
}