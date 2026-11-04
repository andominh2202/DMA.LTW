using Microsoft.AspNetCore.Mvc;
using Dma_Lab5_Lab6.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dma_Lab5_Lab6.ViewComponents
{
    public class RenderViewComponent : ViewComponent
    {
        private List<MenuItem> MenuItems = new List<MenuItem>();

        public RenderViewComponent()
        {
            MenuItems = new List<MenuItem>()
            {
                new MenuItem() { Id = 1, Name = "Sinh viên", Link = "/Student/Index" },
                new MenuItem() { Id = 2, Name = "Người học", Link = "/Learner" },
                new MenuItem() { Id = 3, Name = "Ngành học", Link = "#" },
                new MenuItem() { Id = 4, Name = "Môn học", Link = "#" }
            };
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderLeftMenu", MenuItems);
        }
    }
}