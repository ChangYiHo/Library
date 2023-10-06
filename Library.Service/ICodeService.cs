using System.Collections.Generic;
using System.Web.Mvc;
using Library.Model;

namespace Library.Service
{
    public interface ICodeService
    {
        
        List<SelectListItem> Getbook_keeper();
        List<SelectListItem> Getbook_keeper_CName_EName();
        List<SelectListItem> Getbook_class();
        List<SelectListItem> Getbook_status();
    }
}