using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CeadeCEtabs;
using ETABS2016;
using EtabsObjects;

namespace CeadeCEtabsSectionParser
{
    public static class CEParser
    {
        public static CeadeCShapes fetch_CeadeCShape_From_E2KObjects(cSapModel mySapModel, model E2KData, string PropertyName)
        {
            CeadeCShapes shape = null;
            // find the shape name similar to th eproperty name 
            if (E2KData.objects.Any(ob => ob.Name == PropertyName))
            {
                CeadeCShapes sh = (E2KData.objects.FirstOrDefault(ob => ob.Name == PropertyName) as CeadeCShapes);
                if (sh != null)
                    shape = sh;
            }
            return shape;
        }
        public static double getFy(cSapModel mySapModel, string materialPropertyName)
        {
            etabsMaterialType type = new etabsMaterialType(mySapModel, materialPropertyName);
            if (type.MatType == eMatType.Rebar)
            {
                etabsMaterialRebar rebar = new etabsMaterialRebar(mySapModel, materialPropertyName);
                return rebar.Fy;
            }
            return 0;
        }
        public static double getFcu(cSapModel mySapModel, string materialPropertyName)
        {
            etabsMaterialType type = new etabsMaterialType(mySapModel, materialPropertyName);
            if (type.MatType == eMatType.Concrete)
            {
                etabsMaterialConcrete conc = new etabsMaterialConcrete(mySapModel, materialPropertyName);
                return conc.Fcu;
            }
            return 0;
        }
        public static string getCurrentEtabsLengthUnit(cSapModel mySapModel)
        {
            etabsPresentUnits LU = new etabsPresentUnits(mySapModel);
            string un = "null";
            switch (LU.lengthUnits)
            {
                case eLength.cm:
                    un = "cm";
                    break;
                case eLength.ft:
                    un = "ft";
                    break;
                case eLength.inch:
                    un = "in";
                    break;
                case eLength.m:
                    un = "m";
                    break;
                case eLength.micron:
                    un = "micron";
                    break;
                case eLength.mm:
                    un = "mm";
                    break;
            }
            return un;
        }
        public static string getCurrentEtabsForceUnit(cSapModel mySapModel)
        {
            etabsPresentUnits LU = new etabsPresentUnits(mySapModel);
            string un = "null";
            switch (LU.forceUnits)
            {
                case eForce.N:
                    un = "N";
                    break;
                case eForce.tonf:
                    un = "ton";
                    break;
                case eForce.kN:
                    un = "KN";
                    break;
                case eForce.lb:
                    un = "lb";
                    break;
                case eForce.kgf:
                    un = "kg";
                    break;
                case eForce.kip:
                    un = "kip";
                    break;
            }
            return un;
        }

    }
}

