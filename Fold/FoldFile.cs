using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;


namespace Fold
{
    public class FoldFile
    {
        public int mintLineLength { get; set; }
        public string LineLength 
        {
            set 
            { 
                int intTemp;

                mintLineLength = int.TryParse(value, out intTemp) ? intTemp : 0;
            }
        }
        public string OriginalFile { get; set; }
        public string DestinationFile { get; set; }
        public bool isValidOriginalFile 
        { 
            get 
            {
                return File.Exists(OriginalFile);
            } 
        }
        public bool isValidDestinationFile
        {
            get
            {
                FileInfo objFileInfo = new FileInfo(DestinationFile);

                return Directory.Exists(objFileInfo.DirectoryName);
            }
        }
        public bool StripOriginalCRLFs { get; set; }
        public void Fold()
        {
            StringBuilder objStrBldr = new StringBuilder();

            // Open the file to read from. 
            objStrBldr.Clear();
            using (StreamReader sr = File.OpenText(OriginalFile))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    if (StripOriginalCRLFs)
                        objStrBldr.Append(s);
                    else
                        objStrBldr.Append(s + Environment.NewLine);
                }
            }

            //chunk the string into specified lengths
            List<string> objStringList = objStrBldr.ToString().Splice(mintLineLength).ToList();

            //clear the stringbuilder and then rewrite the contents to it
            objStrBldr.Clear();
            foreach (string strLine in objStringList)
                objStrBldr.AppendLine(strLine);
            // Create/Overwrite the file to write to. 
            File.WriteAllText(DestinationFile, objStrBldr.ToString());
        }
        public string DisplayParameterValues 
        {
            get
            {
                StringBuilder objStrBldr = new StringBuilder();

                objStrBldr.AppendLine("\tLine Length: " + mintLineLength);
                objStrBldr.AppendLine("\tValid Orginal File: " + isValidOriginalFile);
                objStrBldr.AppendLine("\tOriginal File: " + OriginalFile);
                objStrBldr.AppendLine("\tValid Destination File: " + isValidDestinationFile);
                objStrBldr.AppendLine("\tDestination File: " + DestinationFile);
                return objStrBldr.ToString();
            }
        }
        public static string DisplayUsage
        {
            get
            {
                StringBuilder objStrBldr = new StringBuilder();

                objStrBldr.AppendLine("Created By: Jake Lardinois");
                objStrBldr.AppendLine("Date Created: 11/20/2012");
                objStrBldr.AppendLine("Fold.exe is an application which takes a text file and 'chunks' it into lines of a specified length.");
                objStrBldr.AppendLine();
                objStrBldr.AppendLine("Fold.exe /L [LineLength] /O [OriginalFileNameAndPath] /D [DestinationFileNameAndPath] /s /?");
                objStrBldr.AppendLine();
                objStrBldr.AppendLine("Example:");
                objStrBldr.AppendLine("Fold.exe /l 94 /o C:\\Temp\\DIRECT.DEP /d C:\\Temp\\SendFile");
                objStrBldr.AppendLine();
                objStrBldr.AppendLine("\t /L - The number of characters to be displayed on each line");
                objStrBldr.AppendLine("\t /O - The original file (complete with path) that is to be folded");
                objStrBldr.AppendLine("\t /D - The name of the new file that will be created (complete with path)");
                objStrBldr.AppendLine("\t /S - If used then the Carriage Returns and Line Feeds from the original file will be stripped prior to folding.");
                return objStrBldr.ToString();
            }
        }
    }
}
