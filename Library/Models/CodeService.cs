using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Models
{
    public class CodeService
    {
        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }


        /// <summary>
        /// 取得圖書類別
        /// </summary>
        /// <param name="BOOK_CLASS_NAME"></param>
        /// <returns></returns>
        public List<SelectListItem> Getbook_category()
        {
            DataTable dt = new DataTable();
            string sql = @"Select BOOK_CLASS_ID As CodeId, BOOK_CLASS_NAME As CodeName 
                           FROM BOOK_CLASS";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData(dt);
        }


        /// <summary>
        /// 取得借閱狀態
        /// </summary>
        /// <param name="CODE_NAME"></param>
        /// <returns></returns>
        public List<SelectListItem> Getbook_status()
        {
            DataTable dt = new DataTable();
            string sql = @"Select CODE_ID As CodeId, CODE_NAME As CodeName 
                           FROM BOOK_CODE";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData(dt);
        }

        /// <summary>
        /// 取得借閱人
        /// </summary>
        /// <param name="USER_ENAME"></param>
        /// <returns></returns>
        public List<SelectListItem> Getbook_borrower()
        {
            DataTable dt = new DataTable();
            string sql = @"Select USER_ID As CodeId, USER_ENAME As CodeName 
                           FROM MEMBER_M";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData(dt);
        }

        /// <summary>
        /// 取得借閱人(中+英)
        /// </summary>
        /// <param name="USER_ENAME"></param>
        /// <returns></returns>
        public List<SelectListItem> Getbook_borrower_CE()
        {
            DataTable dt = new DataTable();
            string sql = @"Select USER_ID As CodeId , USER_ENAME + '-'+ USER_CNAME As CodeName 
                           FROM MEMBER_M";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData(dt);
        }

        public List<Models.LibraryUpdate>FindBookData(string bookId)
        {
            DataTable dt = new DataTable();

            string sql = @"SELECT BD.BOOK_ID ,BD.BOOK_NAME, BD.BOOK_AUTHOR, BD.BOOK_PUBLISHER, BD.BOOK_NOTE,
                                  CONVERT(VARCHAR(MAX),BD.BOOK_BOUGHT_DATE,111) AS BOOK_BOUGHT_DATE,
                                  BD.BOOK_CLASS_ID,BCD.CODE_ID, CODE_NAME, BOOK_KEEPER
                           FROM dbo.BOOK_DATA AS BD 
                                JOIN BOOK_CLASS AS BC ON BD.BOOK_CLASS_ID = BC.BOOK_CLASS_ID
								JOIN BOOK_CODE AS BCD ON BD.BOOK_STATUS = BCD.CODE_ID AND CODE_TYPE = 'BOOK_STATUS'
                                LEFT JOIN MEMBER_M AS MM ON BD.BOOK_KEEPER = MM.USER_ID
                          WHERE BOOK_ID = @BOOK_ID;";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.Add(new SqlParameter("@BOOK_ID", bookId));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapBookData(dt);
        }


        /// <summary>
        /// Maping 代碼資料
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<SelectListItem> MapCodeData(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["CodeName"].ToString(),
                    Value = row["CodeId"].ToString()
                });
            }
            return result;
        }

        /// <summary>
        /// Maping 書籍資料
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<Models.LibraryUpdate> MapBookData(DataTable dt)
        {
            List<Models.LibraryUpdate> result = new List<LibraryUpdate>();

            foreach(DataRow row in dt.Rows)
            {
                result.Add(new Models.LibraryUpdate()
                {
                    BOOK_NAME = row["BOOK_NAME"].ToString(),
                    BOOK_AUTHOR = row["BOOK_AUTHOR"].ToString(),
                    BOOK_PUBLISHER = row["BOOK_PUBLISHER"].ToString(),
                    BOOK_NOTE = row["BOOK_NOTE"].ToString(),
                    BOOK_BOUGHT_DATE = row["BOOK_BOUGHT_DATE"].ToString().Replace("/", "-"),
                    BOOK_CLASS_NAME = row["BOOK_CLASS_ID"].ToString(),
                    CODE_NAME = row["CODE_ID"].ToString(),
                    USER_NAME = row["BOOK_KEEPER"].ToString(),
                    BOOK_ID = (int)row["BOOK_ID"]
                });
            }
            return result;
        }

    }
}