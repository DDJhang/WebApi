using MySql.Data.MySqlClient;
using MyWebApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
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

        public static string DateTimeToPunchString(DateTime date)
        {
            return date.Hour.ToString() + ":" + date.Minute.ToString() + ":" + date.Second.ToString();
        }

        public static bool CheckTableExist(string connectString, string tableName)
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
