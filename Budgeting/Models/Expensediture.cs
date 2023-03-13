using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Budgeting.Models
{
    [Table("Tbl_ExpenseTracker")]
    public class Expensediture
    {
        [Key]
        public int Id { get; set; }
        public string User_unique_id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        
    }
}
