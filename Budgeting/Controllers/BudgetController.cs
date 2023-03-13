using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budgeting.Controllers
{
    public class BudgetController : Controller
    {
        
        public IActionResult BudgetIndex()
        {
            return View("BudgetIndex");
        }
    }
}
