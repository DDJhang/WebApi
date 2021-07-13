using Dapper;
using Dapper.FluentMap;
using MySql.Data.MySqlClient;
using MyWebApi.Context;
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
        private PunchClockContext _context;

        private string _connectString = "Data Source=localhost;port=3306;User ID=root; Password=123456; database='account';";

        public PunchClockRepository(PunchClockContext context)
        {
            _context = context;

            FluentMapper.EntityMaps.Clear();
            FluentMapper.Initialize(config => {
                config.AddMap(new PunchClockMap());
            });
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<OneDayPunchModel> GetPunchData(string account)
        {
            using (var connnect = new MySqlConnection(_connectString))
            {
                var tableName = Method.DateTimeToTableName(DateTime.Now);
                var sql = "SELECT * FROM " + tableName + " WHERE account = @account";
                var model = await connnect.QueryFirstAsync<OneDayPunchModel>(sql, param: new
                {
                    account,
                });

                return model;
            }
        }
        
        public async Task<List<OneDayPunchModel>> GetPunchListByAccount(string account, AttendanceStatus status)
        {
            if (int.TryParse(status.ToString(), out int days))
            {
                List<OneDayPunchModel> list = new List<OneDayPunchModel>();
                for (int i = 0; i < days; i++)
                {
                    var date = DateTime.Now.AddDays(-i);
                    var tableName = Method.DateTimeToTableName(date);
                    if (!Method.CheckTableExist(_connectString, tableName))
                        break;

                    using (var connnect = new MySqlConnection(_connectString))
                    {
                        var sql = "SELECT * FROM " + tableName + " WHERE account = @account";
                        var model = await connnect.QueryFirstAsync<OneDayPunchModel>(sql, param: new
                        {
                            account,
                        });

                        if (model == null)
                        {
                            list.Add(new OneDayPunchModel() {
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
            else
            {
                return null;
            }
        }

        public async Task<Dictionary<string, IEnumerable<OneDayPunchModel>>> GetAllPunchList(AttendanceStatus status)
        {
            if (int.TryParse(status.ToString(), out int days))
            {
                Dictionary<string, IEnumerable<OneDayPunchModel>> dict = new Dictionary<string, IEnumerable<OneDayPunchModel>>();

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
            else
            {
                return null;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
