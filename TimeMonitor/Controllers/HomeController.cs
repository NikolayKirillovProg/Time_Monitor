using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TimeMonitor.Entities;
using Microsoft.Ajax.Utilities;
using TimeMonitor.ViewModels;
using AutoMapper;
using System.Web.Services.Description;
using System.Web.UI.WebControls;

namespace TimeMonitor.Controllers
{
    public class HomeController : Controller
    {
        dbContext db = new dbContext();

        public ActionResult Index()
        {
            var users = db.Users.Where(x => x.IsDeleted == false).ToList();
            var usersVMList = new List<UserViewModel>();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserViewModel>()
            .ForMember(dest => dest.Reports, opt => opt.Ignore()));
            var mapper = new Mapper(config);

            usersVMList = mapper.Map<List<UserViewModel>>(users);

            return View(usersVMList);
        }

        #region Пользователи
        [HttpGet]
        public ActionResult AddUser()
        {
            var newuser = new UserViewModel();
            newuser.EmailIsValid = true;
            newuser.UserIsValid = true;
            return PartialView("AddUser", newuser);
        }

        [HttpPost]
        public ActionResult AddUser(UserViewModel newuser)
        {

            newuser.Email = newuser.Email == null ? newuser.Email : newuser.Email.Trim(' ');
            newuser.Name_1 = newuser.Name_1 == null ? newuser.Name_1 : newuser.Name_1.Trim(' ');
            newuser.Name_2 = newuser.Name_2 == null ? newuser.Name_2 : newuser.Name_2.Trim(' ');
            newuser.Name_3 = newuser.Name_3 == null ? newuser.Name_3 : newuser.Name_3.Trim(' ');
            var check = db.Users.Any(x => x.Email == newuser.Email && x.IsDeleted == false);
            if (check)
            {
                newuser.EmailIsValid = false;
            }
            else
            {
                newuser.EmailIsValid = true;
            }

            if (newuser.Email == "" || newuser.Email == null)
            {
                check = true;
                newuser.UserIsValid = false;
            }

            if (newuser.Name_1 == "" || newuser.Name_1 == null)
            {
                check = true;
                newuser.UserIsValid = false;
            }

            if (newuser.Name_2 == "" || newuser.Name_2 == null)
            {
                check = true;
                newuser.UserIsValid = false;
            }


            if (check == true)
            {
                return PartialView("AddUser", newuser);
            }
            else
            {
                var adduser = db.Users.Create();
                adduser.Name_1 = newuser.Name_1;
                adduser.Name_2 = newuser.Name_2;
                adduser.Name_3 = newuser.Name_3;
                adduser.Email = newuser.Email;
                adduser.IsDeleted = false;

                db.Users.Add(adduser);
                db.SaveChanges();
                var result = new ResultViewModel();
                result.Message = "Пользователь добавлен";
                result.BackUrl = "Index";
                return PartialView("Result", result);
            }
        }

        public ActionResult DeleteUser(int userId)
        {
            var user = db.Users.FirstOrDefault(x => x.User_Id == userId);
            if (user != null)
            {
                user.IsDeleted = true;
                db.SaveChanges();
                var result = new ResultViewModel();
                result.Message = "Пользователь удален";
                result.BackUrl = "Index";
                return PartialView("Result", result);
            }
            else
            {
                var result = new ResultViewModel();
                result.Message = "Пользователь с таким Id {0} не найден" + userId;
                result.BackUrl = "Index";
                return PartialView("Result", result);
            }
        }

