using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathTool : MonoBehaviour
{
    //Takes the potential grid space and outputs it into screen space
    public Vector3 GridToScreen(Vector3 gridSpace, Grid2D.Grid g)
    {
        return (gridSpace * g.gridSize) + g.origin;
    }

    //Takes in screen space and outputs it as grid space
    public Vector3 ScreenToGrid(Vector3 screenSpace, Grid2D.Grid g)
    {
        return (screenSpace - g.origin) / g.gridSize;
    }
    public static float Deg2Rad(float angle)
    {
        return angle * Mathf.Deg2Rad;
    }
    public static float Rad2Deg(float rad)
    {
        return rad * Mathf.Rad2Deg;
    }
    public Vector3 RotatePoint(Vector3 center, float angle, Vector3 pointIN)
    {
        Vector3 T = new Vector3();
        pointIN = pointIN - center;
        T.x = pointIN.x * Mathf.Cos(Deg2Rad(angle)) - pointIN.y * Mathf.Sin(Deg2Rad(angle));
        T.y = pointIN.x * Mathf.Sin(Deg2Rad(angle)) + pointIN.y * Mathf.Cos(Deg2Rad(angle));
        return T + center;
    }

    public Vector3 CircleRadiusPoint(Vector3 Origin, Vector3 Center, float angle, float radius)
    {
        return RotatePoint(Origin, angle, Center);
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
}
