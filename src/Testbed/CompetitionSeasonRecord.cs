namespace Testbed
{
    public class CompetitionSeasonRecord
    {
        public int competition_id { get; set; }
        public int season_id { get; set; }
        public string country_name { get; set; }
        public string competition_name { get; set; }
        public string competition_gender { get; set; }
        public string season_name { get; set; }
        public DateTime match_updated { get; set; }
        public DateTime match_available { get; set; }
    }
}