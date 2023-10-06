using System.Collections.Generic;
using Library.Model;

namespace Library.Service
{
    public interface ILibraryService
    {
        void DeleteBookByID(string bookId);
        List<Model.Library> GetLibraryByCondtioin(LibrarySearch arg);
        int InsertLibrary(LibraryInsert libraryinsert);
        void UpdateLibrary(LibraryUpdate libraryupdate);
        List<LibraryUpdate> FindBookData(string bookId);
    }
}