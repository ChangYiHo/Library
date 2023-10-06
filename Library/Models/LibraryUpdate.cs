using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Library.Models
{
    public class LibraryUpdate
    {
        [DisplayName("書名")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BOOK_NAME { get; set; }

        [DisplayName("作者")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BOOK_AUTHOR { get; set; }

        [DisplayName("出版商")]
        public string BOOK_PUBLISHER { get; set; }

        [DisplayName("內容簡介")]
        public string BOOK_NOTE { get; set; }

        [DisplayName("購書日期")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BOOK_BOUGHT_DATE { get; set; }

        [DisplayName("圖書類別")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BOOK_CLASS_NAME { get; set; }

        [DisplayName("借閱狀態")]
        [Required(ErrorMessage = "此欄位必填")]
        public string CODE_NAME { get; set; }

        [DisplayName("借閱人")]
        public string USER_NAME { get; set; }

        public int BOOK_ID { get; set; }

    }
}