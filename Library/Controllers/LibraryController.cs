using Library.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class LibraryController : Controller
    {
        private ICodeService codeService { get; set; }

        private ILibraryService libraryService { get; set; }


        
        /// <summary>
        /// 抓取書籍類別資料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetBookClassData()
        {
            return Json(this.codeService.Getbook_class());
        }
        /// <summary>
        /// 抓取借閱人資料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetBookKeeperData()
        {
            return Json(this.codeService.Getbook_keeper());
        }
        /// <summary>
        /// 抓取書籍狀態資料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetBookStatusData()
        {
            return Json(this.codeService.Getbook_status());
        }
        /// <summary>
        /// 抓取中英名字
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult GetCEName()
        {
            return Json(this.codeService.Getbook_keeper_CName_EName());
        }

        ///// <summary>
        ///// (查詢)圖書資料查詢畫面
        ///// </summary>
        ///// <returns></returns>
        public ActionResult Search()
        {
            return View();
        }

        /// <summary>
        /// (查詢)圖書資料查詢結果
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        [HttpPost()]
        public JsonResult Search(Library.Model.LibrarySearch arg)
        {
            return Json(libraryService.GetLibraryByCondtioin(arg));
        }



        /// <summary>
        /// 新增書籍畫面
        /// </summary>
        /// <returns></returns>
        public ActionResult Insert()
        {
            return View();
        }

        /// <summary>
        /// 新增書籍
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        /// 
        [HttpPost()]
        public JsonResult Insert(Library.Model.LibraryInsert insertData)
        {
            if (ModelState.IsValid) //如果有值
            {
                libraryService.InsertLibrary(insertData);
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }



        /// <summary>
        /// 刪除書籍
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpPost()]
        public JsonResult DeleteBook(string bookId)
        {
            try
            {
                this.libraryService.DeleteBookByID(bookId);
                return this.Json(true);

            }

            catch (Exception ex)
            {
                return this.Json(false);
            }
        }

        /// <summary>
        /// 修改畫面
        /// </summary>
        /// 

        [HttpGet()]
        public ActionResult Update(string bookId)
        {
            return View();
        }

        /// <summary>
        /// 拿取書籍資料
        /// </summary>
        /// 
        [HttpPost]
        public JsonResult GetBookData(string bookId)
       {
            var bookdata = this.libraryService.FindBookData(bookId).FirstOrDefault();
            return Json(bookdata);
        }



        /// <summary>
        /// 修改書籍結果
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        /// 
        [HttpPost()]
        public JsonResult UpdateBook(Library.Model.LibraryUpdate data)
        {
            this.libraryService.UpdateLibrary(data);
            return Json("");
        }

    }
}