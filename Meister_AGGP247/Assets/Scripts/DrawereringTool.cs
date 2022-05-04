using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawereringTool : MonoBehaviour
{
    public static Grid2D grid;

    static Vector3 start;
    static Vector3 sectickerPos;
    static Vector3 mintickerPos;
    static Vector3 houtickerPos;
    public static float ticTime = 1f;
    static float timeCheck;
    static List<Line> ParaA = new List<Line>();
    static List<Line> ParaB = new List<Line>();
    static List<Line> ParaC = new List<Line>();
    static List<Line> ParaD = new List<Line>();
    static List<Vector3> Points = new List<Vector3>();
    private void Start()
    {
        GetParabolas();
        timeCheck = Time.time;
    }
    public static void drawOrigin(Vector3 Center, float size, Color c)
    {
        Glint.AddCommand(new Line(new Vector3(Center.x + size, Center.y, 0), new Vector3(Center.x, Center.y + size, 0), c));
        Glint.AddCommand(new Line(new Vector3(Center.x, Center.y + size, 0), new Vector3(Center.x - size, Center.y, 0), c));
        Glint.AddCommand(new Line(new Vector3(Center.x - size, Center.y, 0), new Vector3(Center.x, Center.y - size, 0), c));
        Glint.AddCommand(new Line(new Vector3(Center.x, Center.y - size, 0), new Vector3(Center.x + size, Center.y, 0), c));
    }
    public static void drawTicker(Vector3 Center, float size)
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
            if (Time.time >= timeCheck + ticTime)
            {
                sectickerPos = MathTool.RotatePoint(Center, -72f, sectickerPos);
                mintickerPos = MathTool.RotatePoint(Center, -14.4f, mintickerPos);
                houtickerPos = MathTool.RotatePoint(Center, -2.88f, houtickerPos);
                timeCheck = Time.time;
            }
        }
        drawOrigin(sectickerPos, size, Color.green);
        drawOrigin(mintickerPos, size, Color.yellow);
        drawOrigin(houtickerPos, size, Color.blue);
    }
    static List<Line> HexLines = new List<Line>();
    public static void drawHexagon(Vector3 Center, float size)
    {
        while (HexLines.Count < 6)
        {
            if (start == Vector3.zero)
            {
                start = new Vector3(Center.x, Center.y + size, 0);
            }
            Vector3 next = MathTool.RotatePoint(Center, 60f, start);
            HexLines.Add(new Line(start, next, Color.red));
            start = next;
        }
        foreach (Line l in HexLines)
        {
            Glint.AddCommand(l);
        }
    }
    public static void drawCircle(int sides, float radius, Vector3 center, Color color)
    {
        Vector3 Position = new Vector3(center.x, center.y + radius, 0);
        Vector3 lastpoint = new Vector3();
        float num = 360 / sides;
        int count = 0;
        Vector3 point = Position;
        while (count <= sides)
        {
            lastpoint = MathTool.CircleRadiusPoint(center, Position, num + (num * count++), radius);
            Glint.AddCommand(new Line(point, lastpoint, color));
            point = lastpoint;
        }
    }
    public static void fillCircle(Vector3 Center, float Radius, Color color)
    {
        int count = 0;
        int countMax = 360;
        float num = 360 / countMax;

        Vector3 Point = new Vector3(Center.x, Center.y + Radius);
        while (count <= countMax)
        {
            Glint.AddCommand(new Line(Center, Point, color));
            Point = MathTool.CircleRadiusPoint(Center, Point, num, Radius);
            count++;
        }

    }
    public static void DrawEllipse(Vector3 center, float radius, int sides)
    {
        float theta = 0;
        float angle = 360 / sides;
        Vector3 point = MathTool.EllipseRadiusPoint(center, radius, 0, grid.grid);
        Vector3 newpoint = new Vector3();
        theta += angle;
        while (theta <= 360)
        {
            newpoint = MathTool.EllipseRadiusPoint(center, radius, theta, grid.grid);
            Glint.AddCommand(new Line(point, newpoint, Color.red));
            point = newpoint;
            theta += angle;
        }
    }
    public static void GetParabolas()
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
    public static void GetParabolaA()
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
    public static void ParabolaA()
    {
        foreach (Line l in ParaA)
        {
            Glint.AddCommand(new Line(MathTool.GridToScreen(l.start, grid.grid), MathTool.GridToScreen(l.end, grid.grid), l.color));
        }
    }
    public static void GetParabolaB()
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
    public static void ParabolaB()
    {
        foreach (Line l in ParaB)
        {
            Glint.AddCommand(new Line(MathTool.GridToScreen(l.start, grid.grid), MathTool.GridToScreen(l.end, grid.grid), l.color));
        }
    }
    public static void GetParabolaC()
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
    public static void ParabolaC()
    {
        foreach (Line l in ParaC)
        {
            Glint.AddCommand(new Line(MathTool.GridToScreen(l.start, grid.grid), MathTool.GridToScreen(l.end, grid.grid), l.color));
        }
    }
    public static void GetParabolaD()
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
    public static void ParabolaD()
    {
        foreach (Line l in ParaD)
        {
            Glint.AddCommand(new Line(MathTool.GridToScreen(l.start, grid.grid), MathTool.GridToScreen(l.end, grid.grid), l.color));
        }
    }
    public static void drawDiamond(Vector3 Center)
    {
        Glint.AddCommand(new Line(new Vector3(Center.x + 25, Center.y, 0), new Vector3(Center.x, Center.y + 50, 0), Color.red));
        Glint.AddCommand(new Line(new Vector3(Center.x, Center.y + 50, 0), new Vector3(Center.x - 25, Center.y, 0), Color.red));
        Glint.AddCommand(new Line(new Vector3(Center.x - 25, Center.y, 0), new Vector3(Center.x, Center.y - 50, 0), Color.red));
        Glint.AddCommand(new Line(new Vector3(Center.x, Center.y - 50, 0), new Vector3(Center.x + 25, Center.y, 0), Color.red));
    }
    public static void DrawShip(Vector3 location, float Rotation)
    {
        Glint.AddCommand(new Line(MathTool.RotatePoint(location, Rotation - 90, new Vector3(location.x + 20, location.y - 15, 0)), MathTool.RotatePoint(location, Rotation - 90, new Vector3(location.x, location.y + 25, 0)), Color.red));
        Glint.AddCommand(new Line(MathTool.RotatePoint(location, Rotation - 90, new Vector3(location.x, location.y + 25, 0)), MathTool.RotatePoint(location, Rotation - 90, new Vector3(location.x - 20, location.y - 15, 0)), Color.red));
        Glint.AddCommand(new Line(MathTool.RotatePoint(location, Rotation - 90, new Vector3(location.x - 20, location.y - 15, 0)), MathTool.RotatePoint(location, Rotation - 90, new Vector3(location.x, location.y, 0)), Color.red));
        Glint.AddCommand(new Line(MathTool.RotatePoint(location, Rotation - 90, new Vector3(location.x, location.y, 0)), MathTool.RotatePoint(location, Rotation - 90, new Vector3(location.x + 20, location.y - 15, 0)), Color.red));
    }
    public static void DrawEscape(Vector3 screenSize)
    {
        Color DrawC = Color.red;
        Vector3 A = new Vector3(screenSize.x - 50, screenSize.y - 10);
        Vector3 B = new Vector3(screenSize.x - 10, screenSize.y - 10);
        Vector3 C = new Vector3(screenSize.x - 10, screenSize.y - 50);
        Vector3 D = new Vector3(screenSize.x - 50, screenSize.y - 50);
        if (MathTool.PointInRectangle(Input.mousePosition, A, B, C, D))
        {
            fillRectangle(A, B, C, D, Color.green);
            if (Input.GetMouseButtonDown(0))
            {
                Menu.LoadMenu();
            }
        }
        Glint.AddCommand(new Line(A, B, DrawC));
        Glint.AddCommand(new Line(B, C, DrawC));
        Glint.AddCommand(new Line(C, D, DrawC));
        Glint.AddCommand(new Line(D, A, DrawC));
        Glint.AddCommand(new Line(A, C, DrawC));
        Glint.AddCommand(new Line(B, D, DrawC));
    }

    public static void SCEscape(Vector3 screenSize)
    {
        Color DrawC = Color.red;
        Vector3 A = new Vector3(screenSize.x - 50, screenSize.y - 10);
        Vector3 B = new Vector3(screenSize.x - 10, screenSize.y - 10);
        Vector3 C = new Vector3(screenSize.x - 10, screenSize.y - 50);
        Vector3 D = new Vector3(screenSize.x - 50, screenSize.y - 50);
        if (MathTool.PointInRectangle(Input.mousePosition, A, B, C, D))
        {
            fillRectangle(A, B, C, D, Color.red);
            if (Input.GetMouseButtonDown(0))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                Application.Quit();
            }
        }
        Glint.AddCommand(new Line(A, B, DrawC));
        Glint.AddCommand(new Line(B, C, DrawC));
        Glint.AddCommand(new Line(C, D, DrawC));
        Glint.AddCommand(new Line(D, A, DrawC));
        Glint.AddCommand(new Line(A, C, DrawC));
        Glint.AddCommand(new Line(B, D, DrawC));
    }

    public static void drawGaugebar(float percentFull, Color color)
    {
        Vector3 A = new Vector3(0, 0);
        Vector3 B = new Vector3(0, 20);
        Vector3 C = new Vector3(500, 20);
        Vector3 D = new Vector3(500, 0);
        Glint.AddCommand(new Line(A, B, color));
        Glint.AddCommand(new Line(B, C, color));
        Glint.AddCommand(new Line(C, D, color));
        Glint.AddCommand(new Line(D, A, color));

        int x = 0;
        while (x < percentFull * 5)
        {
            Glint.AddCommand(new Line(new Vector3(x, 0), new Vector3(x, 20), color));
            x++;
        }
    }