        public ActionResult ChangeUser(int userId)
        {
            var user = new User();
            var userDB = db.Users.FirstOrDefault(x => x.User_Id == userId && x.IsDeleted == false);
            if (userDB == null)
            {
                var result = new ResultViewModel();
                result.Message = "Пользователь с таким Id {0} не найден" + userId;
                result.BackUrl = "Index";
                return PartialView("Result", result);
            }
            else
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserViewModel>()
                .ForMember(dest=>dest.Reports, opt=>opt.Ignore()));
                var mapper = new Mapper(config);
                var userVM = mapper.Map<UserViewModel>(userDB);
                userVM.EmailIsValid = true;
                userVM.UserIsValid = true;
                return PartialView("EditUser", userVM);
            }

        }

        public ActionResult SaveChanges(UserViewModel user)
        {
            user.Email = user.Email == null ? user.Email : user.Email.Trim(' ');
            user.Name_1 = user.Name_1 == null ? user.Name_1 : user.Name_1.Trim(' ');
            user.Name_2 = user.Name_2 == null ? user.Name_2 : user.Name_2.Trim(' ');
            user.Name_3 = user.Name_3 == null ? user.Name_3 : user.Name_3.Trim(' ');
            var check = db.Users.Any(x => x.Email == user.Email && x.IsDeleted == false && x.User_Id != user.User_Id);
            if (check)
            {
                user.EmailIsValid = false;
            }
            else
            {
                user.EmailIsValid = true;
            }

            if (user.Email == "" || user.Email == null)
            {
                check = true;
                user.UserIsValid = false;
            }

            if (user.Name_1 == "" || user.Name_1 == null)
            {
                check = true;
                user.UserIsValid = false;
            }

            if (user.Name_2 == "" || user.Name_2 == null)
            {
                check = true;
                user.UserIsValid = false;
            }

            if (check == true)
            {
                return PartialView("EditUser", user);
            }
            else
            {
                var userDB = db.Users.FirstOrDefault(x => x.User_Id == user.User_Id && x.IsDeleted == false);
                userDB.Name_1 = user.Name_1;
                userDB.Name_2 = user.Name_2;
                userDB.Name_3 = user.Name_3;
                userDB.Email = user.Email;
                db.SaveChanges();
                var result = new ResultViewModel();
                result.Message = "Пользователь изменен";
                result.BackUrl = "Index";
                return PartialView("Result", result);
            }
        }
        #endregion

        #region Отчеты

        public ActionResult Reports()
        {
            var repDBList = db.Reports.Where(x => x.IsDeleted == false);
            var reportVMList = new List<ReportViewModel>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Report, ReportViewModel>());
            var mapper = new Mapper(config);
            reportVMList = mapper.Map<List<ReportViewModel>>(repDBList);

            var users = db.Users.Where(x => x.IsDeleted == false).ToList();
            int selectedIndex = 0;
            SelectList usersList = new SelectList(users, "User_Id", "Email", selectedIndex);
            ViewBag.UserList = usersList;
            return View("Reports");
        }


        public ActionResult UserReports(ReportViewModel report, int userId = 0)
        {
            if (report.User_id == 0)
            {
                report.User_id = userId;
            }
            var user = db.Users.FirstOrDefault(x => x.User_Id == report.User_id && x.IsDeleted == false);
            ViewBag.User_id = report.User_id;
            ViewBag.FIO = user.Name_1 + " " + user.Name_2 + " " + user.Name_3;
            var reportDb = db.Reports.Where(x => x.User_id == report.User_id && x.IsDeleted == false);
            var reportVMList = new List<ReportViewModel>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Report, ReportViewModel>());
            var mapper = new Mapper(config);
            reportVMList = mapper.Map<List<ReportViewModel>>(reportDb);
            return View("UserReports", reportVMList);
        }

        [HttpGet]
        public ActionResult AddReport(int user_id)
        {
            var newReport = new ReportViewModel();
            newReport.IsValid = true;
            newReport.User_id = user_id;
            newReport.Date = DateTime.Now;
            return PartialView("AddReport", newReport);
        }

        [HttpPost]
        public ActionResult AddReport(ReportViewModel report)
        {
            report.Summary = report.Summary == null ? report.Summary : report.Summary.Trim(' ');

            if (report.Summary == "" || report.Summary == null)
            {
                report.IsValid = false;
            }

            if (report.Hours == 0)
            {
                report.IsValid = false;
            }

            if (report.Minutes >= 60)
            {
                report.IsValid = false;
            }
            if (!report.IsValid)
            {
                return PartialView("AddReport", report);
            }
            else
            {
                var addReport = db.Reports.Create();
                addReport.Summary = report.Summary;
                addReport.Hours = report.Hours;
                addReport.Minutes = report.Minutes;
                addReport.Date = report.Date;
                addReport.IsDeleted = false;
                addReport.User_id = report.User_id;
                db.Reports.Add(addReport);
                db.SaveChanges();
                var result = new ResultViewModel();
                result.Message = "Отчет добавлен";
                result.BackUrl = "UserReports";
                ViewBag.UserId = report.User_id;
                return PartialView("Result", result);
            }
        }

        public ActionResult EditReport(Guid rep_id)
        {
            var repDB = db.Reports.FirstOrDefault(x => x.Rep_id == rep_id && x.IsDeleted == false);
            if (repDB == null)
            {
                var result = new ResultViewModel();
                result.Message = "Отчет с таким Id {0} не найден" + rep_id;
                result.BackUrl = "Reports";
                return PartialView("Result", result);
            }
            else
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Report, ReportViewModel>());
                var mapper = new Mapper(config);
                var repVM = mapper.Map<ReportViewModel>(repDB);
                repVM.IsValid = true;
                return PartialView("EditReport", repVM);
            }

        }


        public ActionResult SaveChangesReport(ReportViewModel report)
        {

            report.Summary = report.Summary == null ? report.Summary : report.Summary.Trim(' ');

            if (report.Summary == "" || report.Summary == null)
            {
                report.IsValid = false;
            }

            if (report.Hours == 0)
            {
                report.IsValid = false;
            }

            if (report.Minutes >= 60)
            {
                report.IsValid = false;
            }
            if (!report.IsValid)
            {
                return PartialView("AddReport", report);
            }
            else
            {
                var cngReport = db.Reports.FirstOrDefault(x => x.Rep_id == report.Rep_id && x.IsDeleted == false);
                cngReport.Summary = report.Summary;
                cngReport.Hours = report.Hours;
                cngReport.Minutes = report.Minutes;
                cngReport.Date = report.Date;
                cngReport.IsDeleted = false;
                cngReport.User_id = report.User_id;
                db.SaveChanges();
                var result = new ResultViewModel();
                result.Message = "Отчет добавлен";
                result.BackUrl = "UserReports";
                ViewBag.UserId = report.User_id;
                return PartialView("Result", result);
            }
        }

        public ActionResult DeleteReport(Guid rep_id)
        {
            var repDb = db.Reports.FirstOrDefault(x => x.Rep_id == rep_id && x.IsDeleted == false);
            if (repDb == null)
            {
                var result = new ResultViewModel();
                result.Message = "Отчет с таким Id {0} не найден" + rep_id;
                result.BackUrl = "Reports";
                return PartialView("Result", result);
            }
            else
            {
                repDb.IsDeleted = true;
                db.SaveChanges();
                var result = new ResultViewModel();
                result.Message = "Отчет удален";
                result.BackUrl = "UserReports";
                ViewBag.UserId = repDb.User_id;
                return PartialView("Result", result);
            }
        }

        #endregion
    }
}