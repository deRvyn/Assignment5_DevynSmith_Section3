﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment6_DevynSmith_Section3.Models
{
    //gives the context to setup the database
    public class BooksDbContext : DbContext
    {
        public BooksDbContext (DbContextOptions<BooksDbContext> options) : base (options)
        {

        }

        public DbSet<Book> Books { get; set; }
    }
}
