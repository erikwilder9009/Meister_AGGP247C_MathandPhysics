using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    List<Vector3> PathPoints;
    int index;
    void Start()
    {
        index = 0;
    }
    void Update()
    {
        DrawereringTool.drawOrigin(PathPoints[index], 10, Color.yellow);
        if (index < PathPoints.Count - 2)
        {
            index += 2;
        }
    }
    public void newCB(List<Vector3> points)
    {
        index = 0;
        PathPoints = points;
    }
}
