using Gomo.Domain;
using MicroOrm.Dapper.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gomo.Repository
{
    public interface IEmployeeRepository: IDapperRepository<Employee>
    {
    }
}
