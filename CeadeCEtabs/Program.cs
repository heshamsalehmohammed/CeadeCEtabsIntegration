using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CeadeCEtabs
{
    static class Program
    {
        public static int version = 1;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Helpers.RegisterMyProtocol();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string checkVersion = Helpers.isVersionUpdated(version.ToString());
            if (checkVersion == "true")
            {
                if (args != null && args.Length > 0)
                {
                    if (Uri.TryCreate(args[0], UriKind.Absolute, out var uri) && string.Equals(uri.Scheme, "CeadeCEtabs", StringComparison.OrdinalIgnoreCase))
                    {
                        string[] argList = Helpers.CeadeCHelpers.analyzeArg(args[0]);
                        string shouldRun = Helpers.shouldIRun(argList); 
                        if (shouldRun == "true")
                        {
                            Application.Run(new CeadeCEtabsMainForm(argList[1]+"&"+argList[2]));
                        }
                        else if (shouldRun == "false")
                        {
                            Application.Run(new UpdateForm());
                        }
                        else if (shouldRun == "ERROR")
                        {
                            Application.Run(new ErrorForm("ERROR", "error in retriving data from the server ,contact us", true));
                        }
                        else if (shouldRun == "INTERNETERROR")
                        {
                            Application.Run(new ErrorForm("INTERNETERROR", "", true));
                        }

                    }
                }
                else
                {
                //   if (System.Diagnostics.Debugger.IsAttached)
                //   {
                //      Application.Run(new CeadeCEtabsMainForm(string "userEtabsPointer"));
                //   }
                //   else
                //   {
                        Application.Run(new ErrorForm("ERROR", "not valid action , contact us", true));
                //   }
                }
            }
            else if (checkVersion == "false")
            {
                Application.Run(new UpdateForm());
            }
            else if (checkVersion == "ERROR")
            {
                Application.Run(new ErrorForm("ERROR", "error in retriving data from the server ,contact us", true));
            }
            else if (checkVersion == "INTERNETERROR")
            {
                Application.Run(new ErrorForm("INTERNETERROR", "", true));
            }

        }
    }
}
