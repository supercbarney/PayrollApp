using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.FakeData
{
    public static class FakeClass
    {
        private static List<Employee> _allEmployees;
     
        public static void Prepare()
        {
            _allEmployees = new List<Employee>();
            var firstEmployee = new Employee();
            firstEmployee.Id = 1;
            firstEmployee.FirstName = "John";
            firstEmployee.LastName = "Doe";
            firstEmployee.GrossAnnualPay = 52000;
            firstEmployee.GrossBiWeeklyPay = 2000;
            firstEmployee.YearlyInsuranceCost = 1000;
            firstEmployee.Dependants = new List<Dependant>();
            firstEmployee.NetAnnualPay = CalculateNetAnnualPay(firstEmployee);
            firstEmployee.NetBiWeeklyPay = CalculateNetBiWeeklyPay(firstEmployee);
            _allEmployees.Add(firstEmployee);
        }

        public static Employee AddOrUpdateEmployee(Employee emp)
        {
            if (_allEmployees.Any(x => x.Id == emp.Id))
            {
                var oldEmp = _allEmployees.RemoveAll(x => x.Id == emp.Id);
            }
            else
            {
                emp.Id = _allEmployees.Any()?_allEmployees.Max(x => x.Id) + 1:1;
                emp.GrossAnnualPay = 52000;
                emp.GrossBiWeeklyPay = 2000;
            }
            emp.YearlyInsuranceCost = CalculateYearlyInsurance(1000, emp.FirstName, emp.LastName);
            emp.NetAnnualPay = CalculateNetAnnualPay(emp);
            emp.NetBiWeeklyPay = CalculateNetBiWeeklyPay(emp);
            _allEmployees.Add(emp);
            return emp;
        }

        public static Employee GetEmployeeById(int id)
        {
            return _allEmployees.FirstOrDefault(x => x.Id == id);
        }

        public static List<Employee> GetAllEmployees()
        {
            return _allEmployees;
        }

        public static Employee AddDependant(Employee emp, string firstName, string lastName)
        {
            if (emp.Dependants == null)
            {
                emp.Dependants = new List<Dependant>();
            }
            emp.Dependants.Add(new Dependant()
            {
                FirstName = firstName,
                LastName = lastName,
                YearlyInsuranceCost = CalculateYearlyInsurance(500,firstName,lastName)
            });
            AddOrUpdateEmployee(emp);
            return emp;
        }

        private static decimal CalculateYearlyInsurance(int startCost, string firstName, string lastName)
        {
            if (firstName.ToLower().StartsWith("a") || lastName.ToLower().StartsWith("a"))
            {
                return startCost * .9m;
            }

            return startCost;
        }

        private static decimal CalculateNetAnnualPay(Employee emp)
        {
            var startPay = emp.GrossAnnualPay;
            var deductions = emp.YearlyInsuranceCost;
            if (emp.Dependants != null && emp.Dependants.Count() > 0)
            {
                deductions = deductions + emp.Dependants.Sum(x => x.YearlyInsuranceCost);
            }
            return Math.Round(startPay - deductions, 2, MidpointRounding.ToEven);
        }

        private static decimal CalculateNetBiWeeklyPay(Employee emp)
        {
            return Math.Round(emp.NetAnnualPay / 26, 2, MidpointRounding.ToEven);
        }
    }
}