using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookcity.AuthHelper;
using bookcity.entitys;
using bookcity.entitys.Response;
using bookcity.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookcity.api.Controllers.users
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        readonly PermissionRequirement _requirement;
        ResponseModel response = ResponseModelFactory.CreateInstance;

        public usersController(PermissionRequirement requirement)
        {
            _requirement = requirement;
        }


        /// <summary>
        /// 登录 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Route("/api/user/login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            // 请求响应实体工厂类
            var response = ResponseModelFactory.CreateInstance;
            // 定义JwtClaimModel，初次登录
            JwtClaimModel jcm = new JwtClaimModel();
            jcm.JwtGuidUser = Guid.NewGuid().ToString("N"); ;
            jcm.GuidUser = jcm.JwtGuidUser;
            jcm.GuidGroup = "";
            jcm.Type = "";

            // 生成jwt的token数据
            dynamic jwtData = AuthHelper.Cas.Jwt.BuildJwtToken(_requirement, jcm);

            // 将用户数据写入Redis, 1分钟有效，老系统使用后销毁
            string redisKey = "SerpLogin:" + jwtData.token_md5;

            response.SetData(jwtData);

            return Ok(response);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Route("/api/user/reg")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string userName, string password, string vcode, string email)
        {
            // 请求响应实体工厂类
            var response = ResponseModelFactory.CreateInstance;
            // 定义JwtClaimModel，初次登录
            JwtClaimModel jcm = new JwtClaimModel();
            jcm.JwtGuidUser = Guid.NewGuid().ToString("N"); ;
            jcm.GuidUser = jcm.JwtGuidUser;
            jcm.GuidGroup = "";
            jcm.Type = "";

            // 生成jwt的token数据
            dynamic jwtData = AuthHelper.Cas.Jwt.BuildJwtToken(_requirement, jcm);

            // 将用户数据写入Redis, 1分钟有效，老系统使用后销毁
            string redisKey = "SerpLogin:" + jwtData.token_md5;

            response.SetData(jwtData);

            return Ok(response);
        }

        /// <summary>
        /// 用户消息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Route("/api/user/alert")]
        [HttpPost]
        public async Task<IActionResult> Alert()
        {
            // 请求响应实体工厂类
            
            // 定义JwtClaimModel，初次登录
            JwtClaimModel jcm = new JwtClaimModel();
            jcm.JwtGuidUser = Guid.NewGuid().ToString("N"); ;
            jcm.GuidUser = jcm.JwtGuidUser;
            jcm.GuidGroup = "";
            jcm.Type = "";

            // 生成jwt的token数据
            dynamic jwtData = AuthHelper.Cas.Jwt.BuildJwtToken(_requirement, jcm);

            // 将用户数据写入Redis, 1分钟有效，老系统使用后销毁
            string redisKey = "SerpLogin:" + jwtData.token_md5;

            response.SetData(jwtData);

            return Ok(response);
        }


        /// <summary>
        /// 用户反馈
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Route("/api/user/addfeedback")]
        [HttpPost]
        public async Task<IActionResult> addfeedback(string uid, string title, string body, string mobile, string linkman)
        {
            // 请求响应实体工厂类
            var response = ResponseModelFactory.CreateInstance;
            // 定义JwtClaimModel，初次登录
            JwtClaimModel jcm = new JwtClaimModel();
            jcm.JwtGuidUser = Guid.NewGuid().ToString("N"); ;
            jcm.GuidUser = jcm.JwtGuidUser;
            jcm.GuidGroup = "";
            jcm.Type = "";

            // 生成jwt的token数据
            dynamic jwtData = AuthHelper.Cas.Jwt.BuildJwtToken(_requirement, jcm);

            // 将用户数据写入Redis, 1分钟有效，老系统使用后销毁
            string redisKey = "SerpLogin:" + jwtData.token_md5;

            response.SetData(jwtData);

            return Ok(response);
        }

        /// <summary>
        /// 注销登录
        /// </summary>
        /// <returns></returns>
        [Route("/api/user/loginout")]
        [HttpGet]
        public async Task<IActionResult> loginout()
        {
            // 请求响应实体工厂类
            var response = ResponseModelFactory.CreateInstance;
            // 定义JwtClaimModel，初次登录
            JwtClaimModel jcm = new JwtClaimModel();
            jcm.JwtGuidUser = Guid.NewGuid().ToString("N"); ;
            jcm.GuidUser = jcm.JwtGuidUser;
            jcm.GuidGroup = "";
            jcm.Type = "";

            // 生成jwt的token数据
            dynamic jwtData = AuthHelper.Cas.Jwt.BuildJwtToken(_requirement, jcm);

            // 将用户数据写入Redis, 1分钟有效，老系统使用后销毁
            string redisKey = "SerpLogin:" + jwtData.token_md5;

            response.SetData(jwtData);

            return Ok(response);
        }


    }
}