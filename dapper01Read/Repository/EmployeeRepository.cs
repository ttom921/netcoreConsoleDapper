using Gomo.Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Gomo.Repository
{
    public class EmployeeRepository : DapperRepository<Employee>,IEmployeeRepository
    {
        public EmployeeRepository(IDbConnection connection, ISqlGenerator<Employee> sqlGenerator)
        : base(connection, sqlGenerator)
        {

        }
    }
}
