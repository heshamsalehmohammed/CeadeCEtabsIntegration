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
    }


    public class model
    {
        public List<CeadeCObject> objects = new List<CeadeCObject>();
    }


    public class CeadeCObject
    {
        public string shapeChildType;
        public string voidIndex;
        public CeadeCRebarsObject rebars;
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
    }

    public class CeadeCLines : CeadeCObject
    {
        public string type = "lines";
        public List<Vector3> vertices;

        public CeadeCLines(List<Vector3> vertices)
        {
            this.vertices = vertices;
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

    }


    public class CeadeCRectangles : CeadeCObject
    {
        public string type = "rectangles";
        public List<Vector3> vertices;


        public CeadeCRectangles(List<Vector3> vertices)
        {
            this.vertices = vertices;
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

    }


    public class CeadeCGpolylines : CeadeCObject
    {
        public string type = "Gpolylines";


        public CeadeCGpolylines(List<CeadeCObject> children)
        {
            this.children = children;
        }

    }



    public class CeadeCShapes : CeadeCObject
    {
        public string type = "shapes";

        public CeadeCShapes(List<CeadeCObject> children)
        {
            this.children = children;
        }

    }


    public class CeadeCRebarsObject
    {
        public List<CeadeCSingleRebar> singleRebars = new List<CeadeCSingleRebar>();
    }


    public class PointRebar : CeadeCRebarsObject
    {
        public string type = "PointRebar";
        public float defaultRebarDiameter;

        public PointRebar(float defaultRebarDiameter)
        {
            this.defaultRebarDiameter = defaultRebarDiameter;
        }

    }

    public class LineRebar : CeadeCRebarsObject
    {
        public string type = "LineRebar";
        public float defaultRebarDiameter;
        public float spacing;
        public bool start;
        public bool end;

        public LineRebar(float defaultRebarDiameter, float spacing, bool start,bool end)
        {
            this.defaultRebarDiameter = defaultRebarDiameter;
            this.spacing = spacing;
            this.start = start;
            this.end = end;
        }       

    }


    public class PolylineRebar : CeadeCRebarsObject
    {
        public string type = "PolylineRebar";
        public float defaultRebarDiameter;
        public float spacing;
        public bool start;
        public bool end;
        public bool corner;

        public PolylineRebar(float defaultRebarDiameter, float spacing, bool start, bool end,bool corner)
        {
            this.defaultRebarDiameter = defaultRebarDiameter;
            this.spacing = spacing;
            this.start = start;
            this.end = end;
            this.corner = corner;
        }

    }


    public class RectangleRebar : CeadeCRebarsObject
    {
        public string type = "RectangleRebar";
        public double defaultRebarDiameter;
        public double spacing;
        public bool start;
        public bool end;
        public bool corner;

        public RectangleRebar(double defaultRebarDiameter, double spacing, bool start, bool end, bool corner)
        {
            this.defaultRebarDiameter = defaultRebarDiameter;
            this.spacing = spacing;
            this.start = start;
            this.end = end;
            this.corner = corner;
        }

    }


    public class ArcRebar : CeadeCRebarsObject
    {
        public string type = "ArcRebar";
        public float defaultRebarDiameter;
        public float spacingA;
        public float spacingL;
        public bool start;
        public bool end;


        public ArcRebar(float defaultRebarDiameter, float spacingA, float spacingL, bool start, bool end)
        {
            this.defaultRebarDiameter = defaultRebarDiameter;
            this.spacingA = spacingA;
            this.spacingL = spacingL;
            this.start = start;
            this.end = end;

        }

    }


    public class CircleRebar : CeadeCRebarsObject
    {
        public string type = "CircleRebar";
        public float defaultRebarDiameter;
        public float spacingA;
        public float spacingL;


        public CircleRebar(float defaultRebarDiameter, float spacingA, float spacingL)
        {
            this.defaultRebarDiameter = defaultRebarDiameter;
            this.spacingA = spacingA;
            this.spacingL = spacingL;
        }

    }

    public class CeadeCSingleRebar
    {
        public Vector3 vertex;
        public double rebarDiameter;
        public string rebarDiameterUnit;
        public bool Active;
        public CeadeCSingleRebar(Vector3 vertex, double rebarDiameter, string rebarDiameterUnit, bool Active)
        {
            this.vertex = vertex;
            this.rebarDiameter = rebarDiameter;
            this.rebarDiameterUnit = rebarDiameterUnit;
            this.Active = Active;
        }
    }






}
