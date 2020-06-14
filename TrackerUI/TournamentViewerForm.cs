using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        BindingSource roundsBinding = new BindingSource();
        BindingSource matchupsBinding = new BindingSource();

        public TournamentViewerForm(TournamnetModel tournamnetModel)
        {
            InitializeComponent();

            tournamnet = tournamnetModel;

            //WireUpRoundsLists();

           // WireUpMatchupsLists();

            LoadFormData();

            LoadRounds();
        }

        private void LoadFormData()
        {
            tournamentName.Text = tournamnet.TournamentName;
        }

        private void WireUpRoundsLists()
        {
            //roundDropDown.DataSource = null;
            roundDropDown.DataSource = rounds;
        }
        private void WireUpMatchupsLists()
        {
         
            //matchupListBox.DataSource = null;
            matchupListBox.DataSource = selectedMatchups;
            matchupListBox.DisplayMember = "DisplayName";
        }
        private void LoadRounds()
        {
            rounds = new BindingList<int>();

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

           // roundsBinding.ResetBindings(false);
            WireUpRoundsLists(); 
        }

        private void roundDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchups();
        }

        private void LoadMatchups()
        {
            int round = (int)roundDropDown.SelectedItem;

            foreach (List<MatchupModel> matchups in tournamnet.Rounds)
            {
                if (matchups.First().MatchupRound == round)
                {

                    selectedMatchups = new BindingList<MatchupModel>(matchups);
                }
            }

           // matchupsBinding.ResetBindings(false);
            WireUpMatchupsLists();
        }

        private void LoadMatchup()
        {
            MatchupModel m = (MatchupModel)matchupListBox.SelectedItem;

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
        private void matchupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchup();
        }
    }
}
