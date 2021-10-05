using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace CS451RWebApp.Controllers 
{
    [ApiController]
    public class BackendController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public BackendController(IConfiguration configuration, ILogger<BackendController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public static bool IsEmpty<T>(List<T> list)
        {
            if (list == null)
            {
                return true;
            }

            return !list.Any();
        }

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

        public class RequestID
        {
            public string ID { get; set; }
        }

        [HttpPost]
        [Route("api/getTransactionHistory")]
        public async Task<ActionResult> GetAsyncTransactionHistory([FromBody] RequestID body) 
        {
            string connString = this._configuration.GetConnectionString("localDB");
            TransactionHistory history = new TransactionHistory();

            try
            {
                //sql connection object
                using var conn = new MySqlConnection(connString);

                //retrieve the SQL Server instance version
                string filePath = $"sql{Path.DirectorySeparatorChar}getTransactionHistory.sql";
                string query = System.IO.File.ReadAllText(filePath);

                //open connection
                await conn.OpenAsync();

                //define the SqlCommand object and execute
                using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", body.ID);

                // using var cmd = new MySqlCommand(query, conn);
                using var reader = await cmd.ExecuteReaderAsync();
                _logger.LogInformation(string.Format("Retrieving TransactionHistory for ID={0} from database.", body.ID));

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
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Problem("Error accessing database, contact site admin for more info");
            }

            if (IsEmpty(history.Transactions))
            {
                return NotFound(string.Format("Account {0} does not exist", body.ID));
            }

            return Ok(history);
        }
    }
}
