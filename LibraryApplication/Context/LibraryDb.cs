using LibraryApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryApplication.Context
{
    public class LibraryDb : DbContext
    {
        public LibraryDb() : base("name=DBCS") { }

        public DbSet<Book> Books { get; set; }

    }
}