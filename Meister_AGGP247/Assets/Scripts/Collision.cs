using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    Vector3 mousePoint;
    public Grid2D grid;
    MathTool MT = new MathTool();

    int shapeIndex;

    
    // Update is called once per frame
    void Update()
    {
        mousePoint = Input.mousePosition;


        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(shapeIndex == 2)
            {
                shapeIndex = 0;
            }
            else
            {
                shapeIndex++;
            }
        }
        if(shapeIndex == 0)
        {
            drawCircle();
        }
        if(shapeIndex == 1)
        {
            drawTriangle();
        }
        if(shapeIndex == 2)
        {
            drawRectangle();
        }
    }

    void drawCircle()
    {
        Color DrawC = Color.red;
        int Sides = 36;
        float Radius = 10 * grid.grid.gridSize;
        Vector3 Center = grid.grid.origin;
        Vector3 Position = new Vector3(grid.grid.origin.x, grid.grid.origin.y + Radius, 0);

        if(PointInCircle(Center, Radius, mousePoint))
        {
            fillCircle(Center, Radius);
        }

        Vector3 point;
        Vector3 lastpoint = new Vector3();
        float num = 360 / Sides;
        int count = 0;
        Circle OSCircle = new Circle(grid.grid.origin, Position, Radius, Sides, 0);
        point = new Vector3(OSCircle.Position.x, OSCircle.Position.y, 0);
        while (count <= Sides)
        {
            lastpoint = MT.CircleRadiusPoint(grid.grid.origin, Position, num + (num * count++), Radius);
            Glint.AddCommand(new Line(point, lastpoint, DrawC));
            point = lastpoint;
        }
    }
    void fillCircle(Vector3 Center, float Radius)
    {
        int count = 0;
        int countMax = 360;
        float num = 360 / countMax;

        Vector3 Point = new Vector3(Center.x, Center.y + Radius);
        while (count <= countMax)
        {
            Glint.AddCommand(new Line(Center, Point, Color.green));
            Point = MT.CircleRadiusPoint(Center, Point, num, Radius);
            count++;
        }

    }
    bool PointInCircle(Vector3 Center, float Radius, Vector3 Point)
    {
        float d = Mathf.Sqrt(Mathf.Pow((Point.x - Center.x), 2) + Mathf.Pow((Point.y - Center.y), 2));
        if(d <= Radius)
        {
            return true;
        }
        return false;
    }


    void drawTriangle()
    {
        Color DrawC = Color.red;
        Vector3 A = MT.GridToScreen(new Vector3(0, 10, 0), grid.grid);
        Vector3 B = MT.GridToScreen(new Vector3(10, -10, 0), grid.grid);
        Vector3 C = MT.GridToScreen(new Vector3(-10, -10, 0), grid.grid);
        if (PointInTriangle(mousePoint, A, B, C))
        {
            fillTriangle(A, C, B);
        }
        Glint.AddCommand(new Line(A, B, DrawC));
        Glint.AddCommand(new Line(B, C, DrawC));
        Glint.AddCommand(new Line(C, A, DrawC));
    }
    void fillTriangle(Vector3 A, Vector3 B, Vector3 C)
    {
        float X = B.x;
        while (X <= C.x)
        {
            Glint.AddCommand(new Line(A, new Vector3(X, B.y, 0), Color.green));
            X++;
        }
    }
    bool PointInTriangle(Vector3 p, Vector3 a, Vector3 b, Vector3 c)
    {
        if (SameSide(p, a, b, c) && SameSide(p, b, a, c) && SameSide(p, c, a, b))
        {
            return true;
        }
        return false;
    }

    void drawRectangle()
    {
        Color DrawC = Color.red;
        Vector3 A = MT.GridToScreen(new Vector3(-10, 10, 0), grid.grid);
        Vector3 B = MT.GridToScreen(new Vector3(10, 10, 0), grid.grid);
        Vector3 C = MT.GridToScreen(new Vector3(10, -10, 0), grid.grid);
        Vector3 D = MT.GridToScreen(new Vector3(-10, -10, 0), grid.grid);
        if (PointInRectangle(mousePoint, A, B, C, D))
        {
            fillRectangle(A, B, C, D);
        }
        Glint.AddCommand(new Line(A, B, DrawC));
        Glint.AddCommand(new Line(B, C, DrawC));
        Glint.AddCommand(new Line(C, D, DrawC));
        Glint.AddCommand(new Line(D, A, DrawC));
    }
    void fillRectangle(Vector3 A, Vector3 B, Vector3 C, Vector3 D)
    {
        float Y = D.y;
        while (Y <= A.y)
        {
            Glint.AddCommand(new Line(new Vector3(D.x, Y, 0), new Vector3(C.x, Y, 0), Color.green));
            Y++;
        }
    }
    bool PointInRectangle(Vector3 p, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        if (PointInTriangle(p, a, b, d) || PointInTriangle(p, b, d, c))
        {
            return true;
        }
        return false;
    }



    bool SameSide(Vector3 p1, Vector3 p2, Vector3 a, Vector3 b)
    {
        Vector3 cp1 = Vector3.Cross(b - a, p1 - a);
        Vector3 cp2 = Vector3.Cross(b - a, p2 - a);
        if(Vector3.Dot(cp1, cp2) >= 0)
        {
            return true;
        }
        return false;
    }
}
