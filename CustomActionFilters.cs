using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace NetFrameworkHelper
{
	/// <summary>
	/// The ValidateAjaxAttribute is usesd like standard MVC model validation.  This is to enable the model errors to be passed 
	/// back to the client as JAON data - instead of sending back a partial view with the errors plugged into the model errors.
	/// This is best used with the universalAjax.js file since it handles pluggin in the data into the appropriate areas.
	/// </summary>
	public class ValidateAjaxAttribute : ActionFilterAttribute
	{
		/// <summary>
		/// Validates a model that was submitted with Ajax.  Will return a 400 error with a JSON containing the error model.
		/// </summary>
		/// <param name="filterContext"></param>
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (filterContext == null)
			{
				throw new ArgumentNullException(nameof(filterContext), "filter context was passed in as null.");
			}
			if (!filterContext.HttpContext.Request.IsAjaxRequest())
				return;

			var modelState = filterContext.Controller.ViewData.ModelState;
			if (!modelState.IsValid)
			{
				var errorModel =
						from x in modelState.Keys
						where modelState[x].Errors.Count > 0
						select new
						{
							key = x,
							errors = modelState[x].Errors.Select(y => y.ErrorMessage).ToArray()
						};
				filterContext.Result = new JsonResult()
				{
					Data = errorModel
				};
				filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
			}
		}
	}

}
