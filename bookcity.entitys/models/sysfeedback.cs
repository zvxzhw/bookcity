using SqlSugar;

namespace bookcity.entitys.models
{
    /// <summary>
    /// 
    /// </summary>
    public class sysfeedback
    {
        /// <summary>
        /// 
        /// </summary>
        public sysfeedback()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String id { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public System.String uid { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public System.String title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public System.String body { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? addtime { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public System.String mobile { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        public System.String linkman { get; set; }
    }
}