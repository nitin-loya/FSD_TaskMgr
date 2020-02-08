using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using log4net;

namespace TaskMgr.Web.Util
{
    public class ApiActionFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);            
            var logger = LogManager.GetLogger(actionContext.ControllerContext.ControllerDescriptor.ControllerName);
            logger.Info(actionContext.ActionDescriptor.ActionName);
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }

    public class ApiExceptionFilter: ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
            var logger = LogManager.GetLogger(actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName);
            logger.ErrorFormat("{0}: {1}\n{2}", actionExecutedContext.ActionContext.ActionDescriptor.ActionName, actionExecutedContext.Exception.Message, actionExecutedContext.Exception.StackTrace);
        }
    }
}