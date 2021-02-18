using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment5_DevynSmith_Section3.Models
{
    //creates the database respository to populate the data and pass it into the index
    public class EFBooksRepository : IBooksRepository
    {
        private BooksDbContext _context;

        //Constructor for the class
        public EFBooksRepository (BooksDbContext context)
        {
            _context = context;
        }
        public IQueryable<Book> Books => _context.Books;
    }
}
