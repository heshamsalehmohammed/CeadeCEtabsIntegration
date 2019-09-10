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
   public static class CESectionParser
   {
        public static CeadeCShapes convertSectionPropertytoCeadecShape(cSapModel mySapModel, string frameSectionPropertyName, eFramePropType sectionPropertyType)
        {

            CeadeCShapes shape = null;
            switch (sectionPropertyType)
            {
                case eFramePropType.Rectangular:
                    shape = CeadeCConvertRectangular(mySapModel,frameSectionPropertyName);
                    break;
                case eFramePropType.SD:
                    shape = CeadeCConvertSD(mySapModel, frameSectionPropertyName);
                    break;
            }
            return shape;
        }
        public static CeadeCShapes CeadeCConvertRectangular(cSapModel mySapModel, string frameSectionPropertyName)
        {
            CeadeCShapes Shape = null;
            List<CeadeCObject> children = new List<CeadeCObject>();
            string RFTMatProp = string.Empty;
            string ShapeMatProp = string.Empty;
            // Parse solid 
            etabsRectangleSection rectangleSectionData = new etabsRectangleSection(mySapModel, frameSectionPropertyName);
            ShapeMatProp = rectangleSectionData.MatProp;
            Vector3 sectionCentroid = new Vector3(0, 0, 0);
            List<Vector3> sectionVertices = new List<Vector3>();
            Vector3 p1 = new Vector3(sectionCentroid.x - rectangleSectionData.T2 / 2, sectionCentroid.y - rectangleSectionData.T3 / 2, 0);
            sectionVertices.Add(p1);
            Vector3 p2 = new Vector3(sectionCentroid.x + rectangleSectionData.T2 / 2, sectionCentroid.y - rectangleSectionData.T3 / 2, 0);
            sectionVertices.Add(p2);
            Vector3 p3 = new Vector3(sectionCentroid.x + rectangleSectionData.T2 / 2, sectionCentroid.y + rectangleSectionData.T3 / 2, 0);
            sectionVertices.Add(p3);
            Vector3 p4 = new Vector3(sectionCentroid.x - rectangleSectionData.T2 / 2, sectionCentroid.y + rectangleSectionData.T3 / 2, 0);
            sectionVertices.Add(p4);
            sectionVertices.Add(p1);
            CeadeCRectangles solid_Rectangle = new CeadeCRectangles(sectionVertices);
            solid_Rectangle.shapeChildType = "solid";
            children.Add(solid_Rectangle);
            // Parse RFT
            etabsFrameRebarType rebarType = new etabsFrameRebarType(mySapModel, frameSectionPropertyName);
            if (rebarType.MyType == 1) // column rebar
            {
                etabsRebarColumn rebarData = new etabsRebarColumn(mySapModel, frameSectionPropertyName);
                RFTMatProp = rebarData.MatPropLong;
                etabsRebarData rebarSizeData = new etabsRebarData(mySapModel, rebarData.RebarSize);
                etabsAllRebarData allRebData = new etabsAllRebarData(mySapModel);
                Vector3 p1_RecRebar = new Vector3(p1.x + rebarData.Cover, p1.y + rebarData.Cover, 0);
                Vector3 p2_RecRebar = new Vector3(p2.x - rebarData.Cover, p2.y + rebarData.Cover, 0);
                Vector3 p3_RecRebar = new Vector3(p3.x - rebarData.Cover, p3.y - rebarData.Cover, 0);
                Vector3 p4_RecRebar = new Vector3(p4.x + rebarData.Cover, p4.y - rebarData.Cover, 0);
                CeadeCRectangles rebarRectangle = new CeadeCRectangles(new List<Vector3>() { p1_RecRebar, p2_RecRebar, p3_RecRebar, p4_RecRebar, p1_RecRebar });
                rebarRectangle.shapeChildType = "rebarsObject";
                rebarRectangle.rebars = new RectangleRebar(rebarSizeData.Diameter, 0, true, true, true);

                int widthRebarNumbers = rebarData.NumberR3Bars;
                int heightRebarNumbers = rebarData.NumberR2Bars;


                if (widthRebarNumbers > 2)
                {
                    double rebarWidth = p2_RecRebar.x - p1_RecRebar.x;
                    int internalRebarsNumbers = widthRebarNumbers - 2;
                    double internalRebarsSpacing = rebarWidth / (internalRebarsNumbers + 1);
                    for (double i = internalRebarsSpacing; i < rebarWidth; i += internalRebarsSpacing)
                    {
                        var singleRebar = new CeadeCSingleRebar(new Vector3(p1_RecRebar.x + i, p1_RecRebar.y, 0), rebarSizeData.Diameter, getCurrentEtabsLengthUnit(mySapModel), true);
                        rebarRectangle.rebars.singleRebars.Add(singleRebar);
                    }
                    for (double i = internalRebarsSpacing; i < rebarWidth; i += internalRebarsSpacing)
                    {
                        var singleRebar = new CeadeCSingleRebar(new Vector3(p4_RecRebar.x + i, p4_RecRebar.y, 0), rebarSizeData.Diameter, getCurrentEtabsLengthUnit(mySapModel), true);
                        rebarRectangle.rebars.singleRebars.Add(singleRebar);
                    }
                }
                if (heightRebarNumbers > 2)
                {
                    double rebarHeight = p4_RecRebar.y - p1_RecRebar.y;
                    int internalRebarsNumbers = heightRebarNumbers - 2;
                    double internalRebarsSpacing = rebarHeight / (internalRebarsNumbers + 1);
                    for (double i = internalRebarsSpacing; i < rebarHeight; i += internalRebarsSpacing)
                    {
                        var singleRebar = new CeadeCSingleRebar(new Vector3(p1_RecRebar.x, p1_RecRebar.y + i, 0), rebarSizeData.Diameter, getCurrentEtabsLengthUnit(mySapModel), true);
                        rebarRectangle.rebars.singleRebars.Add(singleRebar);
                    }
                    for (double i = internalRebarsSpacing; i < rebarHeight; i += internalRebarsSpacing)
                    {
                        var singleRebar = new CeadeCSingleRebar(new Vector3(p2_RecRebar.x, p2_RecRebar.y + i, 0), rebarSizeData.Diameter, getCurrentEtabsLengthUnit(mySapModel), true);
                        rebarRectangle.rebars.singleRebars.Add(singleRebar);
                    }
                }
                // Corner Rebars

                var singleRebar1 = new CeadeCSingleRebar(p1_RecRebar, rebarSizeData.Diameter, getCurrentEtabsLengthUnit(mySapModel), true);
                rebarRectangle.rebars.singleRebars.Add(singleRebar1);

                var singleRebar2 = new CeadeCSingleRebar(p2_RecRebar, rebarSizeData.Diameter, getCurrentEtabsLengthUnit(mySapModel), true);
                rebarRectangle.rebars.singleRebars.Add(singleRebar2);

                var singleRebar3 = new CeadeCSingleRebar(p3_RecRebar, rebarSizeData.Diameter, getCurrentEtabsLengthUnit(mySapModel), true);
                rebarRectangle.rebars.singleRebars.Add(singleRebar3);

                var singleRebar4 = new CeadeCSingleRebar(p4_RecRebar, rebarSizeData.Diameter, getCurrentEtabsLengthUnit(mySapModel), true);
                rebarRectangle.rebars.singleRebars.Add(singleRebar4);
                children.Add(rebarRectangle);
                Shape = new CeadeCShapes(children);
                AsignAutoDesign_Data(Shape, "mm", getFy(mySapModel,RFTMatProp), "N/mm2", getFcu(mySapModel,ShapeMatProp), "N/mm2");

            }
            else if (rebarType.MyType == 2) // beam rabar or none rebar
            {
                MessageBox.Show("select a column to export , not a beam. ");
                Shape = null;
            }
            else if (rebarType.MyType == 0)
            {
                MessageBox.Show("select a reinforced column . ");
                Shape = null;
            }

            return Shape;
        }

        public static CeadeCShapes CeadeCConvertSD(cSapModel mySapModel, string frameSectionPropertyName)
        {
            CeadeCShapes Shape = null;
            List<CeadeCObject> children = new List<CeadeCObject>();
            string RFTMatProp = string.Empty;
            string ShapeMatProp = string.Empty;
            // Parse solid 
            etabsSDSection rectangleSectionData = new etabsSDSection(mySapModel, frameSectionPropertyName);
            ShapeMatProp = rectangleSectionData.MatProp;


            return Shape;
        }








        public static double getFy(cSapModel mySapModel,string materialPropertyName)
        {
            etabsMaterialType type = new etabsMaterialType(mySapModel, materialPropertyName);
            if (type.MatType == eMatType.Rebar)
            {
                etabsMaterialRebar rebar = new etabsMaterialRebar(mySapModel, materialPropertyName);
                return rebar.Fy;
            }
            return 0;
        }
        public static double getFcu(cSapModel mySapModel,string materialPropertyName)
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
        public static string getCurrentEtabsForceUnit(cSapModel mySapModel)
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
        public static void AsignAutoDesign_Data(CeadeCShapes shape, string shapeUnit, double fy, string fyUnit, double fcu, string fcuUnit)
        {
            shape.AutoDesign.shapeUnit.Add(shapeUnit);
            shape.AutoDesign.fy.Add(fy);
            shape.AutoDesign.fyUnit.Add(fyUnit);
            shape.AutoDesign.fcu.Add(fcu);
            shape.AutoDesign.fcuUnit.Add(fcuUnit);
        }
        public static void AsignAutoDesign_SA(CeadeCShapes shape, double P, string PUnit, double Mx, string MxUnit, double My, string MyUnit)
        {
            shape.AutoDesign.P.Add(P);
            shape.AutoDesign.PUnit.Add(PUnit);
            shape.AutoDesign.Mx.Add(Mx);
            shape.AutoDesign.MxUnit.Add(MxUnit);
            shape.AutoDesign.My.Add(My);
            shape.AutoDesign.MyUnit.Add(MyUnit);
        }
    }
}

