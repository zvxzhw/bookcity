using SqlSugar;

namespace bookcity.entitys.models
{
    /// <summary>
    /// 
    /// </summary>
    public class sysbook_info
    {
        /// <summary>
        /// 
        /// </summary>
        public sysbook_info()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String bookName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String author { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? lastUpdate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String intro { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String source { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String bookNum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String category { get; set; }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        public System.Int32? chapterCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String bookImg { get; set; }
    }
}