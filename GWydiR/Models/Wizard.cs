using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace GWydiR
{
    public class Wizard : GWydiR.Interfaces.ModelInterfaces.IWizard
    {

        private string SubscriptionsFileName;
        public List<string> SIDList { get; set; }

        public Wizard()
        {

            // get a palce to store app data
            SubscriptionsFileName = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Subscriptions.dat";

            SIDList = new List<string>();
            // if there is a file of subscitions to read
            if (File.Exists(SubscriptionsFileName))
            {
                // open in and fill SIDList with it's contense
                FileReader reader = makeReader();
                SIDList = reader.Read(SubscriptionsFileName);
            }
            else
            {
                // else create the file for future use. This way we can assume, unless an exception occurs, that the subscriptions file exists.
                FileWriter writer = makeWriter();
                writer.Create(SubscriptionsFileName);//TODO currently broken
            }
        }

        /// <summary>
        /// Method to deal with button clicks from UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateButtonClick(object sender, EventArgs e, string SID)
        {
            Console.WriteLine("Eventhandler");
            if (!hasSID(SID))
            {
                addSID(SID);
            }
            else
            {
                Console.WriteLine("Already have SID {0}",SID);
            }
        }
        /// <summary>
        /// Factory method for file reader, abstraction for isolation testing
        /// </summary>
        /// <returns></returns>
        public virtual FileReader makeReader()
        {
            return new FileReader();
        }

        /// <summary>
        /// Factory method for file writer, abstraction for isolation testing
        /// </summary>
        /// <returns></returns>
        public virtual FileWriter makeWriter()
        {
            return new FileWriter();
        }

        public bool hasSID(string SID)
        {
            //check it exists in the list
            bool returnValue = SIDList.Contains(SID);
            // return true if it is, false if not
            return returnValue;
        }

        /// <summary>
        /// Method to check that an SID is valid
        /// </summary>
        /// <param name="SID"></param>
        /// <returns></returns>
        public bool isValidSID(string SID)
        {
            // regex for Azure Subscription ID
            Regex SIDRegex = new Regex("([0-9]|[a-f]){6,6}-([0-9]|[a-f]){4,4}-([0-9]|[a-f]){4,4}-([0-9]|[a-f]){4,4}-([0-9]|[a-f]){12,12}");
            bool returnValue = SIDRegex.IsMatch(SID);
            // if the regexdoesnt match the SID
            if(!returnValue)
            {
                throw new GWydiR.Exceptions.InvalidSIDException();
            }
            return returnValue;
        }

        /// <summary>
        /// Method to add SID's to  the list.
        /// </summary>
        /// <param name="SID"></param>
        public void addSID(string SID)
        {
            // if its a valid SID
            isValidSID(SID);
            
            //if it is not already in the list
            if (!hasSID(SID))
            {
                // add it to the list
                SIDList.Add(SID);
            }

            // once added to the list, it should also be written to the file
            // so that future intantiations of the object will have access to this new
            // SID
            FileWriter writer = makeWriter();
            writer.Write(SubscriptionsFileName, SID);
        }


    }
}
