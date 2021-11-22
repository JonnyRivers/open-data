namespace Testbed
{
    public class LineupTeamRecord
    {
        public int team_id { get; set; }
        public string team_name { get; set; }
        public IEnumerable<LineupPlayerRecord> lineup { get; set; }
    }
}