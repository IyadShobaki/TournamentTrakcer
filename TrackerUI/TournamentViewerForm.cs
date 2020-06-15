using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class TournamentViewerForm : Form
    {
        private TournamnetModel tournamnet;
        //List<int> rounds = new List<int>();
        //List<MatchupModel> selectedMatchups = new List<MatchupModel>();
        BindingList<int> rounds = new BindingList<int>();
        BindingList<MatchupModel> selectedMatchups = new BindingList<MatchupModel>();


        public TournamentViewerForm(TournamnetModel tournamnetModel)
        {
            InitializeComponent();

            tournamnet = tournamnetModel;

            WireUpLists();

            LoadFormData();

            LoadRounds();
        }

        private void LoadFormData()
        {
            tournamentName.Text = tournamnet.TournamentName;
        }

        private void WireUpLists()
        {
      
            roundDropDown.DataSource = rounds;
            matchupListBox.DataSource = selectedMatchups;
            matchupListBox.DisplayMember = "DisplayName";
        }
      
        private void LoadRounds()
        {
            rounds.Clear();
            rounds.Add(1);
            int currRound = 1;

            foreach (List<MatchupModel> matchups in tournamnet.Rounds)
            {
                if (matchups.First().MatchupRound > currRound)
                {
                    currRound = matchups.First().MatchupRound;
                    rounds.Add(currRound);
                    
                }
            }
            LoadMatchups(1);
        }

        private void roundDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchups((int)roundDropDown.SelectedItem);
        }

        private void LoadMatchups(int round)
        {

            foreach (List<MatchupModel> matchups in tournamnet.Rounds)
            {
                if (matchups.First().MatchupRound == round)
                {
                    selectedMatchups.Clear();
                    foreach (MatchupModel m in matchups)
                    {
                        if (m.Winner == null || !unplayedOnlyCheckBox.Checked)
                        {
                            selectedMatchups.Add(m);
                        }
                      
                    }           
                }
            }

            if (selectedMatchups.Count > 0)
            {
                LoadMatchup(selectedMatchups.First()); 
            }

            DisplayMatchupInfo();
        }

        private void DisplayMatchupInfo()
        {
            bool isVisible = selectedMatchups.Count > 0;
            teamOneName.Visible = isVisible;
            teamOneScoreLabel.Visible = isVisible;
            teamOneScoreValue.Visible = isVisible;

            teamTwoName.Visible = isVisible;
            teamTwoScoreLabel.Visible = isVisible;
            teamTwoScoreValue.Visible = isVisible;

            versusLabel.Visible = isVisible;
            scoreButton.Visible = isVisible;

        }
        private void LoadMatchup(MatchupModel m)
        {
            if (m != null)   //Iyad add it -- without this if statement the application will crash. Teacher code worked without it!!! 
            {

                for (int i = 0; i < m.Entries.Count; i++)
                {
                    if (i == 0)
                    {
                        if (m.Entries[0].Teamcompeting != null)
                        {
                            teamOneName.Text = m.Entries[0].Teamcompeting.TeamName;
                            teamOneScoreValue.Text = m.Entries[0].Score.ToString();

                            teamTwoName.Text = "<bye>";
                            teamTwoScoreValue.Text = "0";
                        }
                        else
                        {
                            teamOneName.Text = "Not Yet Set";
                            teamOneScoreValue.Text = "";
                        }
                    }

                    if (i == 1)
                    {
                        if (m.Entries[1].Teamcompeting != null)
                        {
                            teamTwoName.Text = m.Entries[1].Teamcompeting.TeamName;
                            teamTwoScoreValue.Text = m.Entries[1].Score.ToString();
                        }
                        else
                        {
                            teamTwoName.Text = "Not Yet Set";
                            teamTwoScoreValue.Text = "";
                        }
                    }
                }
            }
        }
        private void matchupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchup((MatchupModel)matchupListBox.SelectedItem);
        }

        private void unplayedOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LoadMatchups((int)roundDropDown.SelectedItem);
        }

        private void scoreButton_Click(object sender, EventArgs e)
        {
            MatchupModel m = (MatchupModel)matchupListBox.SelectedItem;
            double teamOneScore = 0;
            double teamTwoScore = 0;

            for (int i = 0; i < m.Entries.Count; i++)
            {
                if (i == 0)
                {
                    if (m.Entries[0].Teamcompeting != null)
                    {         
                        bool scoreValid = double.TryParse(teamOneScoreValue.Text, out teamOneScore);
                        if (scoreValid)
                        {
                            m.Entries[0].Score = teamOneScore;
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid score for team 1.");
                            return;
                        }
                       
                    }         
                }

                if (i == 1)
                {
                    if (m.Entries[1].Teamcompeting != null)
                    {
                        bool scoreValid = double.TryParse(teamTwoScoreValue.Text, out teamTwoScore);
                        if (scoreValid)
                        {
                            m.Entries[1].Score = teamTwoScore;
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid score for team 2.");
                            return;
                        }
                    }
                    
                }
            }

            TournamentLogic.UpdateTournamentResult(tournamnet);

            LoadMatchups((int)roundDropDown.SelectedItem);
        }
    }
}





