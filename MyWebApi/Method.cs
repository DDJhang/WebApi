using MyWebApi.Model;
using System;

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
                Delete = model.Inactive
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
    }
}
