using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public class MatchupEntryModel //change it to public
    {
        /// <summary>
        /// Represents one team in the matchup.
        /// </summary>
        public TeamModel Teamcompeting { get; set; }
        /// <summary>
        /// Represents the score for this particular team.
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// Represents the matchup that this team came 
        /// from as the winner
        /// </summary>
        public MatchupModel ParentMatchup { get; set; }


        //XML comments are very usful to document the method or the cnstructor and the parameters
        //and it will shows the comments when you hover over the method or the params

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="initialScore">
        ///// 
        ///// </param>
        //public MatchupEntryModel(double initialScore)
        //{
        //    Console.WriteLine();
        //}

    }
}
