using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LibraryApplication.Context;
using LibraryApplication.Models;

namespace LibraryApplication.Controllers
{
    public class HomeController : Controller
    {
        private LibraryDb db = new LibraryDb();

        // GET: Home
        public ActionResult Index()
        {
            int maxListCount = 3;
            int pageNum = 1;
            string keyword = Request.QueryString["keyword"] ?? string.Empty;
            string SearchList = Request.QueryString["SearchList"] ?? string.Empty;
            int totalCount = 0;

            if (Request.QueryString["page"] != null)
                pageNum = Convert.ToInt32(Request.QueryString["page"]);


            //LINQ Order by before using skip "Select * from Books order by Book_U desc"
            var books = new List<Book>();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                books= db.Books.OrderBy(x => x.Book_U)
                    .Skip((pageNum - 1) * maxListCount)
                    .Take(maxListCount).ToList();
                totalCount = db.Books.Count();
            }
            else {


                switch (SearchList)
                {
                    case "Title":
                        books = db.Books.Where(x => x.Title.Contains(keyword)).ToList();
                        totalCount= db.Books.Where(x => x.Title.Contains(keyword)).Count();
                        break;
                    case "Writer":
                        books = db.Books.Where(x => x.Writer.Contains(keyword)).ToList();
                        totalCount = db.Books.Where(x => x.Writer.Contains(keyword)).Count();

                        break;
                    case "Publisher":
                        books = db.Books.Where(x => x.Publisher.Contains(keyword)).ToList();
                        totalCount = db.Books.Where(x => x.Publisher.Contains(keyword)).Count();

                        break;
                }


                books = books.OrderBy(x => x.Book_U)
                        .Skip((pageNum - 1) * maxListCount)
                        .Take(maxListCount).ToList();
         

            }
            ViewBag.Page = pageNum;
            ViewBag.TotalCount = totalCount;
            ViewBag.MaxListCount = maxListCount;
            ViewBag.SearchList = SearchList;
            ViewBag.Keyword = keyword;


            // string query = "SELECT * FROM Books";
            return View(books);
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Book_U,Title,Writer,Summary,Publisher,Published_date")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Home/Edit/5
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Book_U,Title,Writer,Summary,Publisher,Published_date")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
