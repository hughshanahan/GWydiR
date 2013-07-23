using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWydiR.Interfaces.ModelInterfaces
{
    public interface IWizard
    {
        List<string> GetSIDList();
        List<string> GetCertList();
         FileReader makeReader();
         FileWriter makeWriter();
         bool hasSID(string SID);
         bool isValidSID(string SID);
         void addSID(string SID);
         void AddCertificate(string certName);
    }
}
