using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budgeting.Models
{
    public class RequestExpenseInsertionModel
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
    }
}
