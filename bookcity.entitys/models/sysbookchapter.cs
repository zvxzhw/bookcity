using SqlSugar;

namespace bookcity.entitys.models
{
    /// <summary>
    /// 
    /// </summary>
    public class sysbookchapter
    {
        /// <summary>
        /// 
        /// </summary>
        public sysbookchapter()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String id { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 cno { get; set; }

        /// <summary>
        /// 书目id
        /// </summary>
        public System.String bookid { get; set; }

        /// <summary>
        /// 章节标题
        /// </summary>
        public System.String chapter { get; set; }

        /// <summary>
        /// 章节内容
        /// </summary>
        public System.String intro { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public System.DateTime? addTime { get; set; }

        /// <summary>
        /// 爬取地址
        /// </summary>
        public System.String linkUrl { get; set; }

        /// <summary>
        /// 是否vip
        /// </summary>
        public System.Int32? isVip { get; set; }

        /// <summary>
        /// 付款点数
        /// </summary>
        public System.Int32? Paypoint { get; set; }
    }
}