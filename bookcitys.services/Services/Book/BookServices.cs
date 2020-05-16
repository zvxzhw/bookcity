using bookcity.entitys.models;
using bookcity.entitys.ViewModel;
using bookcity.irepository.Book;
using bookcity.iservices.Services.Book;
using bookcitys.services.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bookcitys.services.Services.Books
{
    public class BookServices :BaseServices<object>,IBookServices
    {
        readonly IBookRepository _bookRepository;
        public BookServices(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<sysbook>> getList()
        {
            List<int?> lst = new List<int?>() { 1,99,3 };
            return await _bookRepository.getBooks(lst);
        }

        public async Task<List<bookResult_ViewModel>> Search(SearchParams searchData)
        {
            List<int?> lst = new List<int?>() { 1, 99, 3 };
            return await _bookRepository.Search(searchData);
        }
    }
}
