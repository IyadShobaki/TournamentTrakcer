using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;



//3-Find the max ID  (use the LINQ query in order to get the max ID)
//4-Add the new record with the new ID (max + 1)
//5-Convert the prizes to List<string>
//6-Save the List<string> to the text file (overwritting) 


//we should add ".TextConnector" to the namespace 
//so the only people to have that using statements get the extra clutter
//we need to change ".TextConnector" to ".TextHelpers" or anything else
//because TextConnector class has the same name
//
namespace TrackerLibrary.DataAccess.TextHelpers
{
    //change the following to public static class
    public static class TextConnectorProcessor
    {
        //create the following method to get the file name and return the file path
        //extension method 
        public static string FullFilePath(this string fileName) // example - PrizeModel.csv
        {
            //D:\C#Projects\TournamentTrackerProject1\TournamentTrakcer\TextFilesDB\PrizeModel.csv
            return $"{ ConfigurationManager.AppSettings["filePath"]}\\{ fileName }";
        }

        //we need another extension method //1-Load the text file
        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file)) //check if the file exists or not
            {
                return new List<string>();
            }
            return File.ReadAllLines(file).ToList();
        }

        public static List<PersonModel> ConvertToPersonModels(this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                PersonModel p = new PersonModel();
                p.Id = int.Parse(cols[0]);
                p.FirstName = cols[1];
                p.LastName = cols[2];
                p.EmailAddress = cols[3];
                p.CellphoneNumber = cols[4];
                output.Add(p);

            }
            return output;
        }
        //now we need to create a method for each model
        public static List<PrizeModel> ConvertToPrizeModels(this List<string> lines)
        {
            //2-Convert the text to List<PrizeModel>
            List<PrizeModel> output = new List<PrizeModel>(); //if there is no data will return an empty list
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                PrizeModel p = new PrizeModel();
                p.Id = int.Parse(cols[0]);
                p.PlaceNumber = int.Parse(cols[1]);
                p.PlaceName = cols[2];
                p.PrizeAmount = decimal.Parse(cols[3]);
                p.PrizePercentage = double.Parse(cols[4]);
                output.Add(p);

            }
            return output;
        }

        public static List<TeamModel> ConvertToTeamModels(this List<string> lines, string peopleFileName)
        {
            //to avoid creating another text file and to avoid store the person data another time 
            //we will store just the ids from the list
           //id,team name,list of ids seperated by the pipe
           //3,Tim's Team,1|3|5    //1|3|5 is each id from each person
            List<TeamModel> output = new List<TeamModel>();
            List<PersonModel> people = peopleFileName.FullFilePath().LoadFile().ConvertToPersonModels();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                TeamModel t = new TeamModel();
                t.Id = int.Parse(cols[0]);
                t.TeamName = cols[1];

                string[] personIds = cols[2].Split('|');

                foreach (string id in personIds)
                {
                    t.TeamMembers.Add(people.Where(x => x.Id == int.Parse(id)).First());
                }
                // output.Add(t);  //I'm not sure why the teacher didn't put this, 
                //but I will discover next video (Lesson 15). He test writing to the file
                //but not reading from it


            }
            return output;
        }
        public static void SaveToPrizeFile(this List<PrizeModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PrizeModel p in models)
            {
                lines.Add($"{ p.Id },{ p.PlaceNumber },{ p.PlaceName },{ p.PrizeAmount },{ p.PrizePercentage }");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToPeopleFile(this List<PersonModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PersonModel p in models)
            {
                lines.Add($"{ p.Id },{ p.FirstName },{ p.LastName },{ p.EmailAddress },{ p.CellphoneNumber }");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToTeamFile(this List<TeamModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (TeamModel t in models)
            {
                lines.Add($"{ t.Id },{ t.TeamName },{ ConvertPeopleListToString(t.TeamMembers) }");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        private static string ConvertPeopleListToString(List<PersonModel> people)
        {
            
            string output = "";
            //if there is 0 people the application will crash so we need to check the leangth of people
            if(people.Count == 0)
            {
                return "";
            }

            foreach (PersonModel p in people)
            {
                output += $"{ p.Id }|";
            }
            output = output.Substring(0, output.Length - 1); //to remove the last pipe | 

            return output;

        }
    }
}
