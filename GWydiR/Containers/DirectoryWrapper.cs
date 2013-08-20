using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GWydiR.Containers
{
    //A class to wrap the static System.io.Directory object so as to allow for mocking when used.
    public class DirectoryWrapper
    {
        private string directoryPath;

        public DirectoryWrapper(string folderpath)
        {
            directoryPath = folderpath;
        }

        internal List<string> GetFiles()
        {
            return Directory.GetFiles(directoryPath).ToList();
        }
    }
}
