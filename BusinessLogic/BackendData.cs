using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Unidecode.NET;
using System.Data.SQLite;
namespace BusinessLogic
{
    public static class BackendData
    {
        //public List<Player> PlayerList = new List<Player>();
        public static FantasyPlayer fantasyPlayer = new FantasyPlayer();
        public static TeamPosition teamPosition = new TeamPosition();
        public static Team teamDetail = new Team();
        public static List<Element> filteredPlayers = new List<Element>();
        private static string json;
        public static int numberOfPlayers = 0;
        public static int numberOfCurrentPlayer = 1;
        public static int databaseLocation = 0;
        private static Element currentPlayer;
        private static int maxGames = 38;

        private static readonly string jsonTextFile = @"C:\Home\dropbox\fantasyfootball\JSON.txt";
        public static readonly string photoFolder = @"C:\Home\dropbox\fantasyfootball\Photos\";
        public static readonly string databaseFolder = @"C:\Home\dropbox\fantasyfootball\";


        #region sqlite database stuff

        public static void Create_Database()
        {
            if (File.Exists(databaseFolder + "FantasyData.sqlite")) { }
            else
            {
                SQLiteConnection.CreateFile(databaseFolder + "FantasyData.sqlite");
            }
        }
        
        
        
        #endregion


        public static void filter_Players(string filter)
        {
            //iterate over the Elements of fantasyPlayer and create a filtered list

            //Clear filtered List of players
            filteredPlayers.Clear();
            //Check to see if filter is a team
            
            
            
                foreach (Element player in fantasyPlayer.Element)
            {
                switch (filter)
                {
                    case "All":
                        filteredPlayers.Add(player);
                        break;
                    case "DreamTeam":
                        if (player.InDreamteam)
                        {
                            filteredPlayers.Add(player);
                        }
                        break;
                    case "GoalKeeper":
                        if (player.ElementType==1)
                        {
                            filteredPlayers.Add(player);
                        }
                        break;
                    case "Defender":
                        if (player.ElementType == 2)
                        {
                            filteredPlayers.Add(player);
                        }
                        break;
                    case "Midfielder":
                        if (player.ElementType == 3)
                        {
                            filteredPlayers.Add(player);
                        }
                        break;
                    case "Forward":
                        if (player.ElementType == 4)
                        {
                            filteredPlayers.Add(player);
                        }
                        break;
                    case "Arsenal":
                        if (player.Team == 1)
                        {
                            filteredPlayers.Add(player);
                        }
                        break;
                    case "Chelsea":
                        if (player.Team == 6)
                        {
                            filteredPlayers.Add(player);
                        }
                        break;
                    case "Liverpool":
                        if (player.Team == 12)
                        {
                            filteredPlayers.Add(player);
                        }
                        break;
                    default:
                        int teamNo = Get_Team_Id(filter);
                        if (player.Team == teamNo)
                         {
                            filteredPlayers.Add(player);
                         }
                        break;
                        
                }
                           

                

            }
            set_numberOfPlayers();
            databaseLocation = 0;
            numberOfCurrentPlayer = databaseLocation + 1;
            currentPlayer = filteredPlayers[databaseLocation];
        }
        public static List<string> getPlayerNames()
        {
            var playersNamesList = new List<string>();
            foreach (var player in filteredPlayers)
            {
                string lastName = player.SecondName.ToASCII();
                playersNamesList.Add(lastName);
            }
            return playersNamesList;
        }
        public static void get_player_search(string player)
        {
            int searchCount = 0;
            foreach (Element search_player in filteredPlayers)
            {
                if ((search_player.SecondName.ToLower().ToASCII() ==player.ToLower()) || (search_player.FirstName.ToLower().ToASCII()==player.ToLower()))
                {
                    numberOfCurrentPlayer = searchCount+1;
                    currentPlayer = filteredPlayers[searchCount];
                    databaseLocation = searchCount;
                }
                searchCount++;
            }
        }
        public static string ToASCII(this string s)
        {
            var temp = String.Join("",
                               s.Normalize(NormalizationForm.FormD)
                .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));
            if (temp == s)
            {
                temp = s.Unidecode();
            }
            return temp;
        }
        public static void set_numberOfPlayers()
        {
            //numberOfPlayers = fantasyPlayer.Element.Length;
            numberOfPlayers = filteredPlayers.Count;
        }
        public static void increment_Player()
        {
            if (databaseLocation < numberOfPlayers-1)
            {
                databaseLocation++;
                numberOfCurrentPlayer = databaseLocation + 1;
                //currentPlayer = fantasyPlayer.Element[databaseLocation];
                currentPlayer = filteredPlayers[databaseLocation];
            }
         }
        public static void increment_Player(Element[] array)
        {
            if (databaseLocation < array.Length)
            {
                databaseLocation++;
                numberOfCurrentPlayer = databaseLocation + 1;
                currentPlayer = array[databaseLocation];
            }
        }
        public static void decrement_Player()
        {
            if (databaseLocation > 0 )
            {
                databaseLocation--;
                numberOfCurrentPlayer = databaseLocation + 1;
                //currentPlayer = fantasyPlayer.Element[databaseLocation];
                currentPlayer = filteredPlayers[databaseLocation];
            }       
       
        }
        public static string get_Current_Player_Code()
        {
            
            return currentPlayer.Code.ToString();

        }
        public static Element get_Current_Player()
        {
            return currentPlayer;
        }
        public static Decimal Calculate_PPPM()
        {
            return ((Convert.ToDecimal(currentPlayer.TotalPoints))/Convert.ToInt32(fantasyPlayer.Game_Week) * maxGames) / (currentPlayer.NowCost / 10);
        }
        public static Decimal Calculate_PTP()
        {
            return ((Convert.ToDecimal(currentPlayer.TotalPoints)) / Convert.ToInt32(fantasyPlayer.Game_Week) * maxGames);
        }
        public static Decimal Get_Games_Played()
        {
            if (currentPlayer.TotalPoints > 0)
            {
                return (Convert.ToDecimal(currentPlayer.TotalPoints) / Convert.ToDecimal(currentPlayer.PointsPerGame));
            }
            else
            {
                return  0;
            }
            
        }
        public static void Update_Data()
        {

            Get_JSON_String();
            fantasyPlayer = FantasyPlayer.FromJson(json);
            teamPosition = TeamPosition.FromJson(json);
            teamDetail = Team.FromJson(json);
            filter_Players("All");
            set_numberOfPlayers();
            //currentPlayer = fantasyPlayer.Element[databaseLocation];
            currentPlayer = filteredPlayers[databaseLocation];
        }
        public static string Get_Player_Position()
        {
            string position ="";
            foreach (ElementType pos in teamPosition.ElementTypes)
            {
                if (currentPlayer.ElementType == pos.Id)
                {
                    position = pos.SingularName;
                }
            }
            return (position);
             
        }
        public static string Get_Team_Name()
        {
            string team = "";
            foreach (TeamElement t in teamDetail.Teams)
            {
                if (currentPlayer.Team == t.Id)
                {
                    team = t.Name + "  (" + t.Id + ") ";

                }
            }
            
            return (team);

        }
        public static List<string> Get_List_Of_Teams()
        {
            List <string> teams = new List<string> ();
            foreach (TeamElement t in teamDetail.Teams)
            {
                teams.Add(t.Name);                
            }

            return teams;

        }
        public static int Get_Team_Id(string teamName)
        {
            int teamID = 1;
            foreach (TeamElement t in teamDetail.Teams)
            {

                
                if (teamName == t.Name)
                {
                    teamID = (Convert.ToInt32(t.Id));
                    
                }
                

                
            }
            //teamID = 2;
            return teamID;

        }
        public static void Download_Photos()
        {
            long photoID;

            WebClient client = new WebClient();
            foreach(Element player in fantasyPlayer.Element)
            {
                photoID = player.Code;
                //check if that file exists
                string holder = photoFolder + (photoID.ToString()) + ".png";

                if (File.Exists(holder))
                {
                    //do nothing
                }else
                try
                {
                    using (client)
                    {
                        client.DownloadFile("https://platform-static-files.s3.amazonaws.com/premierleague/photos/players/110x140/p" + photoID.ToString() + ".png",
                                             photoFolder + photoID.ToString() + ".png");
                    }
                }
                catch (Exception e) { };

            }
        }
        private static void Get_JSON_String()
        {
            // This servicepointmanager is added to avoid SSL error when accessing this site.
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient() { Encoding = Encoding.UTF8 };
            try
            {
                using (client)
                {

                    if (File.Exists(jsonTextFile))
                    {
                        //Read json from text file
                        json = System.IO.File.ReadAllText(jsonTextFile);
                     }
                    else
                    {
                        //connect to the Fantasy Football JSON file
                        json = client.DownloadString("https://fantasy.premierleague.com/drf/bootstrap-static");

                        //If no text file JSON.txt Write JSON file to local text file Later to be implemented as history of JSON files in a database
                        //this text file is temproray for readiing the output of the reader routine

                        using (System.IO.StreamWriter file =
                                 new System.IO.StreamWriter(jsonTextFile))
                            file.WriteLine(json);
                    }
                }

            }
            catch (Exception e) { };
        }
        public static string Get_Pretty_JSON()
        {
            dynamic prettyJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(prettyJson,Formatting.Indented);        }
    }
}
