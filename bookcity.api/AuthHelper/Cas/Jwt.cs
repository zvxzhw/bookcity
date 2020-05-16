using bookcity.entitys;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace bookcity.AuthHelper.Cas
{
    public class Jwt
    {

        /// <summary>
        /// 生成jwt的token数据
        /// </summary>
        /// <param name="_requirement"></param>
        /// <param name="jcm"></param>
        /// <returns>jwtData</returns>
        public static dynamic BuildJwtToken(PermissionRequirement _requirement, JwtClaimModel jcm)
        {
            // 写入jwt里的数据
            var claims = new List<Claim>{
                // 原始Jwt用户Guid
                new Claim(JwtClaimTypes.JwtGuidUser, jcm.JwtGuidUser),
                // 当前/代理GuidUser
                new Claim(JwtClaimTypes.GuidUser, jcm.GuidUser),
                // 当前/代理GuidGroup
                new Claim(JwtClaimTypes.GuidGroup, jcm.GuidGroup),
                // 当前/代理Type
                new Claim(JwtClaimTypes.Type, jcm.Type),
                // 过期时间（2小时）：2020/3/18 10:39:54
                new Claim(JwtClaimTypes.Expiration, DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString())
            };
            // 生成jwt的token数据
            dynamic jwtData = JwtToken.BuildJwtToken(claims.ToArray(), _requirement);
            return jwtData;
        }

        /// <summary>
        /// GetJwtValue 简化JwtValue读取
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="jwtClaim"></param>
        /// <returns></returns>
        public static string GetJwtValue(HttpContext httpContext, string jwtClaim)
        {
            var auth = httpContext.AuthenticateAsync();
            return auth.Result.Principal.Claims.First(t => t.Type.Equals(jwtClaim))?.Value;
        }
    }
}
