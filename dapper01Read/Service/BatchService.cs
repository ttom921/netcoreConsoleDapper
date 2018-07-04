using Dapper;
using Gomo.Domain;
using Gomo.Repository;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;

namespace GomoService
{
    public interface IBatchService
    {
        void WriteInformation(string input);
        Employee GetOne(int id);
        IEnumerable<Employee> GetEmployees();

    }
    public class BatchService : IBatchService
    {
        private readonly string _baseUrl;
        private readonly string _token;
        private IEmployeeRepository _EmployeeRepository;
        readonly ILogger _logger;
       

        public BatchService(IConfigurationRoot config, IEmployeeRepository employeeRepository, ILogger logger)
        {
            var baseUrl = config["SomeConfigItem:BaseUrl"];
            var token = config["SomeConfigItem:Token"];
            _baseUrl = baseUrl;
            _token = token;

            _EmployeeRepository = employeeRepository;
            _logger = logger;
           
        }
        public void WriteInformation(string input)
        {
            Console.WriteLine(input);
            Console.WriteLine(_baseUrl);
            Console.WriteLine(_token);
            _logger.Information("log test");
            var myemployee = GetOne(1);
            Console.WriteLine("employee ->id: {0}, first_name: {1}, last_name:{2}", myemployee.id, myemployee.first_name, myemployee.last_name);
            Console.WriteLine("=================================");
           var employees = GetEmployees();
            foreach (var employee in employees)
            {
                Console.WriteLine("employee ->id: {0}, first_name: {1}, last_name:{2}", employee.id, employee.first_name, employee.last_name);
            }
        }

        public Employee GetOne(int id)
        {
            var employee = _EmployeeRepository.Find(x => x.id == id);
            return employee;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            var employees= _EmployeeRepository.FindAll();
            return employees;
        }
    }
}
