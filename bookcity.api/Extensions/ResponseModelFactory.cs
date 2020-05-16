using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookcity.entitys.Response;

namespace bookcity.Extensions
{
    /// <summary>
    /// /
    /// </summary>
    public class ResponseModelFactory
    {
        /// <summary>
        /// 
        /// </summary>
        public static ResponseModel CreateInstance => new ResponseModel();

        // <summary>
        // 
        // </summary>
        // public static ResponseMessageModel CreateMessagInstance => new ResponseMessageModel();

        /// <summary>
        /// 
        /// </summary>
        public static ResponseResultModel CreateResultInstance => new ResponseResultModel();
    }
}
