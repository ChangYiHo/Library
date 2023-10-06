using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Library.Models
{
    public class LibrarySearch
    {
        /// <summary>
        /// 書名
        /// </summary>
        /// 
        [DisplayName("書名")]
        public string BOOK_NAME { get; set; }

        /// <summary>
        /// 圖書類別
        /// </summary>
        /// 
        [DisplayName("圖書類別")]
        public string BOOK_CLASS_NAME { get; set; }

        /// <summary>
        /// 借閱人
        /// </summary>
        /// 
        [DisplayName("借閱人")]
        public string USER_ENAME { get; set; }

        /// <summary>
        /// 借閱狀態
        /// </summary>
        /// 
        [DisplayName("借閱狀態")]
        public string CODE_NAME { get; set; }
    }
}