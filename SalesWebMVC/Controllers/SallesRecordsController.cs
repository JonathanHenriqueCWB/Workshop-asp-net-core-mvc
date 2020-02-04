using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SalesWebMVC.Controllers
{
    public class SallesRecordsController : Controller
    {
        #region Index
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region SimpleSearch
        public IActionResult SimpleSearch()
        {
            return View();
        }
        #endregion

        #region GroupSearch
        public IActionResult GroupSearch()
        {
            return View();
        }
        #endregion
    }
}