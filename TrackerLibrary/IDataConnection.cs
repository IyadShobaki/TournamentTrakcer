using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public interface IDataConnection //change it to public 
    {
        //we don't need a public access modifier because in interfaces every items are public
        PrizeModel CreatePrize(PrizeModel model); //who ever implemets this interface will have
                                                    //a method called CreatePrize
    }
}



//Note 1
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