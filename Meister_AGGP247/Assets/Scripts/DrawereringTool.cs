using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawereringTool : MonoBehaviour
{
    public Grid2D grid;

    MathTool MT = new MathTool();

    Vector3 start;
    Vector3 sectickerPos;
    Vector3 mintickerPos;
    Vector3 houtickerPos;
    public float ticTime = 1f;
    float timeCheck;


    List<Line> ParaA = new List<Line>();
    List<Line> ParaB = new List<Line>();
    List<Line> ParaC = new List<Line>();
    List<Line> ParaD = new List<Line>();
    List<Vector3> Points = new List<Vector3>();


    private void Start()
    {
        GetParabolas();
        timeCheck = Time.time;
    }



    public void drawOrigin(Vector3 Center, float size, Color c)
    {
        Glint.AddCommand(new Line(new Vector3(Center.x + size, Center.y, 0), new Vector3(Center.x, Center.y + size, 0), c));
        Glint.AddCommand(new Line(new Vector3(Center.x, Center.y + size, 0), new Vector3(Center.x - size, Center.y, 0), c));
        Glint.AddCommand(new Line(new Vector3(Center.x - size, Center.y, 0), new Vector3(Center.x, Center.y - size, 0), c));
        Glint.AddCommand(new Line(new Vector3(Center.x, Center.y - size, 0), new Vector3(Center.x + size, Center.y, 0), c));
    }


    public void drawTicker(Vector3 Center, float size)
    {
        drawOrigin(Center, size, Color.green);
        //System.DateTime.Now
        if (sectickerPos == Vector3.zero)
        {
            sectickerPos = new Vector3(Center.x, Center.y + (size * 8), 0);
            mintickerPos = new Vector3(Center.x, Center.y + (size * 12), 0);
            houtickerPos = new Vector3(Center.x, Center.y + (size * 16), 0);
        }
        else
        {
            //SecondsHand
            if(Time.time >= timeCheck + ticTime)
            {
                sectickerPos = MT.RotatePoint(Center, -72f, sectickerPos);
                mintickerPos = MT.RotatePoint(Center, -14.4f, mintickerPos);
                houtickerPos = MT.RotatePoint(Center, -2.88f, houtickerPos);
                timeCheck = Time.time;
            }
        }
        drawOrigin(sectickerPos, size, Color.green);
        drawOrigin(mintickerPos, size, Color.yellow);
        drawOrigin(houtickerPos, size, Color.blue);
    }

    List<Line> HexLines = new List<Line>();
    public void drawHexagon(Vector3 Center, float size)
    {
        while (HexLines.Count < 6)
        {
            if (start == Vector3.zero)
            {
                start = new Vector3(Center.x, Center.y + size, 0);
            }
            Vector3 next = MT.RotatePoint(Center, 60f, start);
            HexLines.Add(new Line(start, next, Color.red));
            start = next;
        }
        foreach(Line l in HexLines)
        {
            Glint.AddCommand(l);
        }
    }
    public void DrawCircle(Vector3 Position, float Radius, int Sides, Color color)
    {
        Vector3 point;
        Vector3 lastpoint = new Vector3();
        float num = 360 / Sides;
        int count = 0;
        Circle OSCircle = new Circle(grid.grid.origin, Position, Radius, Sides, 0);
        point = new Vector3(OSCircle.Position.x, OSCircle.Position.y + Radius, 0);
        while(count <= Sides)
        {
            lastpoint = MT.CircleRadiusPoint(grid.grid.origin, Position, num + (num * count++), Radius);
            Glint.AddCommand(new Line(point, lastpoint, color));
            point = lastpoint;
        }
    }

    public void DE(Vector3 center, float radius, int sides)
    {
        
            
    }

    public void DrawEllipse(Vector3 center, float radius, int sides)
    {
        float theta = 0;
        float angle = 360/sides;
        Vector3 point = MT.EllipseRadiusPoint(center, radius, 0, grid.grid);
        Vector3 newpoint = new Vector3();
        theta += angle;
        while (theta <= 360)
        {
            newpoint = MT.EllipseRadiusPoint(center, radius, theta, grid.grid);
            Glint.AddCommand(new Line(point, newpoint, Color.red));
            point = newpoint;
            theta += angle;
        }
    }


    public void GetParabolas()
    {
        ParaA = new List<Line>();
        ParaB = new List<Line>();
        ParaC = new List<Line>();
        ParaD = new List<Line>();
        Points = new List<Vector3>();
        GetParabolaA();
        Points = new List<Vector3>();
        GetParabolaB();
        Points = new List<Vector3>();
        GetParabolaC();
        Points = new List<Vector3>();
        GetParabolaD();
    }

    void GetParabolaA()
    {
        int y = 0;
        int x = -grid.grid.divisionCount;
        while (x < grid.grid.divisionCount)
        {
            y = x * x;
            Points.Add(new Vector3(x, y, 0));
            x++;
        }
        Vector3 start = Vector3.zero;
        foreach (Vector3 v in Points)
        {
            if (start == Vector3.zero)
            {
                start = v;
            }
            else
            {
                ParaA.Add(new Line(start, v, Color.red));
                start = v;
            }
        }
    }
    public void ParabolaA()
    {
        foreach (Line l in ParaA)
        {
            Glint.AddCommand(new Line(MT.GridToScreen(l.start, grid.grid), MT.GridToScreen(l.end, grid.grid), l.color));
        }
    }

    void GetParabolaB()
    {
        float y = 0;
        float x = -grid.grid.divisionCount;
        while (x < grid.grid.divisionCount)
        {
            //y = x ^ 2 + 2x + 1
            y = (x * x) + (2 * x) + 1;
            Points.Add(new Vector3(x, y, 0));
            x++;
        }
        Vector3 start = Vector3.zero;
        foreach (Vector3 v in Points)
        {
            if (start == Vector3.zero)
            {
                start = v;
            }
            else
            {
                ParaB.Add(new Line(start, v, Color.red));
                start = v;
            }
        }
    }
    public void ParabolaB()
    {
        foreach (Line l in ParaB)
        {
            Glint.AddCommand(new Line(MT.GridToScreen(l.start, grid.grid), MT.GridToScreen(l.end, grid.grid), l.color));
        }
    }

    void GetParabolaC()
    {
        float y = 0;
        int x = -grid.grid.divisionCount;
        while (x < grid.grid.divisionCount)
        {
            //y = -2x ^ 2 + 10x + 12
            y = -2 * (x - 6) * (x + 1);
            Points.Add(new Vector3(x, y, 0));
            x++;
        }
        Vector3 start = Vector3.zero;
        foreach (Vector3 v in Points)
        {
            if (start == Vector3.zero)
            {
                start = v;
            }
            else
            {
                ParaC.Add(new Line(start, v, Color.red));
                start = v;
            }
        }
    }
    public void ParabolaC()
    {
        foreach (Line l in ParaC)
        {
            Glint.AddCommand(new Line(MT.GridToScreen(l.start, grid.grid), MT.GridToScreen(l.end, grid.grid), l.color));
        }
    }

    void GetParabolaD()
    {
        float x = 0;
        int y = -grid.grid.divisionCount;
        while (y < grid.grid.divisionCount)
        {
            //x = -y ^ 3
            x = Mathf.Pow(-y, 3);
            Points.Add(new Vector3(x, y, 0));
            y++;
        }
        Vector3 start = Vector3.zero;
        foreach (Vector3 v in Points)
        {
            if (start == Vector3.zero)                                                                                              
            {
                start = v;
            }
            else
            {
                ParaD.Add(new Line(start, v, Color.red));
                start = v;
            }
        }
    }
    public void ParabolaD()
    {
        foreach (Line l in ParaD)
        {
            Glint.AddCommand(new Line(MT.GridToScreen(l.start, grid.grid), MT.GridToScreen(l.end, grid.grid), l.color));
        }
    }




    public void drawDiamond(Vector3 Center)
    {
        Glint.AddCommand(new Line(new Vector3(Center.x + 25, Center.y, 0), new Vector3(Center.x, Center.y + 50, 0), Color.red));
        Glint.AddCommand(new Line(new Vector3(Center.x, Center.y + 50, 0), new Vector3(Center.x - 25, Center.y, 0), Color.red));
        Glint.AddCommand(new Line(new Vector3(Center.x - 25, Center.y, 0), new Vector3(Center.x, Center.y - 50, 0), Color.red));
        Glint.AddCommand(new Line(new Vector3(Center.x, Center.y - 50, 0), new Vector3(Center.x + 25, Center.y, 0), Color.red));
    }
}
