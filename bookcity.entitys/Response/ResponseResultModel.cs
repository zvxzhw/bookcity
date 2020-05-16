using System;
using System.Collections.Generic;
using System.Text;

namespace bookcity.entitys.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class ResponseResultModel
    {
        /// <summary>
        /// 请求响应实体类
        /// </summary>
        public ResponseResultModel()
        {
            code = 200;
        }
        /// <summary>
        /// 响应代码
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 返回的响应数据
        /// </summary>
        public object data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_data"></param>
        public void SetData(object _data)
        {
            data = _data;
        }
    }
}
