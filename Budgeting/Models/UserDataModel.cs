using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Budgeting.Models
{
    [Table("TblUser")]
    public class UserDataModel
    {
		[Key]
        public int User_id { get; set; }
		public string User_unique_id { get; set; }
		public string User_name { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string Role { get; set; }
		public DateTime Create_date_time { get; set; }
		public string Create_user_id { get; set; }
		public DateTime Modified_date_time { get; set; }
		public string Modified_user_id { get; set; }
		public int Del_flag { get; set; }

	}
}
