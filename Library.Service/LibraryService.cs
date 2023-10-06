using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public class LibraryService : ILibraryService
    {
        private Library.Dao.ILibraryDao libraryDao { get; set; }

        public int InsertLibrary(Library.Model.LibraryInsert libraryinsert)
        {
            return libraryDao.InsertLibrary(libraryinsert);
        }

        public void DeleteBookByID(string bookId)
        {
            libraryDao.DeleteBookByID(bookId);
        }
        public List<Library.Model.Library> GetLibraryByCondtioin(Library.Model.LibrarySearch arg)
        {
            return libraryDao.GetLibraryByCondtioin(arg);
        }
        public void UpdateLibrary(Library.Model.LibraryUpdate libraryupdate)
        {
            libraryDao.UpdateLibrary(libraryupdate);
        }
        public List<Library.Model.LibraryUpdate> FindBookData(string bookId)
        {
            return libraryDao.FindBookData(bookId);
        }
    }
}