<<<<<<< Updated upstream
=======
    public static void drawPowerbar(Vector3 TankPos, float percentFull, Color color, SCCanvas.Scene canvas)
    {
        Vector3 A = new Vector3(canvas.origin.x + (TankPos.x - 50) * canvas.Zoom, canvas.origin.y - (50 * canvas.Zoom), 0);
        Vector3 C = new Vector3(canvas.origin.x + (TankPos.x + 50) * canvas.Zoom, canvas.origin.y - (70 * canvas.Zoom), 0);
        drawRectangle(A, C, color);

        float x = Mathf.RoundToInt(A.x);
        DrawereringTool.fillRectangle(A, new Vector3(A.x + ((C.x - A.x) * (percentFull / 100)), C.y), color);
    }
    public static void drawRotatebar(Vector3 TankPos, float percentFull, Color color, SCCanvas.Scene canvas)
    {
        Vector3 A = new Vector3(canvas.origin.x + (TankPos.x - 50) * canvas.Zoom, canvas.origin.y - (80 * canvas.Zoom), 0);
        Vector3 C = new Vector3(canvas.origin.x + (TankPos.x + 50) * canvas.Zoom, canvas.origin.y - (100 * canvas.Zoom), 0);
        drawRectangle(A, C, color);

        float x = Mathf.RoundToInt(A.x);
        DrawereringTool.fillRectangle(A, new Vector3(A.x + ((C.x - A.x) * (percentFull / 100)), C.y), color);
    }

