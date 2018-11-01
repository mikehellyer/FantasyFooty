using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessLogic;


namespace FantasyFootball
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string noPicFile = @"C:\Home\dropbox\fantasyfootball\Photos\Photo-Missing.png";
        
        
        BitmapImage noPlayerImage = new BitmapImage();
        public MainWindow()
        {
            InitializeComponent();
            BackendData.Update_Data();
            lblCurrentWeek.Content = "Game Week: " + BackendData.fantasyPlayer.Game_Week;
            lblPlayerCount.Content = BackendData.numberOfCurrentPlayer.ToString() + " Of " + BackendData.numberOfPlayers.ToString();
            
            //Add items to listbox
            cmbSelectFilter.Items.Add("All");
            cmbSelectFilter.Items.Add("DreamTeam");
            cmbSelectFilter.Items.Add("GoalKeeper");
            cmbSelectFilter.Items.Add("Defender");
            cmbSelectFilter.Items.Add("Midfielder");
            cmbSelectFilter.Items.Add("Forward");
            foreach (string t in BackendData.Get_List_Of_Teams())
            {
                cmbSelectFilter.Items.Add(t);
            }
            


            Update_Screen();
        }

        

        private void Update_Screen()
        {
            Element currentPlayer = BackendData.get_Current_Player();
           
            //string playerImage = @"C:\Home\dropbox\fantasyfootball\Photos\" + BackendData.get_Current_Player_Code() + ".png";
            try
            {
                
                Image screenImage = new Image();
                BitmapImage playerImage = new BitmapImage();
                string picfile = @"C:\Home\dropbox\fantasyfootball\Photos\" + BackendData.get_Current_Player_Code() + ".png";
                lblPhotoFile.Content = picfile;
                
                playerImage.BeginInit();
                playerImage.UriSource = new Uri(picfile, UriKind.Absolute);
                playerImage.EndInit();
                imgPlayer.Source = playerImage;
                
            }
            catch (Exception imageException)
            {
                Image screenImage = new Image();
                BitmapImage playerImage = new BitmapImage();
                string picfile = noPicFile;
                lblPhotoFile.Content = picfile;

                playerImage.BeginInit();
                playerImage.UriSource = new Uri(picfile, UriKind.Absolute);
                playerImage.EndInit();
                imgPlayer.Source = playerImage; ;
            };

            lblPlayerCount.Content = BackendData.numberOfCurrentPlayer.ToString() + " Of " + BackendData.numberOfPlayers.ToString();
                      
            var temp = Encoding.UTF8.GetBytes(currentPlayer.FirstName.ToString());
            lblFirstName.Content = (currentPlayer.FirstName);
            lblSecondName.Content = currentPlayer.SecondName;
            lblTeam.Content = BackendData.Get_Team_Name();
            lblTotalPoints.Content = "Total Points: " + currentPlayer.TotalPoints;
            
            if (currentPlayer.TotalPoints > 0)
            { lblAverageMinutes.Content = "Avg. Minutes : " + (currentPlayer.Minutes / Convert.ToDecimal(BackendData.Get_Games_Played())).ToString("0"); }
            else
            { lblAverageMinutes.Content = "Avg. minutes : 0"; }
            lblPosition.Content = BackendData.Get_Player_Position();
            lblValue.Content = "Value: " + (currentPlayer.NowCost);
            lblPPG.Content = "Points Per Game: " + currentPlayer.PointsPerGame.ToString();
                        
            DateTimeOffset? date1 = (currentPlayer.NewsAdded);
            lblChanceOfPlaying.Content = (String.Format("{0:dd/MMM/yyyy}", date1) + " - " + currentPlayer.News);

            

            lblPPPM.Content = "PPPM(PTP): " + ((BackendData.Calculate_PPPM()).ToString("0.##") + "(" + BackendData.Calculate_PTP().ToString("0"))+")";

            if (currentPlayer.TotalPoints > 0)
            {
                lblGamesPlayed.Content = "No Of Games: " + BackendData.Get_Games_Played().ToString("0");
            }
            else
            { lblGamesPlayed.Content = "No Of Games: N/A"; }

            lblPPW.Content = "Points Per Week: " + ((currentPlayer.TotalPoints) / Convert.ToDecimal(BackendData.fantasyPlayer.Game_Week)).ToString("0.#");
            
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            BackendData.increment_Player();
            Update_Screen();
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            BackendData.decrement_Player();
            Update_Screen();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text != "")
            {
                //get the searched for player
                BackendData.get_player_search(txtSearch.Text);
                Update_Screen();
            }
        }

        private void checkAutoComplete()
        {
            var playersNameList = BackendData.getPlayerNames();
            //txtSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //txtSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //txtSearch.AutoCompleteCustomSource.AddRange(playersNameList.ToArray());


        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbSelectFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BackendData.filter_Players(cmbSelectFilter.SelectedItem.ToString());
            
            Update_Screen();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
             string etr = e.Key.ToString();

        if (etr == "Return")
        {
                if (txtSearch.Text != "")
                {
                    //get the searched for player
                    BackendData.get_player_search(txtSearch.Text);
                    Update_Screen();
                }
            }
        }

        private void frmMainScreen_GotMouseCapture(object sender, MouseEventArgs e)
        {

        }

        private void btnPhotoUpdate_Click(object sender, RoutedEventArgs e)
        {
            BackendData.Download_Photos();
        }

        private void mnuDatabaseTools_Click(object sender, RoutedEventArgs e)
        {
            DatabaseTools DatabaseWindow = new DatabaseTools();
            DatabaseWindow.Show();
            
        }

        private void mnuUpdatePhotos_Click(object sender, RoutedEventArgs e)
        {
            BackendData.Download_Photos();
        }
    }
}
