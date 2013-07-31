using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWydiR.Containers
{
    /// <summary>
    ///  This is an object that represents a subscripton
    /// </summary>

    public class Subscription
    {
        public string SID { get; set; }
        public string CertName { get; set; }
        public string STSThumbPrint { get; set; }
        public string ManagementThumbprint { get; set; }
    }
}
