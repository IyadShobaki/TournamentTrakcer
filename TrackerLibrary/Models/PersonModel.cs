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
    /// <summary>
    /// Represents one person.
    /// </summary>
    public class PersonModel //change it to public 
    {
        /// <summary>
        /// The unique identifier for the person.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The first name of the person.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The last name of the person.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The primary email address of the person.
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// The primary cell phone number of the person.
        /// </summary>
        public string CellphoneNumber { get; set; }


        //to show in the drop down and the list, we need a good combined property
        //uses on or more of the previous properties 

        public string FullName
        {
            get { return $"{ FirstName } { LastName }"; }
           
        }


    }
}
