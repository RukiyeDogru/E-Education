using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Web.Views.Shared.Components.SideBar
{
    public class SidebarViewComponent : ViewComponent
    {
         

        public SidebarViewComponent( )
        {
            
        }

        public IViewComponentResult Invoke()
        {
            //var user = userService.GetClaims(userService.GetId(Convert.ToInt32(action)));
            //ViewBag.Roles = user.Select(m=>m.Module).ToList();
            return View();
        }
    }
}
