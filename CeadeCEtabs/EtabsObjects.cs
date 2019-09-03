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
         Public   int NumberResults;
         Public   string[] Obj;
         Public   double[] ObjSta ;
         Public   string[] Elm ;
         Public   double[] ElmSta ;
         Public   string[] LoadCase ;
         Public   string[] StepType ;
         Public   double[] StepNum ;
         Public   double[] P ;
         Public   double[] V2 ;
         Public   double[] V3 ;
         Public   double[] T ;
         Public   double[] M2 ;
         Public   double[] M3 ;

        public etabsAnalysisResults(ETABS2016.cSapModel mySapModel , string objectName ,eItemTypeElm itemType )
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
         mySapModel.Results.FrameForce(objectName, itemType, ref this.NumberResults, ref this.Obj, ref this.ObjSta, ref this.Elm, ref this.ElmSta, ref this.LoadCase, ref this.StepType, ref this.StepNum, ref this.P, ref this.V2, ref this.V3, ref this.T, ref this.M2, ref this.M3);
        }
    }


    
    public class etabsSelectedObjects
    {
            Public  int NumberItems;
            Public int[] ObjectType;
            Public string[] ObjectName;

            public etabsSelectedObjects(ETABS2016.cSapModel mySapModel )
            {
            this.NumberItems = 0;
            this.ObjectType = new int[0];
            this.ObjectName = new string[0];
            mySapModel.Select.GetSelected(ref this.NumberItems, ref this.ObjectType, ref this.ObjectName);
            }
    }




}
