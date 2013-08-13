using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace GWydiR.Containers
{
    
    [Serializable]
    public class ServiceConfiguration
    {
        [XmlAttribute]
        public string serviceName;
        [XmlAttribute]
        public int osFamily;
        [XmlAttribute]
        public string osVersion;

        public Role Role;

        public ServiceConfiguration()
        {
            Role = new Role();
        }

        public ServiceConfiguration(int instanceCount,string StorageAccConnectionString,string StsCertificateThumbprint,string ManagementCertificateThumbprint,string HostUrl, bool InsecureAccess,string ServiceName,string SID)
        {
            ServiceName = "GenericWorkerRole";
            osFamily = 2;
            osVersion = "*";

            Role = new Role();

            Role.name = "Cloud.WebRole";

            Role.Instances.count = instanceCount;
            Role.ConfigurationSettings.Add(new Setting("Microsoft.EMIC.Cloud.GenericWorker.ConnectionString", StorageAccConnectionString));
            Role.ConfigurationSettings.Add(new Setting("Microsoft.EMIC.Cloud.STS.Certificate.Thumbprint", StsCertificateThumbprint));
            Role.ConfigurationSettings.Add(new Setting("Microsoft.EMIC.Cloud.STS.URL", "http://" + HostUrl + "/STS/UsernamePassword.svc"));
            Role.ConfigurationSettings.Add(new Setting("Microsoft.EMIC.Cloud.SecuredJobManagementSiteURL", "http://my.genericworker.net/JobManagement/"));
            Role.ConfigurationSettings.Add(new Setting("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString", StorageAccConnectionString));
            Role.ConfigurationSettings.Add(new Setting("Microsoft.EMIC.Cloud.GenericWorker.ParallelTasks","1"));
            Role.ConfigurationSettings.Add(new Setting("Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled", "True"));
            Role.ConfigurationSettings.Add(new Setting("Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername", "emicloud"));
            Role.ConfigurationSettings.Add(new Setting("Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword", StsCertificateThumbprint));
            Role.ConfigurationSettings.Add(new Setting("Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration", "2013-12-31T23:59:59.0000000+01:00"));
            Role.ConfigurationSettings.Add(new Setting("Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled", "True"));
            Role.ConfigurationSettings.Add(new Setting("Microsoft.EMIC.Cloud.WCF.SharedMachineSymmetricKey", "cHz9DpC42rR+oeWI1y6YqCNcFJFieKjU/O3tPcCHDfE="));
            Role.ConfigurationSettings.Add(new Setting("Microsoft.EMIC.Cloud.SerializedGlobalSecurityPolicy","<ClaimRequirementsPolicy>  <Policy Operation=\"http://schemas.ggf.org/bes/2006/08/bes-factory/BESFactoryPortType/CreateActivity\">    <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Researcher</Claim>  </Policy>  <Policy Operation=\"http://schemas.ggf.org/bes/2006/08/bes-factory/BESFactoryPortType/GetActivityDocuments\">    <Any>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Researcher</Claim>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Compute Administrator</Claim>    </Any>  </Policy>  <Policy Operation=\"http://schemas.ggf.org/bes/2006/08/bes-factory/BESFactoryPortType/GetActivityStatuses\">    <Any>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Researcher</Claim>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Compute Administrator</Claim>    </Any>  </Policy>  <Policy Operation=\"http://schemas.ggf.org/bes/2006/08/bes-factory/BESFactoryPortType/TerminateActivities\">    <Any>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Researcher</Claim>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Compute Administrator</Claim>    </Any>  </Policy>  <Policy Operation=\"http://schemas.ggf.org/bes/2006/08/bes-factory/BESFactoryPortType/GetFactoryAttributesDocument\">    <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Compute Administrator</Claim>  </Policy>  <Policy Operation=\"http://tempuri.org/IScalingService/ListDeployments\">    <Any>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Researcher</Claim>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Compute Administrator</Claim>    </Any>  </Policy>  <Policy Operation=\"http://tempuri.org/IScalingService/UpdateDeployment\">    <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Compute Administrator</Claim>  </Policy>  <Policy Operation=\"http://schemas.ggf.org/bes/2006/08/bes-factory/BESFactoryPortType/GetJobs\">    <Any>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Researcher</Claim>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Compute Administrator</Claim>    </Any>  </Policy>  <Policy Operation=\"http://schemas.ggf.org/bes/2006/08/bes-factory/BESFactoryPortType/GetAllJobs\">    <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Compute Administrator</Claim>  </Policy>  <Policy Operation=\"http://schemas.ggf.org/bes/2006/08/bes-factory/BESFactoryPortType/GetHierarchy\">    <Any>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Researcher</Claim>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Compute Administrator</Claim>    </Any>  </Policy>  <Policy Operation=\"http://schemas.ggf.org/bes/2006/08/bes-factory/BESFactoryPortType/GetRoot\">    <Any>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Researcher</Claim>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Compute Administrator</Claim>    </Any>  </Policy>  <Policy Operation=\"http://schemas.ggf.org/bes/2006/08/bes-factory/BESFactoryPortType/GetJobsByGroup\">    <Any>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Researcher</Claim>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Compute Administrator</Claim>    </Any>  </Policy>  <Policy Operation=\"http://tempuri.org/INotificationService/CreateSubscription\">    <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Researcher</Claim>  </Policy>  <Policy Operation=\"http://tempuri.org/INotificationService/CreateSubscriptionForStatuses\">    <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Researcher</Claim>  </Policy>  <Policy Operation=\"http://tempuri.org/INotificationService/CreateSubscriptionForGroupStatuses\">    <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Researcher</Claim>  </Policy>  <Policy Operation=\"http://tempuri.org/INotificationService/Unsubscribe\">    <Any>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Researcher</Claim>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Compute Administrator</Claim>    </Any>  </Policy>  <Policy Operation=\"http://schemas.ggf.org/bes/2006/08/bes-factory/BESFactoryPortType/CancelGroup\">    <Any>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Researcher</Claim>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Compute Administrator</Claim>    </Any>  </Policy>  <Policy Operation=\"http://schemas.ggf.org/bes/2006/08/bes-factory/BESFactoryPortType/CancelHierarchy\">    <Any>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Researcher</Claim>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Compute Administrator</Claim>    </Any>  </Policy>  <Policy Operation=\"http://schemas.ggf.org/bes/2006/08/bes-factory/BESFactoryPortType/RemoveTerminatedJobs\">    <Any>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Researcher</Claim>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Compute Administrator</Claim>    </Any>  </Policy>  <Policy Operation=\"http://schemas.ggf.org/bes/2006/08/bes-factory/BESFactoryPortType/GetNumberOfJobs\">    <Any>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Researcher</Claim>      <Claim Type=\"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\">VENUS-C Compute Administrator</Claim>    </Any>  </Policy></ClaimRequirementsPolicy>"));
            Role.ConfigurationSettings.Add(new Setting("Microsoft.EMIC.Cloud.STS.Azure.ConnectionString", StorageAccConnectionString));
            Role.ConfigurationSettings.Add(new Setting("Microsoft.EMIC.Cloud.Security.AllowInsecureAccess", InsecureAccess.ToString()));
            Role.ConfigurationSettings.Add(new Setting("Microsoft.EMIC.Cloud.GenericWorker.IsAccountingOn", "True"));
            Role.ConfigurationSettings.Add(new Setting("Microsoft.EMIC.Cloud.GenericWorker.IsWebRole", "True"));
            Role.ConfigurationSettings.Add(new Setting("Microsoft.EMIC.Cloud.GenericWorker.JobEntriesPerPage","100"));
            Role.ConfigurationSettings.Add(new Setting("Microsoft.EMIC.Cloud.GenericWorker.URL", "http://" + HostUrl + "/JobSubmission/SecureService.svc"));
            Role.ConfigurationSettings.Add(new Setting("Microsoft.EMIC.Cloud.ScalingService.URL", "http://" + HostUrl + "/ScalingService/SecureService.svc"));
            Role.ConfigurationSettings.Add(new Setting("Microsoft.EMIC.Cloud.NotificationService.URL", "http://" + HostUrl + "/NotificationService/SecureService.svc"));
            Role.ConfigurationSettings.Add(new Setting("Microsoft.EMIC.Cloud.Azure.ServiceName",ServiceName));
            Role.ConfigurationSettings.Add(new Setting("Microsoft.EMIC.Cloud.Azure.SubscriptionId", SID));
            Role.ConfigurationSettings.Add(new Setting("Microsoft.EMIC.Cloud.Azure.MgmtCertThumbprint", ManagementCertificateThumbprint));

            Role.Certificates.Add(new Certificate("Microsoft.EMIC.Cloud.SSLCert", StsCertificateThumbprint,"sha1"));
            Role.Certificates.Add(new Certificate("Microsoft.EMIC.Cloud.MgmtCert", ManagementCertificateThumbprint,"sha1"));
            Role.Certificates.Add(new Certificate("Microsoft.EMIC.Cloud.STSCert", StsCertificateThumbprint,"sha1"));
            Role.Certificates.Add(new Certificate("Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption", StsCertificateThumbprint,"sha1"));
        }
    }

    [Serializable]
    public class Setting
    {
        [XmlAttribute]
        public string name;
        [XmlAttribute]
        public string value;

        public Setting()
        {

        }

        public Setting(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
    }

    [Serializable]
    public class Role
    {
        [XmlAttribute]
        public string name;
        public Instances Instances;
        public List<Setting> ConfigurationSettings;
        public List<Certificate> Certificates;

        public Role()
        {
            Instances = new Instances();
            ConfigurationSettings = new List<Setting>();
            Certificates = new List<Certificate>();
        }
    }

    [Serializable]
    public class Certificate
    {
        [XmlAttribute]
        public string name;
        [XmlAttribute]
        public string thumbprint;
        [XmlAttribute]
        public string thumbprintAlgorithm;

        public Certificate()
        {

        }

        public Certificate(string name, string thumbprint, string algorithm)
        {
            this.name = name;
            this.thumbprint = thumbprint;
            this.thumbprintAlgorithm = algorithm;
        }
    }

    [Serializable]
    public class Instances
    {
        [XmlAttribute]
        public int count;
    }
}
