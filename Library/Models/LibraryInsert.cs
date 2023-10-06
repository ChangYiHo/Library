using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Library.Models
{
    public class LibraryInsert
    {
        /// <summary>
        /// 書名
        /// </summary>
        /// 
        [DisplayName("*書名")]
        [Required(ErrorMessage ="此欄位必填...")]
        public string BOOK_NAME { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        ///
        [DisplayName("*作者")]
        [Required(ErrorMessage = "此欄位必填...")]
        public string BOOK_AUTHOR { get; set; }

        /// <summary>
        /// 出版商
        /// </summary>
        ///
        [DisplayName("*出版商")]
        [Required(ErrorMessage = "此欄位必填...")]
        public string BOOK_PUBLISHER { get; set; }

        /// <summary>
        /// 內容簡介
        /// </summary>
        ///
        [DisplayName("*內容簡介")]
        [Required(ErrorMessage = "此欄位必填...")]
        public string BOOK_NOTE { get; set; }

        /// <summary>
        /// 購書日期
        /// </summary>
        ///
        [DisplayName("*購書日期")]
        [Required(ErrorMessage = "此欄位必填...")]
        public string BOOK_BOUGHT_DATE { get; set; }

        /// <summary>
        /// 圖書類別
        /// </summary>
        ///
        [DisplayName("*圖書類別")]
        [Required(ErrorMessage = "此欄位必填...")]
        public string BOOK_CLASS_NAME { get; set; }
    }
}