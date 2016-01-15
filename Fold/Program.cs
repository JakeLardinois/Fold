using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fold
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string strArgs = string.Join(" ", args).Remove(0,1);//joins the initial argument array and removes the first space, else the space is also split into an argument
                string[] objArgs = strArgs.Split('/');
                FoldFile objFoldFile = new FoldFile();


                for (int x = 0; x < objArgs.Length; x++)//loop through all the arguments
                { 
                    switch (objArgs[x].ToUpper()[0])//examine the first character of the argument
                    {
                        case '?':
                            DisplayUsage();
                            Environment.Exit(0);
                            break;
                        case 'S'://strip CRLF's from original file before folding file
                            objFoldFile.StripOriginalCRLFs = true;
                            break;
                        case 'L'://line length
                            if (objArgs[x][1] != ' ')//I want a space after the parameter
                                DisplayUsage();
                            else
                                objFoldFile.LineLength = objArgs[x].Substring(1);//return the argument minus the commandline parameter
                            break;
                        case 'O'://original or source file
                            if (objArgs[x][1] != ' ')
                                DisplayUsage();
                            else
                                objFoldFile.OriginalFile = objArgs[x].Substring(1);
                            break;
                        case 'D'://destination file
                            if (objArgs[x][1] != ' ')
                                DisplayUsage();
                            else
                                objFoldFile.DestinationFile = objArgs[x].Substring(1);
                            break;
                        default:
                            DisplayUsage();
                            break;
                    }
                }

                if (objFoldFile.isValidOriginalFile && objFoldFile.isValidDestinationFile)
                {
                    objFoldFile.Fold();
                }
                else
                {
                    Console.WriteLine("There was an Error.  Check your parameters...");
                    Console.WriteLine(objFoldFile.DisplayParameterValues);
                }
            }
            else
                DisplayUsage();
        }

        private static void DisplayUsage()
        {
            Console.WriteLine(FoldFile.DisplayUsage);
        }

        private static void GetVersion()
        {
            //Version objVersion;

            //if (ApplicationDeployment.IsNetworkDeployed)
            //{
            //    objVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;
            //    MessageBox.Show(string.Format("ClickOnce published Version: v{0}.{1}.{2}.{3}", objVersion.Major, objVersion.Minor, objVersion.Build, objVersion.Revision),
            //    "Version", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //}
        }
    }
}
