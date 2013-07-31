using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using GWydiR.Utilities;
using GWydiR.Containers;

namespace GWydiR
{
    public class Wizard : GWydiR.Interfaces.ModelInterfaces.IWizard
    {

        private string SubscriptionsFileName;

        public List<string> SIDList { get; set; }

        public List<string> CertList { get; set; }

        public List<string> GetSIDList()
        {
            if (SIDList.Count < 1)
            {
                SIDList = (makeSubscriptionsParser()).ParseSids(SubscriptionsList);
            }
            return SIDList;
        }

        public List<string> GetCertList()
        {
            if (CertList.Count < 1)
            {
                CertList = (makeSubscriptionsParser()).ParseCertificateNames(SubscriptionsList);
            }
            return CertList;
        }

        private List<Subscription> SubscriptionsList { get; set; }

        // this does too much work, should abstract out file reading and writing in the constructor to another class
        // dedicated to reading in SID's
        public Wizard()
        {

            // get a palce to store app data
            SubscriptionsFileName = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Subscriptions.dat";

            SubscriptionsList = new List<Subscription>();
            SIDList = new List<string>();
            CertList = new List<string>();
            #region needs moving to setup/parser object
            // if there is a file of subscitions to read
            if (File.Exists(SubscriptionsFileName))
            {
                // open in and fill SIDList with it's contense
                FileReader reader = makeReader();
                SubscriptionFileParser parser = makeSubscriptionsParser();
                SubscriptionsList = parser.ParseSubscriptions(reader.Read(SubscriptionsFileName));
            }
            else
            {
                // else create the file for future use. This way we can assume, unless an exception occurs, that the subscriptions file exists.
                FileWriter writer = makeWriter();
                writer.Create(SubscriptionsFileName);
            }
            #endregion
        }

        public Wizard(List<List<string>> SubscriptionList)
        {
            this.SubscriptionsList = SubscriptionsList;
            SubscriptionsFileName = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Subscriptions.dat";
        }

        protected virtual SubscriptionFileParser makeSubscriptionsParser()
        {
            return new SubscriptionFileParser();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SID"></param>
        /// <returns></returns>
        public bool hasSID(string SID)
        {
            //check it exists in the list
            List<string> sidList = GetSIDList();
            bool returnValue = sidList.Contains(SID);
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="certName"></param>
        public void AddCertificate(string certName)
        {
            //if certificate exists, don't add it
            if(!HasCert(certName))
                CertList.Add(certName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testName1"></param>
        /// <returns></returns>
        public bool HasCert(string testName1)
        {
            bool returnValue = false;
            //check each certificate name
            foreach (string name in CertList)
            {
                //if matches return true
                if(name == testName1)
                    returnValue = true;
            }
            return returnValue;
        }

        /// <summary>
        /// A method to add a new subscription to the subscriptions list.
        /// </summary>
        /// <param name="SID"></param>
        /// <param name="CertificateName"></param>
        public void AddSubscription(string SID, string certificateName, string STSCertificateThumbPrint, string ManagementCertificateThumbPrint)
        {
            if (!HasSubscription(SID, certificateName))
            {
                Subscription subscription = new Subscription();
                subscription.SID = SID;
                subscription.CertName = certificateName;
                subscription.STSThumbPrint = STSCertificateThumbPrint;
                subscription.ManagementThumbprint = ManagementCertificateThumbPrint;
                SubscriptionsList.Add(subscription);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Subscription> GetSubscriptions()
        {
            return SubscriptionsList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testSID"></param>
        /// <param name="testCertName"></param>
        /// <returns></returns>
        public bool HasSubscription(string testSID, string testCertName)
        {
            bool returnValue = false;

            foreach (Subscription subscription in SubscriptionsList)
            {
                if (subscription.SID == testSID && subscription.CertName == testCertName)
                    returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SID"></param>
        /// <param name="Cert"></param>
        /// <returns></returns>
        public string GetSTSThumbPrint(string SID, string Cert)
        {
            string returnString = "";
            foreach (Subscription subscription in SubscriptionsList)
            {
                if (subscription.SID == SID && subscription.CertName == Cert)
                {
                    returnString = subscription.STSThumbPrint;
                }
            }
            return returnString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SID"></param>
        /// <param name="Cert"></param>
        /// <returns></returns>
        public string GetManagementThumbPrint(string SID, string Cert)
        {
            string returnString = "";
            foreach (Subscription subscription in SubscriptionsList)
            {
                if (subscription.SID == SID && subscription.CertName == Cert)
                {
                    returnString = subscription.ManagementThumbprint;
                }
            }
            return returnString;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SaveSubscriptions()
        {
            FileWriter writer = makeWriter();
            List<string> subscriptions = new List<string>();
            foreach(Subscription s in SubscriptionsList)
            {
                subscriptions.Add(s.SID + "," + s.CertName + "," + s.STSThumbPrint + "," + s.ManagementThumbprint);
            }
            writer.Write(SubscriptionsFileName, subscriptions);
        }
    }
}
