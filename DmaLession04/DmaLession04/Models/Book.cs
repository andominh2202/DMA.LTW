using Microsoft.AspNetCore.Mvc.Rendering;

namespace DmaLession04.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
        public string TotalPages { get; set; }
        public string Summary { get; set; }

        public List<Book> GetBooksList()
        {
            List<Book> books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Chí Phèo",
                    AuthorId = -1,
                    GenreId = 1,
                    Image = "https://example.com/gatsby.jpg",
                    Price = 00000,
                    Summary = "",
                    TotalPages = "250"
                },
                new Book(){ },
                new Book(){ },
                new Book(){ }
            };
            return books;
        }

        public Book GetBookById(int id)
        {
            Book book = this.GetBooksList().FirstOrDefault(b => b.Id == id);
            return book;
        }

        public List<SelectListItem> Authors { get; } = new List<SelectListItem>{
            new SelectListItem {Value="1", Text="Nam Cao"},
            new SelectListItem {Value="2", Text="Ngô Tất Tố"},

        };

        public List<SelectListItem> Genres { get; } = new List<SelectListItem>
        {
            new SelectListItem {Value="1", Text="Truyện tranh"},
            new SelectListItem {Value="2", Text="Văn học"},
        };
    }
}
