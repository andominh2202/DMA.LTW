using Microsoft.AspNetCore.Mvc;
using Dma_Lab3_Lab4.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dma_Lab3_Lab4.ViewComponents
{
    public class RenderViewComponent : ViewComponent
    {
        private List<MenuItem> MenuItems = new List<MenuItem>();

        public RenderViewComponent()
        {
            MenuItems = new List<MenuItem>()
            {
                new MenuItem() { Id = 1, Name = "Students", Link = "/Student/Index" },
                new MenuItem() { Id = 2, Name = "Learners", Link = "/Learner" },
                new MenuItem() { Id = 3, Name = "Branches", Link = "#" },
                new MenuItem() { Id = 4, Name = "Courses", Link = "#" }
            };
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderLeftMenu", MenuItems);
        }
    }
}