using SqlSugar;

namespace bookcity.entitys.models
{
    /// <summary>
    /// 
    /// </summary>
    public class sysdictionary
    {
        /// <summary>
        /// 
        /// </summary>
        public sysdictionary()
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
        public System.String dictionary_name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String dictionary_value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String dictionary_parentid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String dictionary_text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String dictionary_code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String remark { get; set; }
    }
}