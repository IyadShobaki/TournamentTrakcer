using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;

namespace TrackerUI
{
    
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Note 5
            //add the following code to initialize the database connection (you need to add TrackerLibrary to referecne first)
            TrackerLibrary.GlobalConfig.InitializeConnections(DatabaseType.Sql);
            Application.Run(new CreateTournamentForm());  //just for testing the connection 

            //Application.Run(new TournamentDashboardForm()); //the original and final form 
        }
    }
}
