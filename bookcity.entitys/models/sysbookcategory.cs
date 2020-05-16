using SqlSugar;

namespace bookcity.entitys.models
{
    /// <summary>
    /// 
    /// </summary>
    public class sysbookcategory
    {
        /// <summary>
        /// 
        /// </summary>
        public sysbookcategory()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String id { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public System.String title { get; set; }
    }
}