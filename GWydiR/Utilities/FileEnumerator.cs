using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GWydiR.Containers;

namespace GWydiR.Utilities
{
    // A class to handle getting a list of all files in a folder
    public class FileEnumerator
    {
        public FileEnumerator()
        {

        }

        //method to return all files in a folder as a list of strings
        public List<string> Enumerate(string folderpath)
        {
            List<string> returnList = new List<string>();

            DirectoryWrapper dir = makeDirectory(folderpath);

            returnList = dir.GetFiles();

            returnList = formatFileNames(returnList);

            return returnList;
        }

        private List<string> formatFileNames(List<string> filePaths)
        {
            List<string> returnList = new List<string>();

            foreach (string path in filePaths)
            {
                string[] tokens = path.Split('\\');
                returnList.Add(tokens[tokens.Length - 1]);
            }

            return returnList;
        }

        // factory method to create a directory wrapper object for mocking
        protected virtual DirectoryWrapper makeDirectory(string folderpath)
        {
            return new DirectoryWrapper(folderpath);
        }
    }
}
