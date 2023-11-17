namespace Dapper_assignment.Models
{
    public class Country
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public float LifeExpectancy { get; set; }
        public enum Continent { Asia, Europe, NorthAmerica, Africa, Oceania, Antarctica, SouthAmerica}
    }
}
