using SqlSugar;

namespace bookcity.entitys.models
{
    /// <summary>
    /// 
    /// </summary>
    public class sysmessage
    {
        /// <summary>
        /// 
        /// </summary>
        public sysmessage()
        {
        }

        /// <summary>
        /// id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public System.String title { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public System.String msgType { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public System.DateTime? addTime { get; set; }

        /// <summary>
        /// 添加用户
        /// </summary>
        public System.String addUser { get; set; }

        /// <summary>
        /// 接收用户
        /// </summary>
        public System.String receiveUser { get; set; }
    }
}