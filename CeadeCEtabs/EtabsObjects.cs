﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETABS2016;

namespace EtabsObjects
{
    // material 
    public class etabsMaterialType
    {
        cSapModel mySapModel;
        public eMatType MatType;
        public int SymType;
        public etabsMaterialType(cSapModel mySapModel, string MaterialPropertyName)
        {
            // Steel	1	
            // Concrete    2
            // NoDesign    3
            // Aluminum    4
            // ColdFormed  5
            // Rebar   6
            // Tendon  7
            // Masonry 8
            this.mySapModel = mySapModel;
            this.SymType = 0;
            this.mySapModel.PropMaterial.GetTypeOAPI(MaterialPropertyName, ref this.MatType, ref this.SymType);
        }
    }
    public class etabsMaterialConcrete
    {
        cSapModel mySapModel;
        public double Fc;
        public double Fcu;
        public bool IsLightweight;
        public double FcsFactor;
        public int SSType;
        public int SSHysType;
        public double StrainAtFc;
        public double StrainUltimate;
        public double FrictionAngle;
        public double DilatationalAngle;
        public etabsMaterialConcrete(cSapModel mySapModel, string MaterialPropertyName)
        {
            this.mySapModel = mySapModel;
            this.Fc = 0;
            this.IsLightweight = false;
            this.FcsFactor = 0;
            this.SSType = 0;
            this.SSHysType = 0;
            this.StrainAtFc = 0;
            this.StrainUltimate = 0;
            this.FrictionAngle = 0;
            this.DilatationalAngle = 0;
            this.mySapModel.PropMaterial.GetOConcrete(MaterialPropertyName, ref this.Fc, ref this.IsLightweight, ref this.FcsFactor, ref this.SSType, ref this.SSHysType, ref this.StrainAtFc, ref this.StrainUltimate, ref this.FrictionAngle, ref this.DilatationalAngle);
            this.Fcu = 1.25 * this.Fc;
        }
    }
    public class etabsMaterialRebar
    {
        cSapModel mySapModel;
        public double Fy;
        public double Fu;
        public double EFy;
        public double EFu;
        public int SSType;
        public int SSHysType;
        public double StrainAtHardening;
        public double StrainUltimate;
        public bool UseCaltransSSDefaults;
        public etabsMaterialRebar(cSapModel mySapModel, string MaterialPropertyName)
        {
            this.mySapModel = mySapModel;
            this.Fy = 0;
            this.Fu = 0;
            this.EFy = 0;
            this.EFu = 0;
            this.SSType = 0;
            this.SSHysType = 0;
            this.StrainAtHardening = 0;
            this.StrainUltimate = 0;
            this.UseCaltransSSDefaults = false;
            this.mySapModel.PropMaterial.GetORebar(MaterialPropertyName, ref this.Fy, ref this.Fu, ref this.EFy, ref this.EFu, ref this.SSType, ref this.SSHysType, ref this.StrainAtHardening, ref this.StrainUltimate, ref this.UseCaltransSSDefaults);
        }
    }
    // Analyze 
    public class etabsAnalysisResults
    {
        cSapModel mySapModel;
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

        public etabsAnalysisResults(cSapModel mySapModel, string objectName, eItemTypeElm itemType, List<string> loadCaseNames, List<string> comboNames)
        {
            this.mySapModel = mySapModel;
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

            this.mySapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();

            for (int i = 0; i < loadCaseNames.Count; i++)
            {
                this.mySapModel.Results.Setup.SetCaseSelectedForOutput(loadCaseNames[i]);
            }

            for (int i = 0; i < comboNames.Count; i++)
            {
                this.mySapModel.Results.Setup.SetComboSelectedForOutput(comboNames[i]);
            }


            this.mySapModel.Results.FrameForce(objectName, itemType, ref this.NumberResults, ref this.Obj, ref this.ObjSta, ref this.Elm, ref this.ElmSta, ref this.LoadCase, ref this.StepType, ref this.StepNum, ref this.P, ref this.V2, ref this.V3, ref this.T, ref this.M2, ref this.M3);
        }
    }
    public class etabsRunnedLoadCases
    {
        cSapModel mySapModel;
        public int NumberItems;
        public string[] CaseName;
        public int[] Status;

        public etabsRunnedLoadCases(cSapModel mySapModel)
        {
            this.mySapModel = mySapModel;
            this.mySapModel = mySapModel;
            this.NumberItems = 0;
            this.CaseName = new string[0];
            this.Status = new int[0];
            this.mySapModel.Analyze.GetCaseStatus(ref this.NumberItems, ref this.CaseName, ref this.Status);
        }

