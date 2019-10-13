using Common;
using Common.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace WebAPI.App_Start.Api_Handler
{
    public class ApiResultAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //action 请求上下文
            var at = actionExecutedContext.ActionContext;

            //ActionContext 返回结果为空时,说明出现异常,则走异常处理过滤器
            if (at.Response == null)
            {
                return;
            }


            //POST,DELETE,PUT需要封装返回结果
            if (at.Request.Method != HttpMethod.Get)
            {
                AjaxResult result = new AjaxResult();
                result.type = ResultType.success;
                result.resultdata = at.Response.Content != null ? at.Response.Content.ReadAsStringAsync().Result : "";

                at.Response = new HttpResponseMessage(HttpStatusCode.OK);
                result.message = "操作成功";
                string jsonStr = JsonConvert.SerializeObject(result);
                //string a = JsonConvert.ToString(result);
                actionExecutedContext.Response.Content = new StringContent(jsonStr);
            }
        }
    }
}