﻿using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CeadeCEtabs
{
    class Helpers
    {
        public static string isVersionUpdated(string currentVersion)
        {
            return httpRequestResponse("version=" + currentVersion);
        }
        public static string shouldIRun(string[] args)
        {
            return httpRequestResponse("id=" + args[1] + "&key=" + args[2]);
        }
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
        public static void RegisterMyProtocol()
        {
            string currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            RegistryKey key = Registry.ClassesRoot.OpenSubKey("CeadeCEtabs");
            if (key == null)
            {
                key = Registry.ClassesRoot.CreateSubKey("CeadeCEtabs");
                key.SetValue(string.Empty, "URL: CeadeCEtabs Protocol");
                key.SetValue("URL Protocol", string.Empty);
                key = key.CreateSubKey("shell");
                key = key.CreateSubKey("open");
                key = key.CreateSubKey("command");
                key.SetValue(string.Empty, currentDirectory + "\\CeadeCEtabs.exe" + " " + "%1");
            }
            key.Close();
        }
        public static string httpRequestResponse(string postData)
        {
            if (!CheckForInternetConnection())
            {
                return "INTERNETERROR";
            }
            try
            {

                WebRequest request = WebRequest.Create("https://ceadec.xyz/CeadeCProducts/CeadeCEtabs/index.php");
                request.Method = "POST";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                string responseFromServer;
                using (dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd();
                }
                response.Close();
                if (responseFromServer.Contains("ERROR"))
                {
                    return "ERROR";
                }
                else
                {
                    return responseFromServer;
                }
            }
            catch
            {
                return "ERROR";
            }
        }
        public static class EtabsHelpers
        {

        }
        public static class CeadeCHelpers
        {
            public static string[] analyzeArg(string arg)
            {
                char[] spearator = { ':', '/' };
                String[] strlist = arg.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                return strlist;
            }
            public static string getEtabsE2KDataFromServer(string etabsE2KDataArg)
            {
                string[] IDKEY = analyzeArg(etabsE2KDataArg);
                if (IDKEY.Length != 3)
                {
                    return "ERROR";
                }
                string postData = "id=" + IDKEY[1] + "&key=" + IDKEY[2];
                string response = Helpers.httpRequestResponse(postData);
                return response;
            }

            public static model convertE2KStringToObject(string E2KString)
            {
                model E2KObject;
                //       try
                //       {
                dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(E2KString);
                E2KObject = new model(jsonObject);
                //       }
                //       catch
                //       {
                //           E2KObject = null;
                //       }
                return E2KObject;
            }
        }
    }
}
