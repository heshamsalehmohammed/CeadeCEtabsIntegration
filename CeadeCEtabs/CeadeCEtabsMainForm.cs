using ETABS2016;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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

        cOAPI myETABSObject = null;
        cSapModel mySapModel = null;

        etabsSelectedObjects selected;
        List<string> selectedFrames;

        etabsRunnedLoadCases runnedCases;
        List<string> FinishedRunnedCases;
        etabsCombos combonames;
        List<string> Finishedcombonames;
        etabsAnalysisResults analysisResults;

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
            Clean();
            if (etabsAttach() ==0)
            {
                return 0;
            }
            if (modelAttach()==0)
            {
                return 0;
            }
            
            selected = new etabsSelectedObjects(mySapModel);
            selectedFrames = selected.selectedType(2);
            listBox2.Items.AddRange(selectedFrames.ToArray());



            runnedCases = new etabsRunnedLoadCases(mySapModel);
            FinishedRunnedCases = runnedCases.getWithStatues(4);
            listBox3.Items.AddRange(FinishedRunnedCases.ToArray());
            combonames = new etabsCombos(mySapModel);
            Finishedcombonames = combonames.comboNames.ToList<string>();
            listBox4.Items.AddRange(Finishedcombonames.ToArray());

            
            if (getAnalysisResultForSelectedFrame() == 1)
            {
                
                fillDataGridFromAnalysisResults();

            }

            return 1;

        }








        private void Button1_Click(object sender, EventArgs e)
        {
            contact_Etabs();
        }


        public int etabsAttach()
        {
            try
            {
                myETABSObject = (cOAPI)System.Runtime.InteropServices.Marshal.GetActiveObject("CSI.ETABS.API.ETABSObject");
            }
            catch (Exception ex)
            {
                MessageBox.Show("No running instance of the program found or failed to attach.");
                return 0;
            }
            return 1;
        }
        public int modelAttach()
        {
            try
            {
                mySapModel = default(cSapModel);
                mySapModel = myETABSObject.SapModel;

            }
            catch (Exception ex)
            {
                MessageBox.Show("No running model found or failed to attach.");
                return 0;
            }
            return 1;
        }

        public int fillDataGridFromAnalysisResults()
        {
            dataGridView1.Rows.Clear();

            if (listBox2.SelectedItem == null && listBox3.SelectedItem == null && listBox4.SelectedItem == null && listBox1.SelectedItem == null)
            {
                return 0;
            }
            if (listBox2.SelectedItem != null)
            {
                for (int i = 0; i < analysisResults.LoadCase.Length; i++)
                {
                    if (listBox1.SelectedItem != null)
                    {
                        if (listBox3.SelectedItem != null)
                        {
                            if (listBox3.SelectedItem.ToString() == analysisResults.LoadCase[i])
                            {
                                if (listBox1.SelectedItem.ToString() == analysisResults.ObjSta[i].ToString())
                                {
                                    string[] row = new string[] { analysisResults.P[i].ToString(), analysisResults.M2[i].ToString(), analysisResults.M3[i].ToString(), analysisResults.V2[i].ToString(), analysisResults.V3[i].ToString(), analysisResults.T[i].ToString() };
                                    dataGridView1.Rows.Add(row);
                                }
                            }
                        }
                        if (listBox4.SelectedItem != null)
                        {
                            if (listBox4.SelectedItem.ToString() == analysisResults.LoadCase[i])
                            {
                                if (listBox1.SelectedItem.ToString() == analysisResults.ObjSta[i].ToString())
                                {
                                    string[] row = new string[] { analysisResults.P[i].ToString(), analysisResults.M2[i].ToString(), analysisResults.M3[i].ToString(), analysisResults.V2[i].ToString(), analysisResults.V3[i].ToString(), analysisResults.T[i].ToString() };
                                    dataGridView1.Rows.Add(row);
                                }
                            }
                        }
                    }
                }

            }
            return 1;
        }

        public int getAnalysisResultForSelectedFrame()
        {

            if (listBox2.SelectedItem != null)
            {
                analysisResults = new etabsAnalysisResults(mySapModel, listBox2.SelectedItem.ToString(), eItemTypeElm.ObjectElm, FinishedRunnedCases, Finishedcombonames);

                if (analysisResults.NumberResults == 0)
                {
                    MessageBox.Show("Run the analysis and try again. ");
                    return 0;
                }
                for (int i = 0; i < listBox3.Items.Count; i++)
                {
                    if (!analysisResults.LoadCase.Contains(listBox3.Items[i]))
                    {
                        listBox3.Items.Remove(listBox3.Items[i]);
                        i--;
                    }
                }
                for (int i = 0; i < listBox4.Items.Count; i++)
                {
                    if (!analysisResults.LoadCase.Contains(listBox4.Items[i]))
                    {
                        listBox3.Items.Remove(listBox4.Items[i]);
                        i--;
                    }
                }
                listBox1.Items.Clear();
                for (int i = 0; i < analysisResults.ObjSta.Length; i++)
                {
                    if (!listBox1.Items.Contains(analysisResults.ObjSta[i]) && listBox2.SelectedItem.ToString() == analysisResults.Obj[i])
                    {
                        listBox1.Items.Add(analysisResults.ObjSta[i]);
                    }
                }
                return 1;
            }
            else
            {
                return 0;
            }

        }



        public void Clean()
        {
            mySapModel = null;
            myETABSObject = null;
            CleanContainers(true);
        }

        public void CleanContainers(bool clearSelectionContainer = false)
        {
            if (clearSelectionContainer)
            {
                listBox2.Items.Clear();
            }
            listBox1.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            dataGridView1.Rows.Clear();
        }

        private void ListBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox4.SelectedIndexChanged -= new EventHandler(ListBox4_SelectedIndexChanged);
            listBox4.ClearSelected();
            fillDataGridFromAnalysisResults();
            listBox4.SelectedIndexChanged += new EventHandler(ListBox4_SelectedIndexChanged);
            
        }

        private void ListBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox3.SelectedIndexChanged -= new EventHandler(ListBox3_SelectedIndexChanged);
            listBox3.ClearSelected();
            fillDataGridFromAnalysisResults();
            listBox3.SelectedIndexChanged += new EventHandler(ListBox3_SelectedIndexChanged);
            
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillDataGridFromAnalysisResults();
        }

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            getAnalysisResultForSelectedFrame();
        }
    }
}
