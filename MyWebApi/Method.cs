using MySql.Data.MySqlClient;
using MyWebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi
{
    public class Method
    {
        public static AccountModelDTO AccountToDTO(AccountModel model)
        {
            var dto = new AccountModelDTO()
            {
                Account = model.Account,
                Name = model.Name,
                Delete = model.Delete
            };

            return dto;
        }

        public static string DateTimeToTableName(DateTime date)
        {
            return date.Year.ToString() + "_" + date.Month.ToString() + "_" + date.Day.ToString();
        }

        public static bool CheckTableExist(string connectString, string tableName)
        {
            MySqlConnection sqlDB = new MySqlConnection(connectString);

            sqlDB.Open();

            var strCmd = "SELECT * FROM sys.tables WHERE name = '" + tableName + "'";
            MySqlCommand cmd = new MySqlCommand(strCmd, sqlDB);

            var reader = cmd.ExecuteReader();
            return reader.HasRows;
        }
    }
}
