using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeadeCEtabs
{
    public class Vector3
    {
        public double x, y, z;
        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vector3(dynamic o)
        {
            this.x = o.x.Value;
            this.y = o.y.Value;
            this.z = o.z.Value;
        }
    }


    public class model
    {
        public List<CeadeCObject> objects;
        public model()
        {
            this.objects = new List<CeadeCObject>();
        }
        public model(dynamic o)
        {
            this.objects = new List<CeadeCObject>();
            for (int i = 0; i < o.objects.Count; i++)
            {
                dynamic ob = o.objects[i];
                CeadeCObject castedOb;
                switch (ob.type.Value)
                {
                    case "points":
                        castedOb = new CeadeCPoints(ob);
                        break;
                    case "lines":
                        castedOb = new CeadeCLines(ob);
                        break;
                    case "polylines":
                        castedOb = new CeadeCPolylines(ob);
                        break;
                    case "Gpolylines":
                        castedOb = new CeadeCGpolylines(ob);
                        break;
                    case "rectangles":
                        castedOb = new CeadeCRectangles(ob);
                        break;
                    case "circles":
                        castedOb = new CeadeCCircles(ob);
                        break;
                    case "arcs":
                        castedOb = new CeadeCArcs(ob);
                        break;
                    case "shapes":
                        castedOb = new CeadeCShapes(ob);
                        break;
                    default:
                        castedOb = null;
                        break;
                }
                if (castedOb != null)
                {
                    this.objects.Add(castedOb);
                }
            }
        }
    }


    public class CeadeCObject
    {
        public string shapeChildType;
        public string voidIndex;
        public CeadeCRebarsObject rebars;
        public string Name = "";
        public List<CeadeCObject> children = new List<CeadeCObject>();
    }


    public class CeadeCPoints : CeadeCObject
    {
        public string type = "points";
        public List<Vector3> vertices;

        public CeadeCPoints(List<Vector3> vertices)
        {
            this.vertices = vertices;
        }
        public CeadeCPoints(dynamic ob)
        {
            List<Vector3> verts = new List<Vector3>();
            for (int i = 0; i < ob.vertices.Count; i++)
            {
                verts.Add(new Vector3(ob.vertices[i]));
            }
            this.vertices = verts;
            if (ob.Name != null)
            {
                this.Name = ob.Name.Value;
            }
            if (ob.shapeChildType != null)
            {
                this.shapeChildType = ob.shapeChildType.Value;
            }
            if (ob.rebars != null)
            {
                this.rebars = new CeadeCPointRebar(ob.rebars);
            }
        }
    }

    public class CeadeCLines : CeadeCObject
    {
        public string type = "lines";
        public List<Vector3> vertices;

        public CeadeCLines(List<Vector3> vertices)
        {
            this.vertices = vertices;
        }
        public CeadeCLines(dynamic ob)
        {
            List<Vector3> verts = new List<Vector3>();
            for (int i = 0; i < ob.vertices.Count; i++)
            {
                verts.Add(new Vector3(ob.vertices[i]));
            }
            this.vertices = verts;
            if (ob.Name != null)
            {
                this.Name = ob.Name.Value;
            }
            if (ob.shapeChildType != null)
            {
                this.shapeChildType = ob.shapeChildType.Value;
            }
            if (ob.rebars != null)
            {
                this.rebars = new CeadeCLineRebar(ob.rebars);
            }
        }
    }


    public class CeadeCPolylines : CeadeCObject
    {
        public string type = "polylines";
        public List<Vector3> vertices;


        public CeadeCPolylines(List<Vector3> vertices)
        {
            this.vertices = vertices;
        }
        public CeadeCPolylines(dynamic ob)
        {
            List<Vector3> verts = new List<Vector3>();
            for (int i = 0; i < ob.vertices.Count; i++)
            {
                verts.Add(new Vector3(ob.vertices[i]));
            }
            this.vertices = verts;
            if (ob.Name != null)
            {
                this.Name = ob.Name.Value;
            }
            if (ob.shapeChildType != null)
            {
                this.shapeChildType = ob.shapeChildType.Value;
            }
            if (ob.rebars != null)
            {
                this.rebars = new CeadeCPolylineRebar(ob.rebars);
            }
        }
    }


    public class CeadeCRectangles : CeadeCObject
    {
        public string type = "rectangles";
        public List<Vector3> vertices;


        public CeadeCRectangles(List<Vector3> vertices)
        {
            this.vertices = vertices;
        }
        public CeadeCRectangles(dynamic ob)
        {
            List<Vector3> verts = new List<Vector3>();
            for (int i = 0; i < ob.vertices.Count; i++)
            {
                verts.Add(new Vector3(ob.vertices[i]));
            }
            this.vertices = verts;
            if (ob.Name != null)
            {
                this.Name = ob.Name.Value;
            }
            if (ob.shapeChildType != null)
            {
                this.shapeChildType = ob.shapeChildType.Value;
            }
            if (ob.rebars != null)
            {
                this.rebars = new CeadeCRectangleRebar(ob.rebars);
            }

        }
    }


    public class CeadeCArcs : CeadeCObject
    {
        public string type = "arcs";
        public Vector3 cPoint;
        public Vector3 sPoint;
        public Vector3 ePoint;
        public bool cw;

        public CeadeCArcs(Vector3 cPoint, Vector3 sPoint, Vector3 ePoint, bool cw)
        {
            this.cPoint = cPoint;
            this.sPoint = sPoint;
            this.ePoint = ePoint;
            this.cw = cw;
        }
        public CeadeCArcs(dynamic ob)
        {
            this.cPoint = ob.cPoint.Value;
            this.sPoint = ob.sPoint.Value;
            this.ePoint = ob.ePoint.Value;
            this.cw = ob.cw.Value;
            if (ob.Name != null)
            {
                this.Name = ob.Name.Value;
            }
            if (ob.shapeChildType != null)
            {
                this.shapeChildType = ob.shapeChildType.Value;
            }
            if (ob.rebars != null)
            {
                this.rebars = new CeadeCArcRebar(ob.rebars);
            }
        }
    }


    public class CeadeCCircles : CeadeCObject
    {
        public string type = "circles";
        public Vector3 cPoint;
        public float radius;

        public CeadeCCircles(Vector3 cPoint, float radius)
        {
            this.cPoint = cPoint;
            this.radius = radius;
        }
        public CeadeCCircles(dynamic ob)
        {
            this.cPoint = ob.cPoint.Value;
            this.radius = ob.radius.Value;
            if (ob.Name != null)
            {
                this.Name = ob.Name.Value;
            }
            if (ob.shapeChildType != null)
            {
                this.shapeChildType = ob.shapeChildType.Value;
            }
            if (ob.rebars != null)
            {
                this.rebars = new CeadeCCircleRebar(ob.rebars);
            }
        }
    }


    public class CeadeCGpolylines : CeadeCObject
    {
        public string type = "Gpolylines";


        public CeadeCGpolylines(List<CeadeCObject> children)
        {
            this.children = children;
        }
        public CeadeCGpolylines(dynamic o)
        {
            this.children = new List<CeadeCObject>();
            for (int i = 0; i < o.children.Count; i++)
            {
                dynamic ob = o.children[i];
                CeadeCObject castedOb;
                switch (ob.type.Value)
                {
                    case "points":
                        castedOb = new CeadeCPoints(ob);
                        break;
                    case "lines":
                        castedOb = new CeadeCLines(ob);
                        break;
                    case "polylines":
                        castedOb = new CeadeCPolylines(ob);
                        break;
                    case "rectangles":
                        castedOb = new CeadeCRectangles(ob);
                        break;
                    case "circles":
                        castedOb = new CeadeCCircles(ob);
                        break;
                    case "arcs":
                        castedOb = new CeadeCArcs(ob);
                        break;
                    default:
                        castedOb = null;
                        break;
                }
                if (castedOb != null)
                {
                    this.children.Add(castedOb);
                }
            }
            if (o.Name != null)
            {
                this.Name = o.Name.Value;
            }
            if (o.shapeChildType != null)
            {
                this.shapeChildType = o.shapeChildType.Value;
            }
        }
    }



    public class CeadeCShapes : CeadeCObject
    {
        public string type = "shapes";
        public List<ShapeAutoDesign> AutoDesign;
        public CeadeCShapes(List<CeadeCObject> children)
        {
            this.children = children;
            AutoDesign = new List<ShapeAutoDesign>();
        }
        public CeadeCShapes(dynamic o)
        {
            this.children = new List<CeadeCObject>();
            for (int i = 0; i < o.children.Count; i++)
            {
                dynamic ob = o.children[i];
                CeadeCObject castedOb;
                switch (ob.type.Value)
                {
                    case "points":
                        castedOb = new CeadeCPoints(ob);
                        break;
                    case "lines":
                        castedOb = new CeadeCLines(ob);
                        break;
                    case "polylines":
                        castedOb = new CeadeCPolylines(ob);
                        break;
                    case "Gpolylines":
                        castedOb = new CeadeCGpolylines(ob);
                        break;
                    case "rectangles":
                        castedOb = new CeadeCRectangles(ob);
                        break;
                    case "circles":
                        castedOb = new CeadeCCircles(ob);
                        break;
                    case "arcs":
                        castedOb = new CeadeCArcs(ob);
                        break;
                    default:
                        castedOb = null;
                        break;
                }
                if (castedOb != null)
                {
                    this.children.Add(castedOb);
                }
            }
            this.Name = o.Name.Value;
            AutoDesign = new List<ShapeAutoDesign>();

        }
    }

    public class ShapeAutoDesign
    {
        public string shapeName;
        public double P;
        public string PUnit;
        public double Mx;
        public string MxUnit;
        public double My;
        public string MyUnit;
        public ShapeAutoDesign(string shapeName ,double P, string PUnit, double Mx, string MxUnit, double My, string MyUnit)
        {
            this.shapeName = shapeName;
            this.P = P;
            this.PUnit = PUnit;
            this.Mx = Mx;
            this.MxUnit = MxUnit;
            this.My = My;
            this.MyUnit = MyUnit;
        }
    }

    public class Designing
    {
        public string userEtabsPointer;
        public List<ShapeAutoDesign> autoDesginList;
        public Designing(string userEtabsPointer)
        {
            this.userEtabsPointer = userEtabsPointer;
            this.autoDesginList = new List<ShapeAutoDesign>();
        }
    }


    /// <summary>
    /// //////////////////////////////////////////////////////Rebars//////////////////////////////////////////////////////
    /// </summary>

    public class CeadeCRebarsObject
    {
        public float defaultRebarDiameter;
        public string defaultRebarDiameterUnit;
        public float defaultRebarFy;
        public string defaultRebarFyUnit;
        public List<CeadeCSingleRebar> singleRebars = new List<CeadeCSingleRebar>();
    }


    public class CeadeCPointRebar : CeadeCRebarsObject
    {
        public string type = "PointRebar";

        public CeadeCPointRebar(float defaultRebarDiameter, string defaultRebarDiameterUnit, float defaultRebarFy, string defaultRebarFyUnit)
        {
            this.defaultRebarDiameter = defaultRebarDiameter;
            this.defaultRebarDiameterUnit = defaultRebarDiameterUnit;
            this.defaultRebarFy = defaultRebarFy;
            this.defaultRebarFyUnit = defaultRebarFyUnit;
        }
        public CeadeCPointRebar(dynamic ob)
        {
            this.defaultRebarDiameter = (float)ob.defaultRebarDiameter.Value;
            this.defaultRebarDiameterUnit = ob.defaultRebarDiameterUnit.Value;
            this.defaultRebarFy = (float)ob.defaultRebarFy.Value;
            this.defaultRebarFyUnit = ob.defaultRebarFyUnit.Value;
            if (ob.singleRebars != null)
            {
                for (int i = 0; i < ob.singleRebars.Count; i++)
                {
                    this.singleRebars.Add(new CeadeCSingleRebar(ob.singleRebars[i]));
                }
            }
        }
    }

    public class CeadeCLineRebar : CeadeCRebarsObject
    {
        public string type = "LineRebar";
        public float spacing;
        public bool start;
        public bool end;

        public CeadeCLineRebar(float defaultRebarDiameter, string defaultRebarDiameterUnit, float defaultRebarFy, string defaultRebarFyUnit, float spacing, bool start, bool end)
        {
            this.defaultRebarDiameter = defaultRebarDiameter;
            this.defaultRebarDiameterUnit = defaultRebarDiameterUnit;
            this.defaultRebarFy = defaultRebarFy;
            this.defaultRebarFyUnit = defaultRebarFyUnit;
            this.spacing = spacing;
            this.start = start;
            this.end = end;
        }
        public CeadeCLineRebar(dynamic ob)
        {
            this.defaultRebarDiameter = (float)ob.defaultRebarDiameter.Value;
            this.defaultRebarDiameterUnit = ob.defaultRebarDiameterUnit.Value;
            this.defaultRebarFy = (float)ob.defaultRebarFy.Value;
            this.defaultRebarFyUnit = ob.defaultRebarFyUnit.Value;
            this.spacing = (float)ob.spacing.Value;
            this.start = ob.start.Value;
            this.end = ob.end.Value;
            if (ob.singleRebars != null)
            {
                for (int i = 0; i < ob.singleRebars.Count; i++)
                {
                    this.singleRebars.Add(new CeadeCSingleRebar(ob.singleRebars[i]));
                }
            }
        }
    }


    public class CeadeCPolylineRebar : CeadeCRebarsObject
    {
        public string type = "PolylineRebar";
        public float spacing;
        public bool start;
        public bool end;
        public bool corner;

        public CeadeCPolylineRebar(float defaultRebarDiameter, string defaultRebarDiameterUnit, float defaultRebarFy, string defaultRebarFyUnit, float spacing, bool start, bool end, bool corner)
        {
            this.defaultRebarDiameter = defaultRebarDiameter;
            this.defaultRebarDiameterUnit = defaultRebarDiameterUnit;
            this.defaultRebarFy = defaultRebarFy;
            this.defaultRebarFyUnit = defaultRebarFyUnit;
            this.spacing = spacing;
            this.start = start;
            this.end = end;
            this.corner = corner;
        }
        public CeadeCPolylineRebar(dynamic ob)
        {
            this.defaultRebarDiameter = (float)ob.defaultRebarDiameter.Value;
            this.defaultRebarDiameterUnit = ob.defaultRebarDiameterUnit.Value;
            this.defaultRebarFy = (float)ob.defaultRebarFy.Value;
            this.defaultRebarFyUnit = ob.defaultRebarFyUnit.Value;
            this.spacing = (float)ob.spacing.Value;
            this.start = ob.start.Value;
            this.end = ob.end.Value;
            this.corner = ob.corner.Value;
            if (ob.singleRebars != null)
            {
                for (int i = 0; i < ob.singleRebars.Count; i++)
                {
                    this.singleRebars.Add(new CeadeCSingleRebar(ob.singleRebars[i]));
                }
            }
        }
    }


    public class CeadeCRectangleRebar : CeadeCRebarsObject
    {
        public string type = "RectangleRebar";
        public double spacing;
        public bool start;
        public bool end;
        public bool corner;

        public CeadeCRectangleRebar(float defaultRebarDiameter, string defaultRebarDiameterUnit, float defaultRebarFy, string defaultRebarFyUnit, double spacing, bool start, bool end, bool corner)
        {
            this.defaultRebarDiameter = defaultRebarDiameter;
            this.defaultRebarDiameterUnit = defaultRebarDiameterUnit;
            this.defaultRebarFy = defaultRebarFy;
            this.defaultRebarFyUnit = defaultRebarFyUnit;
            this.spacing = spacing;
            this.start = start;
            this.end = end;
            this.corner = corner;
        }
        public CeadeCRectangleRebar(dynamic ob)
        {
            this.defaultRebarDiameter = (float)ob.defaultRebarDiameter.Value;
            this.defaultRebarDiameterUnit = ob.defaultRebarDiameterUnit.Value;
            this.defaultRebarFy = (float)ob.defaultRebarFy.Value;
            this.defaultRebarFyUnit = ob.defaultRebarFyUnit.Value;
            this.spacing = (float)ob.spacing.Value;
            this.start = ob.start.Value;
            this.end = ob.end.Value;
            this.corner = ob.corner.Value;
            if (ob.singleRebars != null)
            {
                for (int i = 0; i < ob.singleRebars.Count; i++)
                {
                    this.singleRebars.Add(new CeadeCSingleRebar(ob.singleRebars[i]));
                }
            }
        }
    }


    public class CeadeCArcRebar : CeadeCRebarsObject
    {
        public string type = "ArcRebar";
        public float spacingA;
        public float spacingL;
        public bool start;
        public bool end;


        public CeadeCArcRebar(float defaultRebarDiameter, string defaultRebarDiameterUnit, float defaultRebarFy, string defaultRebarFyUnit, float spacingA, float spacingL, bool start, bool end)
        {
            this.defaultRebarDiameter = defaultRebarDiameter;
            this.defaultRebarDiameterUnit = defaultRebarDiameterUnit;
            this.defaultRebarFy = defaultRebarFy;
            this.defaultRebarFyUnit = defaultRebarFyUnit;
            this.spacingA = spacingA;
            this.spacingL = spacingL;
            this.start = start;
            this.end = end;

        }
        public CeadeCArcRebar(dynamic ob)
        {
            this.defaultRebarDiameter = (float)ob.defaultRebarDiameter.Value;
            this.defaultRebarDiameterUnit = ob.defaultRebarDiameterUnit.Value;
            this.defaultRebarFy = (float)ob.defaultRebarFy.Value;
            this.defaultRebarFyUnit = ob.defaultRebarFyUnit.Value;
            this.spacingA = (float)ob.spacingA.Value;
            this.spacingL = (float)ob.spacingL.Value;
            this.start = ob.start.Value;
            this.end = ob.end.Value;
            if (ob.singleRebars != null)
            {
                for (int i = 0; i < ob.singleRebars.Count; i++)
                {
                    this.singleRebars.Add(new CeadeCSingleRebar(ob.singleRebars[i]));
                }
            }
        }
    }


    public class CeadeCCircleRebar : CeadeCRebarsObject
    {
        public string type = "CircleRebar";

        public float spacingA;
        public float spacingL;


        public CeadeCCircleRebar(float defaultRebarDiameter, string defaultRebarDiameterUnit, float defaultRebarFy, string defaultRebarFyUnit, float spacingA, float spacingL)
        {
            this.defaultRebarDiameter = defaultRebarDiameter;
            this.defaultRebarDiameterUnit = defaultRebarDiameterUnit;
            this.defaultRebarFy = defaultRebarFy;
            this.defaultRebarFyUnit = defaultRebarFyUnit;
            this.spacingA = spacingA;
            this.spacingL = spacingL;
        }
        public CeadeCCircleRebar(dynamic ob)
        {
            this.defaultRebarDiameter = (float)ob.defaultRebarDiameter.Value;
            this.defaultRebarDiameterUnit = ob.defaultRebarDiameterUnit.Value;
            this.defaultRebarFy = (float)ob.defaultRebarFy.Value;
            this.defaultRebarFyUnit = ob.defaultRebarFyUnit.Value;
            this.spacingA =(float)ob.spacingA.Value;
            this.spacingL = (float)ob.spacingL.Value;
            if (ob.singleRebars != null)
            {
                for (int i = 0; i < ob.singleRebars.Count; i++)
                {
                    this.singleRebars.Add(new CeadeCSingleRebar(ob.singleRebars[i]));
                }
            }
        }
    }

    public class CeadeCSingleRebar
    {
        public Vector3 vertex;
        public double rebarDiameter;
        public string rebarDiameterUnit;
        public double fy;
        public string fyUnit;
        public bool Active;
        public CeadeCSingleRebar(Vector3 vertex, double rebarDiameter, string rebarDiameterUnit, double fy, string fyUnit, bool Active)
        {
            this.vertex = vertex;
            this.rebarDiameter = rebarDiameter;
            this.rebarDiameterUnit = rebarDiameterUnit;
            this.fy = fy;
            this.fyUnit = fyUnit;
            this.Active = Active;
        }
        public CeadeCSingleRebar(dynamic ob)
        {
            this.vertex = new Vector3(ob.vertex);
            this.rebarDiameter = (float)ob.rebarDiameter.Value;
            this.rebarDiameterUnit = ob.rebarDiameterUnit.Value;
            this.fy = (float)ob.fy.Value;
            this.fyUnit = ob.fyUnit.Value;
            this.Active = ob.Active.Value;
        }
    }






}
