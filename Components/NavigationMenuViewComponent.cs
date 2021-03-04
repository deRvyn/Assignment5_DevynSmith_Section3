using Assignment7_DevynSmith_Section3.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment7_DevynSmith_Section3.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        //bringing in the books repo so we can access the data
        private IBooksRepository repository;

        public NavigationMenuViewComponent (IBooksRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            //gets the category information to select it visually
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            //returns the correct data to set category filter
            return View(repository.Books
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));                ;
        }
    }
}
