using Dapper;
using Dapper.FluentMap;
using MySql.Data.MySqlClient;
using MyWebApi.Definition;
using MyWebApi.Model;
using MyWebApi.Repository.Interface;
using MyWebApi.Repository.Map;
using System;
using System.Collections.Generic;
using System.Data;
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

        public string CreateDB()
        {
            //string _checkTableSqlCmd = "Data Source=localhost;port=3306;User ID=root; Password=123456; Persist Security Info=yes";
            var tableName = Method.DateTimeToTableName(DateTime.Now);
            if (CheckTableExist(_connectString, tableName))
                return "Already TABLE => " + tableName;

            var strCmd = "CREATE TABLE " + tableName + " (account VARCHAR(50)  NOT NULL, name VARCHAR(50)  NOT NULL, punchin CHAR(10)  NOT NULL, punchout CHAR(10)  NOT NULL, PRIMARY KEY (account));";

            var connectString = "Database=account; Data Source=127.0.0.1; User Id=root; port=3306; Password=123456;";
            using (var connection = new MySqlConnection(connectString))
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(strCmd, connection))
                {
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    finally
                    {
                        connection.Close();

                        cmd.Dispose();
                        connection.Dispose();
                    }
                }
            }

            return "Created TABLE => " + tableName;
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
                if (!CheckTableExist(_connectString, tableName))
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
                if (!CheckTableExist(_connectString, tableName))
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

        public bool CheckTableExist(string tableName)
        {
            return CheckTableExist(_connectString, tableName);
        }

        private bool CheckTableExist(string connectString, string tableName)
        {
            MySqlConnection sqlDB = new MySqlConnection(connectString);

            sqlDB.Open();

            var strCmd = "SELECT * FROM information_schema.TABLES where table_name = '" + tableName + "' AND TABLE_SCHEMA = 'account';";

            MySqlDataAdapter adp = new MySqlDataAdapter(strCmd, sqlDB);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            return ds.Tables[0].Rows.Count > 0;
        }
    }
}
