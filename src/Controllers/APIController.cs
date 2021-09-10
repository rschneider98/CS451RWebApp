using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CS451RWebApp.Controllers 
{
    //set the connection string
    public static class DBInfo
    {
        public const string connString = @"server=raspberrypi;database=music_library; 
                                            uid=rbs;pwd=KnightsTables;";
    }        

    [Route("api/transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase 
    {
        // GET: api/<TransactionsController>/
        // Multiple Parameters
        [HttpGet()]
        public async Task<string> GetAsync(string id) 
        {
            //variables to store the query results
            string output = "[";
            string songName, artistName, albumName;
            int songID, artistID, albumID;

            try
            {
                //sql connection object
                using var conn = new MySqlConnection(DBInfo.connString);

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

        // POST api/<TransactionsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TransactionsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TransactionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
