using Dapper;
using MySql.Data.MySqlClient;
using System;

namespace MyWebApi.Observable
{
    public class LogObserver : IObserver<LogMessage>
    {
        private string _connectString = "Data Source=localhost;port=3306;User ID=root; Password=123456; database='account';";

        public async void OnNext(LogMessage value)
        {
            using var connection = new MySqlConnection(_connectString);
            var cmd = "INSERT INTO logmessage (account, type, time, message) " +
                      "VALUES(@account, @type, @time, @message)";
            await connection.ExecuteAsync(cmd, value);
        }

        public void OnError(Exception error)
        {
            Console.WriteLine(error.Message);
        }

        public void OnCompleted()
        {
            Console.WriteLine("Log Message Is Completed.....");
        }
    }
}
