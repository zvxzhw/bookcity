using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using bookcity.iservices.Services.Book;
using bookcity.entitys.models;
using bookcity.Extensions;
using bookcity.entitys.ViewModel;
using Microsoft.AspNetCore.Authorization;
using bookcity.entitys.Response;

namespace bookcity.api.Controllers.books
{
    [Route("api/books/[action]")]
    [ApiController]
   // [Authorize]
    public class booksController : ControllerBase
    {
        IBookServices _bookServices;
        ResponseModel response = ResponseModelFactory.CreateInstance;

        public booksController(IBookServices bookServices)
        {
            _bookServices = bookServices;
        }

        [HttpPost]
        public async Task<List<sysbook>> List()
        {
            return await _bookServices.getList();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Search(SearchParams search)
        {

            //var response = ResponseModelFactory.CreateInstance;

            //Guid guidUser = new Guid(AuthHelper.Cas.Jwt.GetJwtValue(HttpContext, JwtClaimTypes.GuidUser));
            //Dictionary<string, object> dataDc = await _dashboardServices.GetToDoData(guidUser, HttpContext);


            List<bookResult_ViewModel> mlist = await _bookServices.Search(search);
            response.SetData(mlist);
            response.SetSuccess();

            //string SqlQuery = "select !isnull(b.bookid)as addbox,a.* from sysbook as a left join (select * from sysbookbox where userid='" + userid + "') as b on a.id=b.bookid where isSTop=99 and bookImg is not null " + ifcase + " order by lastUpdate desc limit " + start + "," + over;


            //DataTable booklist = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
            //foreach (DataRow cdr in booklist.Rows)
            //{
            //    Dictionary<string, object> item = new Dictionary<string, object>();
            //    item.Add("id", cdr["id"].ToString());
            //    item.Add("bookName", cdr["bookName"].ToString());
            //    item.Add("lastUpdate", cdr["lastUpdate"].ToString());
            //    item.Add("author", cdr["author"].ToString());
            //    item.Add("intro", cdr["intro"].ToString());
            //    item.Add("category", cdr["category"].ToString());
            //    string bokImg = cdr.IsNull("bookImg") ? "/images/nocover.jpg" : cdr["bookImg"].ToString();
            //    item.Add("bookImg", bokImg);

            //    bool addbox = cdr["addbox"].ToString() == "1" ? true : false;
            //    item.Add("addbox", addbox);

            //    list.Add(item);
            //}

            //RDS.JsonResult = list;
            //return Json(RDS, JsonRequestBehavior.AllowGet);

            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoadList(int start, int over, string keywords, string userid)
        {
            

            return Ok(response);
            //RDS.Msg = "数据加载完成";
            //RDS.Status = 1;

            //List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            //string ifcase = string.IsNullOrEmpty(keywords) ? "" : " and (bookName like '%" + keywords + "%' or author like '%" + keywords + "%' ) ";

            //string SqlQuery = "select !isnull(b.bookid)as addbox,a.* from sysbook as a left join (select * from sysbookbox where userid='" + userid + "') as b on a.id=b.bookid where isSTop=99 and bookImg is not null " + ifcase + " order by lastUpdate desc limit " + start + "," + over;
            //DataTable booklist = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
            //foreach (DataRow cdr in booklist.Rows)
            //{
            //    Dictionary<string, object> item = new Dictionary<string, object>();
            //    item.Add("id", cdr["id"].ToString());
            //    item.Add("bookName", cdr["bookName"].ToString());
            //    item.Add("lastUpdate", cdr["lastUpdate"].ToString());
            //    item.Add("author", cdr["author"].ToString());
            //    item.Add("intro", cdr["intro"].ToString());
            //    item.Add("category", cdr["category"].ToString());

            //    bool addbox = cdr["addbox"].Equals(1) ? true : false;
            //    item.Add("addbox", addbox);
            //    string bokImg = cdr.IsNull("bookImg") ? "/images/nocover.jpg" : cdr["bookImg"].ToString();
            //    item.Add("bookImg", bokImg);
            //    list.Add(item);
            //}

            //RDS.JsonResult = list;
            //return Json(RDS, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 用户浏览记录
        /// </summary>
        /// <param name="idlist">书籍ID清单</param>
        /// <returns>书目清单</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UserHistory(string[] idlist)
        {


            return Ok(response);
 
            //if (idlist != null && idlist.Length > 0)
            //{
            //    string strids = string.Join("','", idlist);
            //    List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

            //    string SqlQuery = "select * from sysbook where id in('" + strids + "') order by lastUpdate desc limit 0,300";
            //    DataTable booklist = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
            //    foreach (DataRow cdr in booklist.Rows)
            //    {
            //        Dictionary<string, string> item = new Dictionary<string, string>();
            //        item.Add("id", cdr["id"].ToString());
            //        item.Add("bookName", cdr["bookName"].ToString());
            //        item.Add("lastUpdate", cdr["lastUpdate"].ToString());
            //        item.Add("author", cdr["author"].ToString());
            //        item.Add("intro", cdr["intro"].ToString());
            //        item.Add("category", cdr["category"].ToString());
            //        string bokImg = cdr.IsNull("bookImg") ? "/images/nocover.jpg" : cdr["bookImg"].ToString();
            //        item.Add("bookImg", bokImg);
            //        list.Add(item);
            //    }

            //    RDS.JsonResult = list;
            //}
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> findbook(string id, string userid)
        {
            //RDS.Msg = "数据加载完成";
            //RDS.Status = 1;

            ////List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

            //Dictionary<string, object> list = new Dictionary<string, object>();

            //string SqlQuery = "select !isnull(b.bookid)as addbox,a.* from sysbook as a left join (select * from sysbookbox where userid='" + userid + "') as b on a.id=b.bookid where a.id='" + id + "'";
            //DataTable booklist = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
            //foreach (DataRow cdr in booklist.Rows)
            //{
            //    Dictionary<string, object> item = new Dictionary<string, object>();
            //    item.Add("id", cdr["id"].ToString());
            //    item.Add("bookName", cdr["bookName"].ToString());
            //    item.Add("lastUpdate", cdr["lastUpdate"].ToString());
            //    item.Add("author", cdr["author"].ToString());
            //    item.Add("intro", cdr["intro"].ToString());
            //    item.Add("category", cdr["category"].ToString());

            //    item.Add("addbox", cdr["addbox"].ToString() == "1" ? true : false);

            //    string bokImg = cdr.IsNull("bookImg") ? "/images/nocover.jpg" : cdr["bookImg"].ToString();

            //    DataTable dtChapter = MySQL.ExecuteDataSet("select cno,chapter from sysbookchapter where bookid='" + id + "' order by cno asc").Tables[0];//"select chapter from sysbookchapter where cno in(select max(cno) from sysbookchapter where bookid='" + id + "' )"
            //    if (dtChapter.Rows.Count > 0)
            //    {
            //        item.Add("lastChapter", dtChapter.Rows[dtChapter.Rows.Count - 1]["chapter"].ToString());
            //        item.Add("firstChapterId", dtChapter.Rows[0]["cno"].ToString());
            //    }
            //    else
            //    {
            //        item.Add("lastChapter", "没有章节");
            //        item.Add("firstChapterId", "没有章节");
            //    }

            //    item.Add("bookImg", bokImg);
            //    list.Add("bookinfo", item);
            //    //ist.Add(item);
            //}

            //List<Dictionary<string, string>> recommands = new List<Dictionary<string, string>>();
            //SqlQuery = "select * from sysbook where isSTop=99 and bookImg is not null order by lastUpdate desc limit 0,5 ";
            //booklist = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
            //foreach (DataRow cdr in booklist.Rows)
            //{
            //    Dictionary<string, string> item = new Dictionary<string, string>();
            //    item.Add("id", cdr["id"].ToString());
            //    item.Add("bookName", cdr["bookName"].ToString());
            //    item.Add("lastUpdate", cdr["lastUpdate"].ToString());
            //    item.Add("author", cdr["author"].ToString());
            //    item.Add("intro", cdr["intro"].ToString());
            //    item.Add("category", cdr["category"].ToString());
            //    string bokImg = cdr.IsNull("bookImg") ? "/images/nocover.jpg" : cdr["bookImg"].ToString();
            //    item.Add("bookImg", bokImg);
            //    recommands.Add(item);
            //    //list.Add(item);
            //}

            //list.Add("recommands", recommands);

            //List<Dictionary<string, string>> chapters = new List<Dictionary<string, string>>();
            //SqlQuery = "select cno,chapter from sysbookchapter where bookid='" + id + "' order by cno asc  ";
            //booklist = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
            //foreach (DataRow cdr in booklist.Rows)
            //{
            //    Dictionary<string, string> item = new Dictionary<string, string>();
            //    item.Add("id", cdr["cno"].ToString());
            //    item.Add("chapter", cdr["chapter"].ToString());

            //    chapters.Add(item);
            //}

            //list.Add("chapters", chapters);

            //RDS.JsonResult = list;
            //return Json(RDS, JsonRequestBehavior.AllowGet);
            var response = ResponseModelFactory.CreateInstance;

            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ChapterSort(string id, string sort)
        {
            //RDS.Msg = "数据加载完成";
            //RDS.Status = 1;
            //sort = sort.ToUpper() == "TRUE" ? "desc" : "asc";
            //Dictionary<string, object> list = new Dictionary<string, object>();

            //List<Dictionary<string, string>> chapters = new List<Dictionary<string, string>>();
            //string SqlQuery = "select cno,chapter from sysbookchapter where bookid='" + id + "' order by cno " + sort;
            //DataTable booklist = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
            //foreach (DataRow cdr in booklist.Rows)
            //{
            //    Dictionary<string, string> item = new Dictionary<string, string>();
            //    item.Add("id", cdr["cno"].ToString());
            //    item.Add("chapter", cdr["chapter"].ToString());

            //    chapters.Add(item);
            //}

            //list.Add("chapters", chapters);

            //RDS.JsonResult = list;
            //return Json(RDS, JsonRequestBehavior.AllowGet);

            var response = ResponseModelFactory.CreateInstance;
            return Ok(response);
        }

        [HttpPost]

        public async Task<IActionResult> bookBoxList()
        {
            //RDS.Msg = "操作完成";
            //RDS.Status = 1;

            //List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

            ////string ifcase = string.IsNullOrEmpty(keywords) ? "" : " and (bookName like '%" + keywords + "%' or author like '%" + keywords + "%' )";

            //string SqlQuery = "select * from sysbook where id in(select distinct bookid from sysbookbox where userid='" + uid + "' ) order by lastUpdate desc limit 0,500";

            //DataTable booklist = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
            //foreach (DataRow cdr in booklist.Rows)
            //{
            //    Dictionary<string, string> item = new Dictionary<string, string>();
            //    item.Add("id", cdr["id"].ToString());
            //    item.Add("bookName", cdr["bookName"].ToString());
            //    item.Add("lastUpdate", cdr["lastUpdate"].ToString());
            //    item.Add("author", cdr["author"].ToString());
            //    item.Add("intro", cdr["intro"].ToString());
            //    item.Add("category", cdr["category"].ToString());
            //    string bokImg = cdr.IsNull("bookImg") ? "/images/nocover.jpg" : cdr["bookImg"].ToString();
            //    item.Add("bookImg", bokImg);
            //    list.Add(item);
            //}

            //RDS.JsonResult = list;
            //return Json(RDS, JsonRequestBehavior.AllowGet);
            var response = ResponseModelFactory.CreateInstance;
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> addbookBox(string bookid)
        {
            //RDS.Msg = "操作完成";
            //RDS.Status = 1;
            //int affect = 0;
            //DataTable dts = MySQL.ExecuteDataSet("select * from sysbookbox where bookid='" + bookid + "' and userid ='" + uid + "'").Tables[0];
            //if (dts.Rows.Count > 0)
            //{
            //    affect = MySQL.ExecuteNonQuery("delete from sysbookbox where bookid='" + bookid + "' and userid ='" + uid + "'");
            //    if (affect > 0) RDS.JsonResult = false;
            //}
            //else
            //{
            //    affect = MySQL.ExecuteNonQuery("insert into sysbookbox(id,bookid,userid) values(uuid(),'" + bookid + "','" + uid + "')");
            //    if (affect > 0) RDS.JsonResult = true;
            //}

            //if (affect > 0)
            //{
            //    RDS.Status = 1;
            //    RDS.Msg = "已添加到书架!";
            //}
            //else
            //{
            //    RDS.Status = 0;
            //    RDS.Msg = "操作失败，无法更新数据库";
            //}
            //return Json(RDS, JsonRequestBehavior.AllowGet);

            var response = ResponseModelFactory.CreateInstance;
            return Ok(response);

        }
    }
}