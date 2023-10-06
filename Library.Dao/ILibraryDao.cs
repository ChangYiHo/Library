using System.Collections.Generic;
using Library.Model;
using System.Data;

namespace Library.Dao
{
    public interface ILibraryDao
    {
        void DeleteBookByID(string bookId);
        List<Model.Library> GetLibraryByCondtioin(LibrarySearch arg);
        int InsertLibrary(LibraryInsert libraryinsert);
        void UpdateLibrary(LibraryUpdate libraryupdate);
        List<LibraryUpdate> FindBookData(string bookId);
        List<LibraryUpdate> MapBookData(DataTable dt);
    }
}