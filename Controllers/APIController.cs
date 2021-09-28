using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;


namespace CS451RWebApp.Controllers 
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase 
    {
        private readonly IConfiguration _configuration;
        public TransactionsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/<TransactionsController>/
        // Multiple Parameters
        [HttpGet()]
        public async Task<string> GetAsync(string id) 
        {
            string connString = this._configuration.GetConnectionString("localDB");
            //variables to store the query results
            string output = "[";
            string songName, artistName, albumName;
            int songID, artistID, albumID;

            try
            {
                //sql connection object
                using var conn = new MySqlConnection(connString);

                //retrieve the SQL Server instance version
                string query = @"SELECT song.songName, artist.artistName, album.albumName, song.songID, artist.artistID, album.albumID
                                    FROM song
                                    LEFT JOIN artist ON song.artistID = artist.artistID
                                    INNER JOIN album ON song.albumID = album.albumID WHERE song.songName LIKE @p;
                                     ";

                //open connection
                await conn.OpenAsync();

                //define the SqlCommand object and execute
                using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@p", string.Format("%{0}%", id));

                // using var cmd = new MySqlCommand(query, conn);
                using var reader = await cmd.ExecuteReaderAsync();

                Console.WriteLine(Environment.NewLine + "Retrieving data from database..." + Environment.NewLine);
                string pattern = "{{\"songName\":\"{0}\",\"artistName\":\"{1}\",\"albumName\":\"{2}\",\"songID\":{3},\"artistID\":{4},\"albumID\":{5}}}";
                bool first = true;

                //check if there are records
                while (await reader.ReadAsync())
                {
                    songName = reader.GetString(0);
                    artistName = reader.GetString(1);
                    albumName = reader.GetString(2);
                    songID = reader.GetInt32(3);
                    artistID = reader.GetInt32(4);
                    albumID = reader.GetInt32(5);

                    //display retrieved record
                    if (!first) { output += ","; }
                    output += string.Format(pattern, songName, artistName, albumName, songID.ToString(), artistID.ToString(), albumID.ToString());
                    first = false;
                }
                output += "]";
            }
            catch (Exception ex)
            {
                //display error message
                Console.WriteLine("Exception: " + ex.Message);
            }


            return output;
        }
    }
}
