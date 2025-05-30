﻿using D8M7Q2_SportEvents.Entities.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace D8M7Q2_SportEvents.Endpoint.Helpers
{
    public class ValidationFilterAttribute: IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var error = new ErrorModel
                (
                    String.Join(',',
                    (context.ModelState.Values.SelectMany(t => t.Errors.Select(z => z.ErrorMessage))).ToArray())
                );
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new JsonResult(error);
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
