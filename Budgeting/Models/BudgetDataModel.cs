using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Budgeting.Models
{
    [Table("TblBudget")]
    public class BudgetDataModel
    {
        [Key]
        public int Id { get; set; }
        public string User_unique_id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
