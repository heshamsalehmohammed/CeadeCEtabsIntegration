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
        etabsAllFrames etabsAllFrames;
        public void contact_CeadeC(List<CeadeCObject> ParsedEtabsObjects)
        {
            model model = new model();
            for (int i = 0; i < ParsedEtabsObjects.Count; i++)
            {
                if (ParsedEtabsObjects[i] != null)
                {
                    model.objects.Add(ParsedEtabsObjects[i]);
                }
            }
            if (model.objects.Count != 0)
            {
                string result = JsonConvert.SerializeObject(model);
                byte[] byt = System.Text.Encoding.UTF8.GetBytes(result);
                // convert the byte array to a Base64 string
                string strModified = Convert.ToBase64String(byt);
                System.Diagnostics.Process.Start("http://localhost/CeadeC/CeadeC/public/CeadeC-PlatForm/index.php" + "?" + "model=" + strModified);
            }
            else
            {
                MessageBox.Show("No Data To Export . ");
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
        public string getCurrentEtabsLengthUnit()
        {
            etabsPresentUnits LU = new etabsPresentUnits(mySapModel);
            switch (LU.lengthUnits)
            {
                case eLength.cm:
                    return "cm";
                    break;
                case eLength.ft:
                    return "ft";
                    break;
                case eLength.inch:
                    return "in";
                    break;
                case eLength.m:
                    return "m";
                    break;
                case eLength.micron:
                    return "micron";
                    break;
                case eLength.mm:
                    return "mm";
                    break;
            }
            return "null";
        }
        public string getCurrentEtabsForceUnit()
        {
            etabsPresentUnits LU = new etabsPresentUnits(mySapModel);
            switch (LU.forceUnits)
            {
                case eForce.N:
                    return "N";
                    break;
                case eForce.tonf:
                    return "ton";
                    break;
                case eForce.kN:
                    return "KN";
                    break;
                case eForce.lb:
                    return "lb";
                    break;
                case eForce.kgf:
                    return "kg";
                    break;
                case eForce.kip:
                    return "kip";
                    break;
            }
            return "null";
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
            etabsSetUnits.etabsSetPresentUnits(mySapModel, eForce.tonf, eLength.m, eTemperature.C);
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
        private void Button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null && listBox2.SelectedItem != null && (listBox3.SelectedItem != null || listBox4.SelectedItem != null))
            {
                etabsSetUnits.etabsSetPresentUnits(mySapModel, eForce.N, eLength.mm, eTemperature.C);
                etabsAllFrames = new etabsAllFrames(mySapModel, "Global");
                string frameUniqueName = listBox2.SelectedItem.ToString();
                string frameSectionPropertyName = etabsAllFrames.PropName[Array.IndexOf(etabsAllFrames.MyName, frameUniqueName)];
                etabsSectionType frameSectionType = new etabsSectionType(mySapModel, frameSectionPropertyName);

                List<CeadeCObject> ParsedEtabsObjects = new List<CeadeCObject>();
                var shape = CESectionParser.convertSectionPropertytoCeadecShape(mySapModel, frameSectionPropertyName, frameSectionType.propType);
                CESectionParser.AsignAutoDesign_SA(shape, Convert.ToDouble(dataGridView1.Rows[0].Cells[0].Value), "ton", Convert.ToDouble(dataGridView1.Rows[0].Cells[2].Value), "ton.m", Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value), "ton.m");
                ParsedEtabsObjects.Add(shape);
                contact_CeadeC(ParsedEtabsObjects);
            }
            else
            {
                MessageBox.Show("No data to be exported. ");
            }
        }

    }






}
