using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Library.Service
{
    public class CodeService : ICodeService
    {
        private Library.Dao.ICodeDao codeDao { get; set; }

        public List<SelectListItem> Getbook_class()
        {
            return codeDao.Getbook_class();
        }

        public List<SelectListItem> Getbook_status()
        {
            return codeDao.Getbook_status();
        }

        public List<SelectListItem> Getbook_keeper()
        {
            return codeDao.Getbook_keeper();
        }

        public List<SelectListItem> Getbook_keeper_CName_EName()
        {
            return codeDao.Getbook_keeper_CName_EName();
        }


    }

}



