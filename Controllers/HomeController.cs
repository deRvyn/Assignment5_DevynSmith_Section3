using Assignment7_DevynSmith_Section3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Assignment7_DevynSmith_Section3.Models.ViewModels;

namespace Assignment7_DevynSmith_Section3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IBooksRepository _repository;

        //number of items per page in pagination
        public int PageSize = 5;

        //constructor for the logger and repository
        public HomeController(ILogger<HomeController> logger, IBooksRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        //index page that uses the repository to get the book data from the database
        public IActionResult Index(string category, string classification, int page = 1)
        {
            return View(new BookListViewModel
            {
                Books = _repository.Books
                    .Where(c => category == null || c.Category == category)
                    .Where(cl => classification == null || cl.Classification == classification)
                    .OrderBy(p => p.BookId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
                ,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalNumItems = category == null ? _repository.Books.Count() :
                        _repository.Books.Where (x => x.Category == category).Count()
                },
                CurrentCategory = category,
                CurrentClassification = classification
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
