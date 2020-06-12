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
    public partial class CreateTeamForm : Form
    {
        //private List<PersonModel> availableTeamMembers = new List<PersonModel>(); //if we need to use the method
        private List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();
        public CreateTeamForm()
        {
            InitializeComponent();

           //CreateSampleData();

            WireUpList();
        }

        //private void LoadListData()
        //{
        //    availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        //}

        //the following method just for testing 
        private void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel { FirstName = "Iyad", LastName = "Shobaki" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Tim", LastName = "Corey" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "Jane", LastName = "Smith" });
            selectedTeamMembers.Add(new PersonModel { FirstName = "Sue", LastName = "Storm" });
        }
        private void WireUpList()
        {
            //Its better to find a better way to refresh the data binding instead
            //of initialize drop down and list box to null
            //the teacher doesn't have a better solution for now
 
            selectTeamMemebrDropDown.DataSource = null;
            //in old version wasn't available the next option (the next code)
            //and you have to create binding data source
            //because the drop down didn't know how to deal with a list directly
            selectTeamMemebrDropDown.DataSource = availableTeamMembers;
            selectTeamMemebrDropDown.DisplayMember = "FullName";

            teamMemebersListBox.DataSource = null;

            teamMemebersListBox.DataSource = selectedTeamMembers;
            teamMemebersListBox.DisplayMember = "FullName";

           
        }
        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PersonModel p = new PersonModel();
                p.FirstName = firstNameValue.Text;
                p.LastName = lastNameValue.Text;
                p.EmailAddress = emailValue.Text;
                p.CellphoneNumber = cellphoneValue.Text;

                p = GlobalConfig.Connection.CreatePerson(p);

                selectedTeamMembers.Add(p);
                WireUpList();

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                cellphoneValue.Text = "";
            }
            else
            {
                MessageBox.Show("You need to fill in all of the fields.");
            }
        }

        private bool ValidateForm()
        {
            if(firstNameValue.Text.Length == 0)
            {
                return false;
            }
            if (lastNameValue.Text.Length == 0)
            {
                return false;
            }
            if (emailValue.Text.Length == 0)
            {
                return false;
            }
            if (cellphoneValue.Text.Length == 0)
            {
                return false;
            }

            return true;
        }

        private void addMemebrButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel) selectTeamMemebrDropDown.SelectedItem;

            if (p != null)
            {
                availableTeamMembers.Remove(p);
                selectedTeamMembers.Add(p);

                WireUpList(); 
            }
            
        }

        private void removeSelectedMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)teamMemebersListBox.SelectedItem;

            if (p != null)
            {
                selectedTeamMembers.Remove(p);
                availableTeamMembers.Add(p);

                WireUpList(); 
            }
        }

        private void createTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = new TeamModel();
            t.TeamName = teamNameValue.Text;
            t.TeamMembers = selectedTeamMembers;

            t = GlobalConfig.Connection.CreateTeam(t);

            //TODO - If we aren't closing this form after creation, reset the form
        }
    }
}
