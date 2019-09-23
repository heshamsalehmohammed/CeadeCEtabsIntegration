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
using CeadeCEtabsSectionParser;
using Microsoft.Win32;
using System.Dynamic;

namespace CeadeCEtabs
{
    public partial class CeadeCEtabsMainForm : Form
    {
        public CeadeCEtabsMainForm(string userEtabsPointer)
        {
            InitializeComponent();
            Designing = new Designing(userEtabsPointer);
        }
        Designing Designing;
        cOAPI myETABSObject = null;
        cSapModel mySapModel = null;
        etabsSelectedObjects selected;
        List<string> selectedFrames;
        etabsRunnedLoadCases runnedCases;
        List<string> FinishedRunnedCases;
        etabsCombos combonames;
        List<string> Finishedcombonames;
        etabsAnalysisResults analysisResults;
        etabsAllFrames etabsAllFrames;

        public void contact_CeadeC()
        {
            if (Designing.autoDesginList.Count != 0)
            {
                string Arg1 = "userEtabsPointer=";
                byte[] byt = System.Text.Encoding.UTF8.GetBytes(Designing.userEtabsPointer);
                string strModified = Convert.ToBase64String(byt);
                Arg1 += strModified;
                string Arg2 = "autoDesignList=";
                string result2 = JsonConvert.SerializeObject(Designing.autoDesginList);
                byte[] byt2 = System.Text.Encoding.UTF8.GetBytes(result2);
                string strModified2 = Convert.ToBase64String(byt2);
                Arg2 += strModified2;
                System.Diagnostics.Process.Start("http://localhost/CeadeC/CeadeC/public/CeadeC-PlatForm/index.php" + "?" + Arg1 + "&" + Arg2);
            }
            else
            {
                ErrorForm errorForm = new ErrorForm("ERROR", "No Data To Export . ", false);
                errorForm.ShowDialog();
            }
        }
        public int contact_Etabs()
        {
            Clean();
            if (etabsAttach() == 0)
            {
                return 0;
            }
            if (modelAttach() == 0)
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
            etabsAllFrames = new etabsAllFrames(mySapModel, "Global");
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
            catch
            {
                ErrorForm errorForm = new ErrorForm("ERROR", "No running instance of the program found or failed to attach.", false);
                errorForm.ShowDialog();
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
            catch
            {
                ErrorForm errorForm = new ErrorForm("ERROR", "No running model found or failed to attach.", false);
                errorForm.ShowDialog();
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
            etabsSetUnits.etabsSetPresentUnits(mySapModel, eForce.tonf, eLength.m, eTemperature.C);
            if (listBox2.SelectedItem != null)
            {
                analysisResults = new etabsAnalysisResults(mySapModel, listBox2.SelectedItem.ToString(), eItemTypeElm.ObjectElm, FinishedRunnedCases, Finishedcombonames);

                if (analysisResults.NumberResults == 0)
                {
                    ErrorForm errorForm = new ErrorForm("ERROR", "Run the analysis and try again. ", false);
                    errorForm.ShowDialog();
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
        private void Button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null && listBox2.SelectedItem != null && (listBox3.SelectedItem != null || listBox4.SelectedItem != null))
            {
                etabsSetUnits.etabsSetPresentUnits(mySapModel, eForce.N, eLength.mm, eTemperature.C);
                etabsAllFrames = new etabsAllFrames(mySapModel, "Global");
                string frameUniqueName = listBox2.SelectedItem.ToString();
                string frameSectionPropertyName = etabsAllFrames.PropName[Array.IndexOf(etabsAllFrames.MyName, frameUniqueName)];
                ShapeAutoDesign AD = new ShapeAutoDesign(frameSectionPropertyName, Convert.ToDouble(dataGridView1.Rows[0].Cells[0].Value), "ton", Convert.ToDouble(dataGridView1.Rows[0].Cells[2].Value), "ton.m", Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value), "ton.m");
                Designing.autoDesginList.Add(AD);
                contact_CeadeC();
            }
            else
            {
                ErrorForm errorForm = new ErrorForm("ERROR", "No data to be exported. ", false);
                errorForm.ShowDialog();
            }
        }
        private void CeadeCEtabsMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void CeadeCEtabsMainForm_Load(object sender, EventArgs e)
        {
           
        }
    }
}
