using System;
using System.Collections.Generic;
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
                        string E2KStringObject = Helpers.CeadeCHelpers.getEtabsE2KDataFromServer(args[0]);
                        if (E2KStringObject == "ERROR")
                        {
                            Application.Run(new ErrorForm("ERROR", "error in retriving data from the server ,contact us", true));
                        }
                        else if (E2KStringObject == "INTERNETERROR")
                        {
                            Application.Run(new ErrorForm("INTERNETERROR", "", true));
                        }
                        else
                        {

                            StreamWriter sw = new StreamWriter("C:\\Users\\Hesham\\Desktop\\Test.txt");

                            //Write a line of text
                            sw.Write(E2KStringObject);

                            //Close the file
                            sw.Close();


                            object E2KObject = Helpers.CeadeCHelpers.convertE2KStringToObject(E2KStringObject);
                            if (E2KObject == null)
                            {
                                Application.Run(new ErrorForm("ERROR", "error handling server data ,contact us", true));
                            }
                            else
                            {
                                Application.Run(new CeadeCEtabsMainForm(E2KObject as model));
                            }
                        }
                    }
                }
                else
                {      
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        StreamReader sr = new StreamReader("C:\\Users\\Hesham\\Desktop\\Test.txt");
                        string E2KStringObject = sr.ReadToEnd();
                        object E2KObject = Helpers.CeadeCHelpers.convertE2KStringToObject(E2KStringObject);
                        Application.Run(new CeadeCEtabsMainForm(E2KObject as model));
                    }
                    else
                    {
                        Application.Exit();
                    }                    
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
