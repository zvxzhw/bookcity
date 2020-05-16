using SqlSugar;

namespace bookcity.entitys.models
{
    /// <summary>
    /// 
    /// </summary>
    public class sysuser
    {
        /// <summary>
        /// 
        /// </summary>
        public sysuser()
        {
        }

        /// <summary>
        /// id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public System.String userName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public System.String password { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public System.String email { get; set; }

        /// <summary>
        /// 令牌
        /// </summary>
        public System.String token { get; set; }

        /// <summary>
        /// 登录过期时间
        /// </summary>
        public System.DateTime? expired { get; set; }

        /// <summary>
        /// 在线状态0 离线 1 在线
        /// </summary>
        public System.Int32? online { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public System.DateTime? addtime { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public System.DateTime? lastLogin { get; set; }

        /// <summary>
        /// 用户代码
        /// </summary>
        public System.String vcode { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public System.UInt32? logincount { get; set; }
    }
}