>>>>>>> Stashed changes

    public static void drawTriangle(Vector3 A, Vector3 B, Vector3 C, Color color)
    {
        Glint.AddCommand(new Line(A, B, color));
        Glint.AddCommand(new Line(B, C, color));
        Glint.AddCommand(new Line(C, A, color));
    }
    public static void fillTriangle(Vector3 A, Vector3 B, Vector3 C, Color color)
    {
        float X = B.x;
        while (X <= C.x)
        {
            Glint.AddCommand(new Line(A, new Vector3(X, B.y, 0), color));
            X++;
        }
    }
    public static void drawRectangle(Vector3 A, Vector3 C, Color color)
    {
        Glint.AddCommand(new Line(A, new Vector3(A.x, C.y, 0), color));
        Glint.AddCommand(new Line(new Vector3(A.x, C.y, 0), C, color));
        Glint.AddCommand(new Line(C, new Vector3(C.x, A.y, 0), color));
        Glint.AddCommand(new Line(new Vector3(C.x, A.y, 0), A, color));
    }
    public static void drawRectangle(Vector3 A, Vector3 B, Vector3 C, Vector3 D, Color color)
    {
        Glint.AddCommand(new Line(A, B, color));
        Glint.AddCommand(new Line(B, C, color));
        Glint.AddCommand(new Line(C, D, color));
        Glint.AddCommand(new Line(D, A, color));
    }
    public static void fillRectangle(Vector3 A, Vector3 C, Color color)
    {
        Vector3 D = new Vector3(A.x, C.y, 0);
        float Y = D.y;
        while (Y <= A.y)
        {
            Glint.AddCommand(new Line(new Vector3(D.x, Y, 0), new Vector3(C.x, Y, 0), color));
            Y++;
        }
    }
    public static void fillRectangle(Vector3 A, Vector3 B, Vector3 C, Vector3 D, Color color)
    {
        float Y = D.y;
        while (Y <= A.y)
        {
            Glint.AddCommand(new Line(new Vector3(D.x, Y, 0), new Vector3(C.x, Y, 0), color));
            Y++;
        }
    }
    public static void fillRectangle(Vector3 A, Vector3 B, Vector3 C, Vector3 D, Color color, Vector3 rotPoint, float rotation)
    {
        float Y = D.y;
        while (Y <= A.y)
        {
            Glint.AddCommand(new Line(MathTool.RotatePoint(rotPoint, rotation, new Vector3(D.x, Y, 0)), MathTool.RotatePoint(rotPoint, rotation, new Vector3(C.x, Y, 0)), Color.red));
            Y++;
        }
    }

    public static void drawLine(Vector3 A, Vector3 B, Color color)
    {
        Glint.AddCommand(new Line(A, B, color));
    }

    
    public static void drawNum(int num, float scale, Vector3 center, Color color)
    {
        if (num == 0)
        {
            Glint.AddCommand(new Line(new Vector3(center.x - (scale / 2), center.y + scale), new Vector3(center.x + (scale / 2), center.y + scale), color));
            Glint.AddCommand(new Line(new Vector3(center.x + (scale / 2), center.y + scale), new Vector3(center.x + (scale / 2), center.y - scale), color));
            Glint.AddCommand(new Line(new Vector3(center.x + (scale / 2), center.y - scale), new Vector3(center.x - (scale / 2), center.y - scale), color));
            Glint.AddCommand(new Line(new Vector3(center.x - (scale / 2), center.y - scale), new Vector3(center.x - (scale / 2), center.y + scale), color));

            Glint.AddCommand(new Line(new Vector3(center.x + (scale / 2), center.y + scale), new Vector3(center.x - (scale / 2), center.y - scale), color));
        }
        if (num == 1)
        {
            Glint.AddCommand(new Line(new Vector3(center.x, center.y - scale), new Vector3(center.x, center.y + scale), color));

            Glint.AddCommand(new Line(new Vector3(center.x, center.y + scale), new Vector3(center.x - (scale / 2), center.y + (scale / 4)), color));

            Glint.AddCommand(new Line(new Vector3(center.x - (scale / 2), center.y - scale), new Vector3(center.x + (scale / 2), center.y - scale), color));
        }
        if (num == 2)
        {
            Glint.AddCommand(new Line(new Vector3(center.x, center.y + scale), new Vector3(center.x - (scale / 2), center.y + (scale / 4)), color));
            Glint.AddCommand(new Line(new Vector3(center.x, center.y + scale), new Vector3(center.x + (scale / 2), center.y + (scale / 4)), color));

            Glint.AddCommand(new Line(new Vector3(center.x + (scale / 2), center.y + (scale / 4)), new Vector3(center.x - (scale / 2), center.y - scale), color));

            Glint.AddCommand(new Line(new Vector3(center.x - (scale / 2), center.y - scale), new Vector3(center.x + (scale - (scale / 4)), center.y - scale), color));
        }
        if (num == 3)
        {
            Glint.AddCommand(new Line( new Vector3(center.x - (scale / 2), center.y + (scale / 2)),new Vector3(center.x, center.y + scale), color));
            Glint.AddCommand(new Line(new Vector3(center.x, center.y + scale), new Vector3(center.x + (scale / 2), center.y + (scale / 4)), color)); 

            Glint.AddCommand(new Line(new Vector3(center.x + (scale / 2), center.y + (scale / 4)), new Vector3(center.x - (scale / 2), center.y), color));
            Glint.AddCommand(new Line(new Vector3(center.x + (scale / 2), center.y - (scale / 4)), new Vector3(center.x - (scale / 2), center.y), color));

            Glint.AddCommand(new Line(new Vector3(center.x, center.y - scale), new Vector3(center.x - (scale / 2), center.y - (scale / 2)), color));
            Glint.AddCommand(new Line(new Vector3(center.x, center.y - scale), new Vector3(center.x + (scale / 2), center.y - (scale / 4)), color));
        }
        if (num == 4)
        {
            Glint.AddCommand(new Line(new Vector3(center.x - (scale / 2), center.y + scale), new Vector3(center.x - (scale / 2), center.y), color)); 
            Glint.AddCommand(new Line(new Vector3(center.x - (scale / 2), center.y), new Vector3(center.x + (scale - (scale / 4)), center.y), color));
            Glint.AddCommand(new Line(new Vector3(center.x + (scale / 2), center.y + scale), new Vector3(center.x + (scale / 2), center.y - scale), color));
        }
        if (num == 5)
        {
            Glint.AddCommand(new Line(new Vector3(), new Vector3(), color));
        }
        if (num == 6)
        {
            Glint.AddCommand(new Line(new Vector3(), new Vector3(), color));
        }
        if (num == 7)
        {
            Glint.AddCommand(new Line(new Vector3(), new Vector3(), color));
        }
        if (num == 8)
        {
            Glint.AddCommand(new Line(new Vector3(), new Vector3(), color));
        }
        if (num == 9)
        {
            Glint.AddCommand(new Line(new Vector3(), new Vector3(), color));
        }
    }
}
