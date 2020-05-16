using bookcity.entitys.models;
using bookcity.entitys.ViewModel;
using bookcity.iservices.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bookcity.iservices.Services.Book
{
    public interface IBookServices:IBaseServices<object>
    {
        Task<List<sysbook>> getList();

        Task<List<bookResult_ViewModel>> Search(SearchParams searchData);
    }
}
