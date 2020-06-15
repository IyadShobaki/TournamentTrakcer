using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelpers;
namespace TrackerLibrary.DataAccess
{
    
    class TextConnector : IDataConnection
    {
        private const string PrizesFile = "PrizeModels.csv";
        private const string PeopleFile = "PersonModels.csv";
        private const string TeamFile = "TeamModels.csv";
        private const string TournamentFile = "TournamentModels.csv";
        private const string MatchupFile = "MatchupModels.csv";
        private const string MatchupEntryFile = "MatchupEntryModels.csv";

        public PersonModel CreatePerson(PersonModel model)
        {
            List<PersonModel> people = PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            int currentId = 1;
            if(people.Count > 0)
            {
                currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;

            people.Add(model);

            people.SaveToPeopleFile(PeopleFile);

            return model;

        }

        // TODo - Wire up the CreatePrize for text files - done
        public PrizeModel CreatePrize(PrizeModel model)
        {
            //Note 4 
            //before we can populate the prize in a text file, 
            //we need to read all the prizes in the text file to know what the new Id should be
            //we need to follow the following steps:
            //1-Load the text file
            //***but we need a method can convert any text file to any kind of list not only PrizeModel
            //we need auto mapper to map from one type of object to another 
            //the method related to data processing, so the teacher decide to put it in DataAccess folder
            //He create a class TextConnectorProcessor.cs and create a generic methods 
            //2-Convert the text to List<PrizeModel>  --- we can't have a generic method
            //1 + 2 steps done in TextConnectorProcessor.cs and the following line of code
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            //3-Find the max ID  (use the LINQ query in order to get the max ID)
            int currentId = 1;

            if(prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;//find the last id and add 1
            }
            model.Id = currentId;

            //4-Add the new record with the new ID (max + 1)
            prizes.Add(model);

            //5-Convert the prizes to List<string>
            //6-Save the List<string> to the text file (overwritting) 
            prizes.SaveToPrizeFile(PrizesFile);

            return model;
        }

        public TeamModel CreateTeam(TeamModel model)
        {
            //1-Load the text file  2-Convert the text to List<PrizeModel> 

            //in sql we send information to 2 different tables (Teams and TeamMembers)
            //To a text file we don't need to do that
            //in TeamModel we have (Id, TeamName, list of PersonModel (TeamMembers))
            //so we don't need to have two different text files. We need just a way to handle the list
            List<TeamModel> teams = TeamFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);

            //3-Find the max ID  (use the LINQ query in order to get the max ID)
            int currentId = 1;

            if (teams.Count > 0)
            {
                currentId = teams.OrderByDescending(x => x.Id).First().Id + 1;//find the last id and add 1
            }
            model.Id = currentId;

            //4-Add the new record with the new ID (max + 1)
            teams.Add(model);

            //5-Convert the prizes to List<string>
            //6-Save the List<string> to the text file (overwritting) 
            teams.SaveToTeamFile(TeamFile);

            return model;
        }

        public void CreateTournament(TournamnetModel model)
        {
            List<TournamnetModel> tournaments = TournamentFile
                .FullFilePath()
                .LoadFile()
                .ConvertToTournamentModels(TeamFile, PeopleFile, PrizesFile);

            int currentId = 1;

            if (tournaments.Count > 0)
            {
                currentId = tournaments.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

           model.SaveRoundsToFile( MatchupFile, MatchupEntryFile);

            tournaments.Add(model);

            tournaments.SaveToTournamentFile(TournamentFile);


        }

        public List<PersonModel> GetPerson_All()
        {
            return PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }

        public List<TeamModel> GetTeam_All()
        {
            return TeamFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);
        }

        public List<TournamnetModel> GetTournament_All()
        {
            return TournamentFile
                .FullFilePath()
                .LoadFile()
                .ConvertToTournamentModels(TeamFile, PeopleFile, PrizesFile);
        }

        public void updateMatchup(MatchupModel model)
        {
            model.updateMatchupToFile();
        }
    }
}



