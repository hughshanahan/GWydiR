using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GWydiR.Utilities
{
    public class FileWriter
    {

        /// <summary>
        /// Factory method to abstract the creation of System.IO API StreamWriter Object 
        /// allowing isolation testing.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected virtual StreamWriter makeStream(string filePath)
        {
            return new StreamWriter(File.Open(filePath, FileMode.Append));
        }

        protected virtual StreamWriter makeStream(string filePath,FileMode mode)
        {
           return new StreamWriter(File.Open(filePath, mode));
        }

        protected virtual FileStream makeFile(string filePath)
        {
            return new FileStream(filePath,FileMode.Create);
        }

        public virtual void Create(string filePath)
        {
           FileStream file =  makeFile(filePath);
           file.Close();
        }

        public virtual void Write(string filePath,List<string> testData)
        {
            StreamWriter writer = makeStream(filePath,FileMode.Create);
            testData.ForEach(x => writer.WriteLine(x));
            writer.Close();
        }

        public virtual void Write(string filePath, string data)
        {
            StreamWriter writer = makeStream(filePath);
            writer.WriteLine(data);
            writer.Close();
        }

        public virtual void Write(string filePath, byte[] data)
        {
            FileStream writer = makeFile(filePath);
            int count = data.Count();
            writer.Write(data, 0, count);
            writer.Close();
        }
    }
}
