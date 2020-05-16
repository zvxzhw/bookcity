using System;
using System.Collections.Generic;
using System.Text;

namespace bookcity.entitys.ViewModel
{
    public class SearchParams
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string keywords { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int? pageIndex { get; set; }
        
        /// <summary>
        /// 页面数据大小
        /// </summary>
        public int? pageSize { get; set; }
    }
}
