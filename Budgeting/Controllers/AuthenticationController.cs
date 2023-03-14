using Budgeting.Models;
using Budgeting.Services;
using Bugeting.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budgeting.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly DBService _db;
        private readonly DapperService dapper;
        public AuthenticationController(DBService dBService, DapperService _dapper)
        {
            _db = dBService;
            dapper = _dapper;
        }
        [ActionName("AuthIndex")]
        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        [ActionName("Verify")]
        public IActionResult LoginVerification(RequestLoginData requestLoginData)
        {
            var email = requestLoginData.Email;
            var password = requestLoginData.Password;
            string _session_id = Guid.NewGuid().ToString();
            DateTime _session_exp_datetime = DateTime.Now.AddDays(1);
            DateTime _logout_datetime = new DateTime(2023, 02, 09, 11, 01, 45);
            string _reason = "login";
            var userModel = _db.getUserData.FirstOrDefault(i => i.Email == email);
            bool isVerifiedPassword = BCrypt.Net.BCrypt.Verify(password, userModel.Password);
            if (userModel != null && isVerifiedPassword)
            {
                var data_in_login = _db.getLoginData.FirstOrDefault(i => i.User_unique_id == userModel.User_unique_id);
                if (data_in_login == null)
                {
                    LoginDataModel loginModel = new LoginDataModel()
                    {
                        User_unique_id = userModel.User_unique_id,
                        Session_id = _session_id,
                        Session_exp_datetime = _session_exp_datetime,
                        Logout_datetime = _logout_datetime,
                        Reason = _reason
                    };
                    _db.getLoginData.Add(loginModel);
                    _db.SaveChanges();

                }
                HttpContext.Session.SetString("UserId", userModel.User_unique_id);
                HttpContext.Session.SetString("SessionId", _session_id);

                return Json(new { isSuccess = true });
            }
            else
            {
                return Json(new { isSuccess = false });
            }
            
        }

        [ActionName("RegisterView")]
        public IActionResult RegistrationView()
        {
            return View("Registration");
        }

        [HttpPost]
        [ActionName("Register")]
        public IActionResult RegistrationVerifying(RegistrationFormModel registrationData)
        {
            var user_uniqueId = Guid.NewGuid().ToString();
            var user_name = registrationData.user_name;
            var password = BCrypt.Net.BCrypt.HashPassword(registrationData.password);
            var email = registrationData.email;
            var role = "member";
            var del_flag = 0;
            DateTime create_date_time = DateTime.Now;
            string create_user_id = "user";
            DateTime modified_date_time = DateTime.Now;
            string modified_user_id = "user";

            string insert_user_table_query = $@"INSERT INTO [dbo].[TblUser]
           ([User_unique_id]
           ,[User_name]
           ,[Password]
           ,[Email]
           ,[Role]
           ,[Create_date_time]
           ,[Create_user_id]
           ,[Modified_date_time]
           ,[Modified_user_id]
           ,[Del_flag])
     VALUES
           ('{user_uniqueId}'
           ,'{user_name}'
           ,'{password}'
           ,'{email}'
           ,'{role}'
           ,'{create_date_time}'
           ,'{create_user_id}'
           ,'{modified_date_time}'
           ,'{modified_user_id}'
           ,'{del_flag}')";

            string all_dataRow_query = $@"SELECT * FROM [dbo].[TblUser]";
            int user_flag;

            var login_data_list = dapper.GetList<UserDataModel>(all_dataRow_query).ToList();
            var user = login_data_list.FirstOrDefault(i => i.User_name == user_name);
            if (user != null)
            {
                return Json(new { message = "user_already_exist" });
            }
            else
            {
                user_flag = dapper.Execute(insert_user_table_query);

                if (user_flag == 1)
                {
                    return Json(new { message = "transaction_done" });
                }
            }
            return null;
        }

        [ActionName("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }
    }
}
