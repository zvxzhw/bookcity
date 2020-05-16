using bookcity.entitys.models;
using bookcity.entitys.ViewModel;
using bookcity.irepository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bookcity.irepository.Book
{
    public interface IBookRepository:IBaseRepository<sysbook>
    {
        Task<List<sysbook>> getBooks(List<int?> lst);

        Task<List<bookResult_ViewModel>> Search(SearchParams searchData);
    }
}