        public List<string> getWithStatues(int statues, bool withModal = false, bool onlyInLoadPatterns = true)
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
                if (this.Status[i] == statues)
                {
                    if (withModal == true || (withModal == false && this.CaseName[i] != "modal"))
                    {
                        if (onlyInLoadPatterns == false || (onlyInLoadPatterns == true && loadPatterns.loadPatternNames.Contains(this.CaseName[i])))
                        {
                            cases.Add(this.CaseName[i]);
                        }
                    }
                }
            }
            return cases;
        }
    }
    // SelectObj
    public class etabsSelectedObjects
    {
        cSapModel mySapModel;
        public int NumberItems;
        public int[] ObjectType;
        public string[] ObjectName;

        public etabsSelectedObjects(cSapModel mySapModel)
        {
            this.mySapModel = mySapModel;
            this.NumberItems = 0;
            this.ObjectType = new int[0];
            this.ObjectName = new string[0];
            this.mySapModel.SelectObj.GetSelected(ref this.NumberItems, ref this.ObjectType, ref this.ObjectName);
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
    // RespCombo
    public class etabsCombos
    {
        cSapModel mySapModel;
        public int NumberNames;
        public string[] comboNames;

        public etabsCombos(cSapModel mySapModel)
        {
            this.mySapModel = mySapModel;
            this.NumberNames = 0;
            this.comboNames = new string[0];
            this.mySapModel.RespCombo.GetNameList(ref this.NumberNames, ref this.comboNames);
        }


    }
    // LoadPatterns
    public class etabsLoadPatterns
    {
        cSapModel mySapModel;
        public int NumberNames;
        public string[] loadPatternNames;

        public etabsLoadPatterns(cSapModel mySapModel)
        {
            this.mySapModel = mySapModel;
            this.NumberNames = 0;
            this.loadPatternNames = new string[0];
            this.mySapModel.LoadPatterns.GetNameList(ref this.NumberNames, ref this.loadPatternNames);
        }


    }
    // FrameObj
    public class etabsFrameSectionProperty
    {
        cSapModel mySapModel;
        public string PropName;
        public string SAut;
        public etabsFrameSectionProperty(cSapModel mySapModel, string frameName)
        {
            this.mySapModel = mySapModel;
            this.PropName = string.Empty;
            this.SAut = string.Empty;
            this.mySapModel.FrameObj.GetSection(frameName, ref this.PropName, ref this.SAut);
        }
    }
    public class etabsFrameNames
    {
        cSapModel mySapModel;
        public int NumberNames;
        public string[] MyNames;
        public etabsFrameNames(cSapModel mySapModel)
        {
            this.mySapModel = mySapModel;
            this.NumberNames = 0;
            this.MyNames = new string[0];
            this.mySapModel.FrameObj.GetNameList(ref this.NumberNames, ref this.MyNames);
        }
    }
    public class etabsFrameLocalAxes
    {
        cSapModel mySapModel;
        public double Ang;
        public bool Advanced;
        public etabsFrameLocalAxes(cSapModel mySapModel, string frameName)
        {
            this.mySapModel = mySapModel;
            this.Ang = 0;
            this.Advanced = false;
            this.mySapModel.FrameObj.GetLocalAxes(frameName, ref this.Ang, ref this.Advanced);
        }
    }
    public class etabsFrameEndPointNames
    {
        cSapModel mySapModel;
        public string Point1Name;
        public string Point2Name;
        public etabsFrameEndPointNames(cSapModel mySapModel, string frameName)
        {
            this.mySapModel = mySapModel;
            this.Point1Name = string.Empty;
            this.Point2Name = string.Empty;
            this.mySapModel.FrameObj.GetPoints(frameName, ref this.Point1Name, ref this.Point2Name);
        }
    }
    public class etabsFramePierName
    {
        cSapModel mySapModel;
        string PierName;
        public etabsFramePierName(cSapModel mySapModel, string frameName)
        {
            this.mySapModel = mySapModel;
            this.PierName = string.Empty;
            this.mySapModel.FrameObj.GetPier(frameName, ref this.PierName);
        }
    }
    public class etabsFrameTransformationMatrix
    {
        cSapModel mySapModel;
        public double[] Value;
        public etabsFrameTransformationMatrix(cSapModel mySapModel, string frameName, bool IsGlobal)
        {
            this.mySapModel = mySapModel;
            this.Value = new double[0];
            this.mySapModel.FrameObj.GetTransformationMatrix(frameName, ref this.Value, IsGlobal);
        }
    }
    public class etabsFrameOutputStations
    {
        cSapModel mySapModel;
        public int MyType;
        public double MaxSegSize;
        public int MinSections;
        public bool NoOutPutAndDesignAtElementEnds;
        public bool NoOutPutAndDesignAtPointLoads;
        public etabsFrameOutputStations(cSapModel mySapModel, string frameName)
        {
            this.mySapModel = mySapModel;
            this.MyType = 0;
            this.MaxSegSize = 0;
            this.NoOutPutAndDesignAtElementEnds = false;
            this.NoOutPutAndDesignAtPointLoads = false;
            this.mySapModel.FrameObj.GetOutputStations(frameName, ref this.MyType, ref this.MaxSegSize, ref this.MinSections, ref this.NoOutPutAndDesignAtElementEnds, ref this.NoOutPutAndDesignAtPointLoads);
        }
    }
    public class etabsAllFrames
    {
        cSapModel mySapModel;
        public int NumberNames;
        public string[] MyName;
        public string[] PropName;
        public string[] StoryName;
        public string[] PointName1;
        public string[] PointName2;
        public double[] Point1X;
        public double[] Point1Y;
        public double[] Point1Z;
        public double[] Point2X;
        public double[] Point2Y;
        public double[] Point2Z;
        public double[] Angle;
        public double[] Offset1X;
        public double[] Offset2X;
        public double[] Offset1Y;
        public double[] Offset2Y;
        public double[] Offset1Z;
        public double[] Offset2Z;
        public int[] CardinalPoint;

        public etabsAllFrames(cSapModel mySapModel, string csys)
        {
            this.mySapModel = mySapModel;
            this.NumberNames = 0;
            this.MyName = new string[0];
            this.PropName = new string[0];
            this.StoryName = new string[0];
            this.PointName1 = new string[0];
            this.PointName2 = new string[0];
            this.Point1X = new double[0];
            this.Point1Y = new double[0];
            this.Point1Z = new double[0];
            this.Point2X = new double[0];
            this.Point2Y = new double[0];
            this.Point2Z = new double[0];
            this.Angle = new double[0];
            this.Offset1X = new double[0];
            this.Offset2X = new double[0];
            this.Offset1Y = new double[0];
            this.Offset2Y = new double[0];
            this.Offset1Z = new double[0];
            this.Offset2Z = new double[0];
            this.CardinalPoint = new int[0];
            this.mySapModel.FrameObj.GetAllFrames(ref this.NumberNames, ref this.MyName, ref this.PropName, ref this.StoryName, ref this.PointName1, ref this.PointName2, ref this.Point1X, ref this.Point1Y, ref this.Point1Z, ref this.Point2X, ref this.Point2Y, ref this.Point2Z, ref this.Angle, ref this.Offset1X, ref this.Offset2X, ref this.Offset1Y, ref this.Offset2Y, ref this.Offset1Z, ref this.Offset2Z, ref this.CardinalPoint);
        }
    }
    //PropFrame
    public class etabsSectionAreaProperties
    {
        cSapModel mySapModel;
        public double Area;
        public double As2;
        public double As3;
        public double Torsion;
        public double I22;
        public double I33;
        public double S22;
        public double S33;
        public double Z22;
        public double Z33;
        public double R22;
        public double R33;

        public etabsSectionAreaProperties(cSapModel mySapModel, string sectionName)
        {
            this.mySapModel = mySapModel;
            this.Area = 0;
            this.As2 = 0;
            this.As3 = 0;
            this.Torsion = 0;
            this.I22 = 0;
            this.I33 = 0;
            this.S22 = 0;
            this.S33 = 0;
            this.Z22 = 0;
            this.Z33 = 0;
            this.R22 = 0;
            this.R33 = 0;
            this.mySapModel.PropFrame.GetSectProps(sectionName, ref this.Area, ref this.As2, ref this.As3, ref this.Torsion, ref this.I22, ref this.I33, ref this.S22, ref this.S33, ref this.Z22, ref this.Z33, ref this.R22, ref this.R33);
        }
    }
    public class etabsAllFrameSections
    {
        cSapModel mySapModel;
        public int NumberNames;
        public string[] MyName;
        public eFramePropType[] PropType;
        public double[] t3;
        public double[] t2;
        public double[] tf;
        public double[] tw;
        public double[] t2b;
        public double[] tfb;

        public etabsAllFrameSections(cSapModel mySapModel)
        {

            this.mySapModel = mySapModel;
            this.NumberNames = 0;
            this.MyName = new string[0];
            this.PropType = new eFramePropType[0];
            this.t3 = new double[0];
            this.t2 = new double[0];
            this.tf = new double[0];
            this.tw = new double[0];
            this.t2b = new double[0];
            this.tfb = new double[0];

            this.mySapModel.PropFrame.GetAllFrameProperties(ref this.NumberNames, ref this.MyName, ref this.PropType, ref this.t3, ref this.t2, ref this.tf, ref this.tw, ref this.t2b, ref this.tfb);
        }

    }
    public class etabsRectangleSection
    {
        cSapModel mySapModel;
        public string FileName;
        public string MatProp;
        public double T3; // depth
        public double T2; // width
        public int Color;
        public string Notes;
        public string GUID;

        public etabsRectangleSection(cSapModel mySapModel, string sectionName)
        {

            this.mySapModel = mySapModel;
            this.FileName = string.Empty;
            this.MatProp = string.Empty;
            this.T3 = 0;
            this.T2 = 0;
            this.Color = 0;
            this.Notes = string.Empty;
            this.GUID = string.Empty;

            this.mySapModel.PropFrame.GetRectangle(sectionName, ref this.FileName, ref this.MatProp, ref this.T3, ref this.T2, ref this.Color, ref this.Notes, ref this.GUID);
        }

    }
    public class etabsConcreteLSection
    {
        cSapModel mySapModel;
        public string FileName;
        public string MatProp;
        public double T3;
        public double T2;
        public double Tf;
        public double TwC;
        public double TwT;
        public bool MirrorAbout2;
        public bool MirrorAbout3;
        public int Color;
        public string Notes;
        public string GUID;

        public etabsConcreteLSection(cSapModel mySapModel, string sectionName)
        {

            this.mySapModel = mySapModel;
            this.FileName = string.Empty;
            this.MatProp = string.Empty;
            this.T3 = 0;
            this.T2 = 0;
            this.Tf = 0;
            this.TwC = 0;
            this.TwT = 0;
            this.MirrorAbout2 = false;
            this.MirrorAbout3 = false;
            this.Color = 0;
            this.Notes = string.Empty;
            this.GUID = string.Empty;

            this.mySapModel.PropFrame.GetConcreteL(sectionName, ref this.FileName, ref this.MatProp, ref this.T3, ref this.T2, ref this.Tf, ref this.TwC, ref this.TwT, ref this.MirrorAbout2, ref this.MirrorAbout3, ref this.Color, ref this.Notes, ref this.GUID);
        }

    }
    public class etabsISection
    {
        cSapModel mySapModel;
        public string FileName;
        public string MatProp;
        public double T3;
        public double T2;
        public double Tf;
        public double Tw;
        public double T2b;
        public double Tfb;
        public int Color;
        public string Notes;
        public string GUID;

        public etabsISection(cSapModel mySapModel, string sectionName)
        {

            this.mySapModel = mySapModel;
            this.FileName = string.Empty;
            this.MatProp = string.Empty;
            this.T3 = 0;
            this.T2 = 0;
            this.Tf = 0;
            this.Tw = 0;
            this.T2b = 0;
            this.Tfb = 0;
            this.Color = 0;
            this.Notes = string.Empty;
            this.GUID = string.Empty;

            this.mySapModel.PropFrame.GetISection(sectionName, ref this.FileName, ref this.MatProp, ref this.T3, ref this.T2, ref this.Tf, ref this.Tw, ref this.T2b, ref this.Tfb, ref this.Color, ref this.Notes, ref this.GUID);
        }

    }
    public class etabsDoubleChannelSection
    {
        cSapModel mySapModel;
        public string FileName;
        public string MatProp;
        public double T3;
        public double T2;
        public double Tf;
        public double Tw;
        public double Dis;
        public int Color;
        public string Notes;
        public string GUID;

        public etabsDoubleChannelSection(cSapModel mySapModel, string sectionName)
        {

            this.mySapModel = mySapModel;
            this.FileName = string.Empty;
            this.MatProp = string.Empty;
            this.T3 = 0;
            this.T2 = 0;
            this.Tf = 0;
            this.Tw = 0;
            this.Dis = 0;
            this.Color = 0;
            this.Notes = string.Empty;
            this.GUID = string.Empty;

            this.mySapModel.PropFrame.GetDblChannel(sectionName, ref this.FileName, ref this.MatProp, ref this.T3, ref this.T2, ref this.Tf, ref this.Tw, ref this.Dis, ref this.Color, ref this.Notes, ref this.GUID);
        }

    }
    public class etabsDoubleAngleSection
    {
        cSapModel mySapModel;
        public string FileName;
        public string MatProp;
        public double T3;
        public double T2;
        public double Tf;
        public double Tw;
        public double Dis;
        public int Color;
        public string Notes;
        public string GUID;

        public etabsDoubleAngleSection(cSapModel mySapModel, string sectionName)
        {

            this.mySapModel = mySapModel;
            this.FileName = string.Empty;
            this.MatProp = string.Empty;
            this.T3 = 0;
            this.T2 = 0;
            this.Tf = 0;
            this.Tw = 0;
            this.Dis = 0;
            this.Color = 0;
            this.Notes = string.Empty;
            this.GUID = string.Empty;

            this.mySapModel.PropFrame.GetDblAngle(sectionName, ref this.FileName, ref this.MatProp, ref this.T3, ref this.T2, ref this.Tf, ref this.Tw, ref this.Dis, ref this.Color, ref this.Notes, ref this.GUID);
        }

    }
    public class etabsSteelAngleSection
    {
        cSapModel mySapModel;
        public string FileName;
        public string MatProp;
        public double T3;
        public double T2;
        public double Tf;
        public double Tw;
        public double r;
        public bool MirrorAbout2;
        public bool MirrorAbout3;
        public int Color;
        public string Notes;
        public string GUID;

        public etabsSteelAngleSection(cSapModel mySapModel, string sectionName)
        {

            this.mySapModel = mySapModel;
            this.FileName = string.Empty;
            this.MatProp = string.Empty;
            this.T3 = 0;
            this.T2 = 0;
            this.Tf = 0;
            this.Tw = 0;
            this.r = 0;
            this.MirrorAbout2 = false;
            this.MirrorAbout3 = false;
            this.Color = 0;
            this.Notes = string.Empty;
            this.GUID = string.Empty;

            this.mySapModel.PropFrame.GetSteelAngle(sectionName, ref this.FileName, ref this.MatProp, ref this.T3, ref this.T2, ref this.Tf, ref this.Tw, ref this.r, ref this.MirrorAbout2, ref this.MirrorAbout3, ref this.Color, ref this.Notes, ref this.GUID);
        }

    }
    public class etabsCircleSection
    {
        cSapModel mySapModel;
        public string FileName;
        public string MatProp;
        public double T3;
        public int Color;
        public string Notes;
        public string GUID;

        public etabsCircleSection(cSapModel mySapModel, string sectionName)
        {

            this.mySapModel = mySapModel;
            this.FileName = string.Empty;
            this.MatProp = string.Empty;
            this.T3 = 0;
            this.Color = 0;
            this.Notes = string.Empty;
            this.GUID = string.Empty;

            this.mySapModel.PropFrame.GetCircle(sectionName, ref this.FileName, ref this.MatProp, ref this.T3, ref this.Color, ref this.Notes, ref this.GUID);
        }

    }
    public class etabsAngleSection
    {
        cSapModel mySapModel;
        public string FileName;
        public string MatProp;
        public double T3;
        public double T2;
        public double Tf;
        public double Tw;
        public int Color;
        public string Notes;
        public string GUID;

        public etabsAngleSection(cSapModel mySapModel, string sectionName)
        {

            this.mySapModel = mySapModel;
            this.FileName = string.Empty;
            this.MatProp = string.Empty;
            this.T3 = 0;
            this.T2 = 0;
            this.Tf = 0;
            this.Tw = 0;
            this.Color = 0;
            this.Notes = string.Empty;
            this.GUID = string.Empty;

            this.mySapModel.PropFrame.GetAngle(sectionName, ref this.FileName, ref this.MatProp, ref this.T3, ref this.T2, ref this.Tf, ref this.Tw, ref this.Color, ref this.Notes, ref this.GUID);
        }

    }
    public class etabsChannelSection
    {
        cSapModel mySapModel;
        public string FileName;
        public string MatProp;
        public double T3;
        public double T2;
        public double Tf;
        public double Tw;
        public int Color;
        public string Notes;
        public string GUID;

        public etabsChannelSection(cSapModel mySapModel, string sectionName)
        {

            this.mySapModel = mySapModel;
            this.FileName = string.Empty;
            this.MatProp = string.Empty;
            this.T3 = 0;
            this.T2 = 0;
            this.Tf = 0;
            this.Tw = 0;
            this.Color = 0;
            this.Notes = string.Empty;
            this.GUID = string.Empty;

            this.mySapModel.PropFrame.GetChannel(sectionName, ref this.FileName, ref this.MatProp, ref this.T3, ref this.T2, ref this.Tf, ref this.Tw, ref this.Color, ref this.Notes, ref this.GUID);
        }

    }
    public class etabsConcreteTeeSection
    {
        cSapModel mySapModel;
        public string FileName;
        public string MatProp;
        public double T3;
        public double T2;
        public double Tf;
        public double TwF;
        public double TwT;
        public bool MirrorAbout3;
        public int Color;
        public string Notes;
        public string GUID;

        public etabsConcreteTeeSection(cSapModel mySapModel, string sectionName)
        {

            this.mySapModel = mySapModel;
            this.FileName = string.Empty;
            this.MatProp = string.Empty;
            this.T3 = 0;
            this.T2 = 0;
            this.Tf = 0;
            this.TwF = 0;
            this.TwT = 0;
            this.MirrorAbout3 = false;
            this.Color = 0;
            this.Notes = string.Empty;
            this.GUID = string.Empty;

            this.mySapModel.PropFrame.GetConcreteTee(sectionName, ref this.FileName, ref this.MatProp, ref this.T3, ref this.T2, ref this.Tf, ref this.TwF, ref this.TwT, ref this.MirrorAbout3, ref this.Color, ref this.Notes, ref this.GUID);
        }

    }
    public class etabsSteelTeeSection
    {
        cSapModel mySapModel;
        public string FileName;
        public string MatProp;
        public double T3;
        public double T2;
        public double Tf;
        public double Tw;
        public double r;
        public bool MirrorAbout3;
        public int Color;
        public string Notes;
        public string GUID;

        public etabsSteelTeeSection(cSapModel mySapModel, string sectionName)
        {

            this.mySapModel = mySapModel;
            this.FileName = string.Empty;
            this.MatProp = string.Empty;
            this.T3 = 0;
            this.T2 = 0;
            this.Tf = 0;
            this.Tw = 0;
            this.r = 0;
            this.MirrorAbout3 = false;
            this.Color = 0;
            this.Notes = string.Empty;
            this.GUID = string.Empty;

            this.mySapModel.PropFrame.GetSteelTee(sectionName, ref this.FileName, ref this.MatProp, ref this.T3, ref this.T2, ref this.Tf, ref this.Tw, ref this.r, ref this.MirrorAbout3, ref this.Color, ref this.Notes, ref this.GUID);
        }

    }
    public class etabsTeeSection
    {
        cSapModel mySapModel;
        public string FileName;
        public string MatProp;
        public double T3;
        public double T2;
        public double Tf;
        public double Tw;
        public int Color;
        public string Notes;
        public string GUID;

        public etabsTeeSection(cSapModel mySapModel, string sectionName)
        {

            this.mySapModel = mySapModel;
            this.FileName = string.Empty;
            this.MatProp = string.Empty;
            this.T3 = 0;
            this.T2 = 0;
            this.Tf = 0;
            this.Tw = 0;
            this.Color = 0;
            this.Notes = string.Empty;
            this.GUID = string.Empty;

            this.mySapModel.PropFrame.GetTee(sectionName, ref this.FileName, ref this.MatProp, ref this.T3, ref this.T2, ref this.Tf, ref this.Tw, ref this.Color, ref this.Notes, ref this.GUID);
        }

    }
    public class etabsTubeSection
    {
        cSapModel mySapModel;
        public string FileName;
        public string MatProp;
        public double T3;
        public double T2;
        public double Tf;
        public double Tw;
        public int Color;
        public string Notes;
        public string GUID;

        public etabsTubeSection(cSapModel mySapModel, string sectionName)
        {

            this.mySapModel = mySapModel;
            this.FileName = string.Empty;
            this.MatProp = string.Empty;
            this.T3 = 0;
            this.T2 = 0;
            this.Tf = 0;
            this.Tw = 0;
            this.Color = 0;
            this.Notes = string.Empty;
            this.GUID = string.Empty;

            this.mySapModel.PropFrame.GetTube(sectionName, ref this.FileName, ref this.MatProp, ref this.T3, ref this.T2, ref this.Tf, ref this.Tw, ref this.Color, ref this.Notes, ref this.GUID);
        }

    }
    public class etabsPipeSection
    {
        cSapModel mySapModel;
        public string FileName;
        public string MatProp;
        public double T3;
        public double TW;
        public int Color;
        public string Notes;
        public string GUID;

        public etabsPipeSection(cSapModel mySapModel, string sectionName)
        {

            this.mySapModel = mySapModel;
            this.FileName = string.Empty;
            this.MatProp = string.Empty;
            this.T3 = 0;
            this.TW = 0;
            this.Color = 0;
            this.Notes = string.Empty;
            this.GUID = string.Empty;

            this.mySapModel.PropFrame.GetPipe(sectionName, ref this.FileName, ref this.MatProp, ref this.T3, ref this.TW, ref this.Color, ref this.Notes, ref this.GUID);
        }

    }
    public class etabsRodSection
    {
        cSapModel mySapModel;
        public string FileName;
        public string MatProp;
        public double T3;
        public int Color;
        public string Notes;
        public string GUID;

        public etabsRodSection(cSapModel mySapModel, string sectionName)
        {

            this.mySapModel = mySapModel;
            this.FileName = string.Empty;
            this.MatProp = string.Empty;
            this.T3 = 0;
            this.Color = 0;
            this.Notes = string.Empty;
            this.GUID = string.Empty;

            this.mySapModel.PropFrame.GetRod(sectionName, ref this.FileName, ref this.MatProp, ref this.T3, ref this.Color, ref this.Notes, ref this.GUID);
        }

    }
    public class etabsPlateSection
    {
        cSapModel mySapModel;
        public string FileName;
        public string MatProp;
        public double T3;
        public double T2;
        public int Color;
        public string Notes;
        public string GUID;

        public etabsPlateSection(cSapModel mySapModel, string sectionName)
        {

            this.mySapModel = mySapModel;
            this.FileName = string.Empty;
            this.MatProp = string.Empty;
            this.T3 = 0;
            this.T2 = 0;
            this.Color = 0;
            this.Notes = string.Empty;
            this.GUID = string.Empty;

            this.mySapModel.PropFrame.GetPlate(sectionName, ref this.FileName, ref this.MatProp, ref this.T3, ref this.T2, ref this.Color, ref this.Notes, ref this.GUID);
        }

    }
    public class etabsGetCoverPlatedI
    {
        cSapModel mySapModel;
        public string SectName;
        public double FyTopFlange;
        public double FyWeb;
        public double FyBotFlange;
        public double Tc;
        public double Bc;
        public string MatPropTop;
        public double Tcb;
        public double Bcb;
        public string MatPropBot;
        public int Color;
        public string Notes;
        public string GUID;
        public etabsGetCoverPlatedI(cSapModel mySapModel, string sectionName)
        {
            this.mySapModel = mySapModel;
            this.SectName = string.Empty;
            this.FyTopFlange = 0;
            this.FyWeb = 0;
            this.FyBotFlange = 0;
            this.Tc = 0;
            this.Bc = 0;
            this.MatPropTop = string.Empty;
            this.Tcb = 0;
            this.Bcb = 0;
            this.MatPropBot = string.Empty;
            this.Color = 0;
            this.Notes = string.Empty;
            this.GUID = string.Empty;
            this.mySapModel.PropFrame.GetCoverPlatedI(sectionName, ref this.SectName, ref this.FyTopFlange, ref this.FyWeb, ref this.FyBotFlange, ref this.Tc, ref this.Bc, ref this.MatPropTop, ref this.Tcb, ref this.Bcb, ref this.MatPropBot, ref this.Color, ref this.Notes, ref this.GUID);
        }
    }
    public class etabsFramePropertiesNames
    {
        cSapModel mySapModel;
        public int NumberNames;
        public string[] MyNames;
        public etabsFramePropertiesNames(cSapModel mySapModel)
        {
            this.mySapModel = mySapModel;
            this.NumberNames = 0;
            this.MyNames = new string[0];
            this.mySapModel.PropFrame.GetNameList(ref this.NumberNames, ref this.MyNames);
        }
    }
    public class etabsSectionType
    {
        cSapModel mySapModel;
        public eFramePropType propType;
        public etabsSectionType(cSapModel mySapModel, string sectionName)
        {
            this.mySapModel = mySapModel;
            this.propType = eFramePropType.Rectangular;
            this.mySapModel.PropFrame.GetTypeOAPI(sectionName, ref this.propType);
        }
    }
    public class etabsNonPrismaticSection
    {
        cSapModel mySapModel;
        public int NumberItems;
        public string[] StartSec;
        public string[] EndSec;
        public double[] MyLength;
        public int[] MyType;
        public int[] EI33;
        public int[] EI22;
        public int Color;
        public string Notes;
        public string GUID;
        public etabsNonPrismaticSection(cSapModel mySapModel, string sectionName)
        {
            this.mySapModel = mySapModel;
            this.NumberItems = 0;
            this.StartSec = new string[0];
            this.EndSec = new string[0];
            this.MyLength = new double[0];
            this.MyType = new int[0];
            this.EI33 = new int[0];
            this.EI22 = new int[0];
            this.Color = 0;
            this.Notes = string.Empty;
            this.GUID = string.Empty;
            this.mySapModel.PropFrame.GetNonPrismatic(sectionName, ref this.NumberItems, ref this.StartSec, ref this.EndSec, ref this.MyLength, ref this.MyType, ref this.EI33, ref this.EI22, ref this.Color, ref this.Notes, ref this.GUID);
        }
    }
    public class etabsFrameRebarType
    {
        // This is 0, 1 or 2, indicating the rebar design type.
        //  0 - None
        //  1 -	Column
        //  2 -	Beam
        cSapModel mySapModel;
        public int MyType;
        public etabsFrameRebarType(cSapModel mySapModel, string sectionName)
        {
            this.mySapModel = mySapModel;
            this.MyType = 0;
            //This function applies only to the following section property types. Calling this function for any other type of frame section property returns an error.
            //Concrete Tee
            //Concrete Angle
            //Concrete Rectangle
            //Concrete Circle
            this.mySapModel.PropFrame.GetTypeRebar(sectionName, ref MyType);
        }
    }
    public class etabsRebarColumn
    {
        cSapModel mySapModel;
        public string MatPropLong;
        public string MatPropConfine;
        public int Pattern;
        public int ConfineType;
        public double Cover;
        public int NumberCBars;
        public int NumberR3Bars;
        public int NumberR2Bars;
        public string RebarSize;
        public string TieSize;
        public double TieSpacingLongit;
        public int Number2DirTieBars;
        public int Number3DirTieBars;
        public bool ToBeDesigned;
        public etabsRebarColumn(cSapModel mySapModel, string sectionName)
        {
            this.mySapModel = mySapModel;
            this.MatPropLong = string.Empty;
            this.MatPropConfine = string.Empty;
            this.Pattern = 0;
            this.ConfineType = 0;
            this.Cover = 0;
            this.NumberCBars = 0;
            this.NumberR3Bars = 0;
            this.NumberR2Bars = 0;
            this.RebarSize = string.Empty;
            this.TieSize = string.Empty;
            this.TieSpacingLongit = 0;
            this.Number2DirTieBars = 0;
            this.Number3DirTieBars = 0;
            this.ToBeDesigned = false;
            this.mySapModel.PropFrame.GetRebarColumn(sectionName, ref this.MatPropLong, ref this.MatPropConfine, ref this.Pattern, ref this.ConfineType, ref this.Cover, ref this.NumberCBars, ref this.NumberR3Bars, ref this.NumberR2Bars, ref this.RebarSize, ref this.TieSize, ref this.TieSpacingLongit, ref this.Number2DirTieBars, ref this.Number3DirTieBars, ref this.ToBeDesigned);
        }
    }
    public class etabsRebarBeam
    {
        cSapModel mySapModel;
        public string MatPropLong;
        public string MatPropConfine;
        public double CoverTop;
        public double CoverBot;
        public double TopLeftArea;
        public double TopRightArea;
        public double BotLeftArea;
        public double BotRightArea;
        public etabsRebarBeam(cSapModel mySapModel, string sectionName)
        {
            this.mySapModel = mySapModel;
            this.MatPropLong = string.Empty;
            this.MatPropConfine = string.Empty;
            this.CoverTop = 0;
            this.CoverBot = 0;
            this.TopLeftArea = 0;
            this.TopRightArea = 0;
            this.BotLeftArea = 0;
            this.BotRightArea = 0;
            this.mySapModel.PropFrame.GetRebarBeam(sectionName, ref this.MatPropLong, ref this.MatPropConfine, ref this.CoverTop, ref this.CoverBot, ref this.TopLeftArea, ref this.TopRightArea, ref this.BotLeftArea, ref this.BotRightArea);
        }
    }

    // SDSection

    public class etabsSDSection
    {
        cSapModel mySapModel;
        public string MatProp;
        public int NumberItems;
        public string[] ShapeName;
        public int[] MyType;
        public int DesignType;
        public int Color;
        public string Notes;
        public string GUID;
        public etabsSDSection(cSapModel mySapModel, string sectionName)
        {

            this.mySapModel = mySapModel;
            this.MatProp = string.Empty;
            this.NumberItems = 0;
            this.ShapeName = new string[0];
            this.MyType = new int[0];

            //1	I-Section
            //2	Channel
            //3	Tee
            //4	Angle
            //5	Double Angle
            //6	Box
            //7	Pipe
            //8	Plate
            //61	Concrete Tee
            //62	Concrete L
            //63	Concrete Box
            //64	Concrete Pipe
            //65	Concrete Cross
            //101	Solid Rectangle
            //102	Solid Circle
            //103	Solid Segment
            //104	Solid Sector
            //201	Polygon
            //301	Reinforcing Single
            //302	Reinforcing Line
            //303	Reinforcing Rectangle
            //304	Reinforcing Circle
            //401	Reference Line
            //402	Reference Circle

            this.DesignType = 0;
            this.Color = 0;
            this.Notes = string.Empty;
            this.GUID = string.Empty;
            this.mySapModel.PropFrame.GetSDSection(sectionName, ref this.MatProp, ref this.NumberItems, ref this.ShapeName, ref this.MyType, ref this.DesignType, ref this.Color, ref this.Notes, ref this.GUID);
        }
    }
    public class SDSectionAngleSection
    {
        cSapModel mySapModel;
        public string MatProp;
        public string PropName;
        public int Color;
        public double XCenter;
        public double YCenter;
        public double H;
        public double Bf;
        public double Tf;
        public double Tw;
        public double Rotation;
        public SDSectionAngleSection(cSapModel mySapModel, string sectionName, string shapeName)
        {
            this.mySapModel = mySapModel;
            this.MatProp = string.Empty;
            this.PropName = string.Empty;
            this.Color = 0;
            this.XCenter = 0;
            this.YCenter = 0;
            this.H = 0;
            this.Bf = 0;
            this.Tf = 0;
            this.Tw = 0;
            this.Rotation = 0;
            this.mySapModel.PropFrame.SDShape.GetAngle(sectionName, shapeName, ref this.MatProp, ref this.PropName, ref this.Color, ref this.XCenter, ref this.YCenter, ref this.H, ref this.Bf, ref this.Tf, ref this.Tw, ref this.Rotation);
        }
    }
    public class SDSectionConcreteLSection
    {
        cSapModel mySapModel;
        public string MatProp;
        public string PropName;
        public int Color;
        public double XCenter;
        public double YCenter;
        public double H;
        public double Bf;
        public double Tf;
        public double Tw;
        public double Rotation;
        public bool MirrorAbout2;
        public bool MirrorAbout3;
        public SDSectionConcreteLSection(cSapModel mySapModel, string sectionName, string shapeName)
        {
            this.mySapModel = mySapModel;
            this.MatProp = string.Empty;
            this.PropName = string.Empty;
            this.Color = 0;
            this.XCenter = 0;
            this.YCenter = 0;
            this.H = 0;
            this.Bf = 0;
            this.Tf = 0;
            this.Tw = 0;
            this.Rotation = 0;
            this.MirrorAbout2 = false;
            this.MirrorAbout3 = false;
            this.mySapModel.PropFrame.SDShape.GetConcreteL(sectionName, shapeName, ref this.MatProp, ref this.PropName, ref this.Color, ref this.XCenter, ref this.YCenter, ref this.H, ref this.Bf, ref this.Tf, ref this.Tw, ref this.Rotation, ref this.MirrorAbout2, ref this.MirrorAbout3);
        }
    }
    public class SDSectionConcreteTeeSection
    {
        cSapModel mySapModel;
        public string MatProp;
        public string PropName;
        public int Color;
        public double XCenter;
        public double YCenter;
        public double H;
        public double Bf;
        public double Tf;
        public double Tw;
        public double Rotation;
        public bool MirrorAbout3;
        public SDSectionConcreteTeeSection(cSapModel mySapModel, string sectionName, string shapeName)
        {
            this.mySapModel = mySapModel;
            this.MatProp = string.Empty;
            this.PropName = string.Empty;
            this.Color = 0;
            this.XCenter = 0;
            this.YCenter = 0;
            this.H = 0;
            this.Bf = 0;
            this.Tf = 0;
            this.Tw = 0;
            this.Rotation = 0;
            this.MirrorAbout3 = false;
            this.mySapModel.PropFrame.SDShape.GetConcreteTee(sectionName, shapeName, ref this.MatProp, ref this.PropName, ref this.Color, ref this.XCenter, ref this.YCenter, ref this.H, ref this.Bf, ref this.Tf, ref this.Tw, ref this.Rotation, ref this.MirrorAbout3);
        }
    }
    public class SDSectionISection
    {
        cSapModel mySapModel;
        public string MatProp;
        public string PropName;
        public int Color;
        public double XCenter;
        public double YCenter;
        public double H;
        public double Bf;
        public double Tf;
        public double Tw;
        public double Bfb;
        public double Tfb;
        public double Rotation;

        public SDSectionISection(cSapModel mySapModel, string sectionName, string shapeName)
        {
            this.mySapModel = mySapModel;
            this.MatProp = string.Empty;
            this.PropName = string.Empty;
            this.Color = 0;
            this.XCenter = 0;
            this.YCenter = 0;
            this.H = 0;
            this.Bf = 0;
            this.Tf = 0;
            this.Tw = 0;
            this.Bfb = 0;
            this.Tfb = 0;
            this.Rotation = 0;
            this.mySapModel.PropFrame.SDShape.GetISection(sectionName, shapeName, ref this.MatProp, ref this.PropName, ref this.Color, ref this.XCenter, ref this.YCenter, ref this.H, ref this.Bf, ref this.Tf, ref this.Tw, ref this.Bfb, ref this.Tfb, ref this.Rotation);
        }
    }
    public class SDSectionTeeSection
    {
        cSapModel mySapModel;
        public string MatProp;
        public string PropName;
        public int Color;
        public double XCenter;
        public double YCenter;
        public double H;
        public double Bf;
        public double Tf;
        public double Tw;
        public double Rotation;

        public SDSectionTeeSection(cSapModel mySapModel, string sectionName, string shapeName)
        {
            this.mySapModel = mySapModel;
            this.MatProp = string.Empty;
            this.PropName = string.Empty;
            this.Color = 0;
            this.XCenter = 0;
            this.YCenter = 0;
            this.H = 0;
            this.Bf = 0;
            this.Tf = 0;
            this.Tw = 0;
            this.Rotation = 0;
            this.mySapModel.PropFrame.SDShape.GetTee(sectionName, shapeName, ref this.MatProp, ref this.PropName, ref this.Color, ref this.XCenter, ref this.YCenter, ref this.H, ref this.Bf, ref this.Tf, ref this.Tw, ref this.Rotation);
        }
    }
    public class SDSectionSolidRectangleSection
    {
        cSapModel mySapModel;
        public string MatProp;
        public string SSOverwrite;
        public int Color;
        public double XCenter;
        public double YCenter;
        public double H;
        public double W;
        public double Rotation;
        public bool Reinf;
        public string MatRebar;

        public SDSectionSolidRectangleSection(cSapModel mySapModel, string sectionName, string shapeName)
        {
            this.mySapModel = mySapModel;
            this.MatProp = string.Empty;
            this.SSOverwrite = string.Empty;
            this.Color = 0;
            this.XCenter = 0;
            this.YCenter = 0;
            this.H = 0;
            this.W = 0;
            this.Rotation = 0;
            this.Reinf = false;
            this.MatRebar = string.Empty;
            this.mySapModel.PropFrame.SDShape.GetSolidRect(sectionName, shapeName, ref this.MatProp, ref this.SSOverwrite, ref this.Color, ref this.XCenter, ref this.YCenter, ref this.H, ref this.W, ref this.Rotation, ref this.Reinf, ref this.MatRebar);
        }
    }
    public class SDSectionSolidCircleSection
    {
        cSapModel mySapModel;
        public string MatProp;
        public string SSOverwrite;
        public int Color;
        public double XCenter;
        public double YCenter;
        public double Diameter;
        public bool Reinf;
        public int NumberBars;
        public double Rotation;
        public double Cover;
        public string RebarSize;
        public string MatRebar;

        public SDSectionSolidCircleSection(cSapModel mySapModel, string sectionName, string shapeName)
        {
            this.mySapModel = mySapModel;
            this.MatProp = string.Empty;
            this.SSOverwrite = string.Empty;
            this.Color = 0;
            this.XCenter = 0;
            this.YCenter = 0;
            this.Diameter = 0;
            this.Reinf = false;
            this.NumberBars = 0;
            this.Rotation = 0;
            this.Cover = 0;
            this.RebarSize = string.Empty;
            this.MatRebar = string.Empty;
            this.mySapModel.PropFrame.SDShape.GetSolidCircle(sectionName, shapeName, ref this.MatProp, ref this.SSOverwrite, ref this.Color, ref this.XCenter, ref this.YCenter, ref this.Diameter, ref this.Reinf, ref this.NumberBars, ref this.Rotation, ref this.Cover, ref this.RebarSize, ref this.MatRebar);
        }
    }
    public class SDSectionReinfSingle
    {
        cSapModel mySapModel;
        public double XCenter;
        public double YCenter;
        public string RebarSize;
        public string MatRebar;

        public SDSectionReinfSingle(cSapModel mySapModel, string sectionName, string shapeName)
        {
            this.mySapModel = mySapModel;
            this.XCenter = 0;
            this.YCenter = 0;
            this.RebarSize = string.Empty;
            this.MatRebar = string.Empty;
            this.mySapModel.PropFrame.SDShape.GetReinfSingle(sectionName, shapeName, ref this.XCenter, ref this.YCenter, ref this.RebarSize, ref this.MatRebar);
        }
    }
    public class SDSectionReinfRectangle
    {
        cSapModel mySapModel;
        public double XCenter;
        public double YCenter;
        public double H;
        public double W;
        public double Rotation;
        public string MatRebar;

        public SDSectionReinfRectangle(cSapModel mySapModel, string sectionName, string shapeName)
        {
            this.mySapModel = mySapModel;
            this.XCenter = 0;
            this.YCenter = 0;
            this.H = 0;
            this.W = 0;
            this.Rotation = 0;
            this.MatRebar = string.Empty;
            this.mySapModel.PropFrame.SDShape.GetReinfRectangular(sectionName, shapeName, ref this.XCenter, ref this.YCenter, ref this.H, ref this.W, ref this.Rotation, ref this.MatRebar);
        }
    }
    public class SDSectionReinfCircle
    {
        cSapModel mySapModel;
        public double XCenter;
        public double YCenter;
        public double Diameter;
        public int NumBars;
        public double Rotation;
        public string RebarSize;
        public string MatRebar;

        public SDSectionReinfCircle(cSapModel mySapModel, string sectionName, string shapeName)
        {
            this.mySapModel = mySapModel;
            this.XCenter = 0;
            this.YCenter = 0;
            this.Diameter = 0;
            this.NumBars = 0;
            this.Rotation = 0;
            this.RebarSize = string.Empty;
            this.MatRebar = string.Empty;
            this.mySapModel.PropFrame.SDShape.GetReinfCircle(sectionName, shapeName, ref this.XCenter, ref this.YCenter, ref this.Diameter, ref this.NumBars, ref this.Rotation, ref this.RebarSize, ref this.MatRebar);
        }
    }
    public class SDSectionReinfCorner
    {
        cSapModel mySapModel;
        public int NumberItems;
        public int[] PointNum;
        public string[] RebarSize;

        public SDSectionReinfCorner(cSapModel mySapModel, string sectionName, string shapeName)
        {
            this.mySapModel = mySapModel;
            this.NumberItems = 0;
            this.PointNum = new int[0];
            this.RebarSize = new string[0];
            this.mySapModel.PropFrame.SDShape.GetReinfCorner(sectionName, shapeName, ref this.NumberItems, ref this.PointNum, ref this.RebarSize);
        }
    }
    public class SDSectionReinfEdge
    {
        cSapModel mySapModel;
        public int NumberItems;
        public int[] EdgeNum;
        public string[] RebarSize;
        public double[] Spacing;
        public double[] Cover;

        public SDSectionReinfEdge(cSapModel mySapModel, string sectionName, string shapeName)
        {
            this.mySapModel = mySapModel;
            this.NumberItems = 0;
            this.EdgeNum = new int[0];
            this.RebarSize = new string[0];
            this.Spacing = new double[0];
            this.Cover = new double[0];
            this.mySapModel.PropFrame.SDShape.GetReinfEdge(sectionName, shapeName, ref this.NumberItems, ref this.EdgeNum, ref this.RebarSize, ref this.Spacing, ref this.Cover);
        }
    }
    public class SDSectionReinfLine
    {
        cSapModel mySapModel;
        public double X1;
        public double Y1;
        public double X2;
        public double Y2;
        public double Spacing;
        public string RebarSize;
        public bool EndBars;
        public string MatRebar;

        public SDSectionReinfLine(cSapModel mySapModel, string sectionName, string shapeName)
        {
            this.mySapModel = mySapModel;
            this.X1 = 0;
            this.Y1 = 0;
            this.X2 = 0;
            this.Y2 = 0;
            this.Spacing = 0;
            this.RebarSize = string.Empty;
            this.EndBars = false;
            this.MatRebar = string.Empty;
            this.mySapModel.PropFrame.SDShape.GetReinfLine(sectionName, shapeName, ref this.X1, ref this.Y1, ref this.X2, ref this.Y2, ref this.Spacing, ref this.RebarSize, ref this.EndBars, ref this.MatRebar);
        }
    }
    // Units
    public class etabsDataBaseUnits
    {
        cSapModel mySapModel;
        public eForce forceUnits;
        public eLength lengthUnits;
        public eTemperature temperatureUnits;
        public etabsDataBaseUnits(cSapModel mySapModel)
        {
            this.mySapModel = mySapModel;
            this.forceUnits = eForce.N;
            this.lengthUnits = eLength.mm;
            this.temperatureUnits = eTemperature.C;
            this.mySapModel.GetDatabaseUnits_2(ref this.forceUnits, ref this.lengthUnits, ref this.temperatureUnits);
        }
    }
    public class etabsPresentUnits
    {
        cSapModel mySapModel;
        public eForce forceUnits;
        public eLength lengthUnits;
        public eTemperature temperatureUnits;
        public etabsPresentUnits(cSapModel mySapModel)
        {
            this.mySapModel = mySapModel;
            this.forceUnits = eForce.N;
            this.lengthUnits = eLength.mm;
            this.temperatureUnits = eTemperature.C;
            this.mySapModel.GetPresentUnits_2(ref this.forceUnits, ref this.lengthUnits, ref this.temperatureUnits);
        }
    }
    public static class etabsSetUnits
    {
        public static void etabsSetPresentUnits(cSapModel mySapModel, eForce forceUnit, eLength lengthUnit, eTemperature temperatureUnit)
        {
            mySapModel.SetPresentUnits_2(forceUnit, lengthUnit, temperatureUnit);
        }
    }
    // RebarsProps
    public class etabsRebarData
    {
        cSapModel mySapModel;
        public double Area;
        public double Diameter;

        public etabsRebarData(cSapModel mySapModel, string RebarName)
        {
            this.mySapModel = mySapModel;
            this.Area = 0;
            this.Diameter = 0;
            this.mySapModel.PropRebar.GetRebarProps(RebarName, ref this.Area, ref this.Diameter);
        }
    }
    public class etabsAllRebarData
    {
        cSapModel mySapModel;
        public int NumberNames;
        public string[] MyName;
        public double[] Areas;
        public double[] Diameters;
        public string[] MyGUID;

        public etabsAllRebarData(cSapModel mySapModel)
        {
            this.mySapModel = mySapModel;
            this.NumberNames = 0;
            this.MyName = new string[0];
            this.Areas = new double[0];
            this.Diameters = new double[0];
            this.MyGUID = new string[0];
            this.mySapModel.PropRebar.GetNameListWithData(ref this.NumberNames, ref this.MyName, ref this.Areas, ref this.Diameters, ref this.MyGUID);
        }
    }
    // sectionDesign
    public class etabsReinforcedCorners
    {
        cSapModel mySapModel;
        public int NumberItems;
        public int[] PointNum;
        public string[] RebarSize;

    }

}
