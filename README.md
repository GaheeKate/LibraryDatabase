# LibraryDatabase
ASP.NET MVC using Entity Framework



---------------------------------------
Today I studied ASP.NET MVC
- Layout: index.cshtml -> Method @RenderBody()
- prop tab tab
- call db with conditions to reduce loading time.
 > books = db.Books.Where(x => x.Title.Contains(keyword)).ToList();
