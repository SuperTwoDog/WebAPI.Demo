﻿using Common.Helper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Token.Model;

namespace Token
{
    /// <summary>
    /// Jwt帮助类
    /// </summary>
    public class JwtHelper
    {
        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="tokenModel">model</param>
        /// <returns></returns>
        public static string IssueJwt(TokenModel tokenModel)
        {
            // 自己封装的 appsettign.json 操作类，看下文
            string iss = ConfigHelper.GetValue("Issuer");//颁发人
            string aud = ConfigHelper.GetValue("Audience");
            string secret = ConfigHelper.GetValue("Secret");//口令加密秘钥

            //var claims = new Claim[] //old
            var claims = new List<Claim>
                {
                 /*
                 * 特别重要：
                   1、这里将用户的部分信息，比如 uid 存到了Claim 中，如果你想知道如何在其他地方将这个 uid从 Token 中取出来，请看下边的SerializeJwt() 方法，或者在整个解决方案，搜索这个方法，看哪里使用了！
                   2、你也可以研究下 HttpContext.User.Claims ，具体的你可以看看 Policys/PermissionHandler.cs 类中是如何使用的。
                 */
                new Claim(JwtRegisteredClaimNames.Jti, tokenModel.Uid.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                //这个就是过期时间，目前是过期1000秒，可自定义，注意JWT有自己的缓冲过期时间
                //new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds()}"),
                new Claim (JwtRegisteredClaimNames.Exp,$"{tokenModel.ExpiryDateTime}"),
                new Claim(JwtRegisteredClaimNames.Iss,iss),
                new Claim(JwtRegisteredClaimNames.Aud,aud),
                
                //new Claim(ClaimTypes.Role,tokenModel.Role),//为了解决一个用户多个角色(比如：Admin,System)，用下边的方法
               };

            // 可以将一个用户的多个角色全部赋予；
            // 作者：DX 提供技术支持；
            claims.AddRange(tokenModel.Roles.Select(s => new Claim(ClaimTypes.Role, s)));
            claims.AddRange(tokenModel.Users.Select(s => new Claim(ClaimTypes.UserData, s)));



            //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: iss,
                claims: claims,
                signingCredentials: creds);

            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);

            return encodedJwt;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr">jwt字符串</param>
        /// <returns></returns>
        public static TokenModel SerializeJwt(string jwtStr)
        {
            TokenModel result = null;
            try
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
                object role;
                object user;
                object time;
                try
                {
                    jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
                    jwtToken.Payload.TryGetValue(ClaimTypes.UserData, out user);
                    jwtToken.Payload.TryGetValue(JwtRegisteredClaimNames.Exp, out time);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                if (role != null && user != null)
                {
                    result = new TokenModel
                    {
                        Uid = jwtToken.Id,
                        Roles = role != null ? UtilConvert.StringToList(role.ToString()) : null,
                        Users = user != null ? UtilConvert.StringToList(user.ToString()) : null,
                    };
                    if (time != null)
                    {
                        result.ExpiryDateTime = Convert.ToDateTime(time.ToString());
                    }
                }
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
    }
}
