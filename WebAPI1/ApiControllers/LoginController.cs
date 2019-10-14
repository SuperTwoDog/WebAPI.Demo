using Common.Helper;
using Common.Model;
using Entity.Models;
using Newtonsoft.Json;
using Services;
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
        #region 数据访问接口
        public UserInfoService userService { get; set; }
        public RoleService roleService { get; set; }

        #endregion

        #region 登录(测试)
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
        /// 测试1
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ApiAuthor(Name = "Mr·Tan", Status = DevStatus.Finish, Time = "2018-08-09")]
        public dynamic CheckLogin1([FromBody]LoginModel dto)
        {
            return dto.Account;
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
                tokenModel.Uid = 1;
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

        #region 登录(正式)

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model">请求数据</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ApiAuthor(Name = "Mr·Tan", Status = DevStatus.Dev, Time = "2019-10-14")]
        public object UserLogin([FromBody]LoginModel model)
        {
            string jwtStr = string.Empty;
            bool suc = false;
            //这里就是用户登录以后，通过数据库去调取数据，分配权限的操作
            QueryParams param = new QueryParams();
            param.UserName = model.Account;
            param.PassWord = model.Password;
            UserInfoModel user = userService.LogOn(param);
            if (user!=null && !string.IsNullOrEmpty(user.RoleID))
            {
                RoleModel role = roleService.QueryById(user.RoleID);
                TokenModel tokenModel = new TokenModel();
                tokenModel.Uid = Convert.ToInt32(DateTime.Now.ToString("yyyyMMddHHssmm"));
                tokenModel.Roles = UtilConvert.StringToList(role == null ? "Common" : role.RoleName);
                tokenModel.Users = UtilConvert.StringToList(user.Account);
                tokenModel.ExpiryDateTime = DateTime.Now.AddMinutes((Convert.ToDouble(ConfigHelper.GetValue("EffectiveMin"))));
                jwtStr = JwtHelper.IssueJwt(tokenModel);
                suc = true;
            }
            else
            {
                jwtStr = "用户名或密码错误！";
            }
            var result = new
            {
                data = new { success = suc, token = jwtStr }
            };
            return Json(result);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns></returns>
        [HttpPost]
        [ApiAuthor(Name = "Mr·Tan", Status = DevStatus.Finish, Time = "2019-10-14")]
        [ApiAuthorize(Roles = "Admin,系统管理员,Common")]
        public dynamic GetUserList(int pageIndex, int pageSize)
        {
            Pagination page = new Pagination()
            {
                PageIndex = pageIndex,
                PageRows = pageSize
            };
            var userList = userService.QueryPageEx(ref page);
            return userList;
        }

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiAuthor(Name = "Mr·Tan", Status = DevStatus.Finish, Time = "2019-10-14")]
        [ApiAuthorize(Roles = "系统管理员")]
        public dynamic GetUserInfoByID(string userID)
        {
            UserInfoModel user = userService.QueryById(userID);
            return user;
        }

        #endregion
    }
}
