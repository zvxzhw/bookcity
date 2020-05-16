using SqlSugar;

namespace bookcity.entitys.models
{
    /// <summary>
    /// 
    /// </summary>
    public class sysbook
    {
        /// <summary>
        /// 
        /// </summary>
        public sysbook()
        {
        }

        /// <summary>
        /// id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String id { get; set; }

        /// <summary>
        /// 书名
        /// </summary>
        public System.String bookName { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public System.String author { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public System.DateTime? lastUpdate { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public System.String intro { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public System.String source { get; set; }

        /// <summary>
        /// 图书网络号
        /// </summary>
        public System.String bookNum { get; set; }

        /// <summary>
        /// 书目种类
        /// </summary>
        public System.String category { get; set; }

        /// <summary>
        /// 停用标志
        /// </summary>
        public System.Int32? isStop { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? startTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? overTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? tsDownload { get; set; }

        /// <summary>
        /// 章节数
        /// </summary>
        public System.Int32? chapterCount { get; set; }

        /// <summary>
        /// 封面
        /// </summary>
        public System.String bookImg { get; set; }

        /// <summary>
        /// 书编号
        /// </summary>
        [SugarColumn(IsIdentity = true)]
        public System.Int32 bookno { get; set; }
    }
}