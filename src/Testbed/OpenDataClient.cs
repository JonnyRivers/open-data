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
            IEnumerable<Dictionary<string, JsonElement>> records = JsonSerializer.Deserialize<IEnumerable<Dictionary<string, JsonElement>>>(json);
            foreach (var record in records)
            {
                IdNamePair eventType = record["type"].Deserialize<IdNamePair>();

                switch (eventType.name)
                {
                    case "Starting XI":
                        ProcessStartingEleven(record);
                        break;
                    case "Half Start":
                        ProcessHalfStart(record);
                        break;
                    case "Pass":
                        ProcessPass(record);
                        break;
                    case "Ball Receipt*":
                        ProcessBallReceipt(record);
                        break;
                    case "Carry":
                        ProcessCarry(record);
                        break;
                    default:
                        throw new Exception("unknown event");
                }
            }
        }

        private void ProcessStartingEleven(Dictionary<string, JsonElement> elementsByName)
        {
            IdNamePair team = elementsByName["team"].Deserialize<IdNamePair>();
            Console.WriteLine($"Starting eleven for {team.name}");
        }

        private void ProcessHalfStart(Dictionary<string, JsonElement> elementsByName)
        {
            IdNamePair team = elementsByName["team"].Deserialize<IdNamePair>();
            Console.WriteLine($"Half starting for {team.name}");
        }

        private void ProcessPass(Dictionary<string, JsonElement> elementsByName)
        {
            IdNamePair player = elementsByName["player"].Deserialize<IdNamePair>();
            Dictionary<string, JsonElement> pass = elementsByName["pass"].Deserialize<Dictionary<string, JsonElement>>();
            IdNamePair recipient = pass["recipient"].Deserialize<IdNamePair>();

            Console.WriteLine($"Pass from {player.name} intended for {recipient.name}");
        }

        private void ProcessBallReceipt(Dictionary<string, JsonElement> elementsByName)
        {
            IdNamePair player = elementsByName["player"].Deserialize<IdNamePair>();

            Console.WriteLine($"Pass received by {player.name}");
        }

        private void ProcessCarry(Dictionary<string, JsonElement> elementsByName)
        {
            IdNamePair player = elementsByName["player"].Deserialize<IdNamePair>();

            Console.WriteLine($"Ball carried by {player.name}");
        }
    }
}
