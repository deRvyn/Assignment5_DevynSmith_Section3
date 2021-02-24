using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment6_DevynSmith_Section3.Models
{
    //creates the ibooks repo
    public interface IBooksRepository
    {
        IQueryable<Book> Books { get; }
    }
}
