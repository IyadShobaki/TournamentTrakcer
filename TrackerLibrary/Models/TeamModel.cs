using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//after we add all models to Models folder is better to add ".Models" after TrackerLibrary
//if you create a new class after creating the Models file ".Models" will added automaticlly
//but becasue we create our models before and add them to the folder later, will not have ".Models"
//namespace TrackerLibrary   --> original 
namespace TrackerLibrary.Models
{
    public class TeamModel //change to public 
    {
        /// <summary>
        /// The set of team members that have been added.
        /// </summary>
        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();
        //we have to initialize the TeamMembers prop because by default will be an uninitialized list
        //in old version of visual studio we have to create a constructor to initialize it. but instead 
        //we initialize it above 
        /// <summary>
        /// The name of the team.
        /// </summary>
        public string TeamName { get; set; }
    }
}
