using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawereringTool : MonoBehaviour
{


    Vector3 start;
    Vector3 sectickerPos;
    Vector3 mintickerPos;
    Vector3 houtickerPos;
    public float ticTime = 1f;
    float timeCheck;
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




    List<Line> ParaA = new List<Line>();
    List<Vector3> Points = new List<Vector3>();
    public void GetParabolas()
    {
        int y = 0;
        int x = -20;
        while (x < 20)
        {
            y = x ^ 2;
            x++;
            Points.Add(new Vector3(x, y, 0));
        }
        Debug.Log(Points.Count);
        Vector3 start = Vector3.zero;
        foreach (Vector3 v in Points)
        {
            if (start == Vector3.zero)
            {
                start = v;
                return;
            }
            else
            {
                ParaA.Add(new Line(start, v, Color.red));
            }
        }

    }
    public void ParabolasA()
    {
        foreach(Line l in ParaA)
        {
            Glint.AddCommand(l);
        }
    }
    public void ParabolasB()
    {
        //y = x ^ 2 + 2x + 1
    }
    public void ParabolasC()
    {
        //y = -2x ^ 2 + 10x + 12
    }
    public void ParabolasD()
    {
        //x = -y ^ 3
    }
    List<Line> Diamond;
    public void drawDiamond(Vector3 Center)
    {
        Glint.AddCommand(new Line(new Vector3(Center.x + 25, Center.y, 0), new Vector3(Center.x, Center.y + 50, 0), Color.red));
        Glint.AddCommand(new Line(new Vector3(Center.x, Center.y + 50, 0), new Vector3(Center.x - 25, Center.y, 0), Color.red));
        Glint.AddCommand(new Line(new Vector3(Center.x - 25, Center.y, 0), new Vector3(Center.x, Center.y - 50, 0), Color.red));
        Glint.AddCommand(new Line(new Vector3(Center.x, Center.y - 50, 0), new Vector3(Center.x + 25, Center.y, 0), Color.red));
    }
    public void drawRotatingDiamond(Vector3 Center)
    {

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
