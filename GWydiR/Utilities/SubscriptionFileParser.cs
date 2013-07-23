﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWydiR.Utilities
{
    /// <summary>
    /// This is a class whos responcibility is to convert data from a file reader into 
    /// data that can be interpreted by any object wishing to use the Subscritions file.
    /// </summary>
    public class SubscriptionFileParser
    {
        // the purpose of this method is to parse the subscriptions file into a list of lists of strings,
        // each separate token between delimiters of each line will be entered into a List of strings. These
        // Lists are then added as elements to a parent lit, which is then returned.
        public virtual List<List<string>> ParseSubscriptions(List<string> list)
        {
            List<List<string>> returnList = new List<List<string>>();

            if (list != null)
            {
                foreach (string line in list)
                {
                    string[] tokens = line.Split(',');
                    returnList.Add(tokens.ToList());
                }
            }

            return returnList;
        }

        // This method searches though a non empty list of lists of strings and puts the first element from each
        // child list into a new list, which is then returned. The purpose of this is to retrieve the SID's which are
        // stored at the begingin of each new line of the subscriptions file.
        public virtual List<string> ParseSids(List<List<string>> subscriptionsList)
        {
            List<string> returnList = new List<string>();
            //subscriptions is not empty
            if (subscriptionsList.Count > 0)
            {
                foreach (List<string> list in subscriptionsList)
                {
                    // add the first element from the list
                    // this should probably be madde into a hashmap for readability
                    returnList.Add(list[0]);
                }
            }

            return returnList;
        }

        /// <summary>
        /// This method searches through a list of lists of strings for the second element of each sub list. 
        /// This is becuase at this position, within the subscriptions file, a string containing the name of the
        /// certificate associated with some SID is stored. These certificate names are then collected into a list
        /// and returned to the caller.
        /// </summary>
        /// <param name="subscriptionsList"></param>
        /// <returns></returns>
        public virtual List<string> ParseCertificateNames(List<List<string>> subscriptionsList)
        {
            List<string> returnList = new List<string>();

            foreach (List<string> list in subscriptionsList)
            {
                // certificate names are stored at the second position
                // in the file
                returnList.Add(list[1]);
            }
            return returnList;
        }
    }
}