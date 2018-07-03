using GomoService;
using System;
using System.Collections.Generic;
using System.Text;

namespace GomoApp
{
    public class Application : IApplication
    {


        private readonly IBatchService _service;
        public Application(IBatchService service)
        {
            _service = service;
        }

        public void Run()
        {
            Console.WriteLine("Application Run");
            _service.WriteInformation("Injected!");
            Console.WriteLine("----------------------------");
            //_service.TestMysqlRead();
            Console.WriteLine("----------------------------");
            //_service.TestTempQuery();
            Console.WriteLine("----------------------------");
            //_service.TestParamQuery();

            Console.ReadKey();
        }
    }
}
