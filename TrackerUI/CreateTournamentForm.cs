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
    public partial class CreateTournamentForm : Form, IPrizeRequester, ITeamRequester
    {
        List<TeamModel> availableTeams = GlobalConfig.Connection.GetTeam_All();
        List<TeamModel> selectedTeams = new List<TeamModel>();
        List<PrizeModel> selectedPrizes = new List<PrizeModel>();


        public CreateTournamentForm()
        {
            InitializeComponent();

            WireUpLists();
        }

        private void WireUpLists()
        {
            selectTeamDropDown.DataSource = null;

            selectTeamDropDown.DataSource = availableTeams;
            //selectTeamDropDown.DisplayMember = "TeamName";
            selectTeamDropDown.DisplayMember = nameof(TeamModel.TeamName);

            tournamentTeamsListBox.DataSource = null;

            tournamentTeamsListBox.DataSource = selectedTeams;
            //tournamentTeamsListBox.DisplayMember = "TeamName";
            tournamentTeamsListBox.DisplayMember = nameof(TeamModel.TeamName);

            prizesListBox.DataSource = null;

            prizesListBox.DataSource = selectedPrizes;
           // prizesListBox.DisplayMember = "PlaceName";  old code 
            prizesListBox.DisplayMember = nameof(PrizeModel.PlaceName); //Youtube video comment
        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {
            
            TeamModel t = (TeamModel)selectTeamDropDown.SelectedItem;
            if(t != null)
            {
                availableTeams.Remove(t);
                selectedTeams.Add(t);

                WireUpLists();
            }
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            // 1- Call the CreatePrizeForm
            CreatePrizeForm frm = new CreatePrizeForm(this);
            frm.Show();

          
        }

        // 2- Get back from the form a PrizeModel
        public void PrizeComplete(PrizeModel model)
        {
            // 3- Take the PrizeModel and put it into our list of selected prizes
            selectedPrizes.Add(model);
            WireUpLists();

        }

        public void TeamComplete(TeamModel model)
        {
            selectedTeams.Add(model);
            WireUpLists();
        }

        private void createNewTeamLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeamForm from = new CreateTeamForm(this);
            from.Show();
        }

        private void removeSelectedPlayerButton_Click(object sender, EventArgs e)
        {
            TeamModel t = (TeamModel)tournamentTeamsListBox.SelectedItem;

            if (t != null)
            {
                selectedTeams.Remove(t);
                availableTeams.Add(t);

                WireUpLists();
            }
        }

        private void removeSelectedPrizeButton_Click(object sender, EventArgs e)
        {
            PrizeModel p = (PrizeModel)prizesListBox.SelectedItem;

            if (p != null)
            {
                selectedPrizes.Remove(p);

                WireUpLists();
            }
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            //Validate data
            decimal fee = 0;
            bool feeAcceptable = decimal.TryParse(entryFeeValue.Text, out fee);
            if (!feeAcceptable)
            {
                MessageBox.Show("You need to enter a valid Entry Fee.", "Invalid Fee",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 1-Create our Tournament model
            TournamnetModel tm = new TournamnetModel();
            tm.TournamentName = tournamentNameValue.Text;
            tm.EntryFee = fee;

            //foreach (PrizeModel prize in selectedPrizes)
            //{
            //    tm.Prizes.Add(prize);
            //}
            //instead of for loop 
            tm.Prizes = selectedPrizes;
            tm.EnteredTeams = selectedTeams;

            // 2-Create/wire up our matchups
            TournamentLogic.CreateRounds(tm);

            // 3-Create Tournament entry
            // 4-Create all the prizes entries
            // 5-Create all of team entries
            //the next line will do the previous three tasks
            GlobalConfig.Connection.CreateTournament(tm);

           
        }
    }
}
