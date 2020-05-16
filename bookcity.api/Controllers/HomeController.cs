using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace bookcity.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        ResultData RDS = new ResultData();

        [HttpPost]
        public ActionResult Search(int start, int over, string keywords,string userid)
        {
            RDS.Msg = "数据加载完成";
            RDS.Status = 1;

            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            string ifcase = string.IsNullOrEmpty(keywords) ? "" : " and (bookName like '%" + keywords + "%' or author like '%" + keywords + "%' )";

            //string SqlQuery = "select * from sysbook where 1=1 " + ifcase + " order by lastUpdate desc limit " + start + "," + over;
            string SqlQuery = "select !isnull(b.bookid)as addbox,a.* from sysbook as a left join (select * from sysbookbox where userid='" + userid + "') as b on a.id=b.bookid where isSTop=99 and bookImg is not null " + ifcase + " order by lastUpdate desc limit " + start + "," + over;



            DataTable booklist = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
            foreach (DataRow cdr in booklist.Rows)
            {
                Dictionary<string, object> item = new Dictionary<string, object>();
                item.Add("id", cdr["id"].ToString());
                item.Add("bookName", cdr["bookName"].ToString());
                item.Add("lastUpdate", cdr["lastUpdate"].ToString());
                item.Add("author", cdr["author"].ToString());
                item.Add("intro", cdr["intro"].ToString());
                item.Add("category", cdr["category"].ToString());
                string bokImg = cdr.IsNull("bookImg") ? "/images/nocover.jpg" : cdr["bookImg"].ToString();
                item.Add("bookImg", bokImg);
                
                bool addbox = cdr["addbox"].ToString()=="1" ? true : false;
                item.Add("addbox", addbox);

                list.Add(item);
            }

            RDS.JsonResult = list;
            return Json(RDS, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult findbook(string id,string userid)
        {
            RDS.Msg = "数据加载完成";
            RDS.Status = 1;

            //List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

            Dictionary<string, object> list = new Dictionary<string, object>();

            string SqlQuery = "select !isnull(b.bookid)as addbox,a.* from sysbook as a left join (select * from sysbookbox where userid='"+userid+"') as b on a.id=b.bookid where a.id='"+id+"'";
            DataTable booklist = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
            foreach (DataRow cdr in booklist.Rows)
            {
                Dictionary<string, object> item = new Dictionary<string, object>();
                item.Add("id", cdr["id"].ToString());
                item.Add("bookName", cdr["bookName"].ToString());
                item.Add("lastUpdate", cdr["lastUpdate"].ToString());
                item.Add("author", cdr["author"].ToString());
                item.Add("intro", cdr["intro"].ToString());
                item.Add("category", cdr["category"].ToString());
                
                item.Add("addbox", cdr["addbox"].ToString()=="1"?true:false);

                string bokImg = cdr.IsNull("bookImg") ? "/images/nocover.jpg" : cdr["bookImg"].ToString();

                DataTable dtChapter = MySQL.ExecuteDataSet("select cno,chapter from sysbookchapter where bookid='" + id + "' order by cno asc").Tables[0];//"select chapter from sysbookchapter where cno in(select max(cno) from sysbookchapter where bookid='" + id + "' )"
                if (dtChapter.Rows.Count > 0)
                {
                    item.Add("lastChapter", dtChapter.Rows[dtChapter.Rows.Count - 1]["chapter"].ToString());
                    item.Add("firstChapterId", dtChapter.Rows[0]["cno"].ToString());
                }
                else
                {
                    item.Add("lastChapter", "没有章节");
                    item.Add("firstChapterId", "没有章节");
                }

                item.Add("bookImg", bokImg);
                list.Add("bookinfo",item);
                //ist.Add(item);
            }

            List<Dictionary<string, string>> recommands = new List<Dictionary<string, string>>();
            SqlQuery = "select * from sysbook where isSTop=99 and bookImg is not null order by lastUpdate desc limit 0,5 ";
            booklist = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
            foreach (DataRow cdr in booklist.Rows)
            {
                Dictionary<string, string> item = new Dictionary<string, string>();
                item.Add("id", cdr["id"].ToString());
                item.Add("bookName", cdr["bookName"].ToString());
                item.Add("lastUpdate", cdr["lastUpdate"].ToString());
                item.Add("author", cdr["author"].ToString());
                item.Add("intro", cdr["intro"].ToString());
                item.Add("category", cdr["category"].ToString());
                string bokImg = cdr.IsNull("bookImg") ? "/images/nocover.jpg" : cdr["bookImg"].ToString();
                item.Add("bookImg", bokImg);
                recommands.Add(item);
                //list.Add(item);
            }

            list.Add("recommands", recommands);

            List<Dictionary<string, string>> chapters = new List<Dictionary<string, string>>();
            SqlQuery = "select cno,chapter from sysbookchapter where bookid='"+id+"' order by cno asc  ";
            booklist = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
            foreach (DataRow cdr in booklist.Rows)
            {
                Dictionary<string, string> item = new Dictionary<string, string>();
                item.Add("id", cdr["cno"].ToString());
                item.Add("chapter", cdr["chapter"].ToString());

                chapters.Add(item);
            }

            list.Add("chapters", chapters);

            RDS.JsonResult = list;
            return Json(RDS, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChapterSort(string id,string sort)
        {
            RDS.Msg = "数据加载完成";
            RDS.Status = 1;
            sort= sort.ToUpper() == "TRUE" ? "desc" : "asc";
            Dictionary<string, object> list = new Dictionary<string, object>();

            List<Dictionary<string, string>> chapters = new List<Dictionary<string, string>>();
            string SqlQuery = "select cno,chapter from sysbookchapter where bookid='" + id + "' order by cno "+sort;
            DataTable booklist = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
            foreach (DataRow cdr in booklist.Rows)
            {
                Dictionary<string, string> item = new Dictionary<string, string>();
                item.Add("id", cdr["cno"].ToString());
                item.Add("chapter", cdr["chapter"].ToString());

                chapters.Add(item);
            }

            list.Add("chapters", chapters);

            RDS.JsonResult = list;
            return Json(RDS, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LoadList(int start,int over,string keywords,string userid)
        {
            RDS.Msg = "数据加载完成";
            RDS.Status = 1;

            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            string ifcase = string.IsNullOrEmpty(keywords) ? "" : " and (bookName like '%"+keywords+"%' or author like '%"+keywords+"%' ) ";

            string SqlQuery = "select !isnull(b.bookid)as addbox,a.* from sysbook as a left join (select * from sysbookbox where userid='"+userid+"') as b on a.id=b.bookid where isSTop=99 and bookImg is not null " + ifcase + " order by lastUpdate desc limit " + start + "," + over;
            DataTable booklist = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
            foreach (DataRow cdr in booklist.Rows)
            {
                Dictionary<string, object> item = new Dictionary<string, object>();
                item.Add("id",cdr["id"].ToString());
                item.Add("bookName", cdr["bookName"].ToString());
                item.Add("lastUpdate", cdr["lastUpdate"].ToString());
                item.Add("author", cdr["author"].ToString());
                item.Add("intro", cdr["intro"].ToString());
                item.Add("category", cdr["category"].ToString());

                bool addbox = cdr["addbox"].Equals(1) ? true : false;
                item.Add("addbox", addbox);
                string bokImg = cdr.IsNull("bookImg") ? "/images/nocover.jpg" : cdr["bookImg"].ToString();
                item.Add("bookImg", bokImg);
                list.Add(item);
            }
            
            RDS.JsonResult = list;
            return Json(RDS, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UserHistory(string[] idlist)
        {
            RDS.Msg = "数据加载完成";
            RDS.Status = 1;
            if (idlist!=null && idlist.Length > 0)
            {
                string strids=string.Join("','",idlist);
                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

                string SqlQuery = "select * from sysbook where id in('" + strids + "') order by lastUpdate desc limit 0,300";
                DataTable booklist = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
                foreach (DataRow cdr in booklist.Rows)
                {
                    Dictionary<string, string> item = new Dictionary<string, string>();
                    item.Add("id", cdr["id"].ToString());
                    item.Add("bookName", cdr["bookName"].ToString());
                    item.Add("lastUpdate", cdr["lastUpdate"].ToString());
                    item.Add("author", cdr["author"].ToString());
                    item.Add("intro", cdr["intro"].ToString());
                    item.Add("category", cdr["category"].ToString());
                    string bokImg = cdr.IsNull("bookImg") ? "/images/nocover.jpg" : cdr["bookImg"].ToString();
                    item.Add("bookImg", bokImg);
                    list.Add(item);
                }

                RDS.JsonResult = list;
            }

            return Json(RDS, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UserAlert(string uid)
        {
            RDS.Msg = "数据加载完成";
            RDS.Status = 1;
            string SqlQuery = "select * from sysmessage WHERE ISNULL(receiveUser) ";
            if (!string.IsNullOrEmpty(uid))
            {
                SqlQuery += " or receiveUser='" + uid + "' ";
            }

            SqlQuery += "  order by addTime desc limit 0,600";

           
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

            DataTable booklist = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
            foreach (DataRow cdr in booklist.Rows)
            {
                Dictionary<string, string> item = new Dictionary<string, string>();
                item.Add("id", cdr["id"].ToString());
                item.Add("title", cdr["title"].ToString());
                item.Add("msgType", cdr["msgType"].ToString());
                item.Add("addTime", cdr["addTime"].ToString());

                list.Add(item);
            }

            RDS.JsonResult = list;

            return Json(RDS, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult addfeedback(string uid, string title, string body,string mobile,string linkman)
        {
            RDS.Msg = "操作完成";
            RDS.Status = 1;

            int affect = MySQL.ExecuteNonQuery("insert into sysfeedback(id,uid,title,body,addtime,mobile,linkman) values(uuid(),'" + uid + "','" + title + "','" + body + "',now(),'" + mobile + "','"+linkman+"')");
            if (affect > 0)
            {
                RDS.Status = 1;
                RDS.Msg = "感谢您的建议，我们会持续改进!";
            }
            else
            {
                RDS.Status = 0;
                RDS.Msg = "操作失败，无法更新数据库";
            }
            return Json(RDS, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult addbookBox(string uid, string bookid)
        {
            RDS.Msg = "操作完成";
            RDS.Status = 1;
            int affect = 0;
            DataTable dts = MySQL.ExecuteDataSet("select * from sysbookbox where bookid='"+bookid+"' and userid ='"+uid+"'").Tables[0];
            if (dts.Rows.Count > 0)
            {
                affect = MySQL.ExecuteNonQuery("delete from sysbookbox where bookid='" + bookid + "' and userid ='" + uid + "'");
                if(affect>0)    RDS.JsonResult = false;
            }
            else
            {
                affect = MySQL.ExecuteNonQuery("insert into sysbookbox(id,bookid,userid) values(uuid(),'" + bookid + "','" + uid + "')");
                if (affect > 0) RDS.JsonResult = true;
            }

            if (affect > 0)
            {
                RDS.Status = 1;
                RDS.Msg = "已添加到书架!";
            }
            else
            {
                RDS.Status = 0;
                RDS.Msg = "操作失败，无法更新数据库";
            }
            return Json(RDS, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult bookBoxList(string uid)
        {
            RDS.Msg = "操作完成";
            RDS.Status = 1;

            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

            //string ifcase = string.IsNullOrEmpty(keywords) ? "" : " and (bookName like '%" + keywords + "%' or author like '%" + keywords + "%' )";

            string SqlQuery = "select * from sysbook where id in(select distinct bookid from sysbookbox where userid='" + uid + "' ) order by lastUpdate desc limit 0,500";

            DataTable booklist = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
            foreach (DataRow cdr in booklist.Rows)
            {
                Dictionary<string, string> item = new Dictionary<string, string>();
                item.Add("id", cdr["id"].ToString());
                item.Add("bookName", cdr["bookName"].ToString());
                item.Add("lastUpdate", cdr["lastUpdate"].ToString());
                item.Add("author", cdr["author"].ToString());
                item.Add("intro", cdr["intro"].ToString());
                item.Add("category", cdr["category"].ToString());
                string bokImg = cdr.IsNull("bookImg") ? "/images/nocover.jpg" : cdr["bookImg"].ToString();
                item.Add("bookImg", bokImg);
                list.Add(item);
            }

            RDS.JsonResult = list;
            return Json(RDS, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult StartRead(string chapterid)
        {
            RDS.Msg = "数据加载完成";
            RDS.Status = 1;

            Dictionary<string, string> item = new Dictionary<string, string>();
            Dictionary<string, object> bookInfo = new Dictionary<string, object>();

            string SqlQuery = "select * from sysbookchapter where cno=" + chapterid + "";
            DataTable booklist = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
            foreach (DataRow cdr in booklist.Rows)
            {
                item.Add("id", cdr["cno"].ToString());
                item.Add("title", cdr["chapter"].ToString());
                item.Add("intro", cdr["intro"].ToString());
                item.Add("bookid", cdr["bookid"].ToString());
            }

            bookInfo.Add("bookData", item);
            if (item.Count > 3)
            {
                List<Dictionary<string, string>> bookChapters = new List<Dictionary<string, string>>();
                SqlQuery = "select * from sysbookchapter where bookid='" + item["bookid"] + "' order by cno asc ";
                booklist = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
                for (int i = 0; i < booklist.Rows.Count; i++)
                {

                    Dictionary<string, string> chapter = new Dictionary<string, string>();
                    chapter.Add("chapterid", booklist.Rows[i]["cno"].ToString());
                    chapter.Add("chaptername", booklist.Rows[i]["chapter"].ToString());
                    bookChapters.Add(chapter);

                    if (booklist.Rows[i]["cno"].ToString().Equals(item["id"]))
                    {
                        item.Add("previd", "");
                        if (i > 0)
                        {
                            item["previd"] = booklist.Rows[i - 1]["cno"].ToString();
                        }

                        if (i + 1 < booklist.Rows.Count)
                        {
                            item.Add("nextid", booklist.Rows[i + 1]["cno"].ToString());
                            //break;
                        }
                    }
                }

                
                bookInfo.Add("bookChapters", bookChapters);
               
                DataTable dtBooks = MySQL.ExecuteDataSet("select * from sysbook where id in(select distinct bookid from sysbookchapter where cno='" + chapterid + "')").Tables[0];

                bookInfo.Add("bookName", dtBooks.Rows[0]["bookName"].ToString());
                
            }
            RDS.JsonResult = bookInfo;
            return Json(RDS, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult goChapter(string chapterid)
        {
            RDS.Msg = "数据加载完成";
            RDS.Status = 1;

            Dictionary<string, string> item = new Dictionary<string, string>();

            string SqlQuery = "select * from sysbookchapter where cno='" + chapterid + "'";
            DataTable booklist = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
            foreach (DataRow cdr in booklist.Rows)
            {
                item.Add("id", cdr["cno"].ToString());
                item.Add("title", cdr["chapter"].ToString());
                item.Add("intro", cdr["intro"].ToString());
                item.Add("bookid", cdr["bookid"].ToString());
            }

            SqlQuery = "select * from sysbookchapter where bookid='" + item["bookid"] + "' order by cno asc ";
            booklist = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
            for (int i = 0; i < booklist.Rows.Count; i++)
            {
                Dictionary<string, string> chapter = new Dictionary<string, string>();
                chapter.Add("chapterid", booklist.Rows[i]["cno"].ToString());
                chapter.Add("chaptername", booklist.Rows[i]["chapter"].ToString());

                if (booklist.Rows[i]["cno"].ToString().Equals(item["id"]))
                {
                    item.Add("previd", "");
                    if (i > 0)
                    {
                        item["previd"] = booklist.Rows[i - 1]["cno"].ToString();
                    }

                    if (i + 1 < booklist.Rows.Count)
                    {
                        item.Add("nextid", booklist.Rows[i + 1]["cno"].ToString());
                    }
                }
            }

            Dictionary<string, object> bookInfo = new Dictionary<string, object>();
            bookInfo.Add("bookData", item);

            RDS.JsonResult = bookInfo;
            return Json(RDS, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult register(string userName,string password,string vcode,string email)
        {
            RDS.Msg = "数据加载完成";
            RDS.Status = 1;

           password = Utility.EncryStore.MakeMd5(password);

            string SqlQuery = "select * from sysuser where userName='" + userName + "' and password='" + password + "'";
            DataTable users = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
            if (users.Rows.Count > 0)
            {
                RDS.Msg = "注册失败，此注册帐号信息已存在";
                RDS.Status = 0;
            }
            else
            {
                string uuid=Guid.NewGuid().ToString("N");
                MySqlParameter[] parms=new MySqlParameter[6];
                parms[0]=new MySqlParameter("@id",uuid);
                parms[1]=new MySqlParameter("@userName",userName);
                parms[2] = new MySqlParameter("@password", password);
                parms[3] = new MySqlParameter("@vcode", vcode);
                parms[4] = new MySqlParameter("@email", email);
                parms[5] = new MySqlParameter("@addtime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                int affect = MySQL.ExecuteNonQuery("insert into sysuser(id,userName,password,vcode,email,addtime)values(@id,@userName,@password,@vcode,@email,@addtime)", parms);
                if (affect > 0)
                {
                    RDS.Msg = "注册成功";
                    RDS.Status = 1;

                    RDS.Data = uuid + ":" + DateTime.Now.AddHours(24).ToString("yyyy-MM-dd HH:mm:ss");
                    RDS.Data = Utility.EncryStore.MakeMd5(RDS.Data);

                    MySQL.ExecuteNonQuery("update sysuser set token='" + RDS.Data + "',logincount=logincount+1,lastLogin='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',online=1,expired='" + DateTime.Now.AddHours(24).ToString("yyyy-MM-dd HH:mm:ss") + "' where id='" + uuid + "'");

                    Session.Add("token", RDS.Data);

                    Dictionary<string, string> items = new Dictionary<string, string>();
                    items.Add("id", uuid);
                    items.Add("userName", userName);
                    items.Add("email", email);
                    RDS.JsonResult = items;
                }
                else
                {
                    RDS.Msg = "注册失败";
                    RDS.Status = 0;
                }
            }

            return Json(RDS, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult checkLogin(string userName,string password,string code)
        {
            RDS.Msg = "数据加载完成";
            RDS.Status = 1;

            password = Utility.EncryStore.MakeMd5(password);
            string SqlQuery = "select * from sysuser where userName='" + userName + "' and password='" + password + "'";
            DataTable users = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
            if (users.Rows.Count > 0)
            {
                RDS.Msg = "登录成功";
                RDS.Status = 1;
                RDS.Data = Utility.EncryStore.MakeMd5(users.Rows[0]["id"].ToString() + ":" + DateTime.Now.AddHours(24).ToString("yyyy-MM-dd HH:mm:ss"));

                MySQL.ExecuteNonQuery("update sysuser set token='" + RDS.Data + "',logincount=logincount+1,lastLogin='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',online=1,expired='" + DateTime.Now.AddHours(24).ToString("yyyy-MM-dd HH:mm:ss") + "' where id='" + users.Rows[0]["id"] + "'");

                Dictionary<string, string> items = new Dictionary<string, string>();
                items.Add("id", users.Rows[0]["id"].ToString());
                items.Add("userName",users.Rows[0]["userName"].ToString());
                items.Add("email", users.Rows[0]["email"].ToString());
                RDS.JsonResult = items;

                Session.Add("token", RDS.Data);
            }
            else
            {
                RDS.Msg = "登录失败";
                RDS.Status = 0;
                RDS.Data = string.Empty;
            }

            return Json(RDS, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult loginout(string id)
        {
            RDS.Msg = "数据加载完成";
            RDS.Status = 1;

            Session.Abandon();

            MySQL.ExecuteNonQuery("update sysuser set online=0 where id='" + id + "'");


            //string SqlQuery = "select * from sysuser where userName='" + userName + "' and password='" + password + "'";
            //DataTable users = MySQL.ExecuteDataSet(SqlQuery).Tables[0];
            //if (users.Rows.Count > 0)
            //{
            //    RDS.Msg = "登录成功";
            //    RDS.Status = 1;
            //    RDS.Data = users.Rows[0]["id"].ToString() + ":" + DateTime.Now.AddHours(24).ToString("yyyy-MM-dd HH:mm:ss");
            //    Dictionary<string, string> items = new Dictionary<string, string>();
            //    items.Add("id", users.Rows[0]["id"].ToString());
            //    items.Add("userName", users.Rows[0]["userName"].ToString());
            //    RDS.JsonResult = items;
            //}
            //else
            //{
            //    RDS.Msg = "登录失败";
            //    RDS.Status = 0;
            //    RDS.Data = string.Empty;
            //}

            return Json(RDS, JsonRequestBehavior.AllowGet);
        }
    }
}
