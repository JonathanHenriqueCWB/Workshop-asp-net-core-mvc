using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.DAL;

namespace SalesWebMVC.Controllers
{
    public class SallesRecordsController : Controller
    {
        #region Construtor e dependencias
        private readonly SalesRecordDAO _salesRecordDAO;
        public SallesRecordsController(SalesRecordDAO salesRecordDAO)
        {
            _salesRecordDAO = salesRecordDAO;
        }
        #endregion
        #region Index
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region SimpleSearch
        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            //Calcula as vendas comforme a data passada por parametro
            var result = await _salesRecordDAO.FindByDateAsync(minDate, maxDate);
            return  View(result);
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