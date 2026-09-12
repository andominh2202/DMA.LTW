using Microsoft.AspNetCore.Mvc;
using DmaLession04.Models;

namespace DmaLession04.ViewComponents
{
    public class BookViewComponent : ViewComponent
    {
        protected Book book = new Book();
        public IViewComponentResult Invoke()
        {
            var books = book.GetBooksList();
            return View(books);
        }
    }
}
