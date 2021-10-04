using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace CS451RWebApp.Controllers 
{
    [Route("api/getTransactionHistory")]
    [ApiController]
    public class TransactionsController : ControllerBase 
    {
        private readonly IConfiguration _configuration;
        private class Transaction
        {
            public int TransactionID { get; set; }
            public int TimeMonth { get; set; }
            public int TimeDay { get; set; }
            public int TimeYear { get; set; }
            public int AmountDollars { get; set; }
            public int AmountCents { get; set; }
            public int EndBalanceDollars { get; set; }
            public int EndBalanceCents { get; set; }
            public string Vendor { get; set; }
        }

        private class TransactionHistory
        {
            public List<Transaction> Transactions { get; set; }
            public TransactionHistory()
            {
                Transactions = new List<Transaction>();
            }
        }

        public TransactionsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public async Task<string> GetAsync(string ID) 
        {
            string connString = this._configuration.GetConnectionString("localDB");
            //variables to store the query results
            string output;
            TransactionHistory history = new TransactionHistory();

            try
            {
                //sql connection object
                using var conn = new MySqlConnection(connString);

                //retrieve the SQL Server instance version
                string query = @"SELECT TransactionID, TimeMonth, TimeDay, TimeYear, AmountDollars, AmountCents, EndBalanceDollars, EndBalanceCents, Vendor
                                FROM transaction
                                WHERE AccountID = @ID;";

                //open connection
                await conn.OpenAsync();

                //define the SqlCommand object and execute
                using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", ID);

                // using var cmd = new MySqlCommand(query, conn);
                using var reader = await cmd.ExecuteReaderAsync();
                Console.WriteLine(Environment.NewLine + "Retrieving data from database..." + Environment.NewLine);

                //check if there are records
                while (await reader.ReadAsync())
                {

                    history.Transactions.Add(new Transaction()
                    {
                        TransactionID = reader.GetInt32(0),
                        TimeMonth = reader.GetInt32(1),
                        TimeDay = reader.GetInt32(2),
                        TimeYear = reader.GetInt32(3),
                        AmountDollars = reader.GetInt32(4),
                        AmountCents = reader.GetInt32(5),
                        EndBalanceDollars = reader.GetInt32(6),
                        EndBalanceCents = reader.GetInt32(7),
                        Vendor = reader.GetString(8),
                    });
                }
                output = JsonSerializer.Serialize(history);
                return output;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
