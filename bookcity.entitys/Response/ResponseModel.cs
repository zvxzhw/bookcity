using System;
using System.Collections.Generic;
using System.Text;

namespace bookcity.entitys.Response
{

    /// <summary>
    /// 请求响应实体
    /// </summary>
    public class ResponseModel
    {
        /// <summary>
        /// 请求响应实体类
        /// </summary>
        public ResponseModel()
        {
            code = 200;
            //message = "操作成功";
        }
        /// <summary>
        /// 响应代码
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 响应消息内容
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 返回的响应数据
        /// </summary>
        public object data { get; set; }

        /// <summary>
        /// 设置响应状态为成功
        /// </summary>
        /// <param name="_message"></param>
        public void SetSuccess(string _message = "成功")
        {
            code = 200;
            message = _message;
        }
        /// <summary>
        /// 设置响应状态为失败
        /// </summary>
        /// <param name="_message"></param>
        public void SetFailed(string _message = "失败")
        {
            message = _message;
            code = 999;
        }

        /// <summary>
        /// 设置响应状态为体验版(返回失败结果)
        /// </summary>
        /// <param name="_message"></param>
        public void SetIsTrial(string _message = "体验版,功能已被关闭")
        {
            message = _message;
            code = 999;
        }

        /// <summary>
        /// 设置响应状态为错误
        /// </summary>
        /// <param name="_message"></param>
        public void SetError(string _message = "错误")
        {
            code = 500;
            message = _message;
        }

        /// <summary>
        /// 设置响应状态为未知资源
        /// </summary>
        /// <param name="_message"></param>
        public void SetNotFound(string _message = "未知资源")
        {
            message = _message;
            code = 404;
        }

        /// <summary>
        /// 设置响应状态为无权限
        /// </summary>
        /// <param name="_message"></param>
        public void SetNoPermission(string _message = "无权限")
        {
            message = _message;
            code = 401;
        }

        /// <summary>
        /// 设置响应数据
        /// </summary>
        /// <param name="_data"></param>
        public void SetData(object _data)
        {
            data = _data;
        }
    }
}
