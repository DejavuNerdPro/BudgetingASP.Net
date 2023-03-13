
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Budgeting.Models
{
    [Table("TblLogin")]
    public class LoginDataModel
    {
        [Key]
        public int Login_id { get; set; }
        public string User_unique_id { get; set; }
        public string Session_id { get; set; }
        public DateTime Session_exp_datetime { get; set; }
        public DateTime Logout_datetime { get; set; }
        public string Reason { get; set; }

    }
}

