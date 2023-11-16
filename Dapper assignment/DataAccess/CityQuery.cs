using Dapper;
using Dapper_assignment.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dapper_assignment.DataAccess
{
    public class CityQuery
    {
        private readonly string _connectionString;

        public CityQuery()
        {
            _connectionString = GetConnectionString();
        }

        private string GetConnectionString()
        {
            // Retrieve the password from environment variables
            string password = Environment.GetEnvironmentVariable("Database__Password");

            // Replace the password in the connection string
            return $"server=localhost;database=world;password={password};user=root";
        }

        public List<City> GetCities(int minPopulation, int maxPopulation)
        {
            List<City> cities = new List<City>();

            string sql = "SELECT * FROM city WHERE population BETWEEN @MinPopulation AND @MaxPopulation";

            using (var connection = new MySqlConnection(_connectionString))
            {
                cities = connection.Query<City>(sql, new { MinPopulation = minPopulation, MaxPopulation = maxPopulation }).ToList();
            }

            return cities;
        }
    }
}