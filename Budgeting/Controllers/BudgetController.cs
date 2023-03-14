using Budgeting.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budgeting.Controllers
{
    public class BudgetController : Controller
    {
        private readonly DapperService dapperService;
        private readonly DBService dbService;
        public BudgetController(DapperService _dapperService,DBService _dbService)
        {
            dapperService = _dapperService;
            dbService = _dbService;
        }
        
        public IActionResult BudgetIndex()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var userModel = dbService.getUserData.FirstOrDefault(i => i.User_unique_id ==userId);
            ViewBag.Username = userModel.User_name;
            return View("BudgetIndex");
        }
    }
}
