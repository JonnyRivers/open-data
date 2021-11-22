using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Testbed
{
    internal class OpenDataClient
    {
        public void Load(string path)
        {
            string compentitionsFilePath = Path.Combine(path, "competitions.json");
            IEnumerable<CompetitionSeasonRecord> competitionRecords = LoadCompetitions(compentitionsFilePath);
            foreach(CompetitionSeasonRecord competitionRecord in competitionRecords)
            {
                string matchesFilePath = Path.Combine(path, "matches", competitionRecord.competition_id.ToString(), $"{competitionRecord.season_id}.json");
                IEnumerable<MatchRecord> matchRecords = LoadMatches(matchesFilePath);
                foreach (MatchRecord matchRecord in matchRecords)
                {
                    string lineupsFilePath = Path.Combine(path, "lineups", $"{matchRecord.match_id}.json");
                    IEnumerable<LineupTeamRecord> lineupRecords = LoadLineups(lineupsFilePath);

                    string eventsFilePath = Path.Combine(path, "events", $"{matchRecord.match_id}.json");
                    LoadEvents(eventsFilePath);
                }
            }
            
        }

        private IEnumerable<CompetitionSeasonRecord> LoadCompetitions(string path)
        {
            string json = File.ReadAllText(path);
            var records = JsonSerializer.Deserialize<IEnumerable<CompetitionSeasonRecord>>(json);
            return records;
        }

        private IEnumerable<MatchRecord> LoadMatches(string path)
        {
            string json = File.ReadAllText(path);
            var records = JsonSerializer.Deserialize<IEnumerable<MatchRecord>>(json);
            return records;
        }

        private IEnumerable<LineupTeamRecord> LoadLineups(string path)
        {
            string json = File.ReadAllText(path);
            var records = JsonSerializer.Deserialize<IEnumerable<LineupTeamRecord>>(json);
            return records;
        }

        private void LoadEvents(string path)
        {
            string json = File.ReadAllText(path);
        }
    }
}
