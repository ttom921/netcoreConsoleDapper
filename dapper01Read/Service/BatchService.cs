using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Serilog;
using System;

namespace GomoService
{
    public interface IBatchService
    {
        void WriteInformation(string input);

    }
    public class BatchService : IBatchService
    {
        private readonly string _baseUrl;
        private readonly string _token;
        readonly ILogger _logger;

        public BatchService(IConfigurationRoot config, ILogger logger)
        {
            var baseUrl = config["SomeConfigItem:BaseUrl"];
            var token = config["SomeConfigItem:Token"];
            _baseUrl = baseUrl;
            _token = token;
            _logger = logger;
            //以下可以2選一,物件屬性和資料表的對映
            //SqlMapper.SetTypeMap(typeof(UserInfo), new ColumnTypeMapper(typeof(UserInfo)));
            //ColumnTypeMapper.RegisterForTypes(typeof(UserInfo), typeof(UserInfo));
        }
        /// <summary>
        /// 參數式查詢
        /// </summary>
        //public void TestParamQuery()
        //{
        //    var cnstr = SqlHelper.GetConnectionString();
        //    using (var cn = new MySqlConnection(cnstr))
        //    {
        //        // 開啟連線
        //        cn.Open();
        //        String sql = "select * from UserInfo where User_Id like @User_Id or UName=@UName";
        //        var parameters = new
        //        {
        //            User_Id="pg%",
        //            UName= "pg%"
        //        };
        //        var UserInfos = cn.Query<UserInfo>(sql, parameters);
        //        foreach (var userinfo in UserInfos)
        //        {
        //            Console.WriteLine("Id: {0}, UserId: {1}, Uname:{2}", userinfo.Id, userinfo.UserId, userinfo.Uname);
        //        }
        //    }
        //}

        ///// <summary>
        ///// 使用泛型版本的 Query 方法
        ///// </summary>
        //public void TestTempQuery()
        //{
            
            

        //    var cnstr = SqlHelper.GetConnectionString();


        //    using (var cn = new MySqlConnection(cnstr))
        //    {
        //        // 開啟連線
        //        cn.Open();
        //        String sql = "select * from UserInfo";
        //        var UserInfos = cn.Query<UserInfo>(sql);
        //        foreach (var userinfo in UserInfos)
        //        {
        //            Console.WriteLine("Id: {0}, UserId: {1}, Uname:{2}", userinfo.Id, userinfo.UserId, userinfo.Uname);
        //        }
        //    }
        //}

        public void WriteInformation(string input)
        {
            Console.WriteLine(input);
            Console.WriteLine(_baseUrl);
            Console.WriteLine(_token);
            _logger.Information("log test");

        }
    }
}
