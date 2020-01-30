using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Helpdesk.Filters
{
    public class ResponseFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.HttpContext.Response.Headers.ContainsKey("Cache-Control"))
                context.HttpContext.Response.Headers.Add("Cache-Control", "no-cache,no-store");
            if (!context.HttpContext.Response.Headers.ContainsKey("Pragma")) // required for Http1.0
                context.HttpContext.Response.Headers.Add("Pragma", "no-cache");
            base.OnActionExecuted(context);
        }
    }
}
