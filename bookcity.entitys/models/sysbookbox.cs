using SqlSugar;

namespace bookcity.entitys.models
{
    /// <summary>
    /// 
    /// </summary>
    public class sysbookbox
    {
        /// <summary>
        /// 
        /// </summary>
        public sysbookbox()
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
        public System.String bookid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String userid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? addtime { get; set; }
    }
}