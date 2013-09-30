using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWydiR.Containers
{
    /// <summary>
    /// An instance of this class represents all the infomation required to identify a subscription as required for GWydiR.
    /// A subscription contains:
    /// <list type="bullet">
    ///     <item>
    ///         <name>SID</name>
    ///         <description> String identifying the subscription ID of an Azure Service Account</description>
    ///     </item>
    ///     
    ///     <item>
    ///         <name>CertName</name>
    ///         <description>This is the value representing the friendly name of the certificates associated with an Azure subscription</description>
    ///     </item>
    ///     
    ///     <item>
    ///         <name>STSThumbprint</name>
    ///         <description>Thumbprint of the certificate, asociated with a subscription, used to secure General worker communications</description>
    ///     </item>
    ///     
    ///     <item>
    ///         <name>ManagementThumbprint</name>
    ///         <description>The thumbprint of the certificate used to secure use of the Azure rest Api communications</description>
    ///     </item>
    /// </list>
    /// </summary>
    public class Subscription
    {
        public string SID { get; set; }
        public string CertName { get; set; }
        public string STSThumbPrint { get; set; }
        public string ManagementThumbprint { get; set; }
    }
}
