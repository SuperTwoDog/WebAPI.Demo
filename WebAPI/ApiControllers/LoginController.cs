using Common.Helper;
using Entity.Models;
using Newtonsoft.Json;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Token;
using Token.Model;
using WebAPI.AuthAttributes;

namespace WebAPI.ApiControllers
{
    /// <summary>
    /// 登录
    /// </summary>
    public class LoginController : ApiController
    {
        #region 登录
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="dto">登录信息</param>
        /// <remarks>
        /// 手持设备登录接口<br/>
        /// 必须要传参数MIME, AppId(后端约定),用户名，密码等<br/>
        /// 返回token、账号、用户姓名
        /// </remarks>
        [HttpPost]
        [AllowAnonymous]
        [ApiAuthor(Name = "Mr·Tan", Status = DevStatus.Finish, Time = "2018-08-09")]
        public dynamic CheckLogin(LoginModel dto)
        {
            return dto;
        }
        
        /// <summary>
        /// 测试用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiAuthor(Name = "Mr·Tan", Status = DevStatus.Finish, Time = "2018-08-09")]
        [ApiAuthorize(Roles = "Admin")]
        public HttpResponseMessage GetUserInfo()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new LoginModel(), "application/json");
        }

        /// <summary>
        /// 获取JWT的方法1
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Token")]
        [AllowAnonymous]
        [ApiAuthor(Name = "Mr·Tan", Status = DevStatus.Finish, Time = "2018-08-09")]
        public object GetJwtStr(string name, string pass)
        {
            string jwtStr = string.Empty;
            bool suc = false;
            //这里就是用户登录以后，通过数据库去调取数据，分配权限的操作
            //这里直接写死了
            if (name == "admin" && pass == "123456")
            {
                TokenModel tokenModel = new TokenModel();
                tokenModel.Uid = "1";
                tokenModel.Roles = UtilConvert.StringToList("admin");
                tokenModel.Users= UtilConvert.StringToList(name);
                tokenModel.ExpiryDateTime = DateTime.Now.AddMinutes((Convert.ToDouble(ConfigHelper.GetValue("EffectiveMin"))));
                jwtStr = JwtHelper.IssueJwt(tokenModel);
                suc = true;
            }
            else
            {
                jwtStr = "login fail!!!";
            }
            var result = new
            {
                data = new { success = suc, token = jwtStr }
            };
            return Json(result);
        }

        /// <summary>
        /// 获取JWT的方法1
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Token1")]
        [AllowAnonymous]
        [ApiAuthor(Name = "Mr·Tan", Status = DevStatus.Finish, Time = "2018-08-09")]
        public object GetJwtStr1(string jwt)
        {
            string jwtStr = string.Empty;
            bool suc = false;
            //这里就是用户登录以后，通过数据库去调取数据，分配权限的操作
            //这里直接写死了
            if (!string.IsNullOrEmpty(jwt))
            {
                TokenModel tokenModel = JwtHelper.SerializeJwt(jwt);
                suc = true;
            }
            else
            {
                jwtStr = "login fail!!!";
            }
            var result = new
            {
                data = new { success = suc, token = jwtStr }
            };
            return Json(result);
        }
        #endregion
    }
}
