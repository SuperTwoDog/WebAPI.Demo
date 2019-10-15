using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Token;
using Token.Model;

namespace WebAPI.AuthAttributes
{
    /// <summary>
    /// 身份认证拦截器
    /// </summary>
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            //前端请求api时会将token存放在名为"auth"的请求头中
            var authHeader = from t in actionContext.Request.Headers where t.Key == "bearer" || t.Key == "Bearer" select t.Value.FirstOrDefault();
            if (authHeader != null)
            {
                string jwt = authHeader.FirstOrDefault();//获取token
                if (!string.IsNullOrEmpty(jwt))
                {
                    try
                    {
                        TokenModel token = JwtHelper.SerializeJwt(jwt);
                        if (token != null)
                        {
                            var roles = this.Roles;
                            var users = this.Users;
                            //判断是否指定角色访问
                            if (!string.IsNullOrEmpty(roles))
                            {
                                string[] rolesArray = roles.ToLower().Split(',');
                                bool isRool = false;
                                foreach (var item in rolesArray)
                                {
                                    if (token.Roles.Exists(x => x == item))
                                    {
                                        isRool = true;
                                    }
                                }
                                if (!isRool)
                                {
                                    return false;
                                }
                            }
                            //判断是否指定用户访问
                            if (!string.IsNullOrEmpty(users))
                            {
                                string[] usersArray = users.ToLower().Split(',');
                                bool isUser = false;
                                foreach (var item in usersArray)
                                {
                                    if (token.Users.Exists(x => x == item))
                                    {
                                        isUser = true;
                                    }
                                }
                                if (!isUser)
                                {
                                    return false;
                                }
                            }
                            //判断口令过期时间
                            if (token.ExpiryDateTime.HasValue && token.ExpiryDateTime < DateTime.Now)
                            {
                                return false;
                            }
                            actionContext.RequestContext.RouteData.Values.Add("Bearer", token);
                            return true;
                        }
                        return false;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
            return false;
        }
    }
}