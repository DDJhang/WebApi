using Dapper;
using Dapper.FluentMap;
using MySql.Data.MySqlClient;
using MyWebApi.Definition;
using MyWebApi.Model;
using MyWebApi.Repository.Interface;
using MyWebApi.Repository.Map;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWebApi.Repository
{
    public class PunchClockRepository : IPunchClockRepository
    {
        private string _connectString = "Data Source=localhost;port=3306;User ID=root; Password=123456; database='account';";

        public PunchClockRepository()
        {
            FluentMapper.EntityMaps.Clear();
            FluentMapper.Initialize(config => {
                config.AddMap(new PunchClockMap());
            });
        }

        public async Task Add(OneDayPunchModel model, string tableName)
        {
            using (var connection = new MySqlConnection(_connectString))
            {
                connection.Open();
                var strCmd = "INSERT INTO " + tableName + " (account, name, punchin, punchout) Values('" +
                             model.Account + "', '" + model.Name + "', '" + model.PunchIn + "', '" + model.PunchOut + "')";
                using (var cmd = new MySqlCommand(strCmd, connection))
                {
                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                    finally
                    {
                        strCmd = null;
                        connection.Close();

                        cmd.Dispose();
                        connection.Dispose();
                    }
                }
            }
        }

        public async Task Update(OneDayPunchModel model, string tableName, PunchType type)
        {
            using (var connection = new MySqlConnection(_connectString))
            {
                var param = type.ToString().ToLower();

                var strTime = Method.DateTimeToPunchString(DateTime.Now);

                connection.Open();
                var strCmd = "UPDATE " + tableName + " SET " + param + " = '" + strTime + 
                             "' WHERE account = '" + model.Account + "'";
                using (var cmd = new MySqlCommand(strCmd, connection))
                {
                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                    finally
                    {
                        strCmd = null;
                        connection.Close();

                        cmd.Dispose();
                        connection.Dispose();
                    }
                }
            }
        }

        public async Task<dynamic> GetPunchData(string account)
        {
            using (var connnect = new MySqlConnection(_connectString))
            {
                var tableName = Method.DateTimeToTableName(DateTime.Now);
                var sql = "SELECT * FROM " + tableName + " WHERE account = @account";
                var model = await connnect.QueryFirstOrDefaultAsync<OneDayPunchModel>(sql, param: new
                {
                    account,
                });

                sql = null;
                return model;
            }
        }
        
        public async Task<List<dynamic>> GetPunchListByAccount(string account, int days)
        {
            List<dynamic> list = new List<dynamic>();
            for (int i = 0; i < days; i++)
            {
                var date = DateTime.Now.AddDays(-i);
                var tableName = Method.DateTimeToTableName(date);
                if (!Method.CheckTableExist(_connectString, tableName))
                    break;

                using (var connnect = new MySqlConnection(_connectString))
                {
                    var sql = "SELECT * FROM " + tableName + " WHERE account = @account";
                    var model = await connnect.QueryFirstOrDefaultAsync<OneDayPunchModel>(sql, param: new
                    {
                        account,
                    });

                    if (model == null)
                    {
                        list.Add(new OneDayPunchModel()
                        {
                            Account = account,
                            Name = "",
                            PunchIn = "",
                            PunchOut = ""
                        });
                    }
                    else
                    {
                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public async Task<Dictionary<string, IEnumerable<dynamic>>> GetAllPunchList(int days)
        {
            Dictionary<string, IEnumerable<dynamic>> dict = new Dictionary<string, IEnumerable<dynamic>>();

            for (int i = 0; i < days; i++)
            {
                var date = DateTime.Now.AddDays(-i);
                var tableName = Method.DateTimeToTableName(date);
                if (!Method.CheckTableExist(_connectString, tableName))
                    break;

                using (var connnect = new MySqlConnection(_connectString))
                {
                    var sql = "SELECT * FROM " + tableName;
                    var models = await connnect.QueryAsync<OneDayPunchModel>(sql);

                    dict.Add(tableName, models);
                }
            }

            return dict;
        }
    }
}
