using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using Library.Model;

namespace Library.Dao
{
    public interface ICodeDao
    {

        List<SelectListItem> Getbook_keeper();
        List<SelectListItem> Getbook_keeper_CName_EName();
        List<SelectListItem> Getbook_class();
        List<SelectListItem> Getbook_status();

    }
}