using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Models
{

    public class LibraryService
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
        /// 新增書籍
        /// </summary>
        /// <param name="libraryinsert"></param>
        /// <returns></returns>
        public int InsertLibrary(Models.LibraryInsert libraryinsert)
        {
            string sql = @"INSERT INTO BOOK_DATA
                         (
                            BOOK_NAME , BOOK_AUTHOR , BOOK_PUBLISHER , BOOK_NOTE ,
                            BOOK_BOUGHT_DATE , BOOK_CLASS_ID , BOOK_STATUS
                         )
                           VALUES
                         (
                            @BOOK_NAME , @BOOK_AUTHOR , @BOOK_PUBLISHER , @BOOK_NOTE ,
                            @BOOK_BOUGHT_DATE , @BOOK_CLASS_NAME,'A'
                         )
                         Select SCOPE_IDENTITY()";
            int BOOK_ID;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BOOK_NAME", libraryinsert.BOOK_NAME));
                cmd.Parameters.Add(new SqlParameter("@BOOK_AUTHOR", libraryinsert.BOOK_AUTHOR));
                cmd.Parameters.Add(new SqlParameter("@BOOK_PUBLISHER", libraryinsert.BOOK_PUBLISHER));
                cmd.Parameters.Add(new SqlParameter("@BOOK_NOTE", libraryinsert.BOOK_NOTE));
                cmd.Parameters.Add(new SqlParameter("@BOOK_BOUGHT_DATE", libraryinsert.BOOK_BOUGHT_DATE));
                cmd.Parameters.Add(new SqlParameter("@BOOK_CLASS_NAME", libraryinsert.BOOK_CLASS_NAME));
                BOOK_ID = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            return BOOK_ID;
        }

        /// <summary>
        /// 刪除書籍
        /// </summary>
        public void DeleteBookByID(string BOOK_ID)
        {
            try
            {
                string sql = "Delete FROM BOOK_DATA Where BOOK_ID=@BOOK_ID";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@BOOK_ID", BOOK_ID));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// 依照條件查詢書籍資料
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public List<Models.Library> GetLibraryByCondtioin(Models.LibrarySearch arg)
        {

            DataTable dt = new DataTable();
            
            string sql = @"SELECT BOOK_ID , BOOK_NAME , BC.BOOK_CLASS_NAME ,
                           CONVERT(varchar(max),BOOK_BOUGHT_DATE,111) AS BOOK_BOUGHT_DATE, USER_ENAME , CODE_NAME
                           FROM dbo.BOOK_DATA AS BD 
                                JOIN BOOK_CLASS AS BC ON BD.BOOK_CLASS_ID = BC.BOOK_CLASS_ID
								JOIN BOOK_CODE AS BCD ON BD.BOOK_STATUS = BCD.CODE_ID AND CODE_TYPE = 'BOOK_STATUS'
                                LEFT JOIN MEMBER_M AS MM ON BD.BOOK_KEEPER = MM.USER_ID
                          WHERE ((BD.BOOK_NAME) LIKE ('%' + @BOOK_NAME + '%')or @BOOK_NAME = '') AND
                                (BC.BOOK_CLASS_ID = @BOOK_CLASS_NAME or @BOOK_CLASS_NAME = '') AND
                                (BD.BOOK_KEEPER = @USER_ENAME or @USER_ENAME = '') AND
                                (BD.BOOK_STATUS = @CODE_NAME or @CODE_NAME = '');";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BOOK_NAME", arg.BOOK_NAME == null ? string.Empty : arg.BOOK_NAME));
                cmd.Parameters.Add(new SqlParameter("@BOOK_CLASS_NAME", arg.BOOK_CLASS_NAME == null ? string.Empty : arg.BOOK_CLASS_NAME));
                cmd.Parameters.Add(new SqlParameter("@USER_ENAME", arg.USER_ENAME == null ? string.Empty : arg.USER_ENAME));
                cmd.Parameters.Add(new SqlParameter("@CODE_NAME", arg.CODE_NAME == null ? string.Empty : arg.CODE_NAME));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapLibraryDataToList(dt);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="libraryupdate"></param>
        public void UpdateLibrary(Models.LibraryUpdate libraryupdate)
        {
            string sql = @"UPDATE BOOK_DATA
                           SET BOOK_NAME = @BOOK_NAME , BOOK_AUTHOR = @BOOK_AUTHOR,
                               BOOK_PUBLISHER = @BOOK_PUBLISHER, BOOK_NOTE = @BOOK_NOTE, 
                               BOOK_BOUGHT_DATE = @BOOK_BOUGHT_DATE, BOOK_CLASS_ID = @BOOK_CLASS_NAME,
                               BOOK_STATUS = @BOOK_STATUS, BOOK_KEEPER = @BOOK_KEEPER
                           WHERE BOOK_ID = @BOOK_ID";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BOOK_NAME", libraryupdate.BOOK_NAME));
                cmd.Parameters.Add(new SqlParameter("@BOOK_AUTHOR", libraryupdate.BOOK_AUTHOR));
                cmd.Parameters.Add(new SqlParameter("@BOOK_PUBLISHER", libraryupdate.BOOK_PUBLISHER));
                cmd.Parameters.Add(new SqlParameter("@BOOK_NOTE", libraryupdate.BOOK_NOTE));
                cmd.Parameters.Add(new SqlParameter("@BOOK_BOUGHT_DATE", libraryupdate.BOOK_BOUGHT_DATE));
                cmd.Parameters.Add(new SqlParameter("@BOOK_CLASS_NAME", libraryupdate.BOOK_CLASS_NAME));
                cmd.Parameters.Add(new SqlParameter("@BOOK_STATUS", libraryupdate.CODE_NAME));
                cmd.Parameters.Add(new SqlParameter("@BOOK_KEEPER", libraryupdate.USER_NAME));
                cmd.Parameters.Add(new SqlParameter("@BOOK_ID", libraryupdate.BOOK_ID));
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                cmd.Transaction = trans;

                try
                {
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                }
                catch(Exception)
                {
                    trans.Rollback();
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
           
        }


        /// <summary>
        /// Map資料進List
        /// </summary>
        /// <param name="employeeData"></param>
        /// <returns></returns>
        private List<Models.Library> MapLibraryDataToList(DataTable employeeData)
        {
            List<Models.Library> result = new List<Library>();
            foreach (DataRow row in employeeData.Rows)
            {
                result.Add(new Library()
                {
                    BOOK_NAME = row["BOOK_NAME"].ToString(),
                    BOOK_CLASS_NAME = row["BOOK_CLASS_NAME"].ToString(),
                    USER_ENAME = row["USER_ENAME"].ToString(),
                    CODE_NAME = row["CODE_NAME"].ToString(),
                    BOOK_BOUGHT_DATE = row["BOOK_BOUGHT_DATE"].ToString(),
                    BOOK_ID = row["BOOK_ID"].ToString()
                });
            }
            return result;
        }


    }
}