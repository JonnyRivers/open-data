namespace Testbed
{
    public class LineupPlayerRecord
    {
        public int player_id { get; set; }
        public string player_name { get; set; }
        public int jersey_number { get; set; }
        public CountryRecord country { get; set; }
    }
}