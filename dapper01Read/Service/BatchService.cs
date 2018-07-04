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
using System.Transactions;

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
            foreach (var itememployee in employees)
            {
                Console.WriteLine("employee ->id: {0}, first_name: {1}, last_name:{2}", itememployee.id, itememployee.first_name, itememployee.last_name);
            }
            Console.WriteLine("=================================");
            //在Employee的屬性上要設定
            Employee employee = new Employee();
            employee.address = "這是住址";
            employee.business_phone = "987654321";
            employee.city = "新北市";
            employee.company = "gomo2o";
            employee.country_region = "樹林區";
            employee.email_address = "e1@gomo2o.com";
            employee.first_name = "姓";
            employee.last_name = "名";
            employee.zip_postal_code = "12345";
            employee.state_province = "TW";
            if(Insert(employee))
            {
                //var retemployee = GetOne(employee.id);
                PrintEmployeeData(employee);
            }
            if(Update(employee))
            {
                PrintEmployeeData(employee);
            }
            if (Delete(employee.id))
            {
                Console.WriteLine("刪除employee ->id: {0}, first_name: {1}, last_name:{2}", employee.id, employee.first_name, employee.last_name);
            }

            
        }
        void PrintEmployeeData(Employee employee)
        {
            Console.WriteLine("employee ->id: {0}, first_name: {1}, last_name:{2}", employee.id, employee.first_name, employee.last_name);
        }
        #region 查詢
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
        #endregion //查詢
        //增
        public bool Insert(Employee employee)
        {
            var ret = _EmployeeRepository.Insert(employee);
            return ret;
        }
        //public bool Insert(Employee employee)
        //{
        //    var ret = false;
        //    using (var transaction = new TransactionScope())
        //    {
        //        ret = _EmployeeRepository.Insert(employee);
        //        transaction.Complete();
        //    }
        //    return ret;
        //}
        //刪 使用linq 來刪除資料，不然物件要設定key
        //public bool Delete(Employee employee)
        //{
        //    var ret = _EmployeeRepository.Delete(new Employee { id=employee.id});
        //    return ret;
        //}
        public bool Delete(int id)
        {
            var ret = _EmployeeRepository.Delete( e => e.id== id);
            return ret;
        }
        //改
        public bool Update(Employee employee)
        {
            employee.address = "修改這是住址";
            employee.first_name = "修改姓";
            var ret = _EmployeeRepository.Update((e => e.id==employee.id), employee);
            return ret;
        }

    }
}
