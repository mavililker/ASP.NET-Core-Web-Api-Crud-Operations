using Assignment.DTOs;
using Assignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Assignment.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetEmployees()
        {
            NORTHWNDContext context = new NORTHWNDContext();
            List<EmployeeDTO> employees = (from e in context.Employees
                                           select new EmployeeDTO()
                                           {
                                               FirstName = e.FirstName,
                                               LastName = e.LastName,
                                               Title = e.Title,
                                               HireDate = e.HireDate
                                           }).ToList();
            return Ok(employees);
        }

        [HttpGet("{id}")]

        public IActionResult GetEmployeeDetails(int id)
        {
            NORTHWNDContext context = new NORTHWNDContext();
            var query = (from e in context.Employees where e.EmployeeId == id select e);
            Employee employee = query.SingleOrDefault();
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            NORTHWNDContext context = new NORTHWNDContext();
            context.Employees.Add(employee);
            context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]

        public IActionResult UpdateEmployee(int id, Employee employee)
        {
            NORTHWNDContext context = new NORTHWNDContext();
            var query = (from e in context.Employees where e.EmployeeId == id select e);
            Employee original = query.SingleOrDefault();

            //Required
            original.LastName = employee.LastName;
            original.FirstName = employee.FirstName;
            //Required

            original.Title = employee.Title;
            original.HireDate = employee.HireDate;
            original.ReportsTo = employee.ReportsTo;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]

        public IActionResult RemoveEmployee(int id)
        {
            NORTHWNDContext context = new NORTHWNDContext();
            
            var query = (from e in context.Employees where e.EmployeeId == id select e);
            Employee employee = query.FirstOrDefault();
            context.Employees.Remove(employee);
            
            context.SaveChanges();

            return Ok();
        }

        [HttpGet("{id}")]

        public IActionResult GetOrderListOfEmployee(int id)
        {
            NORTHWNDContext context = new NORTHWNDContext();
            List<OrderDTO> data = (from e in context.Employees
                                   join o in context.Orders on e.EmployeeId equals o.EmployeeId
                                   join p in context.OrderDetailsExtendeds on o.OrderId equals p.OrderId
                                   join c in context.Customers on o.CustomerId equals c.CustomerId
                                   where e.EmployeeId == id
                                   select new OrderDTO()
                                   {
                                       OrderId = o.OrderId,
                                       ContactName = c.ContactName,
                                       OrderDate = o.OrderDate,
                                       ExtendedPrice = p.ExtendedPrice,
                                   }
                                   ).ToList();
            return Ok(data);
        }

        [HttpGet("{id}")]

        public IActionResult GetTerritories(int id)
        {
            NORTHWNDContext context = new NORTHWNDContext();
            List<TerritoryDTO> data2 = (from e in context.Employees
                                        join t in context.EmployeeTerritories on e.EmployeeId equals t.EmployeeId
                                        join r in context.Territories on t.TerritoryId equals r.TerritoryId
                                        join g in context.Regions on r.RegionId equals g.RegionId
                                        where e.EmployeeId == id
                                        select new TerritoryDTO()
                                        {
                                            TerritoryDescription = r.TerritoryDescription,
                                            RegionDescription = g.RegionDescription,
                                        }
                                        ).ToList();
            return Ok(data2);
        }

        [HttpPost]
        public IActionResult AddEmployeeTerritory(EmployeeTerritory employeeTerritory)
        {
            NORTHWNDContext context = new NORTHWNDContext();
            context.EmployeeTerritories.Add(employeeTerritory);
            context.SaveChanges();
            return Ok();
        }
    }
}
