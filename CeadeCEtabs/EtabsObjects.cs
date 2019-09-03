using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETABS2016;

namespace EtabsObjects
{
    public class etabsAnalysisResults
    {
         Public   int NumberResults = 0;
         Public   string[] Obj = new string[0];
         Public   double[] ObjSta = new double[0];
         Public   string[] Elm = new string[0];
         Public   double[] ElmSta = new double[0];
         Public   string[] LoadCase = new string[0];
         Public   string[] StepType = new string[0];
         Public   double[] StepNum = new double[0];
         Public   double[] P = new double[0];
         Public   double[] V2 = new double[0];
         Public   double[] V3 = new double[0];
         Public   double[] T = new double[0];
         Public   double[] M2 = new double[0];
         Public   double[] M3 = new double[0];



        public etabsAnalysisResults(mySapModel)
        {
            mySapModel.Results.FrameForce(objectName, eItemTypeElm.ObjectElm, ref this.NumberResults, ref this.Obj, ref this.ObjSta, ref this.Elm, ref this.ElmSta, ref this.LoadCase, ref this.StepType, ref this.StepNum, ref this.P, ref this.V2, ref this.V3, ref this.T, ref this.M2, ref this.M3);
        }
    }







}
