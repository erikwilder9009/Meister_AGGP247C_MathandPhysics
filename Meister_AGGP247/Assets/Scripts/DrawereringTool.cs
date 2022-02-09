using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawereringTool : MonoBehaviour
{
    public Grid2D grid;

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
            sectickerPos = new Vector3(Center.x, Center.y + 25, 0);
            mintickerPos = new Vector3(Center.x, Center.y + 30, 0);
            houtickerPos = new Vector3(Center.x, Center.y + 35, 0);
        }
        else
        {
            //SecondsHand
            if(Time.time >= timeCheck + ticTime)
            {
                sectickerPos = RotatePoint(Center, -72f, sectickerPos);
                mintickerPos = RotatePoint(Center, -14.4f, mintickerPos);
                houtickerPos = RotatePoint(Center, -2.88f, houtickerPos);
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
                start = new Vector3(Center.x, Center.y + 25, 0);
            }
            Vector3 next = RotatePoint(Center, 60f, start);
            HexLines.Add(new Line(start, next, Color.red));
            start = next;
        }
        foreach(Line l in HexLines)
        {
            Glint.AddCommand(l);
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
            Glint.AddCommand(new Line(grid.GridToScreen(l.start), grid.GridToScreen(l.end), l.color));
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
            Glint.AddCommand(new Line(grid.GridToScreen(l.start), grid.GridToScreen(l.end), l.color));
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
            Glint.AddCommand(new Line(grid.GridToScreen(l.start), grid.GridToScreen(l.end), l.color));
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
            Glint.AddCommand(new Line(grid.GridToScreen(l.start), grid.GridToScreen(l.end), l.color));
        }
    }




    public void drawDiamond(Vector3 Center)
    {
        Glint.AddCommand(new Line(new Vector3(Center.x + 25, Center.y, 0), new Vector3(Center.x, Center.y + 50, 0), Color.red));
        Glint.AddCommand(new Line(new Vector3(Center.x, Center.y + 50, 0), new Vector3(Center.x - 25, Center.y, 0), Color.red));
        Glint.AddCommand(new Line(new Vector3(Center.x - 25, Center.y, 0), new Vector3(Center.x, Center.y - 50, 0), Color.red));
        Glint.AddCommand(new Line(new Vector3(Center.x, Center.y - 50, 0), new Vector3(Center.x + 25, Center.y, 0), Color.red));
    }


    public static float V3ToAngle(Vector3 startPoint, Vector3 endPoint)
    {
        return Mathf.Atan2(endPoint.x, endPoint.y) * 180/Mathf.PI; ;
    }
    public static float LineToAngle(Line line)
    {
        V3ToAngle(line.start, line.end);
        return 0;
    }
    public static Vector3 RotatePoint(Vector3 center, float angle, Vector3 pointIN)
    {
        Vector3 T = new Vector3();
        pointIN = pointIN - center;
        T.x = pointIN.x * Mathf.Cos(Deg2Rad(angle)) - pointIN.y * Mathf.Sin(Deg2Rad(angle));
        T.y = pointIN.x * Mathf.Sin(Deg2Rad(angle)) + pointIN.y * Mathf.Cos(Deg2Rad(angle));
        return T + center;
    }
    public static float Deg2Rad(float angle)
    {
        return angle * Mathf.Deg2Rad;
    }
}
