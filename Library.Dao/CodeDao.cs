using Library.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Library.Dao
{
    public class CodeDao : ICodeDao
    {
        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return Library.Common.ConfigTool.GetDBConnectionString("DBConn");
        }


        /// <summary>
        /// 取得圖書類別
        /// </summary>
        /// <param name="BOOK_CLASS_NAME"></param>
        /// <returns></returns>
        public List<SelectListItem> Getbook_class()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT BOOK_CLASS_ID AS CodeId, BOOK_CLASS_NAME AS CodeName 
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
            string sql = @"SELECT CODE_ID AS CodeId, CODE_NAME AS CodeName 
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
        public List<SelectListItem> Getbook_keeper()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT USER_ID AS CodeId, USER_ENAME AS CodeName 
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
        public List<SelectListItem> Getbook_keeper_CName_EName()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT USER_ID AS CodeId , USER_ENAME + '-'+ USER_CNAME AS CodeName 
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


    }
}
