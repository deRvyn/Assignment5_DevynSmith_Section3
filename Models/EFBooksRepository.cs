using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment5_DevynSmith_Section3.Models
{
    public class EFBooksRepository : IBooksRepository
    {
        private BooksDbContext _context;

        //Constructor
        public EFBooksRepository (BooksDbContext context)
        {
            _context = context;
        }
        public IQueryable<Book> Books => _context.Books;
    }
}
