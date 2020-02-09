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
            //Caso as datas não sejam imformadas a lista será refetente ao ano
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);

            }
            if (!maxDate.HasValue)
            {
                maxDate = new DateTime(DateTime.Now.Year, 12, 31);

            }

            //Passar as datas para view para poder exibir na caixa de seleção de data
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM/dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM/dd");

            //Calcula as vendas comforme a data passada por parametro
            var result = await _salesRecordDAO.FindByDateAsync(minDate, maxDate);
            return  View(result);
        }
        #endregion
        #region GroupSearch
        public async Task<IActionResult> GroupSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result =  _salesRecordDAO.FindByDateGroupingAsync(minDate, maxDate);
            return View(result);
        }
        #endregion
    }
}