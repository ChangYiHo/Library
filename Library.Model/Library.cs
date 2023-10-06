using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class Library
    {
        public string BOOK_ID { get; set; }

        /// <summary>
        /// 書名
        /// </summary>
        public string BOOK_NAME { get; set; }

        /// <summary>
        /// 圖書類別
        /// </summary>
        public string BOOK_CLASS_ID { get; set; }

        /// <summary>
        /// 借閱人
        /// </summary>
        public string USER_ENAME { get; set; }

        /// <summary>
        /// 借閱狀態
        /// </summary>
        public string CODE_NAME { get; set; }

        /// <summary>
        /// 購書日期
        /// </summary>
        public string BOOK_BOUGHT_DATE { get; set; }

        /// <summary>
        /// 圖書類別
        /// </summary>
        public string BOOK_CLASS_NAME { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string BOOK_AUTHOR { get; set; }

        /// <summary>
        /// 出版商
        /// </summary>
        public string BOOK_PUBLISHER { get; set; }

        /// <summary>
        /// 內容簡介
        /// </summary>
        public string BOOK_NOTE { get; set; }
    }
}
