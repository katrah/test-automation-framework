using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace Common.Tools
{
    public class FileProcessor
    {
        public static string[] GetFileHeaders(string filePath, string delimiter)
        {
            if (File.Exists(filePath))
            {
                using (TextFieldParser parser = new TextFieldParser(filePath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(delimiter);
                    return parser.ReadFields();
                }
            }
            throw new Exception(string.Format("Could not find the file: {0}", filePath));
        }

        /// <summary>
        /// Reads a delimited file and parses it into a List of string arrays where each value (string[])
        /// in the List is a row from the file and each value in each row is a parsed field.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="delimiter"></param>
        /// <param name="trim">Optional parameter to remove characters at the start of each field (Item1)
        /// and at the end of each field (Item2).  Null or empty Item values means no trimming for the
        /// start or end of the field (respectively).</param>
        /// <returns></returns>
        public static List<string[]> ParseDelimitedFile(string file, string delimiter, Tuple<string, string> trim = null)
        {
            List<string[]> parsedData = new List<string[]>();
            if (File.Exists(file))
            {
                using (TextFieldParser parser = new TextFieldParser(file))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(delimiter);
                    while (!parser.EndOfData)
                    {
                        string[] tmp = parser.ReadFields();
                        if (trim != null)
                        {
                            tmp = trimStrings(tmp, trim);
                            parsedData.Add(tmp);
                        }
                        else
                        {
                            parsedData.Add(tmp);
                        }
                    }
                }
                return parsedData;
            }
            throw new Exception(string.Format("Could not find the file: {0}", file));
        }

        private static string[] trimStrings(string[] vals, Tuple<string, string> trim)
        {
            string[] retVal = new string[vals.Length];
            for (int i = 0; i < vals.Length; i++)
            {
                string tmp = vals[i];
                if (!string.IsNullOrEmpty(trim.Item1))
                {
                    int index = tmp.IndexOf(trim.Item1);
                    if (index > -1)
                    {
                        tmp = tmp.Remove(index, trim.Item1.Length);
                    }
                }

                if (!string.IsNullOrEmpty(trim.Item2))
                {
                    int index = tmp.LastIndexOf(trim.Item2);
                    if (index > -1)
                    {
                        tmp = tmp.Remove(index, trim.Item2.Length);
                    }
                }
                retVal[i] = tmp;
            }
            return retVal;
        }
    }
}
