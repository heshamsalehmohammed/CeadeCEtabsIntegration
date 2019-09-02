﻿using ETABS2016;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace CeadeCEtabs
{
    public partial class CeadeCEtabsMainForm : Form
    {
        public CeadeCEtabsMainForm()
        {
            InitializeComponent();
        }

        ETABS2016.cOAPI myETABSObject = null;
        ETABS2016.cSapModel mySapModel = null;

        public void contact_CeadeC()
        {
            model model = new model();

            CeadeCRectangles rectangle1 = new CeadeCRectangles(
                new List<Vector3>() {
                new Vector3(0, 0, 0) ,
                new Vector3(500, 0, 0) ,
                new Vector3(500, 500, 0),
                new Vector3(0,500,0),
                new Vector3(0,0,0)
               });
            rectangle1.shapeChildType = "solid";

            CeadeCRectangles rectangle2 = new CeadeCRectangles(
                new List<Vector3>() {
                new Vector3(50, 50, 0) ,
                new Vector3(450, 50, 0) ,
                new Vector3(450, 450, 0),
                new Vector3(50,450,0),
                new Vector3(50,50,0)
                });
            rectangle2.shapeChildType = "rebarsObject";
            rectangle2.rebars = new RectangleRebar(10, 400, false, true, true);

            var singleRebar1 = new CeadeCSingleRebar(new Vector3(50, 50, 0),10,"mm",true);
            rectangle2.rebars.singleRebars.Add(singleRebar1);

            var singleRebar2 = new CeadeCSingleRebar(new Vector3(450, 50, 0), 10, "mm", true);
            rectangle2.rebars.singleRebars.Add(singleRebar2);

            var singleRebar3 = new CeadeCSingleRebar(new Vector3(450, 450, 0), 10, "mm", true);
            rectangle2.rebars.singleRebars.Add(singleRebar3);

            var singleRebar4 = new CeadeCSingleRebar(new Vector3(50, 450, 0), 10, "mm", true);
            rectangle2.rebars.singleRebars.Add(singleRebar4);

            CeadeCShapes shape1 = new CeadeCShapes(new List<CeadeCObject>() { rectangle1 , rectangle2 });
            model.objects.Add(shape1);


            string result = JsonConvert.SerializeObject(model);

            byte[] byt = System.Text.Encoding.UTF8.GetBytes(result);

            // convert the byte array to a Base64 string

            string strModified = Convert.ToBase64String(byt);


            System.Diagnostics.Process.Start("http://localhost/CeadeC/CeadeC/public/CeadeC-PlatForm/index.php"+"?"+ "model=" + strModified);

  
        }


        public void contact_Etabs()
        {
            etabsAttach();
            mySapModel = default(ETABS2016.cSapModel);
            mySapModel = myETABSObject.SapModel;
           
            mySapModel = null;
            myETABSObject = null;

        }

        public List<string> etabsSelected(string type)
        {

        List<string> selectedType = new List<string>();

        
        int NumberItems = 0;
	int[] ObjectType = new int[](0);
        string[] ObjectName = new string[](0);

        mySapModel. GetSelected(ref  NumberItems,ref  ObjectType,ref  ObjectName);

        for (int i = 0; i < NumberItems; i++)
        {
          if(ObjectType[i] ==type  ){
             selectedType.Add(ObjectName[i] );
          }
        }

        return selectedType;

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            contact_Etabs();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            etabsAttach();
        }

        public void etabsAttach()
        {
            try
            {
                myETABSObject = (ETABS2016.cOAPI)System.Runtime.InteropServices.Marshal.GetActiveObject("CSI.ETABS.API.ETABSObject");
            }
            catch (Exception ex)
            {
                MessageBox.Show("No running instance of the program found or failed to attach.");

            }
        }
    }
}
