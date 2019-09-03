﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETABS2016;

namespace EtabsObjects
{
    public class etabsAnalysisResults
    {
        public int NumberResults;
        public string[] Obj;
        public double[] ObjSta;
        public string[] Elm;
        public double[] ElmSta;
        public string[] LoadCase;
        public string[] StepType;
        public double[] StepNum;
        public double[] P;
        public double[] V2;
        public double[] V3;
        public double[] T;
        public double[] M2;
        public double[] M3;

        public etabsAnalysisResults(cSapModel mySapModel, string objectName, eItemTypeElm itemType, List<string> loadCaseNames, string[] comboNames)
        {
            this.NumberResults = 0;
            this.Obj = new string[0];
            this.ObjSta = new double[0];
            this.Elm = new string[0];
            this.ElmSta = new double[0];
            this.LoadCase = new string[0];
            this.StepType = new string[0];
            this.StepNum = new double[0];
            this.P = new double[0];
            this.V2 = new double[0];
            this.V3 = new double[0];
            this.T = new double[0];
            this.M2 = new double[0];
            this.M3 = new double[0];

            mySapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();

            for (int i = 0; i < loadCaseNames.Count; i++)
            {
                mySapModel.Results.Setup.SetCaseSelectedForOutput(loadCaseNames[i]);
            }

            for (int i = 0; i < comboNames.Length; i++)
            {
                mySapModel.Results.Setup.SetComboSelectedForOutput(comboNames[i]);
            }


            mySapModel.Results.FrameForce(objectName, itemType, ref this.NumberResults, ref this.Obj, ref this.ObjSta, ref this.Elm, ref this.ElmSta, ref this.LoadCase, ref this.StepType, ref this.StepNum, ref this.P, ref this.V2, ref this.V3, ref this.T, ref this.M2, ref this.M3);
        }
    }



    public class etabsSelectedObjects
    {
        public int NumberItems;
        public int[] ObjectType;
        public string[] ObjectName;

        public etabsSelectedObjects(cSapModel mySapModel)
        {
            this.NumberItems = 0;
            this.ObjectType = new int[0];
            this.ObjectName = new string[0];
            mySapModel.SelectObj.GetSelected(ref this.NumberItems, ref this.ObjectType, ref this.ObjectName);
        }
        public List<string> selectedType(int type)
        {
            // Object Type  :           // 1 -  Point object            // 2 -  Frame object            // 3 -  Cable object            // 4 -  Tendon object            // 5 -  Area object            // 6 -  Solid object            // 7 -  Link object
            List<string> selectedType = new List<string>();
            for (int i = 0; i < this.NumberItems; i++)
            {
                if (this.ObjectType[i] == type)
                {
                    selectedType.Add(this.ObjectName[i]);
                }
            }
            return selectedType;
        }
    }

    public class etabsRunnedLoadCases
        Private cSapModel mySapModel;
        public int NumberItems;
        public string[] CaseName;
        public int[] Status;

        public etabsRunnedLoadCases(cSapModel mySapModel)
        {
            this.mySapModel = mySapModel;
            this.NumberItems = 0;
            this.CaseName = new string[0];
            this.Status = new int[0];
            mySapModel.Analyze.GetCaseStatus(ref this.NumberItems, ref this.CaseName, ref this.Status);
        }

        public List<string> getWithStatues(int statues ,bool withModal = false, bool onlyInLoadPatterns = true)
        {
            // statues 
            //1- Not run
            //2- Could not start
            //3- Not finished
            //4- Finished
            List<string> cases = new List<string>();
            etabsLoadPatterns loadPatterns = new etabsLoadPatterns(this.mySapModel);
            for (int i = 0; i < this.NumberItems; i++)
            {
                if (this.Status[i] == statues )
                {
                    if( withModal == true || (withModal == false && this.CaseName[i] != "modal") ){
                       if( onlyInLoadPatterns == false || (onlyInLoadPatterns == true && loadPatterns.loadPatternNames.Contains(this.CaseName[i] ) ) ){
                         cases.Add(this.CaseName[i]);
                       }
                    }
                }
            }
            return cases;
        }
    }


    public class etabsCombos
    {
        public int NumberNames;
        public string[] comboNames;

        public etabsCombos(cSapModel mySapModel)
        {
            this.NumberNames = 0;
            this.comboNames = new string[0];
            mySapModel.RespCombo.GetNameList(ref this.NumberNames, ref this.comboNames);
        }


    }

     public class etabsLoadPatterns
    {
        public int NumberNames;
        public string[] loadPatternNames;

        public etabsLoadPatterns(cSapModel mySapModel)
        {
            this.NumberNames = 0;
            this.loadPatternNames = new string[0];
            SapModel.LoadPatterns.GetNameList(ref this.NumberNames, ref this.loadPatternNames);
        }


    }


}
