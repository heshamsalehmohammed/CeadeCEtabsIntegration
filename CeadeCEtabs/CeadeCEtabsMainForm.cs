using ETABS2016;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;
using EtabsObjects;

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

            var singleRebar1 = new CeadeCSingleRebar(new Vector3(50, 50, 0), 10, "mm", true);
            rectangle2.rebars.singleRebars.Add(singleRebar1);

            var singleRebar2 = new CeadeCSingleRebar(new Vector3(450, 50, 0), 10, "mm", true);
            rectangle2.rebars.singleRebars.Add(singleRebar2);

            var singleRebar3 = new CeadeCSingleRebar(new Vector3(450, 450, 0), 10, "mm", true);
            rectangle2.rebars.singleRebars.Add(singleRebar3);

            var singleRebar4 = new CeadeCSingleRebar(new Vector3(50, 450, 0), 10, "mm", true);
            rectangle2.rebars.singleRebars.Add(singleRebar4);

            CeadeCShapes shape1 = new CeadeCShapes(new List<CeadeCObject>() { rectangle1, rectangle2 });
            model.objects.Add(shape1);


            string result = JsonConvert.SerializeObject(model);

            byte[] byt = System.Text.Encoding.UTF8.GetBytes(result);

            // convert the byte array to a Base64 string

            string strModified = Convert.ToBase64String(byt);


            System.Diagnostics.Process.Start("http://localhost/CeadeC/CeadeC/public/CeadeC-PlatForm/index.php" + "?" + "model=" + strModified);


        }


        public int contact_Etabs()
        {
            etabsAttach();
            modelAttach();
            etabsSelectedObjects selected = new etabsSelectedObjects(mySapModel);
            List<string> selectedFrames = selected.selectedType(2);
            if (selectedFrames.Count == 0)
            {
                MessageBox.Show("select a frame object and try again. ");
                return 0;
            }
            else if (selectedFrames.Count != 1)
            {
                MessageBox.Show("according to your membership : only 1 frame can be sent to the server ");
                return 0;
            }

            etabsAnalysisResults analysisResults = new etabsAnalysisResults(mySapModel, selectedFrames[0], eItemTypeElm.ObjectElm);
            if (analysisResults.NumberResults == 0)
            {
                MessageBox.Show("Run the analysis and try again. ");
                return 0;
            }

            etabsClean();
            return 1;

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
        public void modelAttach()
        {
            try
            {
                mySapModel = default(ETABS2016.cSapModel);
                mySapModel = myETABSObject.SapModel;

            }
            catch (Exception ex)
            {
                MessageBox.Show("No running model found or failed to attach.");

            }
        }
        public void etabsClean()
        {
            mySapModel = null;
            myETABSObject = null;
        }

    }
}
