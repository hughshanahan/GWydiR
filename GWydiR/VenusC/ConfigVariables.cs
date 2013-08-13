/// This is a changed version of a Microsoft File
///
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWydiR.VenusC
{
    public class ConfigVariables
    {

        public int InstanceCount { get; set; }

        public string ConnectionString { get; set; }

        public string STSThumbprint { get; set; }

        public string ManagementThumbprint { get; set; }

        public string DeploymentURL { get; set; }

        public string RemoteAccountUserName { get; set; }

        public string EncryptedPassword { get; set; }

        public bool AllowInsecure { get; set; }

        public bool EnableAccounting { get; set; }

        public string ServiceName { get; set; }

        public string SubscriptionId { get; set; }

    }
}
