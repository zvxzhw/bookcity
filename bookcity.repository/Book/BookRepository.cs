using bookcity.entitys.models;
using bookcity.irepository.Book;
using bookcity.irepository.UnitOfWork;
using bookcity.repository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using SqlSugar;
using bookcity.entitys.ViewModel;

namespace bookcity.repository.Book
{
    public class BookRepository : BaseRepository<sysbook>, IBookRepository
    {
        public BookRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<List<sysbook>> getBooks(List<int?> lstStatus)
        {
            
            return (await this.Query(t => lstStatus.Contains(t.isStop))).Take(100).ToList();
        }

        public Task<List<bookResult_ViewModel>> Search(SearchParams searchData)
        {
            string userid = searchData.userid??string.Empty;
            int pageIndex = searchData.pageIndex == null ? 1 : Convert.ToInt32(searchData.pageIndex);
            int pageSize = searchData.pageSize == null ? 50 : Convert.ToInt32(searchData.pageSize);

            var list = base.Db.Queryable<sysbook, sysbookbox>((book, bbox) => new object[] {
            JoinType.Left,book.id==bbox.bookid
            })
                .Where((book, bbox) => book.isStop == 99 && book.bookImg != null);

            if (!string.IsNullOrEmpty(searchData.keywords)) 
            {
                searchData.keywords = searchData.keywords.Trim();
                list = list.Where(book => book.author.Contains(searchData.keywords) || book.bookName.Contains(searchData.keywords));
            }
            //if (condition.ContainsKey("userid")) { list = list.Where((book, bbox) => bbox.userid== condition["userid"]); }

            //base.Db.Ado.ExecuteCommand("Set Names 'utf8'", new SugarParameter[] { });
            return list.Select((book, bbox) => new bookResult_ViewModel
            {
                id = book.id,
                author = book.author,
                bookName =book.bookName,
                category =book.category,
                intro =book.intro,
                bookImg = book.bookImg,
                lastUpdate = book.lastUpdate,               
                addbox = bbox.bookid == null ? true : false
            })
                .OrderBy("lastUpdate desc").Skip((pageIndex-1)* pageSize).Take(pageSize).ToListAsync();
        }
    }
}
