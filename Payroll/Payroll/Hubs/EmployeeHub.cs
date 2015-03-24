using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Payroll.Models;
using Payroll.FakeData;
using Newtonsoft.Json;

namespace Payroll.Hubs
{
    public class EmployeeHub : Hub
    {
        public void AddOrUpdateEmployee(string jsonEmp,string connId)
        {
            var emp = JsonConvert.DeserializeObject<Employee>(jsonEmp);
            emp = FakeClass.AddOrUpdateEmployee(emp);
            Clients.Client(connId).addOrUpdateEmployee(emp);
            Clients.All.updateAllEmployees(FakeClass.GetAllEmployees());
        }

        public void SaveDependant(string jsonEmp, string firstName, string lastName,string connId)
        {
            var emp = JsonConvert.DeserializeObject<Employee>(jsonEmp);
            FakeClass.AddDependant(emp, firstName, lastName);
            Clients.Client(connId).addOrUpdateEmployee(emp);
            Clients.All.updateAllEmployees(FakeClass.GetAllEmployees());
        }
    }
}