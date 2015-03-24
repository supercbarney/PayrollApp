using Payroll.FakeData;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = FakeClass.GetAllEmployees();
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var model = FakeClass.GetEmployeeById(id);
            if (model == null)
            {
                RedirectToAction("Index");
                return null;
            }
            return RedirectToAction("EditEmployee",model);
        }

        public ActionResult CreateEmployee()
        {
            var model = new Employee();
            model.Dependants = new List<Dependant>();
            return RedirectToAction("EditEmployee", model);
        }

        public ActionResult EditEmployee(Employee emp)
        {
            return View(emp);
        }
    }
}