//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using TrackerLibrary.Models;

//namespace TrackerUI
//{
//    public partial class TournamentViewerForm : Form
//    {
//        private TournamnetModel tournamnet;
//        //List<int> rounds = new List<int>();
//        //List<MatchupModel> selectedMatchups = new List<MatchupModel>();
//        BindingList<int> rounds = new BindingList<int>();
//        BindingList<MatchupModel> selectedMatchups = new BindingList<MatchupModel>();


//        public TournamentViewerForm(TournamnetModel tournamnetModel)
//        {
//            InitializeComponent();

//            tournamnet = tournamnetModel;

//            // WireUpRoundsLists();

//            //WireUpMatchupsLists();

//            LoadFormData();

//            LoadRounds();
//        }

//        private void LoadFormData()
//        {
//            tournamentName.Text = tournamnet.TournamentName;
//        }

//        private void WireUpRoundsLists()
//        {
//            //roundDropDown.DataSource = null;
//            roundDropDown.DataSource = rounds;
//        }
//        private void WireUpMatchupsLists()
//        {

//            //matchupListBox.DataSource = null;
//            matchupListBox.DataSource = selectedMatchups;
//            matchupListBox.DisplayMember = "DisplayName";
//        }
//        private void LoadRounds()
//        {
//            rounds = new BindingList<int>();

//            rounds.Add(1);
//            int currRound = 1;

//            foreach (List<MatchupModel> matchups in tournamnet.Rounds)
//            {
//                if (matchups.First().MatchupRound > currRound)
//                {
//                    currRound = matchups.First().MatchupRound;
//                    rounds.Add(currRound);

//                }
//            }

//            // roundsBinding.ResetBindings(false);
//            WireUpRoundsLists();
//        }

//        private void roundDropDown_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            LoadMatchups();
//        }

//        private void LoadMatchups()
//        {
//            int round = (int)roundDropDown.SelectedItem;

//            foreach (List<MatchupModel> matchups in tournamnet.Rounds)
//            {
//                if (matchups.First().MatchupRound == round)
//                {

//                    selectedMatchups = new BindingList<MatchupModel>(matchups);
//                }
//            }

//            // matchupsBinding.ResetBindings(false);
//            WireUpMatchupsLists();
//        }

//        private void LoadMatchup()
//        {
//            MatchupModel m = (MatchupModel)matchupListBox.SelectedItem;

//            for (int i = 0; i < m.Entries.Count; i++)
//            {
//                if (i == 0)
//                {
//                    if (m.Entries[0].Teamcompeting != null)
//                    {
//                        teamOneName.Text = m.Entries[0].Teamcompeting.TeamName;
//                        teamOneScoreValue.Text = m.Entries[0].Score.ToString();

//                        teamTwoName.Text = "<bye>";
//                        teamTwoScoreValue.Text = "0";
//                    }
//                    else
//                    {
//                        teamOneName.Text = "Not Yet Set";
//                        teamOneScoreValue.Text = "";
//                    }
//                }

//                if (i == 1)
//                {
//                    if (m.Entries[1].Teamcompeting != null)
//                    {
//                        teamTwoName.Text = m.Entries[1].Teamcompeting.TeamName;
//                        teamTwoScoreValue.Text = m.Entries[1].Score.ToString();
//                    }
//                    else
//                    {
//                        teamTwoName.Text = "Not Yet Set";
//                        teamTwoScoreValue.Text = "";
//                    }
//                }
//            }
//        }
//        private void matchupListBox_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            LoadMatchup();
//        }

//        private void unplayedOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
//        {

//        }
//    }
//}
