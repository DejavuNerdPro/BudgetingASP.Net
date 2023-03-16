using Budgeting.Services;
using Budgeting.Models;
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

            string query_total_budget = $@"SELECT SUM(Amount) amount FROM [dbo].[TblBudget]
                           WHERE User_unique_id='{userId}'";
            var totalBudget = dapperService.GetItem<int>(query_total_budget);
            ViewBag.TotalBudget = totalBudget;

            string query_test_total= $@"SELECT SUM(Amount) amount FROM [dbo].[TblExpensediture]
                           WHERE User_unique_id='{userId}' AND TransactionType='Test'";
            var totalTest = dapperService.GetItem<int>(query_test_total);
            ViewBag.TotalTest = totalTest;

            string query_salary_total = $@"SELECT SUM(Amount) amount FROM [dbo].[TblExpensediture]
                           WHERE User_unique_id='{userId}' AND TransactionType='Salary'";
            var totalSalary = dapperService.GetItem<int>(query_salary_total);
            ViewBag.TotalSalary = totalSalary;

            var test_remaining = totalBudget - totalTest;
            var salary_remaining = totalBudget - totalSalary;
            ViewBag.TestRemaining = test_remaining;
            ViewBag.SalaryRemaining = salary_remaining;

            int test_percent = (test_remaining / totalBudget) * 100;
            int salary_percent = (salary_remaining / totalBudget)*100;
            ViewBag.testPercent = test_percent;
            ViewBag.salaryPercent = salary_percent;
            return View("BudgetIndex");
        }

        [ActionName("BudgetInsertion")]
        [HttpPost]
        public IActionResult Insertion(RequestBudgetInsertionModel request)
        {
            string name = request.Name;
            decimal amount = request.Amount;
            string userId= HttpContext.Session.GetString("UserId");
            DateTime date = DateTime.Now;
            BudgetDataModel budget = (BudgetDataModel)dbService.getBudget.FirstOrDefault(i => i.Description == name);

            var model = new BudgetDataModel() { 
            User_unique_id=userId,
            Description=name,
            Amount=amount,
            Date=date
            };

            if (budget == null)
            {
                dbService.Add(model);
                dbService.SaveChanges();
                return Json(new { response = "insertion_done" });
            }
            else
            {
                return Json(new { response = "already_exist" });
            }
        }

        [ActionName("ExpenseInsertion")]
        [HttpPost]
        public IActionResult Expensediture(RequestExpenseInsertionModel request)
        {
            string name = request.Name;
            decimal amount = request.Amount;
            string category = request.Category;
            string userId = HttpContext.Session.GetString("UserId");
            DateTime date = DateTime.Now;

            Expensediture expense = dbService.getExpensediture.FirstOrDefault(i => i.Description == name);

            var model = new Expensediture()
            {
                User_unique_id = userId,
                Description=name,
                Amount=amount,
                Date=date,
                TransactionType=category
            };

            if (expense == null)
            {
                dbService.Add(model);
                dbService.SaveChanges();
                return Json(new { response = "insertion_done" });
            }
            else
            {
                return Json(new { response = "already_exist" });
            }

        }

        [ActionName("RecentExpense")]
        public IActionResult RetrieveExpenseTopFive()
        {
            string query = $@"SELECT TOP 5 * FROM [dbo].[TblExpensediture]
                            ORDER BY Date DESC";
            var _list = dapperService.GetList<Expensediture>(query);
            var model = new ResponseExpenseditureViewModel()
            {
                list=_list
            };
            return View("BudgetRecentExpense",model);
        }
        
    }
}
