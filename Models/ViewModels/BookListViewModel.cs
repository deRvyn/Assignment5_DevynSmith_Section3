using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment6_DevynSmith_Section3.Models.ViewModels
{
    public class BookListViewModel
    {
        //giving the data that is needed to display on Index.html
        public IEnumerable<Book> Books { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
