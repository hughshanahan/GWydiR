using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GWydiR
{
    public class FileReader
    {
        /// <summary>
        /// Factory Method to build a new stream reader, alows for isolated testing
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public virtual StreamReader makeStream(string filePath) 
        {
            return new StreamReader(File.Open(filePath,FileMode.Open));
        }

        /// <summary>
        /// Read method, read a whole file and splits it inot a lit of lines
        /// </summary>
        /// <param name="testPath"></param>
        /// <returns></returns>
        public virtual List<string> Read(string testPath)
        {
            StreamReader reader = makeStream(testPath);
            // split each line into a string
            List<string> returnData = reader.ReadToEnd().Split('\r').ToList();
            reader.Close();
            //remove null refferences
            if (returnData.Contains(string.Empty))
                returnData.Remove(string.Empty);

            return returnData;
        }
    }
}
