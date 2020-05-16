using System;

namespace bookcity.entitys
{
    public static class JwtClaimTypes : Object
    {
        /// <summary>
        /// 原始Jwt用户Guid
        /// </summary>
        public const String JwtGuidUser = "JwtGuidUser";
        
        /// <summary>
        /// 当前/代理GuidUser
        /// </summary>
        public const String GuidUser = "GuidUser";

        /// <summary>
        /// 当前/代理GuidGroup
        /// </summary>
        public const String GuidGroup = "GuidGroup";

        /// <summary>
        /// 当前/代理Type
        /// </summary>
        public const String Type = "Type";

        /// <summary>
        /// Expiration
        /// </summary>
        public const String Expiration = "Expiration";

    }

    public class JwtClaimModel
    {
        /// <summary>
        /// 原始Jwt用户Guid
        /// </summary>
        public string JwtGuidUser { get; set; }

        /// <summary>
        /// 当前/代理GuidUser
        /// </summary>
        public string GuidUser { get; set; }

        /// <summary>
        /// 当前/代理GuidGroup
        /// </summary>
        public string GuidGroup { get; set; }

        /// <summary>
        /// 当前/代理Type
        /// </summary>
        public string Type { get; set; }


    }
}
