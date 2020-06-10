using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public class TeamModel //change to public 
    {
        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();
        //we have to initialize the TeamMembers prop because by default will be an uninitialized list
        //in old version of visual studio we have to create a constructor to initialize it. but instead 
        //we initialize it above 
        public string TeamName { get; set; }
    }
}
