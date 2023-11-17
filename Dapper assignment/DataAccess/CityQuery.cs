using Dapper;
using Dapper_assignment.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using static Dapper_assignment.Models.Country;

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

        public List<City> GetCitiesByPopulationBetween(int minPopulation, int maxPopulation)
        {
            List<City> cities = new List<City>();

            string sql = "SELECT * FROM city WHERE population BETWEEN @MinPopulation AND @MaxPopulation";

            using (var connection = new MySqlConnection(_connectionString))
            {
                cities = connection.Query<City>(sql, new { MinPopulation = minPopulation, MaxPopulation = maxPopulation }).ToList();
            }

            return cities;
        }
        public List<City> GetLimitedCities(int limit)
        {
            List<City> cities = new List<City>();

            string sql = "SELECT * FROM city LIMIT @Limit";

            using (var connection = new MySqlConnection(_connectionString))
            {
                cities = connection.Query<City>(sql, new { Limit = limit }).ToList();
            }

            return cities;
        }

        public List<City> GetCitiesByCountryCode(string countryCode)
        {
            List<City> cities = new List<City>();

            string sql = "SELECT * FROM city WHERE CountryCode = @CountryCode";

            using (var connection = new MySqlConnection(_connectionString))
            {
                cities = connection.Query<City>(sql, new { CountryCode = countryCode }).ToList();
            }

            return cities;
        }

        public List<Country> GetEuropeanCountriesByLifeExpectancy()
        {
            List<Country> countries = new List<Country>();

            string sql = "SELECT * FROM country WHERE continent = 'Europe'" +
                         "ORDER BY LifeExpectancy DESC";

            using (var connection = new MySqlConnection(_connectionString))
            {
                countries = connection.Query<Country>(sql).ToList();
            }

            return countries;
        }

        public List<City> GetCitiesInCountry(string countryCode)
        {
            List<City> cities = new List<City>();

            string sql = "SELECT * FROM city WHERE CountryCode = @CountryCode";

            using (var connection = new MySqlConnection(_connectionString))
            {
                cities = connection.Query<City>(sql, new { CountryCode = countryCode }).ToList();
            }

            return cities;
        }

        public List<City> GetCitiesByContinentWithLifeExpectancyAbove(Continent continent, float minLifeExpectancy)
        {
            List<City> cities = new List<City>();

            // Convert enum to underlying type (usually int)
            int continentValue = Convert.ToInt32(continent);

            string sql = "SELECT * FROM city WHERE CountryCode IN " +
                         "(SELECT CountryCode FROM country WHERE Continent = @Continent AND LifeExpectancy > @MinLifeExpectancy)";

            using (var connection = new MySqlConnection(_connectionString))
            {
                cities = connection.Query<City>(sql, new { Continent = continentValue, MinLifeExpectancy = minLifeExpectancy }).ToList();
            }

            return cities;
        }
    }
}
