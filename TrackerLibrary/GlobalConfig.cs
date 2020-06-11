using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    //Note 2
    //static class to hold our connection interface. the next step is to create a class
    //that will connect to our database
    public static class GlobalConfig //change it to public static to be always visible
    {

        //we need to make the following code static 
        //we need to make set; private, so only the methods inside this static class
        //can change the value of connections, but every one can read the value of connections

        //Because we have the option to save to both a text file/s and a database,
        //we create a list of IDataConnection, because we could have more data sources to save
        //or to pull from

        //we need to initialize the list by adding (= new List<IDataConnection>();)
        public static List<IDataConnection> Connections { get; private set; } = new List<IDataConnection>();

        public static void InitializeConnections(bool database, bool textFile)
        {
            //you can initialize the list here too (if you have old version of .net framework)
            //Connections = new List<IDataConnection>();

            if (database)
            {
                //TODo - Create the SQL Connection (first one)         
                SqlConnector sql = new SqlConnector();
                Connections.Add(sql);
                //TODO - Set up the SQL Connector properly

            }
            if (textFile)  //because we need to have the option to have both connection, we didn't use (else if) statement
            {
                //TODo - Create the Text Connection (first one)
                TextConnection text = new TextConnection();
                Connections.Add(text);
                //TODO - Set up Text Connection properly
            }
        }

    }

    //note 1
    //------------------------------ Create Prize form as an example
    // if we don't use interface to connect the database we will have to put the following in each
    //model and will be hard to update, change or add:
    //if(usingSQL == true){
    //open database connection
    //save the data
    //get back the update model
    //}
    //and maybe the following if tou use text files to store data:
    //if(usingTextFile == true){
    //open text file
    //generate id
    //save the data
    //}
    //for example if you need to add MySQL as a database, you ill have to add in every form 
    //the previous code is not scalable and not dry and you will be repeating yourself

    //-------
    //we have to ask questions about the database
    //1- how do we get that connection information?
    //2- how do we connect to two different data sources to do the same task (SQL, text file, etc.)
    //we should have some type of data that is accessible from every where in the applictaion and tell
    //us which type of data to use (some kind of global variable) (global variable try to avoid them,
    //becasue they take up memory unnecessary) but in this case we need one.
    //we will use a static class to hold our data
    //static class for data source info
    //for the second question (to connect to different data sources) the key here that
    //we need to do the same thing just with different data source. So if you need to do the same
    //task, but behind the scenes it will done in two different ways "WE NEED INTERFACE" (contract)
}
