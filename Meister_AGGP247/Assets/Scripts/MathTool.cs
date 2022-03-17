using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathTool : MonoBehaviour
{
    //Takes the potential grid space and outputs it into screen space
    static public Vector3 GridToScreen(Vector3 gridSpace, Grid2D.Grid g)
    {
        return (gridSpace * g.gridSize) + g.origin;
    }

    //Takes in screen space and outputs it as grid space
    static public Vector3 ScreenToGrid(Vector3 screenSpace, Grid2D.Grid g)
    {
        return (screenSpace - g.origin) / g.gridSize;
    }
    static public float Deg2Rad(float angle)
    {
        return angle * Mathf.Deg2Rad;
    }
    public static float Rad2Deg(float rad)
    {
        return rad * Mathf.Rad2Deg;
    }
    static public Vector3 RotatePoint(Vector3 center, float angle, Vector3 pointIN)
    {
        Vector3 T = new Vector3();
        pointIN = pointIN - center;
        T.x = pointIN.x * Mathf.Cos(Deg2Rad(angle)) - pointIN.y * Mathf.Sin(Deg2Rad(angle));
        T.y = pointIN.x * Mathf.Sin(Deg2Rad(angle)) + pointIN.y * Mathf.Cos(Deg2Rad(angle));
        return T + center;
    }
    static public Vector3 CircleRadiusPoint(Vector3 Origin, Vector3 Center, float angle, float radius)
    {
        return RotatePoint(Origin, angle, Center);
    }
    static public Vector3 EllipseRadiusPoint(Vector3 center, float radius, float angle, Grid2D.Grid g)
    {
        Vector3 v = new Vector3();
        v.x = (float)(center.x + (radius * g.gridSize) * Mathf.Cos(Deg2Rad(angle)));
        v.y = (float)(center.y - 0.5 * (radius * g.gridSize) * Mathf.Sin(Deg2Rad(angle)));
        return v;
    }
    public static float V3ToAngle(Vector3 startPoint, Vector3 endPoint)
    {
        return Mathf.Atan2(endPoint.x, endPoint.y) * 180 / Mathf.PI; ;
    }
    public static float LineToAngle(Line line)
    {
        V3ToAngle(line.start, line.end);
        return 0;
    }
    static public bool PointInTriangle(Vector3 p, Vector3 a, Vector3 b, Vector3 c)
    {
        if (SameSide(p, a, b, c) && SameSide(p, b, a, c) && SameSide(p, c, a, b))
        {
            return true;
        }
        return false;
    }
    static public bool PointInRectangle(Vector3 p, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        if (PointInTriangle(p, a, b, d) || PointInTriangle(p, b, d, c))
        {
            return true;
        }
        return false;
    }
    public static bool PointInCircle(Vector3 Center, float Radius, Vector3 Point)
    {
        float d = Mathf.Sqrt(Mathf.Pow((Point.x - Center.x), 2) + Mathf.Pow((Point.y - Center.y), 2));
        if (d <= Radius)
        {
            return true;
        }
        return false;
    }
    static public bool SameSide(Vector3 p1, Vector3 p2, Vector3 a, Vector3 b)
    {
        Vector3 cp1 = Vector3.Cross(b - a, p1 - a);
        Vector3 cp2 = Vector3.Cross(b - a, p2 - a);
        if (Vector3.Dot(cp1, cp2) >= 0)
        {
            return true;
        }
        return false;
    }
}
